using System;
using System.Collections.Generic;
using Autofac;
using StudentManager.Common.Aggregates.StudentAggregate;
using StudentManager.DataAccess.SQL.Repositories;
using StudentManager.DataAccess.StoredProcedures.Repositories;

namespace StudentManager.Presentation.ConsoleApp
{
    static class Program
    {
        private static IContainer Container { get; set; }
        private static IStudentRepository studentRepository; 
        static void Main(string[] args)
        {
            Initialize();
            studentRepository = Container.Resolve<IStudentRepository>();
            int operation;
            do
            {
                operation = GetDataAccess();
                switch (operation)
                {
                    case 1:
                        List();
                        break;
                    case 2:
                        AddStudent();
                        break;
                    case 3:
                        UpdateStudent();
                        break;
                    case 4:
                        DeleteStudent();
                        break;
                    default:
                        Environment.Exit(0);
                        break;
                }
            } while (operation != 5);
        }
        private static void Initialize()
        {
            var builder = new ContainerBuilder();
            var connectionType = GetConnectionType();
            switch (connectionType)
            {
                case 1:
                    builder.RegisterType<SqlStudentRepository>().As<IStudentRepository>();
                    break;
                case 2:
                    builder.RegisterType<StoredProceduresStudentRepository>().As<IStudentRepository>();
                    break;
                default:
                    Environment.Exit(0);
                    break;
            }
            Container = builder.Build();
        }

        private static int GetConnectionType()
        {
            Console.WriteLine("Choose the type of connection: \n (1)  SQL Server \n (2)  Stored Procedures \n (3)  Exit");
            var connectionType = int.Parse(Console.ReadLine());
            while (connectionType != 1 && connectionType != 2 && connectionType != 3)
            {
                Console.WriteLine("Incorrect operation, please choose again: \n (1)  SQL Server \n (2)  Stored Procedures \n (3)  Exit");
                connectionType = int.Parse(Console.ReadLine());
            }
            return connectionType;
        }

        private static int GetDataAccess()
        {
            Console.WriteLine("What do you want to do? \n (1)  Get all students \n (2)  Add new student \n (3)  Update student \n (4)  Delete student \n (5)  Exit");
            var operation = int.Parse(Console.ReadLine());
            while (operation != 1 && operation != 2 && operation != 3 && operation != 4 && operation != 5)
            {
                Console.WriteLine("Incorrect operation, please choose again: \n (1)  Get all students \n (2)  Add new student \n (3)  Update student \n (4)  Delete student \n (5)  Exit");
                operation = int.Parse(Console.ReadLine());
            }
            return operation;
        }

        private static void List()
        {
            var studentList = studentRepository.GetAll();
            foreach (var item in studentList)
            {
                Console.WriteLine("Id: " + item.Id + "  Name: " + item.Name + "  Last Name: " + item.LastName + "  Birthdate: " + item.BirthDate + "\n");
            }
        }

        private static void AddStudent()
        {
            Console.WriteLine("Name: ");
            var studentName = Console.ReadLine();
            Console.WriteLine("Last Name: ");
            var studentLastName = Console.ReadLine();
            Console.WriteLine("Birthdate (dd/mm/yyyy): ");
            var studentBirthdate = DateTime.Parse(Console.ReadLine());

            var student = new Student
            {
                Name = studentName,
                LastName = studentLastName,
                BirthDate = studentBirthdate
            };
            studentRepository.Create(student);
        }

        private static void UpdateStudent()
        {
            Console.WriteLine("Id:\n");
            var studentId = int.Parse(Console.ReadLine());
            Console.WriteLine("Name:\n");
            var studentName = Console.ReadLine();
            Console.WriteLine("Last Name:\n");
            var studentLastName = Console.ReadLine();
            Console.WriteLine("Birthdate (dd/mm/yyyy):\n");
            var studentBirthdate = DateTime.Parse(Console.ReadLine());

            var student = new Student
            {
                Id = studentId,
                Name = studentName,
                LastName = studentLastName,
                BirthDate = studentBirthdate
            };
            studentRepository.Update(student);
        }

        private static void DeleteStudent()
        {
            Console.WriteLine("Id:\n");
            var studentId = int.Parse(Console.ReadLine());
            studentRepository.DeleteById(studentId);
        }

    }

   
}
