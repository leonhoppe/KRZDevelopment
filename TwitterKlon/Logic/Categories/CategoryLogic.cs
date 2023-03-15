using System.Collections.Generic;
using TwitterKlon.Logic.Categories.DTOs;
using TwitterKlon.Contract.Logic.Categories;
using TwitterKlon.Contract.Persistence.Categories;

namespace TwitterKlon.Logic.Categories
{
    public class CategoryLogic : ICategoryLogic
    {
        private readonly ICategoryRepository _categories;

        public CategoryLogic(ICategoryRepository categories)
        {
            _categories = categories;
        }

        public ILogicResult<Category> AddCategory(CategoryUpdate editor)
        {
            return LogicResult<Category>.Ok(_categories.AddCategory(editor));
        }

        public ILogicResult DeleteCategory(string id)
        {
            _categories.DeleteCategory(id);
            return LogicResult.Ok();
        }

        public ILogicResult<Category> EditCategory(string id, CategoryUpdate editor)
        {
            return LogicResult<Category>.Ok(_categories.EditCategory(id, editor));
        }

        public ILogicResult<IEnumerable<Category>> GetAllCategories()
        {
            return LogicResult<IEnumerable<Category>>.Ok(_categories.GetAllCategories());
        }

        public ILogicResult<Category> GetCategory(string id)
        {
            return LogicResult<Category>.Ok(_categories.GetCategory(id));
        }
    }
}
