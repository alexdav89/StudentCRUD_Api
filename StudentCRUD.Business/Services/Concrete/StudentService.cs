using Core.Common.Misc;
using StudentCRUD.Business.Entities.DTOs;
using StudentCRUD.Business.Entities.DomainModels;
using StudentCRUD.Business.Services.Abstraction;
using StudentCRUD.Data.Repositories.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentCRUD.Business.Services.Concrete {
    public class StudentService : ServiceBase<IStudentRepository, Student, string>
    , IStudentService {
    private readonly IStudentRepository _studentRepository;

    public StudentService(IStudentRepository studentRepository):base(studentRepository) {
    _studentRepository = studentRepository;
    }

   

public Page<IEnumerable<Student>> Search(SearchStudentDto searchDto) {
    long total = 0;
    var data = _baseRepository.Search(searchDto, ref total);
    return new Page<IEnumerable<Student>> {
        PageSize = searchDto.PageSize,
        CurrentPage = searchDto.PageIndex,
        Result = data,
        Total = total
    };
}

 }
    }
