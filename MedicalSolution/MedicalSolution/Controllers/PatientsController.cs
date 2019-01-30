using AssistantClient.Model;
using MedicalSolution.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MedicalSolution.Controllers
{
    public class PatientsController : ApiController
    {
        DataService _ds = new DataService();

        public ObservableCollection<Patient> Get()
        {
            ObservableCollection<Patient> patients = _ds.ReadData();
            return patients;
        }

        public HttpResponseMessage Post([FromUri] bool edit, [FromBody] Patient patient)
        {
            if (edit)
            {
                _ds.SaveEditedData(patient);
            }
            else
            {
                _ds.SaveNewData(patient);
            }

            var response = Request.CreateResponse<Patient>(System.Net.HttpStatusCode.Created, patient);

            return response;
        }

    }
}
