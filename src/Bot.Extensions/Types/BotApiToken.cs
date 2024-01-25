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

/// <summary>
/// Represents a Bot API Token, which is a 64-bit integer value with a 35-character alphanumeric string separated by a colon.
/// </summary>
[ValueObject(
    typeof(string),
    conversions: /*Conversions.EfCoreValueConverter
        |*/
    Conversions.SystemTextJson | Conversions.TypeConverter
)]
[StructLayout(LayoutKind.Auto)]
public partial record struct BotApiToken : IRegexValueObject<BotApiToken>
{
    const int BotIdLength = 10;
    const int TokenLength = 35;

    /// <summary>
    /// The prefix string for the URI.
    /// </summary>
    /// <value>https://api.telegram.org/bot</value>
    public const string UriPrefixString = "https://api.telegram.org/bot";

    /// <summary>
    /// The template string for the URI.
    /// </summary>
    /// <value><inheritdoc cref="UriPrefixString" path="/value" />/{0}</value>
    public const string UriTemplateString = $"{UriPrefixString}{{0}}";

    /// <summary>
    /// An example value string for the Bot API Token.
    /// </summary>
    /// <value>1234567890:abcdefgHIJKLMNOPQRSTUVWXYZ1234567-_</value.
    public const string ExampleValueString = "1234567890:abcdefgHIJKLMNOPQRSTUVWXYZ1234567-_";

    /// <summary>
    /// A description of the Bot API Token.
    /// </summary>
    /// <value>A BotApiToken is a 64-bit integer value with a 35-character alphanumeric string separated by a colon.</value>
    public const string DescriptionString =
        "A BotApiToken is a 64-bit integer value with a 35-character alphanumeric string separated by a colon.";

    /// <summary>
    /// The empty value for the Bot API Token.
    /// </summary>
    /// <value>0000000000:000000</value>
    public static readonly string EmptyValueString =
        $"{new string('0', BotIdLength)}:{new string('0', TokenLength)}";

    /// <summary>
    /// The length of the Bot API Token.
    /// </summary>
    public static readonly int Length = EmptyValueString.Length;

    /// <summary>
    /// The regular expression string for validating the Bot API Token.
    /// </summary>
    public const string RegexString = @"(?<BotId>[0-9]{8,10}):(?<Token>[0-9a-zA-Z\-_]{30,35})";

    private static readonly Lazy<Regx> Regexp =
        new(() => new Regx(RegexString, Rxo.Compiled | Rxo.IgnoreCase));

#if NET7_0_OR_GREATER
    [GeneratedRegex(RegexString, Rxo.Compiled | Rxo.IgnoreCase)]
    public static partial Regex Regex();
#else
    public static Regx Regex() => Regexp.Value;
#endif

#if NET6_0_OR_GREATER
    /// <summary>
    /// The description of the Bot API Token.
    /// </summary>
    // static string IRegexValueObject<BotApiToken>.Description => Description;

    /// <summary>
    /// An example value of the Bot API Token.
    /// </summary>
    // static BotApiToken IRegexValueObject<BotApiToken>.ExampleValue =>
    //     From(ExampleValueString) with
    //     {
    //         OriginalString = ExampleValueString
    //     };
#endif

    /// <summary>
    /// The empty Bot API Token.
    /// </summary>
    public static BotApiToken Empty =>
        From(EmptyValueString) with
        {
            OriginalString = EmptyValueString
        };

    // /// <summary>
    // /// Compares the current instance with another object.
    // /// </summary>
    // public int CompareTo(object? obj) =>
    //     obj is BotApiToken other
    //         ? CompareTo(other)
    //         : throw new ArgumentException($"object must be of type {nameof(BotApiToken)}");

    /// <summary>
    /// Gets the Bot ID from the Bot API Token.
    /// </summary>
    public long? BotId =>
        long.TryParse(Regex().Match(Value).Groups[nameof(BotId)].Value, out var botId)
            ? botId
            : default;

    /// <summary>
    /// Checks if the Bot API Token is empty.
    /// </summary>
    public readonly bool IsEmpty => this == Empty;

#if NET6_0_OR_GREATER
    /// <summary>
    /// The regular expression string for validating the Bot API Token.
    /// </summary>
    // static string IRegexValueObject<BotApiToken>.RegexString => RegexString;
#else
    // readonly Regx IRegexValueObject<BotApiToken>.Regex() => Regex();

