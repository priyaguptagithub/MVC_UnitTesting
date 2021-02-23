using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using HCL.Blog.Services;
using HCL.Blog.Models;
using HCL.Blog.ViewModels;

namespace HCL.Blog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

     
      

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        private readonly IUserAccountService _userAccountService;

        //public HomeController(ILogger<HomeController> logger,IUserAccountService userAccountService)
        //{
        //    _logger = logger;
        //    _userAccountService = userAccountService;
        //}
        public HomeController(IUserAccountService userAccountService)
        {

            _userAccountService = userAccountService;
        }
        public IActionResult Index()
        {
            var userAccounts = _userAccountService.GetAll();

            var viewModel = userAccounts.Select(userAccount => new UserAccountViewModel
            {
                Id = userAccount.Id,
                FirstName = userAccount.FirstName,
                Surname = userAccount.Surname,
            });

            return View(viewModel);
        }

        public IActionResult Account(int id)
        {
            var userAccount = _userAccountService.Get(id);

            var viewModel = new UserAccountViewModel
            {
                Id = userAccount.Id,
                FirstName = userAccount.FirstName,
                Surname = userAccount.Surname,
            };

            return View(viewModel);
        }

        public string GetFirstName(int userAccountId)
        {
            return _userAccountService.GetFirstName(userAccountId);
        }

        

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
