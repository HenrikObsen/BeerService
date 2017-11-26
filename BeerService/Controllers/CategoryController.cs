using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;

namespace BeerService
{
    public class CategoryController : ApiController
    {
        CategoryFac cf = new CategoryFac();
        BeerFac bf = new BeerFac();

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

        [Route("api/Category/GetName/{id}")]
        [HttpGet]
        public string GetName(int id)
        {
            return cf.Get(id).Name;
        }

        [Route("api/Category/Count/")]
        [HttpGet]
        public int Count()
        {
            return cf.Count();
        }

        [Route("api/Category/Get/{id}")]
        [HttpGet]
        public Category GetCategory(int id)
        {
            return cf.Get(id);
        }



        [Route("api/Category/GetAll/")]
        [HttpGet]
        public IEnumerable<Category> GetAllBeers()
        {
            //ResponseModel _objResponseModel = new ResponseModel();
            //_objResponseModel.Data = cf.GetAll();
            //_objResponseModel.Status = true;
            //_objResponseModel.Message = "Data Received successfully";
            return cf.GetAll();
        }

        [Route("api/Beer/Get/{id}")]
        [HttpGet]
        public Beer GetBeer(int id)
        {
            return bf.Get(id);
        }

        [Route("api/Category/GetAll/{column}/{direction}")]
        [HttpGet]
        public IEnumerable<Category> GetAll(string column,string direction)
        {
            return cf.GetAll(column,direction);
        }


        [Route("api/Category/Add/")]
        [HttpPost]
        public string Add(Category c)
        {
            cf.Insert(c);

            return c.Name;
        }



        [Route("api/Category/Delete/{id}")]
        [HttpGet]
        public void Delete(int id)
        {
            cf.Delete(id);
        }



        [Route("api/Category/Update/")]
        [HttpPost]
        public bool Update(Category c)
        {
            bool valid = true;

            if (ModelState.IsValid)
            {
                cf.Update(c);
            }
            else
            {
                valid = false;
            }
            return valid;
        }



        [Route("api/Category/UploadFile/")]
        [HttpPost()]
        public string UploadFiles()
        {
            Uploader u = new Uploader();
            int iUploadedCnt = 0;
            string sPath = "";

            sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Images/");
            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

            // CHECK THE FILE COUNT.
            for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
            {
                System.Web.HttpPostedFile hpf = hfc[iCnt];

                if (hpf.ContentLength > 0)
                {
                    // CHECK IF THE SELECTED FILE(S) ALREADY EXISTS IN FOLDER. (AVOID DUPLICATE)

                    if (!File.Exists(sPath + Path.GetFileName(hpf.FileName)))
                    {
                                                string filen = Path.GetFileName(hpf.FileName);

                        // SAVE THE FILES IN THE FOLDER.
                        hpf.SaveAs(sPath + filen);
                        u.ReSizeImage(sPath + filen, sPath + "Thump/", 120);

                        iUploadedCnt = iUploadedCnt + 1;
                    }
                }
            }

            
            // RETURN A MESSAGE.
            if (iUploadedCnt > 0)
            {
                return iUploadedCnt + " billeder uploadet!";
            }
            else
            {
                return "Upload Failed!!!";
            }
        }

        
        [Route("api/data/GetSeacretData")]
        [HttpGet]
        public string GetSeacretData()
        {
            //ValidateToken();
            return "En hemmelig tekst!!!";
        }
        

        public void ValidateToken()
        {
            var token = Request.Headers.GetValues("_token").FirstOrDefault();
            if (token != "Test123")
            {
                throw new System.Web.HttpException(403, "You must be logged in to access this resource.");
            }
        }
    }
}