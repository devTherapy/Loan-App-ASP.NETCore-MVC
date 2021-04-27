using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using NLog;

namespace SageraLoans.UI.Controllers
{

    /// <summary>
    /// To handle Server error and status codes like 404
    /// </summary>
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;
        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeResult(int statusCode)
        {
            switch (statusCode)
            {
               case 404:
                   ViewBag.Message = "OOps, can't find what you are looking for";
                   break;
            }
            return View("NotFound");
        }

        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            _logger.LogCritical(exceptionDetails.Path);
            _logger.LogCritical(exceptionDetails.Error.StackTrace);
            _logger.LogCritical(exceptionDetails.Error.Source);

            return View("Error");

        }
    }
}
