using Core.Common.Misc;
using StudentCRUD.Business.Entities.DTOs;
using StudentCRUD.Business.Entities.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentCRUD.Business.Services.Abstraction {
    public interface IStudentService : IServiceBase<Student,string>
    {
   

Page<IEnumerable<Student>> Search(SearchStudentDto searchDto);

 }
    }
