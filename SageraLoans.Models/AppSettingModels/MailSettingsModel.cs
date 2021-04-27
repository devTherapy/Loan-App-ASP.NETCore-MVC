using System;
using System.Collections.Generic;
using System.Text;

namespace SageraLoans.Models.AppSettingModels
{
   public  class MailSettingsModel
    {
        public string SenderEmail { get; set; }
        public string SendGrid_API_KEY { get; set; }

    }
}
