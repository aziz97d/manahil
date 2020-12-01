using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace manahil.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        [Route("Error/{StatusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry Resource Not Found";
                    break;
            }


            return View("NotFound");
        }
    }
}
