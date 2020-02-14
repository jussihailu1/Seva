using System;
using System.Collections.Generic;
using System.Text;
using Seva.Interface.ILogics;
using Seva.Logic;
using Seva.Logic.Logics;

namespace Seva.Factory.Logic
{
    public class LogicFactory
    {
        public static IUserLogic CreateUserLogic() => new UserLogic();
        public static IOfferedServiceLogic CreateOfferedServiceLogic() => new OfferedServiceLogic();
        public static IUsedServiceLogic CreateUsedServiceLogic() => new UsedServiceLogic();
        public static IImageLogic CreateImageLogic() => new ImageLogic();
        public static ICategoryLogic CreateCategoryLogic() => new CategoryLogic();
        public static IReviewLogic CreateReviewLogic() => new ReviewLogic();
    }
}
