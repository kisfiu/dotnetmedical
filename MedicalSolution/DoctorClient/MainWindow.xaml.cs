using AssistantClient.Model;
using AssistantClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DoctorClient
{

    public partial class MainWindow : Window
    {
        PatientListViewModel _plVM = new PatientListViewModel();
        DispatcherTimer dt = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            Task t = GetPatients();
            this.DataContext = _plVM;
            this.PatientView.Visibility = Visibility.Hidden;
            dt.Interval = new TimeSpan(0, 0, 1);
            dt.Tick += Dt_Tick;
            dt.Start();
        }

        private void Dt_Tick(object sender, EventArgs e)
        {
            Task t = GetPatients();
        }

        private async Task GetPatients()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:52509");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                HttpResponseMessage response = await client.GetAsync("/api/patients");
                response.EnsureSuccessStatusCode();
                var patients = await response.Content.ReadAsAsync<ObservableCollection<Patient>>();
                _plVM.PatientList = patients;
            }
            catch (Exception)
            {
                MessageBox.Show("A beteglista üres!");
            }
        }

        private void PatientListView_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dt.Stop();
            this.PatientListView.Visibility = Visibility.Hidden;
            this.PatientView.Visibility = Visibility.Visible;
        }

        private void ClosePatientDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            this.PatientView.Visibility = Visibility.Hidden;
            this.PatientListView.Visibility = Visibility.Visible;
            dt.Start();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.dt.Stop();
            this.Close();
        }

        private void DeletePatientMouseRight_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListView)
            {
                ListView list = (ListView)sender;
                Patient patient = (Patient)list.SelectedItem; //probléma - SelectedItem null & -1 SelectedIndex - pedig benne van a listában


                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Biztosan törli a beteget?", "Törlés", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    DeleteAndUpdate(patient);
                }
                else
                {
                    System.Windows.MessageBox.Show("Delete operation Terminated");
                }

            }
        }

        private async Task DeletePatient(Patient patient)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:52509");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                var response = await client.PostAsJsonAsync("/api/deletepatient/", patient);
                response.EnsureSuccessStatusCode();
                MessageBox.Show("Beteg sikeresen törölve!", "Törlés", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show("Hiba a törlés során!");
            }
        }

        private async Task DeleteAndUpdate(Patient patient)
        {
            await DeletePatient(patient);
            await GetPatients();
        }
    }
}
