using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TwitterKlon.Logic;
using TwitterKlon.Contract.Logic.Categories;
using TwitterKlon.Logic.Categories.DTOs;

namespace TwitterKlon.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryLogic _logic;

        public CategoryController(ICategoryLogic logic)
        {
            _logic = logic;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetAllCategories()
        {
            return this.FromLogicResult(_logic.GetAllCategories());
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<Category> GetCategory(string id)
        {
            return this.FromLogicResult(_logic.GetCategory(id));
        }

        [HttpPost]
        public ActionResult<Category> AddCategory([FromBody] CategoryUpdate editor)
        {
            return this.FromLogicResult(_logic.AddCategory(editor));
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<Category> EditCategory(string id, [FromBody] CategoryUpdate editor)
        {
            return this.FromLogicResult(_logic.EditCategory(id, editor));
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteCategory(string id)
        {
            return this.FromLogicResult(_logic.DeleteCategory(id));
        }
    }
}
