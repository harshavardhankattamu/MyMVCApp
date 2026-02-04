using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace My_MVCApp.Controllers
{
    public class IndiaController : Controller
    {
        public IActionResult Index()
        {
            throw new DivideByZeroException("u r trying to divide by zero");
            return View();  
        }
        public IActionResult Error()
        {
            var a=HttpContext.Features.Get<IExceptionHandlerFeature>();
            ViewData["m"]="Message : "+a.Error.Message;
            ViewData["o"] = "Source : " + a.Error.Source;
            return View();
        }
    }
}
