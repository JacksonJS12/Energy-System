namespace PropertyTests
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using EnergySystem.Services.Data.Grid;
    using EnergySystem.Services.Data.Property;
    using EnergySystem.Web.Controllers;
    using EnergySystem.Web.ViewModels.Property;

    using FluentAssertions;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    using Moq;

    using Xunit;

    public class PropertyControllerTests
    {
        private readonly Mock<IPropertyService> _mockPropertyService;
        private readonly Mock<IGridService> _mockGridService;
        private readonly PropertyController _controller;

        public PropertyControllerTests()
        {
            this._mockPropertyService = new Mock<IPropertyService>();
            this._mockGridService = new Mock<IGridService>();
            this._controller = new PropertyController(this._mockPropertyService.Object, _mockGridService.Object);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "user123"),
            },
            "mock"));

            this._controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
        }

        [Fact]
        public void Create_Get_ShouldReturnViewWithModel()
        {
            this._mockGridService.Setup(service => service.GetAllAsKeyValuePairs())
                .Returns(new List<KeyValuePair<string, string>>());

            var result = this._controller.Create();

            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            var model = viewResult.Model.Should().BeOfType<CreateInputModel>().Subject;
            model.GridsItems.Should().NotBeNull();
        }

        [Fact]
        public async Task Create_Post_ShouldRedirectToAll_WhenModelIsValid()
        {
            var input = new CreateInputModel();
            this._mockPropertyService.Setup(service => service.CreateAsync(It.IsAny<CreateInputModel>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var result = await this._controller.Create(input);

            var redirectToAction = result.Should().BeOfType<RedirectToActionResult>().Subject;
            redirectToAction.ActionName.Should().Be("All");
        }

        [Fact]
        public void All_ShouldReturnViewWithProperties()
        {
            this._mockPropertyService.Setup(service => service.GeAll<PropertyInListViewModel>(It.IsAny<string>()))
                .Returns(new List<PropertyInListViewModel>());

            var result = this._controller.All();

            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            var model = viewResult.Model.Should().BeOfType<PropertiesListViewModel>().Subject;
            model.Properties.Should().NotBeNull();
        }

        [Fact]
        public void Details_ShouldReturnView_WhenPropertyExists()
        {
            var propertyId = Guid.NewGuid().ToString();
            this._mockPropertyService.Setup(service => service.GetById<SinglePropertyViewModel>(propertyId, It.IsAny<string>()))
                .Returns(new SinglePropertyViewModel());

            var result = this._controller.Details(propertyId);

            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            viewResult.Model.Should().BeOfType<SinglePropertyViewModel>();
        }

        [Fact]
        public async Task Delete_ShouldRedirectToAll_WhenDeletionIsSuccessful()
        {
            var propertyId = "testId";
            this._mockPropertyService.Setup(service => service.DeleteAsync(propertyId, It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var result = await this._controller.Delete(propertyId);

            var redirectToAction = result.Should().BeOfType<RedirectToActionResult>().Subject;
            redirectToAction.ActionName.Should().Be("All");
        }

        [Fact]
        public void Edit_Get_ShouldReturnView_WhenPropertyExists()
        {
            var propertyId = "testId";
            var model = new EditPropertyInputModel();
            this._mockPropertyService.Setup(service => service.GetById<EditPropertyInputModel>(propertyId, It.IsAny<string>()))
                .Returns(model);
            this._mockGridService.Setup(service => service.GetAllAsKeyValuePairs())
                .Returns(new List<KeyValuePair<string, string>>());

            var result = this._controller.Edit(propertyId);

            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            viewResult.Model.Should().BeOfType<EditPropertyInputModel>();
        }

        [Fact]
        public void Create_Get_ShouldRequireAuthorization()
        {
            var attributes = typeof(PropertyController).GetMethod("Create", new Type[] {})
                .GetCustomAttributes(typeof(AuthorizeAttribute), true);

            attributes.Should().NotBeEmpty();
        }

        [Fact]
        public void Create_Post_ShouldRequireAuthorization()
        {
            var attributes = typeof(PropertyController).GetMethod("Create", new Type[] { typeof(CreateInputModel) })
                .GetCustomAttributes(typeof(AuthorizeAttribute), true);

            attributes.Should().NotBeEmpty();
        }

        [Fact]
        public void All_ShouldRequireAuthorization()
        {
            var attributes = typeof(PropertyController).GetMethod("All")
                .GetCustomAttributes(typeof(AuthorizeAttribute), true);

            attributes.Should().NotBeEmpty();
        }

        [Fact]
        public void Details_ShouldRequireAuthorization()
        {
            var attributes = typeof(PropertyController).GetMethod("Details")
                .GetCustomAttributes(typeof(AuthorizeAttribute), true);

            attributes.Should().NotBeEmpty();
        }

        [Fact]
        public void Edit_Get_ShouldRequireAuthorization()
        {
            var attributes = typeof(PropertyController).GetMethod("Edit", new Type[] { typeof(string) })
                .GetCustomAttributes(typeof(AuthorizeAttribute), true);

            attributes.Should().NotBeEmpty();
        }

        [Fact]
        public void Edit_Post_ShouldRequireAuthorization()
        {
            var attributes = typeof(PropertyController).GetMethod("Edit", new Type[] { typeof(EditPropertyInputModel), typeof(string) })
                .GetCustomAttributes(typeof(AuthorizeAttribute), true);

            attributes.Should().NotBeEmpty();
        }

        [Fact]
        public void Delete_ShouldRequireAuthorization()
        {
            var attributes = typeof(PropertyController).GetMethod("Delete")
                .GetCustomAttributes(typeof(AuthorizeAttribute), true);

            attributes.Should().NotBeEmpty();
        }
    }
}