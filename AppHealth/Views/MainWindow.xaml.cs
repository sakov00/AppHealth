using AppHealth.ViewModels;
using Autofac;
using CreateGraphByPoints.Containers;
using System.Windows;

namespace AppHealth
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            using (var scope = AutofacConfig.GetContainer.BeginLifetimeScope())
            {
                DataContext = scope.Resolve<MainViewModel>();
            }
        }
    }
}
