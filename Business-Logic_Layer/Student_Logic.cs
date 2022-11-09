using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer;
using Business_Logic_Layer.Interfaces;
using Infrastructure;

namespace BusinessLogicLayer
{
    public class Students_Logic : IStudent //D.I, injecting the IStudent interface to this class.
    {
        private SchoolSystemDbContext SMDContext;
        public Students_Logic(SchoolSystemDbContext SMDb)
        {
            SMDContext = SMDb;
        }

        public Students_Logic()
        {
        }

        public async Task<List<Core.Models.Student>> GetStudents()
        {
            List<GetStudents> students = new List<GetStudents>();//created a list of "students" from the "GetStudents" class.
                                                                 //which will help pull all Studentss when accessing this endpoint irrespective of the validations on the model.
            var studentsFromDb = await SMDContext.Students.ToListAsync();
            foreach (var item in studentsFromDb)
            {
                GetStudents std = new GetStudents();
                std.FirstName = item.FirstName;
                std.Age = item.Age;
                std.Country = item.Country;
                students.Add(std);
            }
            return studentsFromDb;
        }

        public async Task<Student> GetStudents(int id)
        {
            var pupil = await SMDContext.Students.Where(student => student.Id == id.ToString()).Select(s => s).FirstOrDefaultAsync();
            return pupil;
        }

        public Student Regr()
        {
            var student = new Student()
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
            SMDContext.SaveChangesAsync();

            return student;
        }
        public async Task<Student> Regr(string SurName, string FirstName, int Age, string Sex, string ClassArmId, string Country, int StudentNo)
        {
            var student = new Student()
            {
                Id = Guid.NewGuid().ToString(),
                SurName = SurName,
                FirstName = FirstName,
                Age = Age,
                Sex = Sex,
                ClassArmId = ClassArmId,
                Country = Country,
                StudentNo = StudentNo,
            };
            SMDContext.Add(student);
            await SMDContext.SaveChangesAsync();

            return student;
        }
        /* public Students_Logic GetAll()
         {
             throw new NotImplementedException();
         }*/
        public async Task<Student> GetS(string id)
        {
            var pupil = await SMDContext.Students.Where(student => student.Id == id).FirstOrDefaultAsync();
            return pupil;
        }

        public async Task<Student> UpdateS(string id, string SurName, string Country)
        {

            var stdnt = SMDContext.Students.Where(v => v.Id == id).Select(student => student).FirstOrDefault();
            stdnt.SurName = SurName;
            stdnt.Country = Country;
            await SMDContext.SaveChangesAsync();
            return stdnt;
        }

        public async Task<Student> DeleteS(string id)
        {
            var pupil = SMDContext.Students.Where(D => D.Id == id).Select(Student => Student).FirstOrDefault();
            SMDContext.Remove(pupil);
            await SMDContext.SaveChangesAsync();
            return pupil;
        }
    }
}
