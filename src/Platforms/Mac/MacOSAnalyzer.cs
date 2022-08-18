/* MIT License

Copyright (c) 2018-2022 AluminiumTech

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
    */

using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

using PlatformKit.Internal.Exceptions;
using PlatformKit.Internal.Licensing;

namespace PlatformKit.Mac;

// ReSharper disable once InconsistentNaming
    /// <summary>
    /// macOS specific extensions to the OSAnalyzer class.
    /// </summary>
    public class MacOSAnalyzer
    {
        protected ProcessManager _processManager;

        public MacOSAnalyzer()
        {
            _processManager = new ProcessManager();
            
            LicenseManager.CheckLicenseStatus();
        }
        
        /// <summary>
        /// Returns whether or not a Mac is Apple Silicon based.
        /// </summary>
        /// <returns></returns>
        public bool IsAppleSiliconMac()
        {
            return GetMacProcessorType() == MacProcessorType.AppleSilicon;
        }
        
        /// <summary>
        /// Gets the type of Processor in a given Mac returned as the MacProcessorType
        /// </summary>
        /// <returns></returns>
        /// <exception cref="PlatformNotSupportedException"></exception>
        /// <exception cref="Exception"></exception>
        public MacProcessorType GetMacProcessorType()
        {
            try
            {
                if (OSAnalyzer.IsMac())
                {
                    return System.Runtime.InteropServices.RuntimeInformation.OSArchitecture switch
                    {
                        Architecture.Arm64 => MacProcessorType.AppleSilicon,
                        Architecture.X64 => MacProcessorType.Intel,
                        _ => MacProcessorType.NotDetected
                    };
                }

                throw new PlatformNotSupportedException();
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception);
                throw new Exception(exception.ToString());
            }
        }

        /// <summary>
        /// Gets a value from the Mac System Profiler information associated with a key
        /// </summary>
        /// <param name="macSystemProfilerDataType"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public string GetMacSystemProfilerInformation(MacSystemProfilerDataType macSystemProfilerDataType, string key)
        {
            string info = _processManager.RunMacCommand("system_profiler SP" + macSystemProfilerDataType);

#if NETSTANDARD2_0_OR_GREATER
            string[] array = info.Split(Convert.ToChar(Environment.NewLine));
#elif NET5_0_OR_GREATER
            string[] array = info.Split(Environment.NewLine);
#endif
            foreach (var str in array)
            {
                if (str.ToLower().Contains(key.ToLower()))
                {
                    return str.Replace(key, String.Empty).Replace(":", String.Empty);
                }
            }

            throw new ArgumentException();
        }    
    
        public bool IsSecureVirtualMemoryEnabled()
        {
            var result = GetMacSystemProfilerInformation(MacSystemProfilerDataType.SoftwareDataType, "Secure Virtual Memory");

            if (result.ToLower().Contains("disabled"))
            {
                return false;
            }
            else if (result.ToLower().Contains("enabled"))
            {
                return true;
            }
            else
            {
                throw new ArgumentException();
            }
        }
    
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public bool IsSystemIntegrityProtectionEnabled()
        {
            var result = GetMacSystemProfilerInformation(MacSystemProfilerDataType.SoftwareDataType, "System Integrity Protection");

            if (result.ToLower().Contains("disabled"))
            {
                return false;
            }
            else if (result.ToLower().Contains("enabled"))
            {
                return true;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public bool IsActivationLockEnabled()
        {
            var result = GetMacSystemProfilerInformation(MacSystemProfilerDataType.HardwareDataType, "Activation Lock Status");

            if (result.ToLower().Contains("disabled"))
            {
                return false;
            }
            else if (result.ToLower().Contains("enabled"))
            {
                return true;
            }
            else
            {
                throw new ArgumentException();
            }
        }
        
        /// <summary>
    /// Returns macOS version as a macOS version enum.
    /// </summary>
    /// <returns></returns>
    public MacOsVersion GetMacOsVersionToEnum()
    {
        return GetMacOsVersionToEnum(DetectMacOsVersion());
    }

    /// <summary>
    /// Converts a macOS version to a macOS version enum.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="OperatingSystemDetectionException"></exception>
    /// <exception cref="PlatformNotSupportedException">Thrown if run on Windows or macOS</exception>
    /// <exception cref="Exception"></exception>
    public MacOsVersion GetMacOsVersionToEnum(Version input)
    {
        try
        {
            if (OSAnalyzer.IsMac())
            {
                if (input.Major == 10)
                {
                    switch (input.Minor)
                    {
                        case 0:
                            //return MacOsVersion.v10_0_Cheetah;
                            return MacOsVersion.NotSupported;
                        case 1:
                            // return MacOsVersion.v10_1_Puma;
                            return MacOsVersion.NotSupported;
                        case 2:
                            // return MacOsVersion.v10_2_Jaguar;
                            return MacOsVersion.NotSupported;
                        case 3:
                            // return MacOsVersion.v10_3_Panther;
                            return MacOsVersion.NotSupported;
                        case 4:
                            // return MacOsVersion.v10_4_Tiger;
                            return MacOsVersion.NotSupported;
                        case 5:
                            // return MacOsVersion.v10_5_Leopard;
                            return MacOsVersion.NotSupported;
                        case 6:
                            //return MacOsVersion.v10_6_SnowLeopard;
                            return MacOsVersion.NotSupported;
                        case 7:
                            //return MacOsVersion.v10_7_Lion;
                            return MacOsVersion.NotSupported;
                        case 8:
                            //return MacOsVersion.v10_8_MountainLion;
                            return MacOsVersion.NotSupported;
                        case 9:
                            return MacOsVersion.v10_9_Mavericks;
                        case 10:
                            return MacOsVersion.v10_10_Yosemite;
                        case 11:
                            return MacOsVersion.v10_11_ElCapitan;
                        case 12:
                            return MacOsVersion.v10_12_Sierra;
                        case 13:
                            return MacOsVersion.v10_13_HighSierra;
                        case 14:
                            return MacOsVersion.v10_14_Mojave;
                        case 15:
                            return MacOsVersion.v10_15_Catalina;
                        //This is for compatibility reasons.
                        case 16:
                            return MacOsVersion.v11_BigSur;
                    }
                }

                if (input.Major == 11) return MacOsVersion.v11_BigSur;
                if (input.Major == 12) return MacOsVersion.v12_Monterey;
                if (input.Major == 13) return MacOsVersion.v13_Ventura;

                throw new OperatingSystemDetectionException();
            }
            else
            {
                throw new PlatformNotSupportedException();
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            throw new Exception(exception.ToString());
        }
    }

    public Version GetMacOsVersionFromEnum(MacOsVersion macOsVersion)
    {
        switch (macOsVersion)
        {
            case MacOsVersion.v10_9_Mavericks:
                return new(10, 9, 0, 0);
            case MacOsVersion.v10_10_Yosemite:
                return new(10, 10, 0, 0);
            case MacOsVersion.v10_11_ElCapitan:
                return new(10, 11, 0, 0);
            case MacOsVersion.v10_12_Sierra:
                return new(10, 12, 0, 0);
            case MacOsVersion.v10_13_HighSierra:
                return new(10, 13, 0, 0);
            case MacOsVersion.v10_14_Mojave:
                return new(10, 14, 0, 0);
            case MacOsVersion.v10_15_Catalina:
                return new(10, 15, 0, 0);
            case MacOsVersion.v11_BigSur:
                return new(11, 0, 0, 0);
            case MacOsVersion.v12_Monterey:
                return new(12, 0, 0, 0);
            case MacOsVersion.v13_Ventura:
                return new(13, 0, 0, 0);
            case MacOsVersion.NotSupported:
                throw new PlatformNotSupportedException();
            case MacOsVersion.NotDetected:
                throw new PlatformNotSupportedException();
            default:
                throw new PlatformNotSupportedException();
        }        
    }


    // ReSharper disable once InconsistentNaming
    public bool IsAtLeastMacOSVersion(MacOsVersion macOsVersion)
    {
        var detected = DetectMacOsVersion();

        var expected = GetMacOsVersionFromEnum(macOsVersion);

        if (detected.Major >= expected.Major)
        {
            if (detected.Minor >= expected.Minor)
            {
                if (detected.Build >= expected.Build)
                {
                    if (detected.Revision >= expected.Revision)
                    {
                        return true;
                    }

                    return false;
                }

                return false;
            }

            return false;
        }

        return false;
    }
    

    /// <summary>
    /// Detects macOS System Information.
    /// </summary>
    /// <returns></returns>
    public MacOsSystemInformation DetectMacSystemInformation()
    {
        MacOsSystemInformation macOsSystemInformation = new MacOsSystemInformation();
        macOsSystemInformation.ProcessorType = GetMacProcessorType();

        macOsSystemInformation.MacOsBuildNumber = DetectMacOsBuildNumber();
        macOsSystemInformation.MacOsVersion = DetectMacOsVersion();
        macOsSystemInformation.DarwinVersion = DetectDarwinVersion();
        macOsSystemInformation.XnuVersion = DetectXnuVersion();

        return macOsSystemInformation;
    }

    /// <summary>
    /// Detects the Darwin Version on macOS
    /// </summary>
    /// <returns></returns>
    /// <exception cref="PlatformNotSupportedException">Throws an error if it is run on Windows or Linux (or any OS which isn't macOS)</exception>
    public Version DetectDarwinVersion()
    {
        if (OSAnalyzer.IsMac())
        {
            var desc = RuntimeInformation.OSDescription;
            var arr = desc.Split(' ');

            int dotCounter = 0;
            
            foreach(var s in arr[1])
            {
                if (s == '.')
                {
                    dotCounter++;
                }
            }

            if (dotCounter == 2)
            {
                arr[1] = arr[1] + ".0";
                return Version.Parse(arr[1]);
            }
            if (dotCounter == 3)
            {
                return Version.Parse(arr[1]);
            }

            throw new PlatformNotSupportedException();
        }
        else
        {
            throw new PlatformNotSupportedException();
        }
    }
    
    /// <summary>
    /// Detects macOS's XNU Version.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="PlatformNotSupportedException">Not supported on platforms other than macOS</exception>
    public Version DetectXnuVersion()
    {
        if (OSAnalyzer.IsMac())
        {
            var desc = RuntimeInformation.OSDescription;
            var arr = desc.Split(' ');
        
            for (int index = 0; index < arr.Length; index++)
            {
                if (arr[index].ToLower().StartsWith("root:xnu-"))
                {
                    arr[index] = arr[index].Replace("root:xnu-", String.Empty)
                        .Replace("~", ".");

                    if (IsAppleSiliconMac())
                    {
                        arr[index] = arr[index].Replace("/RELEASE_ARM64_T8101", String.Empty);
                    }
                    else
                    {
                        arr[index] = arr[index].Replace("/RELEASE_X86_64", String.Empty);
                    }
                    
                    
                #if DEBUG
                    Console.WriteLine(arr[index]);
                #endif
                    
                    return Version.Parse(arr[index]);
                }
            }

            throw new PlatformNotSupportedException();
        }
        else
        {
            throw new PlatformNotSupportedException();
        }
    }

    /// <summary>
    /// Detects the macOS version and returns it as a System.Version object.
    /// </summary>
    /// <returns></returns>
    public Version DetectMacOsVersion()
    {
        var version = GetMacSwVersInfo()[1].Replace("ProductVersion:", String.Empty).Replace(" ", String.Empty);

        int dotCounter = 0;

        foreach (var str in version)
        {
            if (str == '.')
            {
                dotCounter++;
            }
        }

        if (dotCounter == 1)
        {
            version += ".0";
        }
        if (dotCounter == 2)
        {
            version += ".0";
        }
        
        return Version.Parse(version);
    }
    
    /// <summary>
    /// Detects the macOS Build Number.
    /// </summary>
    /// <returns></returns>
    public string DetectMacOsBuildNumber()
    {
        return GetMacSwVersInfo()[2].ToLower().Replace("BuildVersion:", String.Empty).Replace(" ", String.Empty);
    }

    // ReSharper disable once IdentifierTypo
    /// <summary>
    /// Gets info from sw_vers command on Mac.
    /// </summary>
    /// <returns></returns>
    private string[] GetMacSwVersInfo()
    {
        // ReSharper disable once StringLiteralTypo
        return new ProcessManager().RunMacCommand("sw_vers").Split(Convert.ToChar(Environment.NewLine));
    }
}