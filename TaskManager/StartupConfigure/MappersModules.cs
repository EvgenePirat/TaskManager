using Autofac;
using AutoMapper;
using BusinessLayer.Mappers;
using TaskManager.Mappers;

namespace TaskManager.StartupConfigure
{
    public class MappersModules : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(options => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CategoryModelProfile());
                cfg.AddProfile(new CategoryDtoProfile());

                cfg.AddProfile(new UserModelProfile());
                cfg.AddProfile(new UserDtoProfile());

                cfg.AddProfile(new TaskDtoProfile());
                cfg.AddProfile(new TaskModelProfile());

            })).SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper())
                .As<IMapper>()
                .InstancePerLifetimeScope();
        }
    }
}
