using DrChronoWebAPI.Dtos.Responses;
using DrChronoWebAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DrChronoWebAPI.Controllers
{
  
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpPost]
        [Route("api/postPatientData")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PostPatentData([FromForm] choronoPatientForm registrationDto)
        {
            var token = _context.drcClientsTokens.Where(x => x.doctorId == registrationDto.DId && x.isActive==true).FirstOrDefault();

            if (token != null)
            {
                var client = new RestClient("https://drchrono.com/");
              
                var request = new RestRequest("api/patients", Method.Post);
                request.AddHeader("Authorization", "Bearer "+token.access_token);
                request.AddParameter("doctor", registrationDto.DId.ToString());
                request.AddParameter("gender", registrationDto.Gender);
                request.AddParameter("first_name", registrationDto.First_Name);
                request.AddParameter("last_name", registrationDto.Last_Name);
                request.AddParameter("date_of_birth", registrationDto.DOB);

                byte[] fileData = null;
                using (var binaryReader = new BinaryReader(registrationDto.Patient_Image_File.OpenReadStream()))
                {
                    fileData = binaryReader.ReadBytes(Convert.ToInt32( registrationDto.Patient_Image_File.Length));
                }


                byte[] fileDatafront = null;
                using (var binaryReader = new BinaryReader(registrationDto.Insurance_Front_Image_File.OpenReadStream()))
                {
                    fileDatafront = binaryReader.ReadBytes(Convert.ToInt32(registrationDto.Insurance_Front_Image_File.Length));
                }

                byte[] fileDataback = null;
                using (var binaryReader = new BinaryReader(registrationDto.Insurance_Back_Image_File.OpenReadStream()))
                {
                    fileDataback = binaryReader.ReadBytes(Convert.ToInt32(registrationDto.Insurance_Back_Image_File.Length));
                }



                request.AddFile("patient_photo", fileData, registrationDto.Patient_Image_File.FileName);
                request.AddFile("primary_insurance.photo_front", fileDatafront, registrationDto.Insurance_Front_Image_File.FileName);
                request.AddFile("primary_insurance.photo_back", fileDataback, registrationDto.Insurance_Back_Image_File.FileName);
                var response =await client.ExecuteAsync(request);
                if (response != null)
                {
                    if (response.StatusCode == HttpStatusCode.Created)
                    {

                        PatientVM objjsonReturn = JsonConvert.DeserializeObject<PatientVM>(response.Content);

                        CreateAppointmentVM createAppointment = new CreateAppointmentVM();

                        createAppointment.doctor = objjsonReturn.doctor;
                        createAppointment.patient = objjsonReturn.id;
                        createAppointment.exam_room = 1;
                        createAppointment.duration = 30;
                        createAppointment.scheduled_time = registrationDto.scheduled_time;
                        createAppointment.office = 338983;
                        createAppointment.icd10_codes = new List<string>() { };
                        createAppointment.custom_fields = new List<CustomField>()
                        {
                           
                        };

                        //var data = JsonConvert.SerializeObject(createAppointment);

                         var clients = new RestClient("https://drchrono.com/");
                        var requests = new RestRequest("api/appointments",Method.Post);
                        requests.AddHeader("Authorization", "Bearer "+token.access_token);
                        requests.AddHeader("Content-Type", "application/json");
                        requests.AddParameter("application/json", createAppointment, ParameterType.RequestBody);
                        var responses =await client.ExecuteAsync(requests);

                        if (responses.StatusCode == HttpStatusCode.Created)
                        {
                            return Ok(new AppoinmentResponse()
                            {
                                Success = true,
                                data = responses.Content
                            });
                        }
                        else
                        {
                            return BadRequest(new AppoinmentResponse()
                            {
                                Success = false,
                                data = responses.Content
                            });
                        }

                      


                    }

                }
                else
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Invalid payload"
                    },
                        Success = false
                    });
                }
            }
            return BadRequest(new RegistrationResponse()
            {
                Errors = new List<string>() {
                        "Invalid payload"
                    },
                Success = false
            });


        }


        [HttpGet]
        [Route("api/redirectUrl")]
        public async Task<IActionResult> RedirectUrl([FromQuery] string code,[FromQuery]string doctorId, [FromQuery] string state)
        {
            var pt = _context.drcClients.Where(x => x.doctorId == Convert.ToInt32(doctorId)).FirstOrDefault();
            if (pt != null)
            {
                // Build up the data to POST.
                List<KeyValuePair<string, string>> postData = new List<KeyValuePair<string, string>>();
                postData.Add(new KeyValuePair<string, string>("code", code));
                postData.Add(new KeyValuePair<string, string>("grant_type", "authorization_code"));
                postData.Add(new KeyValuePair<string, string>("redirect_uri", pt.redirectUri));
                postData.Add(new KeyValuePair<string, string>("client_id", pt.clientId));
                postData.Add(new KeyValuePair<string, string>("client_secret", pt.clientSecret));

                HttpClient client = new HttpClient();

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                FormUrlEncodedContent content = new FormUrlEncodedContent(postData);
                HttpResponseMessage response = await client.PostAsync(pt.tokenEndpoint, content);

                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body. Blocking!
                    var dataObject = response.Content.ReadAsStringAsync().Result;

                    drcClientsToken objjsonReturn = JsonConvert.DeserializeObject<drcClientsToken>(dataObject);
                    //  var t = await getPatientList(objjsonReturn);
                    //   ViewBag.Code = objjsonReturn.access_token;
                    objjsonReturn.doctorId = pt.doctorId;
                    objjsonReturn.createdDate = DateTime.Now;
                    objjsonReturn.expiryDate = DateTime.Now.AddMinutes(Convert.ToDouble(objjsonReturn.expires_in));
                    objjsonReturn.code =code;
                    objjsonReturn.state = state;

                    var doc = _context.drcClientsTokens.Where(x => x.doctorId == Convert.ToInt32(doctorId)).FirstOrDefault();
                    if (doc != null)
                    {
                        objjsonReturn.doctorId = doc.doctorId;
                        objjsonReturn.updateBy = "TestUpdated";
                        objjsonReturn.updateDate = DateTime.Now;
                        objjsonReturn.isActive = true;
                        _context.Update(objjsonReturn);
                        _context.SaveChanges();
                    }
                    else
                    {
                        objjsonReturn.createdBy = "TestCreated";
                        objjsonReturn.createdDate = DateTime.Now;
                        objjsonReturn.isActive = true;
                        _context.Add(objjsonReturn);
                        _context.SaveChanges();
                    }

                }
                else
                {
                    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                }

            }
           
         
            return Ok();
        }



    }
}
