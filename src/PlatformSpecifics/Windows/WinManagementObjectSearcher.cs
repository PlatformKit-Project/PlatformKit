/* MIT License

Copyright (c) 2018-2021 AluminiumTech

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
using System.IO;

namespace AluminiumTech.DevKit.PlatformKit.PlatformSpecifics.Windows
{
    public class WinManagementObjectSearcher : IWinManagementObjectSearcher
    {
        protected AluminiumTech.DevKit.PlatformKit.PlatformManager platform;
        protected AluminiumTech.DevKit.PlatformKit.ProcessManager processManager;

        /// <summary>
        ///
        /// WARNING: DO NOT RUN on NON-Windows platforms. This will result in errors.
        /// </summary>
        /// <param name="queryObjectsList"></param>
        /// <param name="wmiClass"></param>
        /// <returns></returns>
        public Dictionary<string, string> Get(List<string> queryObjectsList, string wmiClass)
        {
            Dictionary<string, string> queryObjectsDictionary = new Dictionary<string, string>();
            
            try
            {
                if (platform.IsWindows())
                {
                    processManager.RunProcessWindows("powershell.exe",
                        "/c Get-WmiObject -Class " + wmiClass + " | Select-Object *");

                    TextReader reader = Console.In;
                    string result = reader.ReadLine()?.Replace(wmiClass, "");

                    foreach (string query in queryObjectsList)
                    {
                        if (result != null && query.Contains(result))
                        {
                            var value = result.Replace(query + "                         : ", "");
                            queryObjectsDictionary.Add(query, value);
                        }
                        else if (result == null)
                        {
                            throw new ArgumentNullException();
                        }
                    }

                    reader.Close();

                    return queryObjectsDictionary;
                }
                else
                {
                    throw new PlatformNotSupportedException();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                return queryObjectsDictionary;
            }
        }
    }
}