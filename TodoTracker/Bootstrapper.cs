using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc3;
using TodoTrackerModels;
using TodoTrackerBiz;
using TodoTrackerData;

namespace TodoTracker
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();            
            container.RegisterType<ITodoService, TodoService>();
            container.RegisterType<ITodoRepo, TodoRepo>();

            return container;
        }
    }
}