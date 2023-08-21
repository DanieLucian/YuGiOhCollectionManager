using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace Logger.Library
{
    public static class Log
    {
        private static readonly string logPath = ConfigurationManager.AppSettings["logPath"];

        public static async Task InitInfo()
        {
            using (StreamWriter streamWriter = new(logPath, append: true))
            {
                streamWriter.WriteLine($"{Environment.NewLine}----{DateTime.Now} : APPLICATION INITIALIZED----");
            }
        }

        public static async Task Info(string message)
        {
            using (StreamWriter streamWriter = new(logPath, append: true))
            {
                streamWriter.WriteLine($"{DateTime.Now} : {message}");
            }
        }
    }
}
