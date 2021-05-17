using System.Windows;

namespace Memorq.Infrastructure
{
    public interface IWindowFactory
    {
        T CreateWindow<T>() where T : Window;
    }
}