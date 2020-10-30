using System.Windows;

namespace AlertToCareFrontend.Views
{
    /// <summary>
    /// Interaction logic for ConfigurationPage.xaml
    /// </summary>
    public partial class ConfigurationPage
    {
        public ConfigurationPage()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if(NavigationService == null)
            {
                return;
            }
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
            else
            {
                NavigationService.Navigate(new HomePage());
            }
        }
    }
}
