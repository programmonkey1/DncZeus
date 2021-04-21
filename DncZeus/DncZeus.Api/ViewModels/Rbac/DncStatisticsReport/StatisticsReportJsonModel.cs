using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DncZeus.Api.ViewModels.Rbac.DncStatisticsReport
{
    public class StatisticsReportJsonModel
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
