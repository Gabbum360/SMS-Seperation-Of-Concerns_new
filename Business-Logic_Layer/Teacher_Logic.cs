using Business_Logic_Layer.Interfaces;
using Core.Models;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class Teachers_Logic : ITeacher
    {
        private SchoolSystemDbContext SMDContext;
        public Teachers_Logic(SchoolSystemDbContext SMDb)
        {
            SMDContext = SMDb;
        }

        public async Task<List<Teacher>> GetTeachers()
        {
            List<GetTeacher> teacher = new List<GetTeacher>();
            var teacherFromDb = await SMDContext.Teachers.ToListAsync();
            foreach (var item in teacherFromDb)
            {
                GetTeacher tchr = new GetTeacher();
                tchr.Name = item.Name;
                tchr.Age = item.Age;
                tchr.Sex = item.Sex;
                tchr.Email = item.Email;
                tchr.StaffNo = item.StaffNo;
                tchr.Country = item.Country;

            }
            return teacherFromDb;
        }

        public async Task<Teacher> RegT(string Name, int Age, string Sex, string Email, string Country)
        {
            var teacher = new Teacher()
            {

                Name = Name,
                Age = Age,
                Sex = Sex,
                Email = Email,
                Country = Country,
            };
            SMDContext.Add(teacher);
            await SMDContext.SaveChangesAsync();
            return teacher;
        }

        public async Task<Teacher> GetT(int id)
        {
            var staff = await SMDContext.Teachers.Where(T => T.Id == Guid.NewGuid()).FirstOrDefaultAsync();
            return staff;
        }

        public async Task<Teacher> UpdateT(int id, UpdateTeacher teacher)// or use this (int id, string(property_name)).
        {
            var staff = SMDContext.Teachers.Where(z => z.Id == Guid.NewGuid()).Select(T => T).FirstOrDefault();
            staff.Country = teacher.Country;
            staff.Age = teacher.Age;
            await SMDContext.SaveChangesAsync();
            return staff;
        }

        //another method to pass a different signature instead of the previous...
        public async Task<Teacher> UpdateT(int id, string Name)
        {
            var staff = SMDContext.Teachers.Where(z => z.Id == Guid.NewGuid()).Select(T => T).FirstOrDefault();
            staff.Name = Name;
            await SMDContext.SaveChangesAsync();
            return staff;
        }

        public async Task<Teacher> DeleteT(int id)
        {
            var staff = SMDContext.Teachers.Where(S => S.Id == Guid.NewGuid()).Select(S => S).FirstOrDefault();
            await SMDContext.SaveChangesAsync();
            return staff;
        }
    }
}
