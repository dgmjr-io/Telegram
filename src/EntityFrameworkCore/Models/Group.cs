/* 
 * Group.cs
 * 
 *   Created: 2023-03-25-12:55:13
 *   Modified: 2023-03-25-12:55:13
 * 
 *   Author: David G. Moore, Jr. <david@dgmjr.io>
 *   
 *   Copyright © 2022 - 2023 David G. Moore, Jr., All Rights Reserved
 *      License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Telegram.Models;
using System.Collections.ObjectModel;
using Telegram.Constants.DbConstants;
using static Telegram.Constants.DbConstants.SchemaNames;
using System.ComponentModel.DataAnnotations;

[Table(TableNames.Group, Schema = TeleSchema)]
public class Group : IGroup
{
    public long Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Username { get; set; } = default!;
}
