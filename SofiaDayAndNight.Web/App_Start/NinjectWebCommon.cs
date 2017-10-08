[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(SofiaDayAndNight.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(SofiaDayAndNight.Web.App_Start.NinjectWebCommon), "Stop")]

namespace SofiaDayAndNight.Web.App_Start
{
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using System;
    using System.Web;
    using System.Data.Entity;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Extensions.Conventions;

    using SofiaDayAndNight.Data;
    using SofiaDayAndNight.Data.Contracts;
    using SofiaDayAndNight.Data.EfDbSetWrappers;
    using SofiaDayAndNight.Data.Services.Contracts;
    using AutoMapper;
    using SofiaDayAndNight.Data.Services;
    using Microsoft.AspNet.Identity;
    using SofiaDayAndNight.Data.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Reflection;
    using SofiaDayAndNight.Web.Helpers;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {

            kernel.Bind(x =>
            {
                x.FromThisAssembly()
                 .SelectAllClasses()
                 .BindDefaultInterface();
            });

            kernel.Bind(x =>
            {
                x.FromAssemblyContaining(typeof(IService))
                 .SelectAllClasses()
                 .BindDefaultInterface();
            });

            kernel.Bind(typeof(DbContext), typeof(SofiaDayAndNightDbContext)).To<SofiaDayAndNightDbContext>().InRequestScope();
            kernel.Bind(typeof(IEfDbSetWrapper<>)).To(typeof(EfDbSetWrapper<>));
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();

            kernel.Bind<IMapper>().To<Mapper>().InSingletonScope();
            kernel.Bind<IConfigurationProvider>().ToMethod(x => Mapper.Configuration);

            kernel.Bind<IPhotoHelper>().To<PhotoHelper>().InSingletonScope();
        }
    }
}