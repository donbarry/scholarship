using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Scholarship.Models
{
    public class UserModel
    {
        public String userName { get; set; }
        public String userPassword { get; set; }
        public String accessToken { get; set; }
        public String refreshToken { get; set; }

    }
}