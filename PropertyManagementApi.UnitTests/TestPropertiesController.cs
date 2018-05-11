using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using PropertyManagement.Models;
using PropertyManagementApi.Controllers;
using Microsoft.AspNetCore.Mvc;

/** * @author Umair Naeem */
namespace PropertyManagementApi.Tests
{
    [TestClass]
    public class TestPropertiesController
    {
        private PMContext _context;
        private PropertiesController _controller;

        [TestInitialize()]
        public void Initialize()
        {
            var options = new DbContextOptionsBuilder<PMContext>()
                               .UseInMemoryDatabase(databaseName: "PM-Demo-UnitTest")
                               .Options;
            _context = new PMContext(options);
            _controller = new PropertiesController(_context);

        }
        [TestCleanup()]
        public void TestCleanup()
        {
            _context.Properties.RemoveRange(_context.Properties);
            _context.Categories.RemoveRange(_context.Categories);
            _context.SaveChanges();
        }
        [TestMethod]
        public void GetAllProperties_ShouldReturnAllProperties()
        {
            // Arrange
            Seed(_context);

            // Act
            var result = _controller.Get() as List<Property>;

            // Assert
            Assert.AreEqual(_context.Properties.CountAsync<Property>(), result.Count);
        }
        [TestMethod]
        public void GetProperty_ShouldReturnCorrectProperty()
        {
            // Arrange
            Seed(_context);

            // Act
            IActionResult result = _controller.GetById(2) ;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_context.Properties.FindAsync(2).Result, result);
        }

        [TestMethod]
        public void GetProperty_ShouldNotFindProperty()
        {
            // Arrange
            Seed(_context);
            
            // Act
            var result = _controller.GetById(999);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
        private void Seed(PMContext context)
        {
            var Categories = new[]
            {
                new Category {ID=1,Name="Category 1"},
                new Category {ID=2,Name="Category 2"},
                new Category {ID=3,Name="Category 3"}
            };
            _context.Categories.AddRange(Categories);

            _context.SaveChanges();

            var Properties = new[]
            {
                new Property {ID=1,Name="Property 1",CategoryId=1,Description="Property 1", Price=9000000M,Category=_context.Categories.Find(1)},
                new Property {ID=2,Name="Property 2",CategoryId=2,Description="Property 2", Price=12000000M,Category=_context.Categories.Find(2)},
                new Property {ID=3,Name="Property 3",CategoryId=2,Description="Property 3", Price=42000000M,Category=_context.Categories.Find(2)}
            };
            _context.Properties.AddRange(Properties);

            _context.SaveChanges();
        }
    }
}
