using Business_Logic_Layer.Interfaces;
using Core.Models;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Controllers
{
    [Route("api/controllers")]
    [ApiController]
    public class SchoolClassController : ControllerBase
    {
        private ISchoolClass _schoolClass;
        public SchoolClassController(ISchoolClass schoolClasses)
        {
            _schoolClass = schoolClasses;
        }
        [HttpGet("get-all-classes")]
        public async Task<IActionResult> GetAllClasses()
        {
            var classes = await _schoolClass.GetClasses();
            return Ok(classes);
        }

        /*[HttpPost("Label-a-class")]
        public async Task<IActionResult> CreateClassProfile(Class cl)
        {
            var classroom = new Class()
            {
                ClassName = cl.ClassName,
                ArmId = cl.ArmId
            };
            SMDContext.Classes.Add(classroom);
            await SMDContext.SaveChangesAsync();
            return Ok(classroom);
        }*/
        [HttpPost("Add-new-classRoom-encrpt")]
        public async Task<IActionResult> AddClassProfile([FromBody]AddClass Classm)
        {
            var classr = await _schoolClass.RegC(Classm.ClassName);
            return Ok(classr);
            
        }

        [HttpPatch("update-class-record-encrpt/{id}")]
        public async Task<IActionResult> EditClassDetails(int id, [FromBody] UpdateClass cl)
        {
            var classToEdit = await _schoolClass.UpdateC(id, cl.ClassName);
            return Ok(classToEdit);
        }

        /*[HttpPatch("update-class-record-/{id}")]
        public async Task<IActionResult> EditClassDetails(int id, Class cl)
        {
            var editedClassroom = SMDContext.Classes.Where(e => e.Id == Guid.NewGuid()).Select(Class => Class).FirstOrDefault();
            editedClassroom.ClassName = cl.ClassName;
            editedClassroom.ArmId = cl.ArmId;
            await SMDContext.SaveChangesAsync();
            return Ok(Ok(editedClassroom));

        }*/

        [HttpDelete("remove-Class/{id}")]
        public async Task<IActionResult> DropClass(int id)
        {
            var erasedClass = await _schoolClass.DropClass(id);
            return Ok(erasedClass);
        }
    }
}
