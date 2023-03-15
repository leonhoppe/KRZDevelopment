using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwitterKlon.Logic;
using TwitterKlon.Logic.Categories;
using TwitterKlon.Logic.Categories.DTOs;

namespace TwitterKlon.Tests.Categories
{
    [TestClass]
    public class CategoryTesterDEFAULT {
        private CategoryLogic CreateTestCategoryLogic() {
            var categories = Variables.CreateTestCategoryRepositoryDEFAULT();
            return new CategoryLogic(categories.Object);
        }

        [TestMethod]
        public void TestCreateCategoryDEFAULT() {
            CategoryLogic logic = CreateTestCategoryLogic();
            ILogicResult<Category> category = logic.AddCategory(Variables.CreateTestCategoryUpdate());
            Assert.AreEqual(Variables.CreateTestCategory(), category.Data);
        }

        [TestMethod]
        public void TestEditCategoryDEFAULT() {
            CategoryLogic logic = CreateTestCategoryLogic();
            ILogicResult<Category> category = logic.EditCategory(Variables.categoryId, Variables.CreateTestCategoryUpdate());
            Assert.AreEqual(Variables.CreateTestCategory(), category.Data);
        }

        [TestMethod]
        public void TestDeleteCategoryDEFAULT() {
            CategoryLogic logic = CreateTestCategoryLogic();
            ILogicResult category = logic.DeleteCategory(Variables.categoryId);
            Assert.AreEqual(LogicResultState.Ok, category.State);
        }

        [TestMethod]
        public void TestGetCategoryDEFAULT() {
            CategoryLogic logic = CreateTestCategoryLogic();
            ILogicResult<Category> category = logic.GetCategory(Variables.categoryId);
            Assert.AreEqual(Variables.CreateTestCategory(), category.Data);
        }

        [TestMethod]
        public void TestGetAllCategoriesDEFAULT() {
            CategoryLogic logic = CreateTestCategoryLogic();
            ILogicResult<IEnumerable<Category>> category = logic.GetAllCategories();
            if (category.Data == null) Assert.Fail();
        }
    }
}