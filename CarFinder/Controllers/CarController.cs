using CarFinder.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;


namespace CarFinder.Controllers
{
    public class CarController : ApiController
    {
        private DbCon db = new DbCon();
        /// <summary>
        /// Retrieves a list of a car from the database based on the year, make, model, but NOT trim
        /// </summary>
        /// <param name="year1"></param>
        /// <param name="year2"></param>
        /// <param name="make"></param>
        /// <param name="model"></param>
        /// <returns></returns>

        public IEnumerable<Car> Get(string year1, string year2, string make, string model)
        {
            // GET: api/Years

            var retval = db.Database.SqlQuery<Car>("EXEC GetCarsByYearMakeAndModel @year, @year2, @make, @model",
                new SqlParameter("year", year1),
                new SqlParameter("year2", year2),
                new SqlParameter("make", make),
                new SqlParameter("model", model));
            return retval;
        }

        /// <summary>
        /// Retrieves a car from the database based on the year, make and model, and trim.
        /// </summary>
        /// <param name="year1"></param>
        /// <param name="year2"></param>
        /// <param name="make"></param>
        /// <param name="model"></param>
        /// <param name="trim"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CarData>> Get(string year1, string year2, string make, string model, string trim)
        {
            
            var ListOfCars = new List<CarData>();
            

            var retval = db.Database.SqlQuery<Car>("EXEC GetCarsByYearMakeModelAndTrim @year, @year2, @make, @model, @trim",
                new SqlParameter("year", year1),
                new SqlParameter("year2", year2),
                new SqlParameter("make", make),
                new SqlParameter("model", model),
                new SqlParameter("trim", trim));


            foreach (var item in retval.ToArray<Car>())
            {
                var newCar = new CarData { car = item };


                using (var client = new HttpClient())
                {
                    try
                    {
                        client.BaseAddress = new System.Uri("http://www.nhtsa.gov");
                        HttpResponseMessage response = await client.GetAsync(
                            "webapi/api/Recalls/vehicle/modelyear/" + item.model_year +
                            "/make/" + item.make +
                            "/model/" + item.model_name +
                            "?format=json"
                            );
                        newCar.recall = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
                    }
                    catch (Exception e)
                    {
                        newCar.recall = "Recall information not available. Unable to connect to external server.";
                    }
                }
                //bring images from api
                using (var imageClient = new WebClient())
                {
                    try
                    {
                        var imageResult = imageClient.DownloadString(
                            "https://ajax.googleapis.com/ajax/services/search/images?v=1.0&q=" +
                            item.model_year + "%20" + make + "%20" + model + (string.IsNullOrEmpty(trim) ? "" : ("%20" + trim))
                            );

                        newCar.images = JsonConvert.DeserializeObject(imageResult);


                    }
                    catch (Exception e)
                    {
                        newCar.images = "Image information not available. Unable to connect to external server.";

                    }
                }

                ListOfCars.Add(newCar);
            }


            return ListOfCars;
        }
    }
}
