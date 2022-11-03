﻿/*
 PlatformKit is dual-licensed under the GPLv3 and the PlatformKit Licenses.
 
 You may choose to use PlatformKit under either license so long as you abide by the respective license's terms and restrictions.
 
 You can view the GPLv3 in the file GPLv3_License.md .
 You can view the PlatformKit Licenses at https://neverspy.tech
  
  To use PlatformKit under a commercial license you must purchase a license from https://neverspy.tech
 
 Copyright (c) AluminiumTech 2018-2022
 Copyright (c) NeverSpy Tech Limited 2022
 */

using System;

namespace PlatformKit.Hardware.Common{
    /// <summary>
    /// A class to help store software component information.
    /// </summary>
    public class SoftwareComponentModel : ComponentModel{

        public bool IsProprietary { get; set; }
        public bool Is64Bit { get; set; }
        
        public Version SoftwareVersion { get; set; }

        public string Developer { get; set; }
        public string Publisher { get; set; }
    }
}