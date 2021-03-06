﻿using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;
using SaxoBooks.Data.Models;
using SaxoBooks.Data.Repository;
using SaxoBooks.Services;
using SaxoBooks.Services.Interfaces;

namespace SaxoBooks
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

            container.RegisterType<IRepository<Book>, Repository<Book>>(new PerRequestLifetimeManager());
            container.RegisterType<ISaxoBooksService, SaxoBooksService>(new PerRequestLifetimeManager());
            container.RegisterType<IConfigurationReader, ConfigurationReader>(new PerRequestLifetimeManager());
            return container;
        }
    }
}