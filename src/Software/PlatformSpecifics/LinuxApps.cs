using System;
using System.Collections.Generic;

namespace PlatformKit.Software;

internal class LinuxApps
{
    internal static AppModel[] GetForLinux()
    {
        List<AppModel> allApps = new List<AppModel>();

        foreach (var app in GetAppsForLinux())
        {
            allApps.Add(app);
        }
        
        
        

    }

    protected static AppModel[] GetAppsForLinux()
    {
    
        string appsFromUsrBin =  new ProcessManager().RunLinuxCommand("ls -p | grep -v /");

#if NET5_0_OR_GREATER
        string[] apps = appsFromUsrBin.Split(Environment.NewLine);
#elif NETCOREAPP2_0_OR_GREATER
        string[] apps = appsFromLs.Split(' ');
#endif

        List<AppModel> appModels = new List<AppModel>();
        
        foreach (var app in apps)
        {
            appModels.Add(new AppModel()
            {
                Name = app,
                InstallationLocation = "/usr/bin/" + app,
            }); 
        }

        return appModels.ToArray();
    }
    
    internal static void OpenForLinux(AppModel appModel)
    {
        
    }
}