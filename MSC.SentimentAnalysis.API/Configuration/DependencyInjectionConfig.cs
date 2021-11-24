using Microsoft.Extensions.DependencyInjection;
using MSC.SentimentAnalysis.API.Data.Repositories;
using MSC.SentimentAnalysis.API.Models.Interfaces;
using MSC.SentimentAnalysis.API.Services;

namespace MSC.SentimentAnalysis.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ISentimentAnalysisService, SentimentAnalysisService>();
            services.AddScoped<IInviteRepository, InviteRepository>();
        }
    }
}
