using StudentCRUD.Business.Entities.DomainModels;
using StudentCRUD.Business.Entities.DTOs;
using StudentCRUD.Business.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace StudentCRUD.Tests.Business {
    public class StudentService_Tests : TestBase {
        IStudentService _studentService;
        public StudentService_Tests(ITestOutputHelper output) : base(output) {
            _studentService = ResolveDependency<IStudentService>
                ();
        }

        [Fact]
        public void SearchStudents_Test() {
            //Arrange
            var searhDto = new SearchStudentDto {
                Id = "123",
                StartBirthDate = new DateTime(1950,1,1),
                EndBirthDate = DateTime.Now,
                SearchTerms = "S",
                Gender= "male"
            };
            //Act
            var result = _studentService.Search(searhDto);
            //Assert
            PrintOutput(result);
        }

        [Fact]
        public void AddStudent_Test() {
            //Arrange
            var student = new Student {
                Id = "123",
                FirstName = "Unit Test",
                LastName = "Unit Test",
                BirthDate = DateTime.Now.AddYears(-30),
                Gender = "Male"
            };
            //Act
            student = _studentService.Add(student);
            //Assert
            PrintOutput(student);

        }
    }
}
