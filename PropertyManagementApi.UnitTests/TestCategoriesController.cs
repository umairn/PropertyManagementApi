using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using PropertyManagement.Models;
using PropertyManagementApi.Controllers;
using Microsoft.AspNetCore.Mvc;


namespace PropertyManagementApi.UnitTests
{
    [TestClass]
    public class TestCategoriesController
    {
        private PMContext _context;
        private CategoriesController _controller;

        [TestInitialize()]
        public void Initialize()
        {
            var options = new DbContextOptionsBuilder<PMContext>()
                               .UseInMemoryDatabase(databaseName: "PM-Demo-UnitTest")
                               .Options;
            _context = new PMContext(options);
            _controller = new CategoriesController(_context);

        }
        [TestCleanup()]
        public void TestCleanup()
        {
            _context.Categories.RemoveRange(_context.Categories);
            _context.SaveChanges();
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
        }
    }
}
