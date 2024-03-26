namespace WTelegram;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using TL;

public partial interface IClient
{
    // TcpFactory TcpHandler { get; }
    String MTProxyUrl { get; }
    Config TLConfig { get; }
    Int32 MaxAutoReconnects { get; }
    Int32 MaxCodePwdAttempts { get; }
    Int32 FloodRetryThreshold { get; }
    Int32 PingInterval { get; }
    Int32 FilePartSize { get; }
    Boolean IsMainDC { get; }
    Boolean Disconnected { get; }
    Int64 UserId { get; }
    User User { get; }
    void Dispose();
    void DisableUpdates(Boolean disable = true);
    void Reset(Boolean resetUser = true, Boolean resetSessions = true);
    Task<Client> GetClientForDC(Int32 dcId, Boolean media_only = true, Boolean connect = true);
    Task ConnectAsync();
    Task<String> Login(String loginInfo);
    Task<User> LoginBotIfNeeded(String bot_token = null);
    Task<User> LoginUserIfNeeded(CodeSettings settings = null, Boolean reloginOnFailedResume = true);
    User LoginAlreadyDone(Auth_AuthorizationBase authorization);
    Task<T> Invoke<T>(IMethod<T> query);
    // Task<InputFileBase> UploadFileAsync(String pathname, ProgressCallback progress = null);
    // Task<InputFileBase> UploadFileAsync(Stream stream, String filename, ProgressCallback progress = null);
    // Task<Messages_MessagesBase> Messages_Search<T>(InputPeer peer, String text = null, Int32 offset_id = 0, Int32 limit = 2147483647);
    // Task<Messages_MessagesBase> Messages_SearchGlobal<T>(String text = null, Int32 offset_id = 0, Int32 limit = 2147483647);
    Task<Message> SendMediaAsync(InputPeer peer, String caption, InputFileBase mediaFile, String mimeType = null, Int32 reply_to_msg_id = 0, MessageEntity[] entities = null, DateTime schedule_date = default(DateTime));
    Task<Message> SendMessageAsync(InputPeer peer, String text, InputMedia media = null, Int32 reply_to_msg_id = 0, MessageEntity[] entities = null, DateTime schedule_date = default(DateTime), Boolean disable_preview = false);
    Task<Message[]> SendAlbumAsync(InputPeer peer, ICollection<InputMedia> medias, String caption = null, Int32 reply_to_msg_id = 0, MessageEntity[] entities = null, DateTime schedule_date = default(DateTime));
    // Task<Storage_FileType> DownloadFileAsync(Photo photo, Stream outputStream, PhotoSizeBase photoSize = null, ProgressCallback progress = null);
    // Task<String> DownloadFileAsync(Document document, Stream outputStream, PhotoSizeBase thumbSize = null, ProgressCallback progress = null);
    // Task<Storage_FileType> DownloadFileAsync(InputFileLocationBase fileLocation, Stream outputStream, Int32 dc_id = 0, Int64 fileSize = 0, ProgressCallback progress = null);
    Task<Storage_FileType> DownloadProfilePhotoAsync(IPeerInfo peer, Stream outputStream, Boolean big = false, Boolean miniThumb = false);
    Task<Messages_Chats> Messages_GetAllChats();
    Task<Messages_Dialogs> Messages_GetAllDialogs(Int32? folder_id = null);
    Task<Channels_ChannelParticipants> Channels_GetAllParticipants(InputChannelBase channel, Boolean includeKickBan = false, String alphabet1 = "АБCДЕЄЖФГHИІJКЛМНОПQРСТУВWХЦЧШЩЫЮЯЗ", String alphabet2 = "АCЕHИJЛМНОРСТУВWЫ", CancellationToken cancellationToken = default(CancellationToken));
    // Task<Channels_AdminLogResults> Channels_GetAdminLog(InputChannelBase channel, Flags events_filter = (Flags)0, String q = null, InputUserBase admin = null);
    Task<UpdatesBase> AddChatUser(InputPeer peer, InputUserBase user);
    Task<UpdatesBase> DeleteChatUser(InputPeer peer, InputUser user);
    Task<UpdatesBase> LeaveChat(InputPeer peer);
    Task<UpdatesBase> EditChatAdmin(InputPeer peer, InputUserBase user, Boolean is_admin);
    Task<UpdatesBase> EditChatPhoto(InputPeer peer, InputChatPhotoBase photo);
    Task<UpdatesBase> EditChatTitle(InputPeer peer, String title);
    Task<Messages_ChatFull> GetFullChat(InputPeer peer);
    Task<UpdatesBase> DeleteChat(InputPeer peer);
    Task<Messages_MessagesBase> GetMessages(InputPeer peer);
    Task<Messages_MessagesBase> GetMessages(InputPeer peer, params InputMessage[] id);
    Task<Messages_AffectedMessages> DeleteMessages(InputPeer peer, params Int32[] id);
    Task<Boolean> ReadHistory(InputPeer peer, Int32 max_id = 0);
    Task<ChatBase> AnalyzeInviteLink(String url, Boolean join = false, IDictionary<Int64, ChatBase> chats = null);
    Task<Messages_ChannelMessages> GetMessageByLink(String url, IDictionary<Int64, ChatBase> chats = null);
}
