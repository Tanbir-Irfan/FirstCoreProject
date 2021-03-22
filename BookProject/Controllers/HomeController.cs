using System;
using Microsoft.AspNetCore.Mvc;

namespace BookProject.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {

        }
        public string Index()
        {
            return "Hello World";
        }
    }
}
