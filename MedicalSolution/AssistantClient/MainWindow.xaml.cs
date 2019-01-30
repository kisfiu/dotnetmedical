using AssistantClient.Model;
using AssistantClient.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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

namespace AssistantClient
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
            this.PatientListView.Visibility = Visibility.Hidden;
            this.NewPatientView.Visibility = Visibility.Hidden;
            this.PatientView.Visibility = Visibility.Hidden;
            dt.Interval = new TimeSpan(0, 0, 1);
            dt.Tick += Dt_Tick;
            dt.Start();
            
        }

        private void Dt_Tick(object sender, EventArgs e)
        {
            Task t = GetPatients();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            dt.Stop();
            this.Close();
        }

        private void NewPatientButton_Click(object sender, RoutedEventArgs e)
        {
            this.PatientListView.Visibility = Visibility.Hidden;
            this.NewPatientView.Visibility = Visibility.Visible;
        }

        private void PatientsButton_Click(object sender, RoutedEventArgs e)
        {
            this.NewPatientView.Visibility = Visibility.Hidden;
            this.PatientListView.Visibility = Visibility.Visible;
        }

        private void AddNewPatientButton_Click(object sender, RoutedEventArgs e)
        {
            long ssn = Convert.ToInt64(this.AddSSN.Text);
            string name = this.AddName.Text;
            string address = this.AddAddress.Text;
            string complaint = this.AddComplaint.Text;
            Patient newPatient = new Patient(ssn,name,address,complaint);
            Task t = SaveAndUpdate(newPatient);

            foreach (Control ctl in this.NewPatientView.Children)
            {
                if (ctl.GetType() == typeof(TextBox))
                    ((TextBox)ctl).Text = String.Empty;
            } //clear data

            this.NewPatientView.Visibility = Visibility.Hidden;
            this.PatientListView.Visibility = Visibility.Visible;
        }

        private void CancelAddingPatientButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (Control ctl in this.NewPatientView.Children)
            {
                if (ctl.GetType() == typeof(TextBox))
                    ((TextBox)ctl).Text = String.Empty;
            } //clear data

            this.NewPatientView.Visibility = Visibility.Hidden;
            this.PatientListView.Visibility = Visibility.Visible;
        }

        private void PatientListView_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dt.Stop();
            this.PatientListView.Visibility = Visibility.Hidden;
            this.PatientView.Visibility = Visibility.Visible;
        }

        private void SaveEditedPatientButton_Click(object sender, RoutedEventArgs e)
        {
            //BindingExpression be = this.EditSSN.GetBindingExpression(TextBox.TextProperty);
            //be.UpdateSource();

            long ssn = Convert.ToInt64(this.EditSSN.Text);
            Patient patient = new Patient(ssn, this.EditName.Text, this.EditAddress.Text, this.EditComplaint.Text);
            Task t = SaveAndUpdateEdit(patient);

            this.PatientView.Visibility = Visibility.Hidden;
            this.PatientListView.Visibility = Visibility.Visible;
            dt.Start();
        }

        private void CancelEditingPatientButton_Click(object sender, RoutedEventArgs e)
        {
            this.PatientView.Visibility = Visibility.Hidden;
            this.PatientListView.Visibility = Visibility.Visible;
            dt.Start();
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
            catch (Exception e)
            {
                MessageBox.Show("A beteglista üres!");
            }
        }

        private async Task SaveNewPatient(Patient patient)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:52509");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                string json = JsonConvert.SerializeObject(patient);
                StringContent sc = new StringContent(json, Encoding.UTF8, "application/json");
                var response = client.PostAsync("/api/patients/values?edit=false", sc).Result;
                response.EnsureSuccessStatusCode();
                MessageBox.Show("Beteg sikeresen hozzáadva!", "Hozzáadás", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show("Hiba a hozzáadás során!");
            }
        }

        private async Task SaveEditedPatient(Patient patient)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:52509");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                string json = JsonConvert.SerializeObject(patient);
                StringContent sc = new StringContent(json, Encoding.UTF8, "application/json");
                var response = client.PostAsync("/api/patients/values?edit=true", sc).Result;
                response.EnsureSuccessStatusCode();
                MessageBox.Show("Beteg sikeresen módosítva!", "Módosítás", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show("Hiba a módosítás során!");
            }
        }

        private async Task SaveAndUpdate(Patient patient)
        {
            await SaveNewPatient(patient);
            await GetPatients();
        }

        private async Task SaveAndUpdateEdit(Patient patient)
        {
            await SaveEditedPatient(patient);
            await GetPatients();
        }

        private void AddSSN_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
