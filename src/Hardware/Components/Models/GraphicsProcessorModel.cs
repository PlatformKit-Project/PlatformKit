﻿/*
 PlatformKit is dual-licensed under the GPLv3 and the PlatformKit Licenses.
 
 You may choose to use PlatformKit under either license so long as you abide by the respective license's terms and restrictions.
 
 You can view the GPLv3 in the file GPLv3_License.md .
 You can view the PlatformKit Licenses at https://neverspy.tech
  
  To use PlatformKit under a commercial license you must purchase a license from https://neverspy.tech
 
 Copyright (c) AluminiumTech 2018-2022
 Copyright (c) NeverSpy Tech Limited 2022
 */

namespace PlatformKit.Hardware.Common{
    /// <summary>
    /// A class to store gpu information.
    /// </summary>
    public class GraphicsProcessorModel : HardwareComponentModel{
        public int NumberOfStreamProcessors { get; set; }
        public int NumberOfTextureMappingUnits { get; set; }
        public int NumberOfRasterOperatingUnits { get; set; }
        
        public int NumberOfComputeUnits { get; set; }
        
        //public GraphicsApiSupport GraphicsApiSupport { get; set; }
        
        public string ArchitectureName { get; set; }
        
        // ReSharper disable once InconsistentNaming
        public int L2CacheSizeMB { get; set; }
        // ReSharper disable once InconsistentNaming
        public int L1CacheSizeKB { get; set; }

        public string GraphicsProcessorModelName { get; set; }
        
        public SoftwareComponentModel Driver { get; set; }
        
        public string MaxRefreshRate { get; set; }
        public string MinRefreshRate { get; set; }

        public MemoryModel VideoMemory { get; set; }
        
        public string VideoModeDescription { get; set; }
        
        public string VideoMode { get; set; }
        public string VideoArchitecture { get; set; }
        
        public int BaseClockSpeedMHz { get; set; }
        public int BoostClockSpeedMHz { get; set; }
        
        public string FabricationProcess { get; set; }
    }
}