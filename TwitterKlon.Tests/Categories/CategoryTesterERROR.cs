using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwitterKlon.Logic;
using TwitterKlon.Logic.Categories;
using TwitterKlon.Logic.Categories.DTOs;

namespace TwitterKlon.Tests.Categories
{
    [TestClass]
    public class CategoryTesterERROR {
        private CategoryLogic CreateTestCategoryLogic() {
            var categories = Variables.CreateTestCategoryRepositoryERROR();
            return new CategoryLogic(categories.Object);
        }

        [TestMethod]
        public void TestCreateCategoryERROR() {
            CategoryLogic logic = CreateTestCategoryLogic();
            ILogicResult<Category> category = logic.AddCategory(null);
            Assert.AreEqual(null, category.Data);
        }

        [TestMethod]
        public void TestEditCategoryERROR() {
            CategoryLogic logic = CreateTestCategoryLogic();
            ILogicResult<Category> category = logic.EditCategory(null, null);
            Assert.AreEqual(null, category.Data);
        }

        [TestMethod]
        public void TestDeleteCategoryERROR() {
            CategoryLogic logic = CreateTestCategoryLogic();
            ILogicResult category = logic.DeleteCategory(null);
            Assert.AreEqual(LogicResultState.Ok, category.State);
        }

        [TestMethod]
        public void TestGetCategoryERROR() {
            CategoryLogic logic = CreateTestCategoryLogic();
            ILogicResult<Category> category = logic.GetCategory(null);
            Assert.AreEqual(null, category.Data);
        }

        [TestMethod]
        public void TestGetAllCategoriesERROR() {
            CategoryLogic logic = CreateTestCategoryLogic();
            ILogicResult<IEnumerable<Category>> category = logic.GetAllCategories();
            if (category.Data == null) Assert.Fail();
        }
    }
}