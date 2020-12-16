using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Common.Data {
    public interface IBaseEntity<KType>  {
        KType Id { get; set; }

    }    
}
