using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BeerService
{
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

    }
}