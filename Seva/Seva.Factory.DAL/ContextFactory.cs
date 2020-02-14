using System;
using System.Collections.Generic;
using System.Text;
using Seva.DAL;
using Seva.DAL.Contexts;
using Seva.Interface;
using Seva.Interface.IContexts;

namespace Seva.Factory.DAL
{
    public class ContextFactory
    {
        public static IUserContext CreateUserContext() => new UserContext();
        public static IOfferedServiceContext CreateOfferedServiceContext() => new OfferedServiceContext();
        public static IUsedServiceContext CreateUsedServiceContext() => new UsedServiceContext();
        public static ICategoryContext CreateCategoryContext() => new CategoryContext();
        public static IReviewContext CreateReviewContext() => new ReviewContext();
        public static IImageContext CreateImageContext() => new ImageContext();
    }
}
