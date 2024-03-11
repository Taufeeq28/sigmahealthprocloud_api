using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.RequestModels
{
    public class VaccinesModel
    {
        public string? Product { get; set; }
        public string? Vaccine {  get; set; }
        public string? manufacturer { get; set;}
        
        public Guid Productid { get; set; }

        public Guid Vaccineid { get; set; }
        public Guid manufacturerid { get; set; }

        public Guid inventoryid { get; set; }

        public string? price { get; set;}
    }
}
