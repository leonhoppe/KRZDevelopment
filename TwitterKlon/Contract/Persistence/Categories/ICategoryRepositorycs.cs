using System.Collections.Generic;
using TwitterKlon.Logic.Categories.DTOs;

namespace TwitterKlon.Contract.Persistence.Categories
{
    public interface ICategoryRepository
    {
        List<Category> GetAllCategories();
        Category GetCategory(string id);
        Category AddCategory(CategoryUpdate editor);
        Category EditCategory(string id, CategoryUpdate editor);
        bool DeleteCategory(string id);
    }
}
