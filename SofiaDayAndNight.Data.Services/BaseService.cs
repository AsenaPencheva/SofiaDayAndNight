//using Bytes2you.Validation;
//using SofiaDayAndNight.Data.Contracts;
//using SofiaDayAndNight.Data.Models.Contracts;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace SofiaDayAndNight.Data.Services
//{
//    public abstract class BaseService<T>
//          where T : class, IDeletable
//    {
//        protected readonly IEfDbSetWrapper<T> setWrapper;
//        protected readonly IUnitOfWork dbContext;

//        public BaseService(IEfDbSetWrapper<T> setWrapper, IUnitOfWork dbContext)
//        {
//            Guard.WhenArgument(setWrapper, "setWrapper").IsNull().Throw();
//            Guard.WhenArgument(dbContext, "dbContext").IsNull().Throw();

//            this.setWrapper = setWrapper;
//            this.dbContext = dbContext;
//        }

//        public T GetById(Guid id)
//        {
//            var result = this.setWrapper.GetById(id);

//            return result;
//        }
//    }
//}
