using System;
using System.Collections.Generic;
using System.Text;
using Seva.Interface.ILogicModels;

namespace Seva.Logic.Models
{
    public class Category : ICategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
