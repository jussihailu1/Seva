using System.Collections.Generic;
using System.Linq;
using Seva.Factory.DAL;
using Seva.Interface.IContextModels;
using Seva.Interface.IContexts;
using Seva.Interface.ILogicModels;
using Seva.Interface.ILogics;
using Seva.Logic.Models;

namespace Seva.Logic.Logics
{
    public class CategoryLogic : ICategoryLogic
    {
        private readonly ICategoryContext _categoryContext;

        public CategoryLogic()
        {
            _categoryContext = ContextFactory.CreateCategoryContext();
        }


        private ICategory ConvertCategory(ICategoryDTO categoryDTO)
        {
            return new Category()
            {
                Id = categoryDTO.Id,
                Name = categoryDTO.Name
            };
        }

        #region Gets

        public ICategory GetCategoryById(int categoryId) => GetAllCategories().Single(category => category.Id == categoryId);

        public ICategory GetCategoryByName(string categoryName) => GetAllCategories().Single(category => category.Name == categoryName);

        public IEnumerable<ICategory> GetAllCategories()
        {
            var categoryDTOs = _categoryContext.GetAllCategories();
            var categoriesToReturn = categoryDTOs.Select(ConvertCategory).ToList();
            return categoriesToReturn;
        }

        #endregion
    }
}
