using System;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using BookProject.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using BookProject.Repository;
using BookProject.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookProject.Controllers
{
    [Route("[Controller]/[action]")]
    public class HomeController : Controller
    {
        // IConfiguration
        //private readonly IConfiguration configuration;

        //public HomeController(IConfiguration _configuration)
        //{
        //    configuration = _configuration;
        //}

        //IOption
        private readonly NewBookAlertConfig _newBookAlertConfiguration;
        private readonly NewBookAlertConfig _thirdPartyBookConfiguration;
        private readonly IMessageRepository _messageRepository;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;

        public HomeController(
            IOptionsSnapshot<NewBookAlertConfig> newBookAlertConfiguration, 
            IMessageRepository messageRepository, 
            IUserService userService,
            IEmailService emailService)
        {
            this._userService = userService;
            this._newBookAlertConfiguration = newBookAlertConfiguration.Get("InternalBook");
            this._thirdPartyBookConfiguration = newBookAlertConfiguration.Get("ThirdPartyBook");
            this._messageRepository = messageRepository;
            this._emailService = emailService;
        }

        [ViewData]
        public string Title { get; set; } // this is called viewdata attribute

        [Route("/home")]
        //[Route("[Controller]")]

        [Route("/")]
        //[Route("[Controller]/[action]")]
        public async Task<ViewResult> Index()
        {

            UserEmailOptions userEmailOptions = new UserEmailOptions()
            {
                toEmails = new List<string>()
                {
                    "test@gmail.com"
                },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{Username}}", "Tanbir Irfan")
                }
            };

            await _emailService.SendTestEmail(userEmailOptions);

            // var userId = _userService.GetUserId();
            // var userLoggedIn = _userService.IsAuthenticated();
            
            // bool isDisplay1 = _newBookAlertConfiguration.DisplayNewBookAlert;
            // bool isDisplay2 = _thirdPartyBookConfiguration.DisplayNewBookAlert;

            // Singleton Repository test (Message Repository)
            //var value = _messageRepository.GetName();

            ////Third way to read configuration file
            //var newBookAlert = new NewBookAlertConfig();
            //_newBookAlertConfiguration.Bind("NewBookAlert",newBookAlert);
            //bool tryV = newBookAlert.DisplayNewBookAlert;

            //Second way to read configuration file
            //var result = configuration.GetSection("NewBookAlert");
            ////var result = configuration.GetSection("NewBookAlert").GetValue<bool>("DisplayNewBookAlert");
            //var result1 = result.GetValue<bool>("DisplayNewBookAlert");
            //var result2 = result.GetValue<string>("BookMsg");

            //First way to read configuration file
            //var result = configuration["AppName"];
            //var key1 = configuration["infoObj:key1"];
            //var key2 = configuration["infoObj:key2"];
            //var key3 = configuration["infoObj:key3:key3obj1"];


            //ViewBag
            //dynamic data = new ExpandoObject();
            //data.Id = 123;
            //data.Name = "Tanbir Irfan";
            //ViewBag.Data = data;
            //ViewBag.Title = "Home Page";

            //ViewData
            //ViewData["Id"] = 123;
            //ViewData["book"] = new BookModel() { Id = 45, Author = "Test Purpose"};

            Title = "Home";
            return View();
        }

        // to get more route constraints please visit the link
        //https://github.com/dotnet/aspnetcore/tree/main/src/Http/Routing/src/Constraints
        //[Route("about-us/{id}/test/{name?}")]
        //[Route("about-us", Name = "about-us", Order = 1)]
        //[HttpGet]
        [Route("/about-us/{name:alpha:minlength(5)}")]
        [Route("/Home/AboutUs")]
        public ViewResult AboutUs() // int id, string name as parameter
        {
            Title = "About";
            return View();
        }


        //[Route("contact-us", Name = "contace-us")]
        public ViewResult ContactUs()
        {
            return View();
        }

        [Route("/test/a{a}")]
        public string Test1(string a)
        {
            return a;
        }

        [Route("/test/b{a}")]
        public string Test2(string a)
        {
            return a;
        }

        [Route("/test/c{a}")]
        public string Test3(string a)
        {
            return a;
        }
    }
}
