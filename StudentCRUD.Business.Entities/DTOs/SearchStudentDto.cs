using System;
using System.Collections.Generic;
using System.Text;

namespace StudentCRUD.Business.Entities.DTOs {
    public class SearchStudentDto :SearchDto{
        public DateTime? StartBirthDate { get; set; }
        public DateTime? EndBirthDate { get; set; }
        public string Gender { get; set; }
        public string Id { get; set; }
    }
}
