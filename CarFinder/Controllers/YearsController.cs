using CarFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CarFinder.Controllers
{
    public class YearsController : ApiController
    {
        private DbCon db = new DbCon();
/// <summary>
/// Retrieves a list of years from database
/// </summary>
/// <returns></returns>
        public IEnumerable<string> Get()
        {
            var retval = db.Database.SqlQuery<string>("EXEC GetYears");
            return retval;
        }

       
    }
}
