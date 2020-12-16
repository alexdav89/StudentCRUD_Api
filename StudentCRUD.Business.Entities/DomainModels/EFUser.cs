using Core.Common.Data;
using Identity.Dapper.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentCRUD.Business.Entities.DomainModels {    
    public class User : IdentityUser, IBaseEntity<int>  {
        public string FirstName { get; set; }
        public string LastName { get; set; }                       
    }
}
