using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_MVCApp.Models;
using System;
namespace My_MVCApp.Controllers
{
    public class MyPizzaController : Controller
    {
        //only work when the connection is present in cobtext file
        //pizzadbContext dc=new pizzadbContext();
        pizzadbContext dc;
        public MyPizzaController(DbContextOptions<pizzadbContext> options)
        {
            dc=new pizzadbContext(options);
        }
        [HttpGet]
        public ViewResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(IFormCollection f)
        {
            string u = f["uname"];
            string p = f["pwd"];
            var res=(from t in dc.Registrations
                    where t.Uname==u && t.Password==p
                    select t).Count();
            if (res > 0)
            {
                HttpContext.Session.SetString("uname", u);
                return RedirectToAction("Menu");
            }
            else
            {
                ViewBag.r = "Login failed";
            }
                return View();
        }
        [HttpGet]
        public ViewResult Register()
        {
            return View();
        }
        [HttpPost]
        
        public ViewResult Register(Registration reg)
        {
            if (ModelState.IsValid)
            {
                //donot run the code if the page is error
                dc.Registrations.Add(reg);
                int res = dc.SaveChanges();
                if (res > 0)
                {
                    ViewBag.msg = "Registration SUccessfull";
                }
                else
                {
                    ViewBag.msg = "Registration Failed";
                }
            }
                return View();
        }
        public IActionResult Menu()
        {
            if (HttpContext.Session.GetString("uname")==null)
            {
                return RedirectToAction("Login");
            }

            var res = (from t in dc.Pizzas select t).ToList();
            int x = 100;
            return View(res);//
        }
        [HttpGet]
        public IActionResult Cart(string pid)
        {
            if (HttpContext.Session.GetString("uname") == null)
            {
                return RedirectToAction("Login");
            }
            var res=(from t in dc.Pizzas
                    where t.Pizzaid==pid
                    select t).ToList();
            
            return View(res);
        }
        [HttpPost]
        public IActionResult Cart(IFormCollection f)
        {
            if (HttpContext.Session.GetString("uname") == null)
            {
                return RedirectToAction("Login");
            }
            // try{
            Userorder u=new Userorder();
            u.Pizzaid = f["pid"];
            u.Username = HttpContext.Session.GetString("uname");
            u.Qty = int.Parse(f["qty"]);
            u.Transdate=DateOnly.FromDateTime(DateTime.Now);


            dc.Userorders.Add(u);
            int res = dc.SaveChanges();

            ViewBag.r = res > 0 ? "Order placed" : "Order not placed";
            var model = dc.Pizzas
                         .Where(p => p.Pizzaid == u.Pizzaid)
                         .ToList();

            return View(model);

}
        public ViewResult Search()
        {
            return View();
        }
        public ViewResult Logout()
        {
            HttpContext.Session.Remove("uname");
            return View();
        }
        public ViewResult Contact()
        {
            return View();
        }


    }
}
