using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Editor.Controllers {


    [Route("")]
    public class HomeController : Controller {

        private readonly ILogger _log;

        public HomeController(ILogger<HomeController> log) {
            _log = log;
        }


        [HttpGet("")]
        public IActionResult Index() {
            _log.LogDebug("Hello, world");
            return View();
        }
    }
}
