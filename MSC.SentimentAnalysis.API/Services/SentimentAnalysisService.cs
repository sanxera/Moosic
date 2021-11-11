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
                ProcessStartInfo start = new ProcessStartInfo
                {
                    FileName =
                        "C:\\Users\\Guilherme Sanches\\AppData\\Local\\Programs\\Python\\Python39\\python.exe",
                    Arguments = $"main.py {comment}",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = false
                };

                using Process process = Process.Start(start);

                if (process == null) return sentiment;

                using StreamReader reader = process.StandardOutput;
                sentiment = reader.ReadToEnd();

                return sentiment.Trim();
            }
            catch (Exception ex)
            {
                return sentiment;
            }
        }
    }
}
