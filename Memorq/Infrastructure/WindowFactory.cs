using System;
using System.Windows;

namespace Memorq.Infrastructure
{
    public class WindowFactory : IWindowFactory
    {
        private IServiceProvider _serviceProvider;
        public WindowFactory(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;
        public T CreateWindow<T>() where T : Window => (T)_serviceProvider.GetService(typeof(T));
    }
}