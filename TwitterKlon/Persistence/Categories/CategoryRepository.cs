using System;
using System.Collections.Generic;
using System.Linq;
using TwitterKlon.Contract.Persistence.Categories;
using TwitterKlon.Logic.Categories.DTOs;

namespace TwitterKlon.Persistence.Categories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DatabaseContext dbContext;

        public CategoryRepository(DatabaseContext context)
        {
            dbContext = context;
        }

        public Category AddCategory(CategoryUpdate editor)
        {
            Category category = new Category { Id = Guid.NewGuid().ToString() };
            editor.EditCategory(category);
            dbContext.Categories.Add(category);
            dbContext.SaveChanges();
            return category;
        }

        public bool DeleteCategory(string id)
        {
            Category category = GetCategory(id);
            dbContext.Categories.Remove(category);
            dbContext.SaveChanges();
            return true;
        }

        public Category EditCategory(string id, CategoryUpdate editor)
        {
            Category category = GetCategory(id);
            editor.EditCategory(category);
            dbContext.Categories.Update(category);
            dbContext.SaveChanges();
            return category;
        }

        public List<Category> GetAllCategories()
        {
            return dbContext.Categories.ToList();
        }

        public Category GetCategory(string id)
        {
            if (!CategoryExists(id)) return null;
            return dbContext.Categories
                .Where(category => category.Id == id)
                .SingleOrDefault();
        }

        private bool CategoryExists(string id)
        {
            return dbContext.Categories
                    .Any(category => category.Id == id);
        }
    }
}
