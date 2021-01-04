using System;
using System.IO;

namespace CGWH.Utilities
{
    public class DebugUtility
    {
        internal static readonly string Path = "Debug.log";

        public static void Log(string message)
        {
            using (StreamWriter writer = new StreamWriter(Path, true))
            {
                writer.WriteLine($"[{DateTime.Now}] {message}");
            }
        }
    }
}
