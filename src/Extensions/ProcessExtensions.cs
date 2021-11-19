using System;
using System.Collections.Generic;
using System.Diagnostics;

using AluminiumTech.DevKit.PlatformKit.Deprecation.Deprecated;

using AluminiumTech.DevKit.PlatformKit.PlatformSpecifics.Windows;

namespace AluminiumTech.DevKit.PlatformKit.Extensions
{
    public static class ProcessExtensions
    {
        /// <summary>
        /// Get the list of processes as a String Array
        /// </summary>
        /// <returns></returns>
         static  string[] ToStringArray(this Process process)
        {
            var strList = new List<string>();
            Process[] processes = Process.GetProcesses();

            foreach (Process proc in processes)
            {
                strList.Add(proc.ProcessName);
            }

            strList.TrimExcess();
            return strList.ToArray();
        }
        
        /// <summary>
        /// Check to see if a process is running or not.
        /// </summary>
        public static bool IsProcessRunning(this Process process, string processName)
        {
            foreach (Process proc in Process.GetProcesses())
            {
                var procName =  proc.ProcessName.Replace("System.Diagnostics.Process (", String.Empty);

                //Console.WriteLine(proc.ProcessName);

                processName = processName.Replace(".exe", String.Empty);
                
                if (procName.ToLower().Equals(processName.ToLower()) ||
                    procName.ToLower().Contains(processName.ToLower()))
                {
                    return true;
                }
            }

            return false;
        }
        
        /// <summary>
        /// Converts a String to a Process
        /// </summary>
        /// <param name="processName"></param>
        /// <returns></returns>
        public static Process ConvertStringToProcess(this Process process, string processName)
        {
            try
            {
                processName = processName.Replace(".exe", "");

                if (IsProcessRunning(process, processName) ||
                    IsProcessRunning(process, processName.ToLower()) ||
                    IsProcessRunning(process, processName.ToUpper())
                )
                {
                    Process[] processes = Process.GetProcesses();

                    foreach (Process p in processes)
                    {
                        if (p.ProcessName.ToLower().Equals(processName.ToLower()))
                        {
                            return p;
                        }
                    }
                }

                return null;
                //  throw new Exception();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }
        }
        
                /// <summary>
        /// Suspends a process using native or imported method calls.
        /// </summary>
        /// <param name="processName"></param>
        /// <exception cref="PlatformNotSupportedException">This is currently only implemented on Windows and will throw an exception if run on Linux or macOS.</exception>
        public static void SuspendProcess(this Process process, string processName) { 
                    PlatformManager _platformManager = new PlatformManager();
                    
            if (_platformManager.IsWindows())
            {
                WindowsProcessSpecifics.Suspend(ConvertStringToProcess(process, processName));
            }
            else if (_platformManager.IsLinux())
            {
                throw new PlatformNotSupportedException();
            }
            else if (_platformManager.IsMac())
            {
                throw new PlatformNotSupportedException();
            }
#if NETCOREAPP3_0_OR_GREATER
            else if (_platformManager.IsFreeBSD())
            {
                throw new PlatformNotSupportedException();
            }
#endif
            else
            {
                throw new PlatformNotSupportedException();
            }
        }

        /// <summary>
        /// Resumes a process using native or imported method calls.
        /// WARNING: This is only implemented on Windows and will throw an exception if run on Linux or macOS.
        /// </summary>
        /// <param name="processName"></param>
        public static void ResumeProcess(this Process process, string processName)
        {
            PlatformManager _platformManager = new PlatformManager();
            
            if (_platformManager.IsWindows())
            {
                if (IsProcessRunning(process, processName))
                {
                    WindowsProcessSpecifics.Resume(ConvertStringToProcess(process, processName));
                }
            }
            else if (_platformManager.IsLinux())
            {
                throw new PlatformNotSupportedException();
            }
            else if (_platformManager.IsMac())
            {
                throw new PlatformNotSupportedException();
            }
#if NETCOREAPP3_0_OR_GREATER
            else if (_platformManager.IsFreeBSD())
            {
                throw new PlatformNotSupportedException();
            }
#endif
            else
            {
                throw new PlatformNotSupportedException();
            }
        }
    }
}