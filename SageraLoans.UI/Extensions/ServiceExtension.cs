using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SageraLoans.Core;
using SageraLoans.Models.AppSettingModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace SageraLoans.UI.Extensions
{
    public static class ServiceExtension
    {

        public static void ConfigureMailing(this IServiceCollection services, IConfiguration configuration)
        {
            var config = configuration.GetSection("MailSetting");
            services.Configure<MailSettingsModel>(config);
            services.AddTransient<IEmailing, Emailing>();
        }
    }
}
