/*
 * Point.cs
 *
 *   Created: 2023-03-09-06:20:26
 *   Modified: 2023-03-09-06:20:27
 *
 *   Author: David G. Moore, Jr. <justin@Dgmjr.com>
 *
 *   Copyright © 2022-2023 David G. Moore, Jr., All Rights Reserved
 *      License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Telegram.Schema.Enum;

[GenerateEnumerationRecordStruct("Point", "Telegram.Schema")]
/// <summary>
/// The part of the face relative to which the mask should be placed. One of “forehead”,
/// “eyes”, “mouth”, or “chin”.
/// </summary>
public enum Point { Chin, Eyes, Forehead, Mouth };
