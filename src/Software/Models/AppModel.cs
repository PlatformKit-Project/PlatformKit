#nullable enable
using System;

namespace PlatformKit.Software;

public class AppModel
{
    public string Name { get; set; }
    
    public string InstallationLocation { get; set; }
    
    public Version? InstalledVersion { get; set; }
}