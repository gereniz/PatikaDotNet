using System;
using System.Linq;
using LinqPractices.DbOperations;
using LinqPractices.Entities;

namespace LinqPractices
{
    class Program
    {
        static void Main(string[] args)
        {
            DataGenerator.Initialize();
            LinqDbContext _context = new LinqDbContext();
            var studentsList = _context.Students.ToList<Student>();

            //Find
            Console.WriteLine("--Find--");
            var student1 = _context.Students.Find(1);
            Console.WriteLine(student1.Name);

            //FirstOrDefault
            Console.WriteLine("--FindOrDefault--");
            //1
            var student2 = _context.Students.Where(s => s.Surname == "Arda").FirstOrDefault();
            Console.WriteLine(student2.Name);
            //2
            student2 = _context.Students.FirstOrDefault(s => s.Surname == "Arda");
            Console.WriteLine(student2.Name);
            
            //SingleOrDefault
            Console.WriteLine("--SingleOrDefault--");
            //1
            var student3 = _context.Students.SingleOrDefault(s => s.Name == "Deniz"); //tek veri geri yoksa hata atar.
            Console.WriteLine(student3.Name);

            //ToList
            Console.WriteLine("--ToList--");
            var studentList = _context.Students.ToList();
            foreach(var student in studentList){
                Console.WriteLine(student.Name);
            } 

            //OrderBy
            Console.WriteLine("--OrderBy--");
            var students = _context.Students.OrderBy(s => s.Id).ToList();
            foreach(var student in students){
                Console.WriteLine(student.Id + " - " + student.Name + " - " + student.Surname);
            }

            //OrderByDescending
            Console.WriteLine("--OrderByDescending--");
            var students2 = _context.Students.OrderByDescending(s => s.Id).ToList();
            foreach(var student in students2){
                Console.WriteLine(student.Id + " - " + student.Name + " - " + student.Surname);
            }

            //Anonymous Object Result
            Console.WriteLine("--Anonymous Object Result--");
            var anonymousObject = _context.Students.Where(w => w.Class == 2)
                                .Select(s => new {
                                    Id = s.Id,
                                    Fullname = s.Name + " " + s.Surname
                                });
            foreach(var obj in anonymousObject){
                Console.WriteLine(obj.Id + " - " + obj.Fullname);
            }
            
        }
    }
}
