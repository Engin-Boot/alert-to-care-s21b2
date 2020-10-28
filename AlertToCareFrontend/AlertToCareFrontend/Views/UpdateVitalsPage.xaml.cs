using AlertToCareFrontend.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace AlertToCareFrontend.Views
{
    /// <summary>
    /// Interaction logic for UpdateVitalsPage.xaml
    /// </summary>
    public partial class UpdateVitalsPage : Page
    {
        public UpdateVitalsPage(string patientId, string bedId)
        {
            InitializeComponent();
            DataContext = new UpdateVitalsViewModel(patientId, bedId);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
            else
            {
                this.NavigationService.Navigate(new MonitorIcuPage());
            }
        }
    }
}
