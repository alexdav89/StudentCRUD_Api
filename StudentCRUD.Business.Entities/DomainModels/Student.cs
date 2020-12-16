using Core.Common.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentCRUD.Business.Entities.DomainModels {
    public class Student : IBaseEntity<string> {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }

    }
}
