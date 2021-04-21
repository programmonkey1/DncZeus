using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DncZeus.Api.ViewModels.Rbac.DncAfterStaff
{
    public class AfterStaffJsonModel
    {
        
            public int Id { get; set; }
            public string ProvinceName { get; set; }
            public string CityName { get; set; }
            public string AreaName { get; set; }
            public string Salesman { get; set; }
            public string AfterSalesStaff { get; set; }
            public string Customer { get; set; }
            public string Telephone1 { get; set; }
            public string Telephone2 { get; set; }
            public string Telephone3 { get; set; }
        
    }
}
