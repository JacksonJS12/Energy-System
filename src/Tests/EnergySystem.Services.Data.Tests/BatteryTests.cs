namespace EnergySystem.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using EnergySystem.Data.Common.Repositories;
    using EnergySystem.Data.Models;
    using EnergySystem.Services.Data.Battery;
    using EnergySystem.Web.Controllers;
    using EnergySystem.Web.ViewModels.Battery;
    using FluentAssertions;
    using Mapping;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Moq;

    using Property;

    using Xunit;

    using Battery=global::Battery;

    public class BatteryServiceTests
    {
        private readonly Mock<IDeletableEntityRepository<Battery>> _mockBatteryRepository;
        private readonly BatteryService _batteryService;
        private readonly Mock<IBatteryService> _mockBatteryService;
        private readonly Mock<IPropertyService> _mockPropertyService;
        private readonly BatteryController _batteryController;

        public BatteryServiceTests()
        {
            this._mockBatteryRepository = new Mock<IDeletableEntityRepository<Battery>>();
            this._batteryService = new BatteryService(this._mockBatteryRepository.Object);
            this._mockBatteryService = new Mock<IBatteryService>();
            this._mockPropertyService = new Mock<IPropertyService>();
            this._batteryController = new BatteryController(this._mockPropertyService.Object, this._mockBatteryService.Object);
            
            AutoMapperConfig.RegisterMappings(typeof(SingleBatteryViewModel).Assembly);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "user123"),
            }, "mock"));

            _batteryController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
        }

        [Fact]
        public void Create_Get_ShouldRequireAuthorization()
        {
            var attributes = typeof(BatteryController).GetMethod("Create", new Type[] { typeof(string) })
                .GetCustomAttributes(typeof(AuthorizeAttribute), true);

            attributes.Should().NotBeEmpty();
        }

        [Fact]
        public void Create_Post_ShouldRequireAuthorization()
        {
            var attributes = typeof(BatteryController).GetMethod("Create", new Type[] { typeof(CreateBatteryInputModel) })
                .GetCustomAttributes(typeof(AuthorizeAttribute), true);

            attributes.Should().NotBeEmpty();
        }

        [Fact]
        public void BatteryManagement_ShouldRequireAuthorization()
        {
            var attributes = typeof(BatteryController).GetMethod("BatteryManagement")
                .GetCustomAttributes(typeof(AuthorizeAttribute), true);

            attributes.Should().NotBeEmpty();
        }

        [Fact]
        public void Edit_Get_ShouldRequireAuthorization()
        {
            var attributes = typeof(BatteryController).GetMethod("Edit", new Type[] { typeof(string) })
                .GetCustomAttributes(typeof(AuthorizeAttribute), true);

            attributes.Should().NotBeEmpty();
        }

        [Fact]
        public void Edit_Post_ShouldRequireAuthorization()
        {
            var attributes = typeof(BatteryController).GetMethod("Edit", new Type[] { typeof(EditBatteryInputModel), typeof(string) })
                .GetCustomAttributes(typeof(AuthorizeAttribute), true);

            attributes.Should().NotBeEmpty();
        }

        [Fact]
        public void Delete_ShouldRequireAuthorization()
        {
            var attributes = typeof(BatteryController).GetMethod("Delete")
                .GetCustomAttributes(typeof(AuthorizeAttribute), true);

            attributes.Should().NotBeEmpty();
        }
    }
}
