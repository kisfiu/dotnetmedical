using AssistantClient.Model;
using MedicalSolution.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace MedicalSolution.Controllers
{
    public class DeletePatientController : ApiController
    {
        DataService _ds = new DataService();

        public HttpResponseMessage Post(Patient patient)
        {
            _ds.DeletePatientData(patient);
            var response = Request.CreateResponse<Patient>(System.Net.HttpStatusCode.Created, patient);

            return response;
        }
    }
}