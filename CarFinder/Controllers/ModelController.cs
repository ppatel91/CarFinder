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
    public class ModelController : ApiController
    {
        private DbCon db = new DbCon();
        /// <summary>
        /// Retrives the car models from the database based on the year and make of a car.
        /// </summary>
        /// <param name="year1"></param>
        /// <param name="year2"></param>
        /// <param name="make"></param>
        /// <returns></returns>
        public IEnumerable<string> Get(string year1, string year2, string make)
        {
            // GET: api/Years

            var retval = db.Database.SqlQuery<string>("EXEC GetModelsByYearAndMake @year, @year2, @make",
                new SqlParameter("year", year1),
                new SqlParameter("year2", year2),
                new SqlParameter("make", make));
            return retval;
        }
    }
}
