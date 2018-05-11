using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PropertyManagement.Models
{
    /** * @author Umair Naeem */
    public class PMContext:DbContext
    {
        public PMContext(DbContextOptions<PMContext> options)
            :base(options)
        { }

        public DbSet<Property> Properties { get; set; }
        public DbSet<Category> Categories { get; set; }
    }

    public class Property
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(200, ErrorMessage = "Property name must be 200 characters or less"), MinLength(5)]
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public decimal Price { get; set; }
        public virtual Category Category { get; set; }
    }

    public class Category
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(25, ErrorMessage = "Property name must be 25 characters or less"), MinLength(5)]
        public string Name { get; set; }
        public virtual ICollection<Property> Properties { get; set; }
    }
}
