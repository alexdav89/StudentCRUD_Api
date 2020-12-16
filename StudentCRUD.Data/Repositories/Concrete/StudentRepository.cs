using System.Linq;
using StudentCRUD.Business.Entities.DTOs;
using StudentCRUD.Business.Entities.DomainModels;
using StudentCRUD.Data.Repositories.Abstraction;
using Core.Common.Data;
using Core.Common.Data.EF;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentCRUD.Data.Repositories.Concrete {
    public class StudentRepository : Repository<Student, string>
    , IStudentRepository {
        public StudentRepository(StudentCRUDContext context) : base(context) {

        }


        public IEnumerable<Student> Search(SearchStudentDto searchDto, ref long total) {
            total = BuildSearchQuery(searchDto).Count();
            var listQ = BuildSearchQuery(searchDto);
            return listQ
                .OrderBy(s => s.LastName)
                .Skip(searchDto.PageSize * (searchDto.PageIndex - 1))
                .Take(searchDto.PageSize).ToList();
        }

        private IQueryable<Student> BuildSearchQuery(SearchStudentDto searchDto) {
            var query = Context.Set<Student>().AsQueryable();
            if (!string.IsNullOrWhiteSpace(searchDto.SearchTerms)) {
                query = query.Where(s => s.FirstName.ToLower().Contains(searchDto.SearchTerms.ToLower()) || s.LastName.ToLower().Contains(searchDto.SearchTerms.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(searchDto.Gender)) {
                query = query.Where(s => s.Gender.ToLower() == searchDto.Gender.ToLower());
            }
            if (searchDto.StartBirthDate != null) {
                query = query.Where(s => s.BirthDate >= searchDto.StartBirthDate.Value);
            }
            if(searchDto.EndBirthDate != null) {
                query = query.Where(s => s.BirthDate <= searchDto.EndBirthDate.Value);
            }
            if (!string.IsNullOrWhiteSpace(searchDto.Id)) {
                query = query.Where(s => s.Id == searchDto.Id);
            }
            return query;
        }


    }
}
