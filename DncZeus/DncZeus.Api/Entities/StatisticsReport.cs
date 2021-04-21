using System;
using System.Collections.Generic;

namespace DncZeus.Api.Entities
{
    public partial class StatisticsReport
    {
        public int Id { get; set; }
        public int? Numberofcustomers { get; set; }
        public int? Numberofdistricts { get; set; }
        public int? Quantityofwatermeters { get; set; }
        public int? Numberofsuccesses { get; set; }
        public int? Numberoffailures { get; set; }
        public int? Offlinequantity { get; set; }
        public int? Numberofnewfiles { get; set; }
        public int? Asof10 { get; set; }
        public int? Asof5 { get; set; }
        public int? Asof0 { get; set; }
        public DateTime? Timeofday { get; set; }
    }
}
