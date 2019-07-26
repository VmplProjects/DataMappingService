using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using System.Xml;
using DataMappingService.Models;
using Newtonsoft.Json;
using System.Xml.Linq;
using DataMappingService.Interfaces;

namespace DataMappingService.Controllers
{
    public class DataMappingController : ApiController
    {
        private readonly IService<CarData> mMappingService;

        public DataMappingController(IService<CarData> mappingService)
        {
            mMappingService = mappingService;
        }

        [HttpPost]
        [Route("data")]
        public IHttpActionResult DataMapping()
        {
            try
            {
                var contractParam = HttpContext.Current.Request.Params["contract"];
                var contract = JsonConvert.DeserializeObject<List<ContractItem>>(contractParam);

                var file = HttpContext.Current.Request.Files[0];

                XDocument doc = XDocument.Load(XmlReader.Create(file.InputStream));

                mMappingService.InitContract(contract);
                mMappingService.InitXDocument(doc);

                mMappingService.DataMap();
            }
            catch (Exception ex)
            {
                // Logger.Error("", ex)
                return BadRequest("Проверьте корректность присланных данных");
            }
            return Ok("Данные успешно сохранены");
        }
    }
}