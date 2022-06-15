using AppHealth.ViewModels;
using Autofac;
using CreateGraphByPoints.Containers;
using System.Windows;

namespace AppHealth
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
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
