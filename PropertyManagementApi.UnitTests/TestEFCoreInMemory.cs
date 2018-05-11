using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using PropertyManagement.Models;

/** * @author Umair Naeem */
namespace PropertyManagementApi.Tests
{
    [TestClass]
    public class TestEFCoreInMemory
    {
        private PMContext _context;
        [TestInitialize()]
        public void Initialize()
        {
            var options = new DbContextOptionsBuilder<PMContext>()
                               .UseInMemoryDatabase(databaseName: "PM-Demo-UnitTest")
                               .Options;
            _context = new PMContext(options);
        }

        [TestCleanup()]
        public void TestCleanup()
        {
            _context.Properties.RemoveRange(_context.Properties);
            _context.Categories.RemoveRange(_context.Categories);
            _context.SaveChanges();
        }
        [TestMethod]
        public void AddCategory_ShouldAddCategory()
        {
            // Arrange
            var catergory = new Category { ID = 1, Name = "Test" };
            _context.Categories.Add(catergory);
            _context.SaveChanges();

            // Act
            Task<Category> obj = _context.Categories.FirstOrDefaultAsync(x => x.ID == catergory.ID);

            // Assert
            Assert.AreEqual(catergory.Name, obj.Result.Name);
        }
        [TestMethod]
        public void AddProperty_ShouldAddProperty()
        {
            // Arrange
            var catergory = new Category { ID = 1, Name = "Test" };
            _context.Categories.Add(catergory);

            _context.SaveChanges();

            var property = new Property { ID = 1, Name = "Test", CategoryId = catergory.ID, Description = "Test Property", Price = 30000m,Category= _context.Categories.Find(1)};
            _context.Properties.Add(property);

            _context.SaveChanges();


            // Act
            Task<Property> obj = _context.Properties.FirstOrDefaultAsync(x => x.ID == property.ID);


            // Assert
            Assert.AreEqual(property.Name, obj.Result.Name);
        }
        [TestMethod]
        public void GetCategoryByID_ShouldReturnCorrectCategory()
        {
            // Arrange
            Seed(_context);

            // Act
            Task<Category> obj = _context.Categories.FirstOrDefaultAsync(x => x.ID == 1);

            // Assert
            Assert.IsNotNull(obj.Result);
        }
        [TestMethod]
        public void GetPropertyByID_ShouldReturnCorrectProperty()
        {
            // Arrange
            Seed(_context);

            // Act
            Task<Property> obj = _context.Properties.FirstOrDefaultAsync(x => x.ID == 1);

            // Assert
            Assert.IsNotNull(obj.Result);
        }
        [TestMethod]
        public void GetPropertiesList_ShouldReturnPropertiesList()
        {
            // Arrange
            Seed(_context);
            // Act
            Task<List<Property>> objs = _context.Properties.ToListAsync();

            // Assert
            Assert.AreEqual(3, objs.Result.Count);
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
