using System.Collections.Generic;
using System.Xml.Linq;
using DataMappingService.Models;

namespace DataMappingService.Interfaces
{
    public interface IService<T> where T : class
    {
        void InitContract(List<ContractItem> contract);

        void InitXDocument(XDocument doc);

        bool DataMap();
    }
}