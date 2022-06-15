using AppHealth.ForWorkWithFiles;
using AppHealth.ViewModels;
using Autofac;
using CreateGraphByPoints.Interfaces;

namespace CreateGraphByPoints.Containers
{
    public static class AutofacConfig
    {
        public static IContainer GetContainer { get; set; }
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<DrawFuncViewModel>().AsSelf();
            builder.RegisterType<WorkFilesViewModel>().AsSelf();
            builder.RegisterType<WorkForJson>().AsSelf();
            builder.RegisterType<WorkForXml>().AsSelf();
            builder.Register(x => new MainViewModel(x.Resolve<DrawFuncViewModel>(), x.Resolve<WorkFilesViewModel>()));

            GetContainer = builder.Build();
            GetContainer.Resolve<DrawFuncViewModel>();
            GetContainer.Resolve<WorkFilesViewModel>();
            GetContainer.Resolve<MainViewModel>();

        }
    }
}
