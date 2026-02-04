using Microsoft.AspNetCore.Mvc;

namespace My_MVCApp.Controllers
{
    public class StudentsController : Controller
    {
        IMath ob;
        public StudentsController(IMath obj)
        {
            ob = obj;
        }
        public ContentResult Hello()//string+html
        {
            return Content("<font color='red'>Hello World</font>","text/html");
        }
        public string Hi()//string
        {
            return "Hello hi  World!!";
        }
        public ViewResult india()
        {
            // only current method to current view page
            // it become null if u 
            ViewData["a"] = "bangalore";
            ViewData["p"] = 10;
            ViewData["q"] = 40;
            ViewBag.m = "delhi";
            ViewBag.c = 50;
            ViewBag.d = 40;
            string[] cn = { "india", "canada", "uk" };
            ViewBag.i = cn;
            //can be used across mult iple pages
            TempData["t"] = "asp.net mvc rocks";
            TempData.Keep();
            //viewbag is always dynamic
            //data is stored in object format
            int x = 100;//customer, list, data
            return View();
        }
        public ViewResult Home()
        {
            return View();
        }
        public ViewResult About()
        {
            return View();
        }
        public ViewResult M1(int id)
        {
            if (id > 10)
            {
                return View("v1");
            }
            else
            {
                return View("v2");
            }
        }
        [ActionName("gm")]
        public string goodmorning()
        {
            return "good morning my dear students!!!";
        }
        
        [NonAction]
        public string hot()
        {
            // any logic which you do not want to expose to the enduser
            return "Weather is very hot ";
        }

        [HttpGet]
        public ViewResult Add()
        {
            return View();
        }
        [HttpPost]
        

        public ViewResult Add(IFormCollection f)
        {
            var res = ob.AddNums(int.Parse(f["t1"]), int.Parse(f["t2"]));
            ViewBag.r="the sum is "+res;
            return View();
        }
        [HttpGet]
        public ViewResult Admin()
        {
            return View();
        }
        [HttpPost]
        public ViewResult Admin(string t1,string t2)
        {
            if(t1=="admin" && t2=="india")
            {
                ViewBag.r = "Valid";
            }
            else
            {
                ViewBag.r = "In Valid";
                ViewBag.r = "In Valid";
            }
                return View();
        }

    }
}
