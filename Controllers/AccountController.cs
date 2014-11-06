using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Scholarship.Controllers
{
    public class AccountController : ApiController
    {
        [Route("api/login")]
        public IHttpActionResult GetScholarship(String title, String purpose)
        {
            var scholarship = db.SCHLRSHPs.Where(s => (s.FRML_SCHLRSHP_NAME.Contains(title) || s.SCHLRSHP_TITLE.Contains(title) || s.SCHLRSHP_PRPS.Contains(purpose)));
            if (scholarship == null)
            {
                return NotFound();
            }
            return Ok(scholarship);
        }

    }
}
