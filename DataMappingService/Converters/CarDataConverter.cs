using System;
using System.Collections.Generic;
using System.Xml.Linq;
using DataMappingService.Interfaces;
using DataMappingService.Models;

namespace DataMappingService.Converters
{
    public class CarDataConverter : IConverter<CarData>
    {
        private List<ContractItem> mContract { get; set; }
        
        public CarData Convert(XElement elem)
        {
            var res = new CarData();
            foreach (var item in mContract)
            {
                var value = GetXmlValue(elem, item.value);
                switch (item.key)
                {
                    case nameof(CarData.CarModel):
                        res.CarModel = value;
                        break;
                    case nameof(CarData.YearOfManufacture):
                        int year;
                        int.TryParse(value, out year);
                        res.YearOfManufacture = year;
                        break;
                    case nameof(CarData.EngineType):
                        EngineTypes type;
                        Enum.TryParse(value, true, out type);
                        res.EngineType = type;
                        break;
                    case nameof(CarData.EngineVolume):
                        decimal vol;
                        decimal.TryParse(value, out vol);
                        res.EngineVolume = vol;
                        break;
                    case nameof(CarData.Color):
                        res.Color = value;
                        break;
                }
            }
            return res;
        }

        public void InitContract(List<ContractItem> contract)
        {
            mContract = contract;
        }

        private string GetXmlValue(XElement elem, string key)
        {
            var children = elem.Elements();

            if (elem.Element(key) != null)
                return elem.Element(key).Value;
            if (elem.Attribute(key) != null)
                return elem.Attribute(key).Value;

            foreach (var item in children)
            {
                if (item.Name == key)
                    return item.Value;
                if (item.Element(key) != null)
                    return item.Element(key).Value;
                if (item.Attribute(key) != null)
                    return item.Attribute(key).Value;
            }

            return elem.Value;
        }
    }
}