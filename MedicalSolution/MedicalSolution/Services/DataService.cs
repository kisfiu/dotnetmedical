using AssistantClient.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace MedicalSolution.Services
{
    public class DataService
    {
        string outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);

        private string PatientToString(Patient patient)
        {
            return patient.SSN.ToString() + ";" + patient.Name + ";" + patient.Address + ";" + patient.Complaint;
        }

        public ObservableCollection<Patient> ReadData()
        {
            ObservableCollection<Patient> patients = new ObservableCollection<Patient>();

            try
            {
                string dataPath = Path.Combine(outPutDirectory, "Resources\\data.txt");
                string data_path = new Uri(dataPath).LocalPath;

                string[] lines = System.IO.File.ReadAllLines(data_path);

                foreach (string line in lines)
                {
                    if(!("").Equals(line) || line != null)
                    {
                        string[] actualData = line.Split(';');
                        long ssn = Convert.ToInt64(actualData[0]);
                        Patient patient = new Patient(ssn, actualData[1], actualData[2], actualData[3]);
                        if (!patients.Contains(patient))
                        {
                            patients.Add(patient);
                        }
                    }
                }
            }
            catch(IOException e)
            {
                Console.WriteLine("Hiba a fájl beolvasása során!");
            }
            return patients;
        }

        public void SaveNewData(Patient patient)
        {
            try
            {
                string dataPath = Path.Combine(outPutDirectory, "Resources\\data.txt");
                string data_path = new Uri(dataPath).LocalPath;

                using (System.IO.StreamWriter sw =
                new System.IO.StreamWriter(data_path, true))
                {
                    sw.WriteLine(PatientToString(patient));
                }
            }
            catch(IOException e)
            {
                Console.WriteLine("Hiba a fájlba írás során!");
            }
        }

        public void SaveEditedData(Patient patient)
        {
            try
            {
                string dataPath = Path.Combine(outPutDirectory, "Resources\\data.txt");
                string data_path = new Uri(dataPath).LocalPath;

                string[] lines = System.IO.File.ReadAllLines(data_path);

                for (int i = 0; i < lines.Length; i++)
                {
                    string[] actualData = lines[i].Split(';');
                    long ssn = Convert.ToInt64(actualData[0]);

                    if (ssn == patient.SSN)
                    {
                        lines[i] = PatientToString(patient);
                        break;
                    }

                }

                using (System.IO.StreamWriter sw =
                new System.IO.StreamWriter(data_path, false))
                {
                    foreach (string line in lines)
                    {
                        sw.WriteLine(line);
                    }
                }
            }
            catch(IOException e)
            {
                Console.WriteLine("Hiba a mentés során!");
            }
        }

        public void DeletePatientData(Patient patient)
        {
            try
            {
                string dataPath = Path.Combine(outPutDirectory, "Resources\\data.txt");
                string data_path = new Uri(dataPath).LocalPath;

                string[] lines = System.IO.File.ReadAllLines(data_path);
                string[] newlines = new string[lines.Length - 1]; //one patient is present only once ensured
                bool deletionDone = false;

                for (int i = 0; i < lines.Length; i++)
                {
                    string[] actualData = lines[i].Split(';');
                    long ssn = Convert.ToInt64(actualData[0]);

                    if (ssn != patient.SSN && !deletionDone)
                    {
                        newlines[i] = lines[i];
                    }
                    else if (ssn != patient.SSN && deletionDone)
                    {
                        newlines[i - 1] = lines[i];
                    }
                    else
                    {
                        deletionDone = true;
                    }
                }

                using (System.IO.StreamWriter sw =
                new System.IO.StreamWriter(data_path, false))
                {
                    foreach (string line in newlines)
                    {
                        sw.WriteLine(line);
                    }
                }
            }
            catch(IOException e)
            {
                Console.WriteLine("Hiba a törlés során!");
            }
        }
    }
}