using System.Windows;

namespace SamrtStock.Desktop
{
    using MahApps.Metro.Controls;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Shell : MetroWindow
    {
        public Shell()
        {
            this.InitializeComponent();
        }

        public ShellViewModel ViewModel
        {
            set
            {
                this.DataContext = value;
            } 
        }
    }
}