    // /// <summary>
    // /// The regular expression string for validating the Bot API Token.
    // /// </summary>
    // readonly string IRegexValueObject<BotApiToken>.RegexString => RegexString;

    // /// <summary>
    // /// The description of the Bot API Token.
    // /// </summary>
    // readonly string IRegexValueObject<BotApiToken>.Description => Description;

    // /// <summary>
    // /// An example value of the Bot API Token.
    // /// </summary>
    // readonly BotApiToken IRegexValueObject<BotApiToken>.ExampleValue => ExampleValue;
#endif

    /// <summary>
    /// An example value of the Bot API Token.
    /// </summary>
    readonly public BotApiToken ExampleValue =>
        From(ExampleValueString) with
        {
            OriginalString = ExampleValueString
        };

#if NET6_0_OR_GREATER
    //static string IRegexValueObject<BotApiToken>.RegexString => RegexString;
    // static string IRegexValueObject<BotApiToken>.Description => Description;
    // string IRegexValueObject<BotApiToken>.RegexString => throw new NotImplementedException();
    // string IRegexValueObject<BotApiToken>.Description => throw new NotImplementedException();
    //public static BotApiToken ExampleValue => From(ExampleValueString);
#endif

    /// <summary>
    /// Gets the URI representation of the Bot API Token.
    /// </summary>
    public Uri Uri => IsEmpty ? null! : new(Format(UriTemplateString, ToString()));

    /// <summary>
    /// Creates a Bot API Token from the given URI string.
    /// </summary>
    public BotApiToken FromUri(string s) => FromUri(s.Remove(0, UriPrefixString.Length));

    /// <summary>
    /// Creates a Bot API Token from the given URI.
    /// </summary>
    public BotApiToken FromUri(Uri u) => FromUri(u.ToString());

    /// <summary>
    /// Tries to parse the string representation of a Bot API Token.
    /// </summary>
    public static bool TryParse(string @string, out BotApiToken? value)
    {
        try
        {
            value = From(@string) with { OriginalString = @string };
            return true;
        }
        catch
        {
            value = default;
            return false;
        }
    }

    /// <summary>
    /// Validates the string representation of a Bot API Token.
    /// </summary>
    public static Validation Validate(string value)
    {
        if (value.Length != Length)
        {
            return Validation.Invalid(
                $"The length of the {nameof(BotApiToken)} must be {Length} characters."
            );
        }
        else if (!Regex().IsMatch(value))
        {
            return Validation.Invalid(
                $"The {nameof(BotApiToken)} must match the regular expression {RegexString}."
            );
        }
        else
        {
            return Validation.Ok;
        }
    }

    /// <summary>
    /// Parses the string representation of a Bot API Token.
    /// </summary>
    public static BotApiToken Parse(string value) => From(value) with { OriginalString = value };

    /// <summary>
    /// Parses the string representation of a Bot API Token with the specified format provider.
    /// </summary>
    public static BotApiToken Parse(string s, IFormatProvider? provider) => Parse(s);

    /// <summary>
    /// Tries to parse the string representation of a Bot API Token with the specified format provider.
    /// </summary>
    public static bool TryParse(string? s, IFormatProvider? provider, out BotApiToken result) =>
        (result = IsNullOrEmpty(Validate(s).ErrorMessage) ? Parse(s) : Empty) != Empty;

    /// <summary>
    /// Determines whether one Bot API Token is less than another Bot API Token.
    /// </summary>
    public static bool operator <(BotApiToken left, BotApiToken right)
    {
        return left.CompareTo(right) < 0;
    }

    /// <summary>
    /// Determines whether one Bot API Token is less than or equal to another Bot API Token.
    /// </summary>
    public static bool operator <=(BotApiToken left, BotApiToken right)
    {
        return left.CompareTo(right) <= 0;
    }

    /// <summary>
    /// Determines whether one Bot API Token is greater than another Bot API Token.
    /// </summary>
    public static bool operator >(BotApiToken left, BotApiToken right)
    {
        return left.CompareTo(right) > 0;
    }

    /// <summary>
    /// Determines whether one Bot API Token is greater than or equal to another Bot API Token.
    /// </summary>
    public static bool operator >=(BotApiToken left, BotApiToken right)
    {
        return left.CompareTo(right) >= 0;
    }

    public string OriginalString { get; init; }
}
