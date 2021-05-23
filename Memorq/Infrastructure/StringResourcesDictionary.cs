using System.Windows;

namespace Memorq.Infrastructure
{
    public class StringResourcesDictionary : IStringResourcesDictionary
    {
        public string GetResource(string key)
        {
            return (string)Application.Current.FindResource(key);
        }
    }
}