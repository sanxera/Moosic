using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSC.SentimentAnalysis.API.Services
{
    public interface ISentimentAnalysisService
    {
        string ExecuteAnalysis(string comment);
    }
}
