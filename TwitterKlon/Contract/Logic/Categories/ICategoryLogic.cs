using System.Collections.Generic;
using TwitterKlon.Logic;
using TwitterKlon.Logic.Categories.DTOs;

namespace TwitterKlon.Contract.Logic.Categories
{
    public interface ICategoryLogic
    {
        ILogicResult<IEnumerable<Category>> GetAllCategories();
        ILogicResult<Category> GetCategory(string id);
        ILogicResult<Category> AddCategory(CategoryUpdate editor);
        ILogicResult<Category> EditCategory(string id, CategoryUpdate editor);
        ILogicResult DeleteCategory(string id);
    }
}
