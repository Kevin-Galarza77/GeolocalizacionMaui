using Microsoft.Extensions.DependencyInjection;
using System;
using Geolocalizacion.Services;

namespace Geolocalizacion
{
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        { 
            return new Window(new AppShell());
        }
    }
}