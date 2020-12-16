using StudentCRUD.Business.Entities.DTOs;
using StudentCRUD.Business.Entities.DomainModels;
using Core.Common.Data;
using Core.Common.Data.EF;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentCRUD.Data.Repositories.Abstraction {
    public interface IStudentRepository : IRepository<Student,string>
    {
   

    IEnumerable<Student> Search(SearchStudentDto searchDto, ref long total);

    }
}
