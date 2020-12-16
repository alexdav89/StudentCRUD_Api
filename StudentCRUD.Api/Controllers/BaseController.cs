using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StudentCRUD.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        public BaseController() {
            var language = GetRequestHeader("app-language");
            if (!string.IsNullOrWhiteSpace(language)) {
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(language);
            }
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en");
        }

        protected JwtSecurityToken GetTokenHeader() {
            var token = GetRequestHeader("token");
            if (!string.IsNullOrWhiteSpace(token)) {
                var stream = $"[{token}]";
                var handler = new JwtSecurityTokenHandler();
                // var jsonToken = handler.ReadToken(headerToken);
                return handler.ReadToken(stream) as JwtSecurityToken;
            }
            return null;
        }            

        
        protected string GetRequestHeader(string headerKey) {
            return Request?.Headers[headerKey];
        }
    }
}