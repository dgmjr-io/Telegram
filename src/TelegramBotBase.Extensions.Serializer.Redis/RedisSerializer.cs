using System;
using System.Data;

using Microsoft.Extensions.Options;

using TelegramBotBase.Args;
using TelegramBotBase.Base;
using TelegramBotBase.Form;
using TelegramBotBase.Interfaces;
using StackExchange.Redis;
using System.Runtime.InteropServices;

namespace TelegramBotBase.Extensions.Serializer.Redis
{
    public class RedisSerializer(IOptions<RedisSerializerOptions> options, type fallbackStateForm) : IStateMachine
    {
        public type FallbackStateForm => fallbackStateForm?.IsSubclassOf(typeof(FormBase)) != true ? throw new ArgumentException($"{nameof(FallbackStateForm)} is not a subclass of {nameof(FormBase)}") : fallbackStateForm;

        private ConnectionMultiplexer _connection;
        protected virtual ConnectionMultiplexer Connection => _connection ??= ConnectionMultiplexer.Connect(options.Value.ConfigurationOptions);

        public StateContainer LoadFormStates()
        {
            using var redis = Connection;
            var db = redis.GetDatabase();

            var container = new StateContainer();

            var keys = db.SetScan("devices_sessions", pageSize: 1000);

            foreach (var key in keys)
            {
                var deviceId = long.Parse(key);

                var se = new StateEntry
                {
                    DeviceId = deviceId,
                    ChatTitle = db.StringGet($"devices_sessions:{deviceId}:deviceTitle"),
                    FormUri = db.StringGet($"devices_sessions:{deviceId}:FormUri"),
                    QualifiedName = db.StringGet($"devices_sessions:{deviceId}:QualifiedName")
                };

                container.States.Add(se);

                if (se.DeviceId > 0)
                {
                    container.ChatIds.Add(se.DeviceId);
                }
                else
                {
                    container.GroupIds.Add(se.DeviceId);
                }

                var dataKeys = db.SetScan($"devices_sessions:{deviceId}:data", pageSize: 1000);

                foreach (var dataKey in dataKeys)
                {
                    var key2 = dataKey;

                    var type = Type.GetType(db.StringGet($"devices_sessions:{deviceId}:data:{key2}:type"));

                    var value = System.Text.Json.JsonSerializer.Deserialize(db.StringGet($"devices_sessions:{deviceId}:data:{key2}:value"), type);

                    se.Values.Add(key2, value);
                }
            }
            redis.Close();

            return container;
        }

        public void SaveFormStates(SaveStatesEventArgs e)
        {
            var container = e.States;

            using var redis = Connection;
            //Cleanup old Session data
            var db = redis.GetDatabase();

            var keys = db.SetScan("devices_sessions", pageSize: 1000);

            foreach (var key in keys)
            {
                var deviceId = long.Parse(key);

                db.KeyDelete($"devices_sessions:{deviceId}:deviceTitle");
                db.KeyDelete($"devices_sessions:{deviceId}:FormUri");
                db.KeyDelete($"devices_sessions:{deviceId}:QualifiedName");

                var dataKeys = db.SetScan($"devices_sessions:{deviceId}:data", pageSize: 1000);

                foreach (var dataKey in dataKeys)
                {
                    db.KeyDelete($"devices_sessions:{deviceId}:data:{dataKey}:type");
                    db.KeyDelete($"devices_sessions:{deviceId}:data:{dataKey}:value");
                }

                db.KeyDelete($"devices_sessions:{deviceId}:data");
            }

            db.KeyDelete("devices_sessions");

            //Prepare new session commands
            var sessionCommand = db.CreateBatch();
            var dataCommand = db.CreateBatch();

            //Store session data in database
            foreach (var state in container.States)
            {
                sessionCommand.SetAddAsync("devices_sessions", state.DeviceId);

                sessionCommand.StringSetAsync($"devices_sessions:{state.DeviceId}:deviceTitle", state.ChatTitle ?? "");
                sessionCommand.StringSetAsync($"devices_sessions:{state.DeviceId}:FormUri", state.FormUri);
                sessionCommand.StringSetAsync($"devices_sessions:{state.DeviceId}:QualifiedName", state.QualifiedName);

                foreach (var data in state.Values)
                {
                    dataCommand.SetAddAsync($"devices_sessions:{state.DeviceId}:data", data.Key);

                    var type = data.Value.GetType();

                    if (type.IsPrimitive || type.Equals(typeof(string)))
                    {
                        dataCommand.StringSetAsync($"devices_sessions:{state.DeviceId}:data:{data.Key}:value", data.Value.ToString());
                    }
                    else
                    {
                        dataCommand.StringSetAsync($"devices_sessions:{state.DeviceId}:data:{data.Key}:value", System.Text.Json.JsonSerializer.Serialize(data.Value));
                    }

                    dataCommand.StringSetAsync($"devices_sessions:{state.DeviceId}:data:{data.Key}:type", type.AssemblyQualifiedName);
                }
            }
        }
    }
}
