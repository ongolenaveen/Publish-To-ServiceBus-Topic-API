using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ServiceBus.Web.App.Contracts;
using ServiceBus.Web.App.Contracts.Models;
using System;
using System.Threading.Tasks;

namespace ServiceBus.Web.Api.Tests
{
    [TestFixture]
    public class TopicsControllerTests: TestBase
    {
        private Mock<IMessageService<Employee>> _mockMessageService;
        private TopicsController _sut;

        [SetUp]
        public void Setup()
        {
            _mockMessageService = _mockRepository.Create<IMessageService<Employee>>();
            _sut = new TopicsController(_mockMessageService.Object);
        }

        [Test]
        public async Task Publish_With_Invalid_Request_Returns_Bad_Response()
        {
            var employee = _fixture.Create<Employee>();
            _sut.ModelState.AddModelError("id", "Employee id is invalid.");
            var response = await _sut.Publish(employee);
            response.Should().BeOfType<BadRequestObjectResult>();
        }

        [Test]
        public void Publish_With_Error_While_Publishing_Raises_Exception()
        {
            var employee = _fixture.Create<Employee>();
            _mockMessageService.Setup(x => x.Publish(It.IsAny<Employee>())).Throws(new SystemException());
            Func<Task> action = async () => await _sut.Publish(employee);
            action.Should().Throw<SystemException>();
        }

        [Test]
        public async Task Publish_With_Valid_Request_Returns_Valid_Response()
        {
            var employee = _fixture.Create<Employee>();
            _mockMessageService.Setup(x => x.Publish(It.IsAny<Employee>())).Returns(Task.FromResult<object>(null));
            var response = await _sut.Publish(employee);
            response.Should().BeOfType<OkResult>();
        }
    }
}
