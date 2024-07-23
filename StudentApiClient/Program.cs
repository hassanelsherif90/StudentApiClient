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

    }

    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int  Grade { get; set; }
    }
}
