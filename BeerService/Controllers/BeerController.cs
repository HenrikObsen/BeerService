using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BeerService
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BeerController : ApiController
    {

     

        BeerFac bf = new BeerFac();
        [Route("api/Beer/Get/{id}")]
        [HttpGet]
        public Beer GetBeer(int id)
        {
            return bf.Get(id);
        }

        [Route("api/Beer/GetAll")]
        [HttpGet]
        public IEnumerable<Beer> GetAll()
        {
            return bf.GetAll();
        }

        [Route("api/Beer/GetList/{id}")]
        [HttpGet]
        public IEnumerable<Beer> GetList(int id)
        {
            return bf.GetBy("CategoryID", id);
        }



        //****************************** Categories *******************************

        //CategoryFac cf = new CategoryFac();

        //[Route("api/Beer/DeleteCat/{id}")]
        //[HttpGet]
        //public bool DeleteCat(int id)
        //{
        //    cf.Delete(id);

        //    return true;
        //}


       
        //[Route("api/Beer/AddCat/")]
        //[HttpPost]
        //public int AddCat(Category c, HttpRequestMessage request)
        //{

        //        IEnumerable<string> headerValues = request.Headers.GetValues("Authorization");
        //        var token = headerValues.FirstOrDefault();
            
        //    int id = 0;
        //    id = cf.Insert(c);

        //    return id;
        //}

    }
}