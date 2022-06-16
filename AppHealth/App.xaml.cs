using CreateGraphByPoints.Containers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AppHealth
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            StartupUri = new Uri("/AppHealth;component/Views/MainWindow.xaml", UriKind.Relative);
            AutofacConfig.ConfigureContainer();
        }
    }
}
