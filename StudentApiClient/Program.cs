﻿
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Text;
using System.Threading.Tasks;

//using Newtonsoft.Json;

namespace StudentApiClient
{
    class Program
    {
        //static readonly HttpClient httpClient = new HttpClient();
        static readonly HttpClient httpClient = new HttpClient();

        static async Task Main(string[] args)
        {
            httpClient.BaseAddress = new Uri("http://localhost:5215/api/Students/"); // Set this to the correct URI for your API

            await GetAllStudents();
            await GetPassedStudents();
            await GetAverageGradeStudent();
            await GetStudentById(1);


            var newStudent = new Student { Name = "Mazen Abdullah", Age = 20, Grade = 85 };
            await AddStudent(newStudent); // Example: Add a new student

            DeleteStudent(1);
           
            await GetAllStudents();

        }

        static async Task GetAllStudents()
        {
            try
            {
                Console.WriteLine("\n_____________________________");
                Console.WriteLine("\nFetching all students...\n");
                List<Student>? students = await httpClient.GetFromJsonAsync<List<Student>>("All");
                if (students != null)
                {
                    foreach (var student in students)
                    {
                        Console.WriteLine($"ID: {student.Id}, Name: {student.Name}, Age: {student.Age}, Age: {student.Grade}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");

            }
        }

        static async Task GetPassedStudents()
        {
            try
            {
                Console.WriteLine("\n_____________________________");
                Console.WriteLine("\nFetching Passed students...\n");
                List<Student>? students = await httpClient.GetFromJsonAsync<List<Student>>("Passed");

                if (students != null)
                {
                    foreach (var student in students)
                    {
                        Console.WriteLine($"ID: {student.Id}, Name: {student.Name}, Age: {student.Age}, Age: {student.Grade}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
           
            }
        }

        static async Task GetAverageGradeStudent()
       {
            try
            {
                Console.WriteLine("\n_____________________________");
                Console.WriteLine("\nFetching AverageGrade students...\n");
                double averageGrade = await httpClient.GetFromJsonAsync<double>("AverageGrade");
                Console.WriteLine($"Average Grade: {averageGrade}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

       }

        static async Task GetStudentById(int id)
        {
            try
            {

                Console.WriteLine("\n_____________________________");
                Console.WriteLine($"Get student with ID {id}...\n");

                var response = await httpClient.GetAsync($"{id}");
                

                if (response.IsSuccessStatusCode)
                {
                    var student = await response.Content.ReadFromJsonAsync<Student>();
                    if (student != null)
                    {
                        Console.WriteLine($"ID: {student.Id}, Name: {student.Name}, Age: {student.Age}, Grade: {student.Grade}");
                    }
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Console.WriteLine($"Bad Request: Not accepted ID {id}");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine($"Not Found: Student with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");

            }
        }
        
        static async Task AddStudent(Student newStudent)
        {
            try
            {
                Console.WriteLine("\n_____________________________");
                Console.WriteLine("\nAdding a new student...\n");

                var response = await httpClient.PostAsJsonAsync("", newStudent);

                if (response.IsSuccessStatusCode)
                {
                    var addedStudent = await response.Content.ReadFromJsonAsync<Student>();
                    Console.WriteLine($"Added Student - ID: {addedStudent.Id}, Name: {addedStudent.Name}, Age: {addedStudent.Age}, Grade: {addedStudent.Grade}");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Console.WriteLine("Bad Request: Invalid student data.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        static async Task DeleteStudent(int id)
        {
            try
            {

                Console.WriteLine("\n_____________________________");
                Console.WriteLine($"\nDeleting student with ID {id}...\n");

                var response = await httpClient.DeleteAsync($"{id}");


                if (response.IsSuccessStatusCode)
                {
                  
                        Console.WriteLine($"Student with ID {id} has been deleted.");
                    
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Console.WriteLine($"Bad Request: Not accepted ID {id}");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine($"Not Found: Student with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");

            }
        }

        static async Task UpdateStudent(int id,Student UpdateStudent)
        {
            try
            {
                Console.WriteLine("\n_____________________________");
                Console.WriteLine("\nUpdate a new student...\n");

                var response = await httpClient.PutAsJsonAsync($"{id}",UpdateStudent);

                if (response.IsSuccessStatusCode)
                {
                    var Student = await response.Content.ReadFromJsonAsync<Student>();
                    Console.WriteLine($"Updated Student - ID: {Student.Id}, Name: {Student.Name}, Age: {Student.Age}, Grade: {Student.Grade}");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Console.WriteLine("Bad Request: Invalid student data.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }

    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int  Grade { get; set; }
    }
}
