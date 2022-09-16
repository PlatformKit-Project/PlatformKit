using System;

namespace PlatformKit.Software;

public class InstalledApps
{

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="PlatformNotSupportedException"></exception>
    public static AppModel[] Get()
    {
        if (OSAnalyzer.IsWindows())
        {
            return WindowsApps.GetForWindows();
        }
        else if (OSAnalyzer.IsMac())
        {
            return MacApps.GetForMac();
        }
        else if (OSAnalyzer.IsLinux())
        {
            return LinuxApps.GetForLinux();
        }
        else if (OSAnalyzer.IsFreeBSD())
        {
            throw new PlatformNotSupportedException();
        }

        throw new PlatformNotSupportedException();
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="appModel"></param>
    /// <exception cref="PlatformNotSupportedException"></exception>
    public static void Open(AppModel appModel)
    {
        if (OSAnalyzer.IsWindows())
        {
           WindowsApps.OpenForWindows(appModel);
        }
        else if (OSAnalyzer.IsMac())
        { 
            MacApps.OpenForMac(appModel);
        }
        else if (OSAnalyzer.IsLinux())
        {
           LinuxApps.OpenForLinux(appModel);
        }

        throw new PlatformNotSupportedException();
    }

}