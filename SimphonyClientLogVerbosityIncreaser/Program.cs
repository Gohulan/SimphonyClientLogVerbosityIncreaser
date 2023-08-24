using System;
using System.Diagnostics;
using System.IO;

namespace LogModifierApp
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.BackgroundColor = ConsoleColor.Black;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Clear();
            Console.WriteLine("=========================================================  ");
            Console.WriteLine("     SIMPHONY CLIENT LOG VERBOSITY INCREASER - #UTIL11 ");
            Console.WriteLine("=========================================================  ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("     INCREASING LOG VERBOSITY LEVEL TO 100MB");
            Console.WriteLine("=========================================================  ");
            Console.WriteLine("");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=========================================================  ");
            Console.WriteLine("# DEVELOPER :- GOHULAN SOMANATHAN ");
            Console.WriteLine("=========================================================  ");
            Console.WriteLine("# COMPANY   :- HOSPITALITY TECHNOLOGY");
            Console.WriteLine("=========================================================  ");
            Console.WriteLine("# APP VER   :- 1.0.0 RELE 2023 AUGUST 24");
            Console.WriteLine("=========================================================  ");
            Console.WriteLine("");

            Console.ForegroundColor = ConsoleColor.White;

            string logFilePath = "log.txt"; // Change this to your desired log file name

            // Check if ServiceHost process is running and kill if necessary
            KillServiceHostProcess();

            // Copy and rename web.config file
            string configFilePath = @"C:\Micros\Simphony\WebServer\wwwroot\EGateway\web.config.txt";
            BackupAndModifyConfig(configFilePath);

            // Add settings to the original web.config file
            AddSettingsToConfig(configFilePath);

            // Log Header 

            string outputContent = @"
=========================================================
     SIMPHONY - CLIENT VERBOSITY INCREASER - #UTIL11
=========================================================
# MODIFIED DATE :- WEB.CONFIG.TXT - " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + @"
=========================================================
# DEVELOPER :- GOHULAN SOMANATHAN
=========================================================
# COMPANY   :- HOSPITALITY TECHNOLOGY
=========================================================
# APP VER   :- 1.0.0 RELE 2023 AUGUST 24
=========================================================
";

            

            // Log message

            LogMessage(logFilePath, outputContent);
            LogMessage(logFilePath, "Log verbosity level increased at " + Environment.MachineName);

            // Start ServiceHost process
            StartServiceHost();

            // Exit the program
            Environment.Exit(0);
        }

        static void KillServiceHostProcess()
        {
            Process[] processes = Process.GetProcessesByName("ServiceHost");
            foreach (Process process in processes)
            {
                process.Kill();
            }
        }

        static void BackupAndModifyConfig(string configFilePath)
        {
            string backupFilePath = configFilePath.Replace("web.config.txt", $"webconfig_{DateTime.Now:yyyyMMddHHmmss}.bak");
            File.Copy(configFilePath, backupFilePath);

            File.Copy(backupFilePath, configFilePath, true);
        }

        static void AddSettingsToConfig(string configFilePath)
        {
            string configContent = File.ReadAllText(configFilePath);
            string newSettings = @"<add key=""LogArchiver.Enabled"" value=""true""/>
    <add key=""LogArchiver.MinDiskUsedPercent"" value=""10""/>
    <add key=""LogArchiver.MaxDiskUsedMB"" value=""100""/>
    <add key=""LogArchiver.DaysToKeep"" value=""30""/>
";
            string modifiedConfigContent = configContent.Replace("</appSettings>", newSettings + "</appSettings>");
            File.WriteAllText(configFilePath, modifiedConfigContent);
        }

        static void LogMessage(string logFilePath, string message)
        {
            using (StreamWriter writer = File.AppendText(logFilePath))
            {
                writer.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
            }
        }

        static void StartServiceHost()
        {
            Process.Start(@"C:\Micros\Simphony\WebServer\ServiceHost.exe");
        }
    }
}
