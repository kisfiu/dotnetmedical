using AssistantClient.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AssistantClient.ViewModel
{
    public class PatientListViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Patient> patientList;

        public PatientListViewModel()
        {
            this.patientList = new ObservableCollection<Patient>();
            //this.patientList = new ObservableCollection<Patient> {
            //    new Patient(123456789, "Kiss Pál", "1032 Budapest, Alma utca 12.", "Fejfájás."),
            //    new Patient(234567890, "Nagy Beáta", "1066 Budapest, Körte utca 91.", "Hányinger, szédülés."),
            //    new Patient(345678901, "Közepes Emil", "1023 Budapest, Málna utca 25.", "Mellkasi fájdalom.")
            //}; //will be requested from server
        }
        public ObservableCollection<Patient> PatientList
        {
            get { return patientList; }
            set { patientList = value; OnPropertyChanged(); } 
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
