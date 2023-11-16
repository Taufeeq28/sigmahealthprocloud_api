using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class users : baseclass
    {
        [Key]
        public int user_id { get; set; }
        [DataType("character varying")]
        public string username { get; set; }
        [DataType("character varying")]
        public string password { get; set; }
        [ForeignKey("fk_county_id")]
        public int county_id { get; set; }
        [ForeignKey("fk_city_id")]
        public int city_id { get; set; }
        [ForeignKey("fk_facility_id")]
        public int facility_id { get; set; }

        [ForeignKey("fk_usertype_id")]
        public int user_type_id { get; set; }
        public virtual user_types user_types { get; set; }
        public virtual counties counties { get; set; }
        public virtual cities cities { get; set; }
        public virtual facilities facilities {  get; set; }
    }
}
