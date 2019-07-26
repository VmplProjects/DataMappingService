using System.Collections.Generic;
using System.Xml.Linq;
using DataMappingService.Models;

namespace DataMappingService.Interfaces
{
    public interface IConverter<out T> where T : class
    {
        T Convert(XElement elem);
        
        void InitContract(List<ContractItem> contract);
    }
}