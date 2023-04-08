/*
 * DatumType.cs
 *
 *   Created: 2023-03-09-06:22:05
 *   Modified: 2023-03-09-06:22:05
 *
 *   Author: David G. Moore, Jr. <justin@Dgmjr.com>
 *
 *   Copyright © 2022-2023 David G. Moore, Jr., All Rights Reserved
 *      License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Telegram.Schema.Enum;

[GenerateEnumerationRecordStruct("DatumType", "Telegram.Schema")]
/// <summary>
/// Element type. One of “personal\_details”, “passport”, “driver\_license”,
/// “identity\_card”, “internal\_passport”, “address”, “utility\_bill”, “bank\_statement”,
/// “rental\_agreement”, “passport\_registration”, “temporary\_registration”,
/// “phone\_number”, “email”.
/// </summary>
public enum DatumType { Address, BankStatement, DriverLicense, Email, IdentityCard, InternalPassport, Passport, PassportRegistration, PersonalDetails, PhoneNumber, RentalAgreement, TemporaryRegistration, UtilityBill };
