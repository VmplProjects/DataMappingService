using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using DataMappingService.Interfaces;
using DataMappingService.Models;

namespace DataMappingService.Services
{
    public class MappingService : IService<CarData>
    {
        //private IRepository<CarData> mCarDataRepository{ get; set }
        private readonly IConverter<CarData> mCarDataConverter;

        private string mModelKey { get; set; }

        private XDocument mXDocument { get; set; }

        public MappingService(IConverter<CarData> carDataConverter)
        {
            mCarDataConverter = carDataConverter;
        }

        public void InitContract(List<ContractItem> contract)
        {
            mCarDataConverter.InitContract(contract);
            mModelKey = contract.First(c => c.key == nameof(CarData.CarModel)).value;
        }

        public void InitXDocument(XDocument xDoc)
        {
            mXDocument = xDoc;
        }

        public bool DataMap()
        {
            try
            {
                var elems = mXDocument.Descendants(mModelKey);
                List<CarData> res = new List<CarData>();
                foreach (var xElement in elems)
                {
                    var item = mCarDataConverter.Convert(xElement.Parent);
                    res.Add(item);
                }
                
                //mCarDataRepository.InsertRange(res);
            }
            catch (Exception ex)
            {
                // logger.Error("", ex)
                return false;
            }
            return true;
        }
    }
}