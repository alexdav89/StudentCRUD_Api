    using StudentCRUD.Business.Services.Abstraction;
    using StudentCRUD.Data.Repositories.Abstraction;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Xunit;
    using Xunit.Abstractions;

    namespace StudentCRUD.Tests.Business {
    public class StudentRepository_Tests : TestBase {
    IStudentRepository _studentRepository;
    public StudentRepository_Tests(ITestOutputHelper output) : base(output) {
    _studentRepository = ResolveDependency<IStudentRepository>
        ();
        }

        [Fact]
        public void Empty_Test() {
        //Arrange

        //Act

        //Assert

        }

        }
        }
