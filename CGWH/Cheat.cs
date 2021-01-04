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
                Process process = Process.GetProcessesByName(PROCESS_NAME).FirstOrDefault();
                if (process != null)
                {
                    Memory = new Memory(PROCESS_NAME);
                    foreach (ProcessModule module in process.Modules)
                    {
                        if (module.ModuleName == MODULE_DLL_NAME)
                        {
                            ModuleAddress = (int)module.BaseAddress;
                            return true;
                        }
                    }
                }
            }
            catch { }
            return false;
        }



        internal const string VERSION_DATE = "VersionDate=Dec 18 2020";
        internal const string VERSION_TIME = "VersionTime=14:30:05";

        internal bool IsValidVersion()
        {
            FileInfo infFile = new FileInfo(Path.Combine(Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Wow6432Node\\Valve\\Steam", "InstallPath", null).ToString(), "\\steamapps\\common\\Counter-Strike Global Offensive\\csgo\\steam.inf"));
            if (infFile.Exists)
            {
                string infText = File.ReadAllText(infFile.FullName);
                if (infText.Contains(VERSION_DATE) && infText.Contains(VERSION_TIME))
                    return true;
            }
            return false;
        }
    }
}
