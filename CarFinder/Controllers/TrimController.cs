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
    public class TrimController : ApiController
    {
        private DbCon db = new DbCon();
        /// <summary>
        /// Retrives the car trims from database based on year, make, and model of a car
        /// </summary>
        /// <param name="year1"></param>
        /// <param name="year2"></param>
        /// <param name="make"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public IEnumerable<string> Get(string year1, string year2, string make, string model)
        {
            // GET: api/Years

            var retval = db.Database.SqlQuery<string>("EXEC GetTrimsForYearMakeAndModel @year, @year2, @make, @model", 
                new SqlParameter("year",year1),
                new SqlParameter("year2", year2),
                new SqlParameter("make",make),
                new SqlParameter("model",model));
            return retval;
        }
    }
}
