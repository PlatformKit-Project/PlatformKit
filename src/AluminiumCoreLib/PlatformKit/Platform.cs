﻿/*
MIT License

Copyright (c) 2018-2020 AluminiumTech

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
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;

namespace AluminiumCoreLib.PlatformKit{
    /// <summary>
    /// A class that helps get system information.
    /// </summary>
    public class Platform {
        /// <summary>
        /// Returns the OS Architecture as a string.
        /// </summary>
        /// <returns></returns>
        private CPUArchitectureFamily GetOSArchitectureFamilyToEnum(){
            var x = ToString().ToLower();

            try{
                if (x.Contains("arm") && !x.Contains("64"))
                {
                    return CPUArchitectureFamily.ARM32;
                }
                else if (x.Contains("x86") && !x.Contains("-64"))
                {
                    return CPUArchitectureFamily.X86;
                }
                else if (x.Contains("arm") && x.Contains("64"))
                {
                    return CPUArchitectureFamily.ARM64;
                }
                else if ((x.Contains("x64")) || (x.Contains("x86") && x.Contains("-64")))
                {
                    return CPUArchitectureFamily.X64;
                }

                return CPUArchitectureFamily.NotDetected;
            }
            catch(Exception ex){
                throw new Exception(ex.ToString());
            }
        }
        /// <summary>
        /// Returns the OS Architecture as a string.
        /// </summary>
        /// <returns></returns>
        public string GetOSArchitectureToString(){
            return RuntimeInformation.OSArchitecture.ToString();
        }
        /// <summary>
        /// Determine what OS is being run
        /// </summary>
        /// <returns></returns>
        public OSPlatform GetOSPlatform() {
            OSPlatform osPlatform = OSPlatform.Create("Other Platform");
            // Check if it's windows
            bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            osPlatform = isWindows ? OSPlatform.Windows : osPlatform;
            // Check if it's osx
            bool isOSX = RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
            osPlatform = isOSX ? OSPlatform.OSX : osPlatform;
            // Check if it's Linux
            bool isLinux = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
            osPlatform = isLinux ? OSPlatform.Linux : osPlatform;
            return osPlatform;
        }

        /// <summary>
        /// Gets the OS platform as a String.
        /// </summary>
        /// <returns></returns>
        public override string ToString(){
            if (GetOSPlatform().ToString().ToLower().Equals("windows")){ 
                    return "Windows";
            }
            else if (GetOSPlatform().ToString().ToLower().Equals("mac") || GetOSPlatform().ToString().ToLower().Equals("mac"))
            {
                if (RuntimeInformation.OSDescription.ToLower().Contains("osx") || RuntimeInformation.OSDescription.ToLower().Contains("darwin")){
                    return "macOS";
                }
            }
            else if (GetOSPlatform().ToString().ToLower().Equals("linux")){
                return "Linux";
            }
            return null;
        }

        /// <summary>
        /// Returns whether or not the current OS is 64 Bit.
        /// </summary>
        /// <returns></returns>
        public bool Is64BitOS(){
            return Environment.Is64BitOperatingSystem;
        }
        /// <summary>
        /// Returns whether or not the App currently running is 64 Bit or not.
        /// </summary>
        /// <returns></returns>
        public bool Is64BitApp(){
            return Environment.Is64BitProcess;
        }

        /// <summary>
        /// Returns the OS Family as an Enum
        /// </summary>
        /// <returns></returns>
        public OperatingSystemFamily ToEnum(){
            if (ToString().ToLower().Equals("windows")){
                        return OperatingSystemFamily.Windows;
            }
            else if (ToString().ToLower().Equals("mac")){
                        return OperatingSystemFamily.macOS;
            }
            else if (ToString().ToLower().Equals("linux")){
                    return OperatingSystemFamily.Linux;
            }
            else if (ToString().ToLower().Equals("unix")){
                return OperatingSystemFamily.Unix;
            }
            return OperatingSystemFamily.NotDetected;
        }

        /// <summary>
        /// Return an app's version as a string.
        /// </summary>
        /// <returns></returns>
        public string GetAppVersionToString() {
            return Assembly.GetEntryAssembly().GetName().Version.ToString();
        }
        /// <summary>
        /// Return an app's version as a Version data type.
        /// </summary>
        /// <returns></returns>
        public System.Version GetAppVersionToVersion() {
            return Assembly.GetEntryAssembly().GetName().Version;
        }

        /// <summary>
        /// Display license information in the Console from a Text File
        /// </summary>
        public void ShowLicenseInConsole(string PathToTextFile, int durationMilliSeconds){
            Stopwatch licenseWatch = new Stopwatch();
            licenseWatch.Reset();
            licenseWatch.Start();
            string[] lines;

            try{
                lines = File.ReadAllLines(PathToTextFile);

                foreach (string line in lines){
                    Console.WriteLine(line);
                }

                Console.WriteLine("                                                         ");
                Console.WriteLine("                                                         ");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Here are some details in case you need them:");
                Console.WriteLine(ex.ToString());
            }

            while (licenseWatch.ElapsedMilliseconds <= durationMilliSeconds){
            //Do nothing to make sure everybody sees the license.
            }
          }
  
    }
}