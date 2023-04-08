/* 
 * IGroup.cs
 * 
 *   Created: 2023-03-25-03:30:54
 *   Modified: 2023-03-25-03:30:54
 * 
 *   Author: David G. Moore, Jr. <david@dgmjr.io>
 *   
 *   Copyright © 2022 - 2023 David G. Moore, Jr., All Rights Reserved
 *      License: MIT (https://opensource.org/licenses/MIT)
 */ 

namespace Telegram.Abstractions;

public interface IGroup
{
    long Id { get; set; }
    string Name { get; set; }
    string Description { get; set; }
    string Username { get; set; }
}
