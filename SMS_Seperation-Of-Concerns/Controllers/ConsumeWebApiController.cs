using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;

namespace SMS_Seperation_Of_Concerns.Controllers
{
        public class ConsumingEndpoints : Controller
        {

            string baseUri = "https://localhost:44353/";
            /// <summary>
            /// this endpoint talks to an external webApi endPoint to Get Data from its Database.
            /// </summary>
            /// <returns></returns>
            [HttpGet("okay")]
            public async Task<List<Student>> ConsumeRetrieve()
            {
                List<Student> students = new List<Student>();
                var client = new HttpClient();

                client.BaseAddress = new Uri(baseUri);
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage res = await client.GetAsync("https://localhost:44353/get-list-of-student");
                if (res.IsSuccessStatusCode)
                {
                    var stdResp = res.Content.ReadAsStringAsync().Result;
                    students = JsonConvert.DeserializeObject<List<Student>>(stdResp);
                }
                return students;
            }
            /// <summary>
            /// although this works but its not needed as part of my task from my Boss.
            ///the external webApi endpoin that has Save() can actually Save and this existing
            /// /WebApi can call that endpoint to save 
            /// </summary>
            /// <param name="student"></param>
            /// <returns></returns>
            [HttpPost("posting")]
            public async Task<Student> ConsumeSave(Student student)
            {
                var std = new Student()
                {
                    Id = student.Id,
                    SurName = student.SurName,
                    FirstName = student.FirstName,
                    Sex = student.Sex,
                    Age = student.Age,
                    ClassArmId = student.ClassArmId,
                    StudentNo = student.StudentNo,
                    Country = student.Country,
                };
                var client = new HttpClient();
                client.BaseAddress = new Uri(baseUri);
                string json = JsonConvert.SerializeObject(std);
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage res = await client.PostAsync("https://localhost:44353/post-student", httpContent);
                if (res.IsSuccessStatusCode)
                {
                    res.EnsureSuccessStatusCode();
                }
                return std;

            }
        }
}
