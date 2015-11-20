using CarFinder.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CarFinder.Controllers
{
    public class MakeController : ApiController
    {
        private DbCon db = new DbCon();
        /// <summary>
        /// Retrieves the make of a car from the databased based on the year.
        /// </summary>
        /// <param name="year1"></param>
        /// <param name="year2"></param>
        /// <returns></returns>
        public IEnumerable<string> Get(string year1, string year2)
        {
            // GET: api/Years

            var retval = db.Database.SqlQuery<string>("EXEC GetMakesByYear @year, @year2",
                new SqlParameter("year",year1),
                new SqlParameter("year2",year2));
            return retval;
        }
    }
}
