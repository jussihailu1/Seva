using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Seva.Interface.ILogicModels;

namespace Seva.Interface.ILogics
{
    public interface ICategoryLogic
    {
        ICategory GetCategoryById(int categoryId);
        ICategory GetCategoryByName(string categoryName);


        #region Gets

        IEnumerable<ICategory> GetAllCategories();

        #endregion
    }
}
