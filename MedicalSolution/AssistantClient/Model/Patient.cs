using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AssistantClient.Model
{
    public class Patient : INotifyPropertyChanged
    {
        private long ssn;
        private string name;
        private string address;
        private string complaint;

        public Patient(long ssn, string name, string address, string complaint)
        {
            this.ssn = ssn;
            this.name = name;
            this.address = address;
            this.complaint = complaint;
        }

        public long SSN
        {
            get { return ssn; }
            set { ssn = value; OnPropertyChanged(); }
        }

        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(); }
        }

        public string Address
        {
            get { return address; }
            set { address = value; OnPropertyChanged(); }
        }

        public string Complaint
        {
            get { return complaint; }
            set { complaint = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
