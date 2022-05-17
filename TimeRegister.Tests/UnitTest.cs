
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit;
using TimeRegisterApp.Context;
using TimeRegisterApp.Controllers;
using TimeRegisterApp.Models;
using TimeRegisterApp.Services;
using Xunit;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TimeRegister.Tests
{
    public class UnitTest
    {
        public readonly ApplicationContext _applicationContext;
        public UnitTest()
        {
            _applicationContext = GetInMemoryContex();
        }

        [Fact]
        public void AddPoject_ReturnsARedirect_AndAddsProject()
        {
            // Arrange
            var mock = new Mock<TimeCounterService>();
            _applicationContext.Add(AddTestProject());

            var controller = new HomeController(_applicationContext, mock.Object);

            // Act
            var result = controller.AddProject(AddTestProject());

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("GetProject", redirectToActionResult.ActionName);
        }

        [Fact]
        public void GetProject_WhenProjectNonExist_ReturnsError()
        {
            // Arrange
            _applicationContext.Add(AddTestProject());
            var mock = new Mock<TimeCounterService>();

            var controller = new HomeController(_applicationContext, mock.Object);

            // Act
            var result = controller.GetProject();

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Error", redirectToActionResult.ActionName);
        }

        [Fact]
        public void IndexReturnsARedirectToIndexHomeWhenIdIsNull()
        {
            int projectId;
            var project = AddTestProject();
            var newCheckpoint = new Checkpoint();

            _applicationContext.Add(project);
            newCheckpoint.ProjectId = project.Id;
            newCheckpoint.MarkedTime = "--.--.--";
            project.Checkpoints.Add(newCheckpoint);

            _applicationContext.SaveChanges();
            projectId = project.Id;

            var mock = new Mock<TimeCounterService>();

            // Arrange
            var controller = new HomeController(_applicationContext, mock.Object);

            // Act
            var result = controller.Index(projectId) as ViewResult;

            // Assert
            var ViewResult = Assert.IsType<ViewResult>(result);
            result.Should().NotBeNull();
        }

        public ApplicationContext GetInMemoryContex()
        {
            var builder = new DbContextOptionsBuilder<ApplicationContext>();
            builder.UseInMemoryDatabase("TestDB");

            return new ApplicationContext(builder.Options);
        }

        public Project AddTestProject()
        {
            return new Project { Name = "NewTestName" };
        }
    }
}
