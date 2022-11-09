using Business_Logic_Layer.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SMS_Seperation_Of_Concerns.Controllers
{
    // [Authorize]
    //[EnableCors("CorsPolicy")]
    [Route("api/controllers")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private IStudent _students;    //injected the interface(IStudents and gave it an "object instance".
        public StudentController(IStudent students)
        {
            _students = students;   //initialised here.
        }

        [HttpGet("get-all-students")]
        public async Task<IActionResult> GetAllStudents()
        {
            var allStdnt = await _students.GetStudents();
            return Ok(allStdnt);

        }

        /*[HttpGet("get-student-with-full_Details/{id}")]
        public async Task<IActionResult> GetOneStudent(int id)
        {
            StudentController stdnt = new StudentController();
            await stdnt.GetStudents(id);
            return (IActionResult)stdnt;
        }*/

        //working.
        [HttpPost("register-new-studnt")]
        public async Task<IActionResult> RegisterNewStudent([FromBody] Student pupil)
        {
            var stdnt = await _students.Regr(pupil.SurName, pupil.FirstName, pupil.Age, pupil.Sex, pupil.ClassArmId, pupil.Country, pupil.StudentNo);
            return Ok(stdnt);
        }

        //working.
        [HttpGet("get-student-with-full_Details/{id}")]
        public async Task<IActionResult> GetOneStudent(string id)
        {
            var pupil = await _students.GetS(id);
            return Ok(pupil);
        }


        [HttpPatch("update-studentRecord/{id}")]
        public async Task<IActionResult> UpdateStudent(string id, [FromBody] UpdateStudent student)
        {
            var student1 = await _students.UpdateS(id, student.SurName, student.Country);
            return Ok(student1);
        }

        //working.
        [HttpDelete("delete-student/{id}")]
        public async Task<IActionResult> DeleteStudent(string id)
        {
            var pupil = await _students.DeleteS(id);
            return Ok("deleted successfully!");
        }

        /*[HttpGet("get-pupil-encrpted/{id}")]
        public async Task<IActionResult> GetOnestudentinfo(int id, [FromBody] GetStudentbyId stdnt)
        {
         var pupil = await SMDContext.Students_Logic.Where(s => s.Id == id.ToString()).FirstOrDefaultAsync();
         stdnt.Id = pupil.Id;
         stdnt.SurName = pupil.SurName;
         stdnt.FirstName = pupil.FirstName;
         return (IActionResult)stdnt;
        }*/

        /*public async Task<IActionResult> GetOneStudentL()
        {
            var stud = await SMDContext.Students_Logic.Include(s=>s.Country).FirstOrDefault(c=>c.Id).ToString();
        }*//*

        [HttpPost("register-new-student")]
        public async Task<IActionResult> RegisterNewStudent(BusinessLogicLayer.Students_Logic pupil)
        {
            
                var student = new Models.Student()
                {
                    Id = "103",
                    SurName = "Fadolamu",
                    FirstName = "Oluwafunmilayo",
                    Age = 25,
                    Sex = "Female",
                    ClassArmId = "1555",
                    Country = "Germany",
                    StudentNo = 20004,
                };
                SMDContext.Add(student);
                await SMDContext.SaveChangesAsync();
            
            return (IActionResult)pupil;
        }

        [HttpPost("insert-new-record")]
        public async Task<IActionResult> InsertNewRecord(BusinessLogicLayer.Students_Logic person)
        {
            
                var pupil = new Models.Student()
                {
                    Id = "102",
                    FirstName = "Clement",
                    SurName = "Ochayi",
                    Age = 22,
                    Sex = "Male",
                    Country = "USA",
                    StudentNo = 20002
                };
                SMDContext.Add(pupil);
                await SMDContext.SaveChangesAsync();
          
            return (IActionResult)person; ;
        }

        [HttpPost("register-new-Std")]
        public async Task<IActionResult> RegisterStudent([FromBody] Student student)//this method allows user input from the endpoint when called.
        {
           
                var newStudent = new Models.Student()
                {
                    Id = student.Id,
                    SurName = student.SurName,
                    FirstName = student.FirstName,
                    Age = student.Age,
                    Sex = student.Sex,
                  //  ClassArmId = student.ClassArmId,
                    Country = student.Country,
                    //StudentNo = student.StudentNo,
                };
                SMDContext.Add(newStudent);
                await SMDContext.SaveChangesAsync();

            return (IActionResult)student;
        }

        [HttpPatch("update-studentRecord/{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] Models.Student studnt)
        {
            var sted = SMDContext.Students_Logic.Where(student => student.Id == id.ToString()).Select(student => student).FirstOrDefault();
            *//*sted.FirstName = studnt.FirstName;
            sted.Age = studnt.Age;
            sted.Sex = studnt.Sex;*//*
            sted.Country = studnt.Country;
            //sted.StudentNo = studnt.StudentNo;
            sted.ClassArmId = studnt.ClassArmId;
            await SMDContext.SaveChangesAsync();
            return (IActionResult)studnt;
        }

        [HttpPatch("change-few-data/{id}")]// this method allows the user to input see and edit only the required. base on the model created.
        public async Task<IActionResult> MakeChanges(int id, [FromBody] UpdateStudent pupil)
        {
            var sted = SMDContext.Students_Logic.Where(student => student.Id == id.ToString()).Select(student => student).FirstOrDefault();
            sted.Country = pupil.Country;
            await SMDContext.SaveChangesAsync();
            return (IActionResult)pupil;
        }

        [HttpDelete("remove-student-from-table")]
        public async Task<IActionResult> DeleteStudent(BusinessLogicLayer.Students_Logic person)
        {
            var student = new Models.Student()
            {
                Id = "103"
            };
            SMDContext.Remove(student);
            await SMDContext.SaveChangesAsync();
            return (IActionResult)person;
        }

        [HttpDelete("delete-student/{id}")]
        public async Task<IActionResult> DeleteStudent(int id, [FromBody] Models.Student student)
        {
            var pupil = SMDContext.Students_Logic.Where(p => p.Id == id.ToString()).Select(student => student).FirstOrDefault();
            SMDContext.Remove(pupil);
            await SMDContext.SaveChangesAsync();
            return (IActionResult)student;
        }*/
    }
}
