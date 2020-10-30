using System.Windows;

namespace AlertToCareFrontend.Views
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void ConfigureButton_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService != null)
            {
                NavigationService.Navigate(new ConfigurationPage());
            }
        }

        private void MonitorIcuButton_Click(object sender, RoutedEventArgs e)
        {
            if(NavigationService != null)
            {
                NavigationService.Navigate(new MonitorIcuPage());
            }    
        }
    }
}
