
namespace SamrtStock.Desktop
{
    using System.Windows;

    using Microsoft.Practices.Prism.UnityExtensions;

    public class SmartStockBootstrapper : UnityBootstrapper
    {
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            // add type registry 
        }

        protected override void ConfigureModuleCatalog()
        {
            // this.ModuleCatalog.AddModule();
            base.ConfigureModuleCatalog();
        }

        protected override DependencyObject CreateShell()
        {
            return new Shell { DataContext = new ShellViewModel() };
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            Application.Current.MainWindow = (Shell)this.Shell;
            Application.Current.MainWindow.Show();
        }
    }
}
