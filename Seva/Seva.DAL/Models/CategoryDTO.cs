using System;
using System.Collections.Generic;
using System.Text;
using Seva.Interface.IContextModels;

namespace Seva.DAL.Models
{
    public class CategoryDTO : ICategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
