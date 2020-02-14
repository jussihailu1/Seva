using System;
using System.Collections.Generic;
using System.Text;
using Seva.Interface.IContextModels;

namespace Seva.Interface.IContexts
{
    public interface ICategoryContext
    {




        //Gets

        IEnumerable<ICategoryDTO> GetAllCategories();
    }
}
