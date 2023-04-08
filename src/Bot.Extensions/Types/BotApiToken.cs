/*
 * BotApiToken.cs
 *
 *   Created: 2022-12-04-09:53:19
 *   Modified: 2022-12-04-09:53:20
 *
 *   Author: David G. Moore, Jr. <david@dgmjr.io>
 *
 *   Copyright © 2022-2023 David G. Moore, Jr., All Rights Reserved
 *      License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Telegram.Bot.Types;
using System;
using System.Runtime.InteropServices;
using Vogen;

[ValueObject(typeof(string), conversions: Conversions.EfCoreValueConverter | Conversions.SystemTextJson | Conversions.TypeConverter)]
// [RegexDto(@"(?<BotId:long>[0-9]{10}):(?<Token>:[0-9a-zA-Z\-_]{35})")]
[StructLayout(LayoutKind.Auto)]
public partial record struct BotApiToken : IStringWithRegexValueObject<BotApiToken>, IComparable<BotApiToken>, IComparable, IEquatable<BotApiToken>
{
    public const string ExampleValueString = "1234567890:abcdefgHIJKLMNOPQRSTUVWXYZ1234567-_";
    public const string Description = "A BotApiToken is a 64-bit integer value with a 35-character alphanumeric string separated by a colon.";
    public static readonly string EmptyValue = new string('0', 10) + ":" + new string('0', 35);
    public static readonly int Length = EmptyValue.Length;
    public const string RegexString = @"(?<BotId>[0-9]{8,10}):(?<Token>[0-9a-zA-Z\-_]{30,35})";
#if NET7_0_OR_GREATER
    [GeneratedRegex(RegexString, RegexOptions.Compiled | RegexOptions.IgnoreCase)]
    public static partial REx Regex();
#else
    private static readonly REx _regex = new(RegexString, RegexOptions.Compiled | RegexOptions.IgnoreCase);
    public static REx Regex() => _regex;
#endif

#if NET6_0_OR_GREATER
    //static string IStringWithRegexValueObject<BotApiToken>.Description => Description;
    static BotApiToken IStringWithRegexValueObject<BotApiToken>.ExampleValue => From(ExampleValueString);
#else
#endif
    public static BotApiToken Empty => From(EmptyValue);
    public int CompareTo(object? obj) => obj is BotApiToken other ? CompareTo(other) : throw new ArgumentException($"object must be of type {nameof(BotApiToken)}");
    public long? BotId => long.TryParse(Regex().Match(Value).Groups[nameof(BotId)].Value, out var botId) ? botId : default;

    public bool IsEmpty => this == Empty;

#if NET6_0_OR_GREATER
    static string IStringWithRegexValueObject<BotApiToken>.RegexString => RegexString;
#else
    REx IStringWithRegexValueObject<BotApiToken>.Regex() => Regex();
    string IStringWithRegexValueObject<BotApiToken>.RegexString => RegexString;
    string IStringWithRegexValueObject<BotApiToken>.Description => Description;
    BotApiToken IStringWithRegexValueObject<BotApiToken>.ExampleValue => From(ExampleValueString);
#endif
    public BotApiToken ExampleValue => throw new NotImplementedException();

#if NET6_0_OR_GREATER
        //static string IStringWithRegexValueObject<BotApiToken>.RegexString => RegexString;
        static string IStringWithRegexValueObject<BotApiToken>.Description => Description;

        // string IStringWithRegexValueObject<BotApiToken>.RegexString => throw new NotImplementedException();

        // string IStringWithRegexValueObject<BotApiToken>.Description => throw new NotImplementedException();

        //public static BotApiToken ExampleValue => From(ExampleValueString);

#endif

    public static bool TryParse(string @string, out BotApiToken? value)
    {
        try { value = From(@string); return true; }
        catch { value = default; return false; }
    }
    public static Validation Validate(string value)
    {
        if (value.Length != Length)
            return Validation.Invalid($"The length of the {nameof(BotApiToken)} must be {Length} characters.");
        else if (!Regex().IsMatch(value))
            return Validation.Invalid($"The {nameof(BotApiToken)} must match the regular expression {RegexString}.");
        else
            return Validation.Ok;
    }

    public static BotApiToken Parse(string value) => From(value);
    public static BotApiToken Parse(string s, IFormatProvider? provider) => Parse(s);
    public static bool TryParse(string? s, IFormatProvider? provider, out BotApiToken result)
        => (result = string.IsNullOrEmpty(Validate(s).ErrorMessage) ? Parse(s) : Empty) != Empty;
    public static bool operator <(BotApiToken left, BotApiToken right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator <=(BotApiToken left, BotApiToken right)
    {
        return left.CompareTo(right) <= 0;
    }

    public static bool operator >(BotApiToken left, BotApiToken right)
    {
        return left.CompareTo(right) > 0;
    }

    public static bool operator >=(BotApiToken left, BotApiToken right)
    {
        return left.CompareTo(right) >= 0;
    }
}
