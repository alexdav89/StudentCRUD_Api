using Core.Common.Misc;
using StudentCRUD.Business.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentCRUD.Business.Entities.DomainModels;
using StudentCRUD.Business.Services.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace StudentCRUD.Api.Controllers {
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StudentController : CrudControllerBase<Student, string> {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService) : base(studentService) {
            _studentService = studentService;
        }


        /// <summary>
        /// Search entity using given criteria
        ///</summary>
        ///<param name="searchDto"></param>
        /// <returns></returns>
        [Route("search")]
        [HttpPost]
        public Page<IEnumerable<Student>> Search(SearchStudentDto searchDto) {         
            return _studentService.Search(searchDto);
        }

    }
}
