using CGWH.Utilities;
using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace CGWH
{
    internal class Cheat
    {
        internal const string NAME = "CGWH";
        internal const string PROCESS_NAME = "csgo";
        internal const string MODULE_DLL_NAME = "client.dll";

        internal int ModuleAddress;
        internal Memory Memory;
        internal bool IsGetProcess()
        {
            try
            {
                Process[] processes = Process.GetProcesses();
                Process process = null;

                for (int i = 0; i < processes.Length; i++)
                {
                    if (processes[i].ProcessName == PROCESS_NAME)
                    {
                        process = processes[i];
                        break;
                    }
                }

                DebugUtility.Log(">> IsGetProcess()");
                DebugUtility.Log($"[DEBUG:1] {(process?.ProcessName ?? "NULL")} P: ({string.Join(" , ", processes.Select(p => p.ProcessName))})");
                if (process != null)
                {
                    Memory = new Memory(PROCESS_NAME);
                    string[] array = new string[process.Modules.Count];
                    for (int i = 0; i < process.Modules.Count; i++)
                    {
                        array[i] = string.Format("{0}:{1}", process.Modules[i].ModuleName, process.Modules[i].FileName);
                        if (process.Modules[i].ModuleName == MODULE_DLL_NAME)
                        {
                            ModuleAddress = (int)process.Modules[i].BaseAddress;
                            return true;
                        }
                    }
                    DebugUtility.Log($"[DEBUG:2] {string.Join(" , ", array)}");
                }
            }
            catch { }
            return false;
        }



        //internal const string VERSION_DATE = "VersionDate=Dec 18 2020";
        internal const string VERSION_DATE = "VersionDate=Jan 07 2021";
        //internal const string VERSION_TIME = "VersionTime=14:30:05";
        internal const string VERSION_TIME = "VersionTime=16:32:47";

        internal bool IsValidVersion()
        {
            string steamPath = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Wow6432Node\\Valve\\Steam", "InstallPath", null).ToString();
            string infPath = steamPath + "\\steamapps\\common\\Counter-Strike Global Offensive\\csgo\\steam.inf";
            DebugUtility.Log(">> IsGetProcess()");
            DebugUtility.Log($"[DEBUG:1] {infPath}");
            if (File.Exists(infPath))
            {
                string infText = File.ReadAllText(infPath);
                if (infText.Contains(VERSION_DATE) && infText.Contains(VERSION_TIME))
                    return true;
            }
            return false;
        }
    }
}
