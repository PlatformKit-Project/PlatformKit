﻿/*
    PlatformKit
    
    Copyright (c) Alastair Lundy 2018-2023
    Copyright (c) NeverSpyTech Limited 2022

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Lesser General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
     any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU Lesser General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

namespace PlatformKit.Windows;

public class NetworkCard
{
    public string Name { get; set; }
    
    public string ConnectionName { get; set; }
    
    public bool DhcpEnabled { get; set; }
    
    public string DhcpServer { get; set; }

    public string[] IpAddresses { get; set; }
}