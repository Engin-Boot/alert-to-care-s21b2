using AlertToCareFrontend.ViewModel;
using System.Windows;

namespace AlertToCareFrontend.Views
{
    /// <summary>
    /// Interaction logic for UpdateVitalsPage.xaml
    /// </summary>
    public partial class UpdateVitalsPage
    {
        public UpdateVitalsPage(string patientId, string bedId)
        {
            InitializeComponent();
            DataContext = new UpdateVitalsViewModel(patientId, bedId);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService != null)
            {
                NavigationService.Navigate(new MonitorIcuPage());
            }
        }
    }
}
