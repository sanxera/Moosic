using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MSC.SentimentAnalysis.API.Services
{
    public class SentimentAnalysisService : ISentimentAnalysisService
    {
        public string ExecuteAnalysis(string comment)
        {
            string sentiment = string.Empty;

            try
            {
                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = "C:\\Users\\Guilherme Sanches\\AppData\\Local\\Programs\\Python\\Python39\\python.exe";
                start.Arguments = $"E:\\pythonProject\\main.py {comment}";
                start.UseShellExecute = false;
                start.RedirectStandardOutput = true;
                using (Process process = Process.Start(start))
                {
                    using (StreamReader reader = process.StandardOutput)
                    {
                        string result = reader.ReadToEnd();
                        Console.Write(result);
                    }
                }

                return sentiment;
            }
            catch (Exception ex)
            {
                return sentiment;
            }
        }
    }
}
