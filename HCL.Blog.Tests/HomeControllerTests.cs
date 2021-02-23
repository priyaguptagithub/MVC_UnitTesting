using HCL.Blog.Controllers;
using HCL.Blog.Models;
using HCL.Blog.Services;
using HCL.Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace HCL.Blog.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void Index_View_Result()
        {
            // Setup
            var expectedUserAccountCount = 2;

            var mockUserAccountService = new Mock<IUserAccountService>();

            mockUserAccountService.Setup(x => x.GetAll())
                .Returns(GetTestUserAccounts());

            // Inject
            var homeController = new HomeController(mockUserAccountService.Object);

            // Act
            var result = homeController.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<IEnumerable<UserAccountViewModel>>(
                viewResult.ViewData.Model);

            Assert.Equal(expectedUserAccountCount, model.Count());
        }

        [Fact]
        public void Account_View_Result_One()
        {
            // Setup
            var expectedUserAccountId = 123;

            var mockUserAccountService = new Mock<IUserAccountService>();

            mockUserAccountService.Setup(x => x.Get(expectedUserAccountId))
                .Returns(GetTestUserAccountOne());

            // Inject
            var homeController = new HomeController(mockUserAccountService.Object);

            // Act
            var result = homeController.Account(expectedUserAccountId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);

            var viewModel = Assert.IsAssignableFrom<UserAccountViewModel>(
                viewResult.ViewData.Model);

            Assert.Equal(expectedUserAccountId, viewModel.Id);
        }

        [Fact]
        public void Account_View_Result_Two()
        {
            // Setup
            var expectedUserAccountId = 456;

            var mockUserAccountService = new Mock<IUserAccountService>();

            mockUserAccountService.Setup(x => x.Get(expectedUserAccountId))
                .Returns(GetTestUserAccountTwo());

            // Inject
            var homeController = new HomeController(mockUserAccountService.Object);

            // Act
            var result = homeController.Account(expectedUserAccountId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);

            var viewModel = Assert.IsAssignableFrom<UserAccountViewModel>(
                viewResult.ViewData.Model);

            Assert.Equal(expectedUserAccountId, viewModel.Id);
        }

        [Fact]
        public void Get_First_Name_Result()
        {
            // Setup
            var userAccountId = 123;
            var userAccountFirstName = "Raj";

            var mockUserAccountService = new Mock<IUserAccountService>();

            mockUserAccountService.Setup(x => x.GetFirstName(userAccountId))
                .Returns(userAccountFirstName);

            // Inject
            var homeController = new HomeController(mockUserAccountService.Object);

            // Act
            var result = homeController.GetFirstName(userAccountId);

            // Assert
            Assert.Equal(userAccountFirstName, result);
        }

        private List<UserAccount> GetTestUserAccounts()
        {
            return new List<UserAccount>()
            {
                GetTestUserAccountOne(),
                GetTestUserAccountTwo(),
            };
        }

        private UserAccount GetTestUserAccountOne()
        {
            return new UserAccount
            {
                Id = 123,
                FirstName = "Raj",
                Surname = "Rawat",
            };
        }

        private UserAccount GetTestUserAccountTwo()
        {
            return new UserAccount
            {
                Id = 456,
                FirstName = "Aryan",
                Surname = "Rawat",
            };
        }
    }
}
