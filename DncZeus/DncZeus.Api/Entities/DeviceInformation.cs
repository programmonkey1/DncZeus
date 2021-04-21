using System;
using System.Collections.Generic;
using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.Entities
{
    public partial class DeviceInformation
    {
        public string Diid { get; set; }
        public string Eunumber { get; set; }
        public string ProductModel { get; set; }
        public string Ecuid { get; set; }
        public string ElectronicUnitNumber { get; set; }
        public string Btid { get; set; }
        public string BatchNumber { get; set; }
        public DateTime? DateOfManufacture { get; set; }
        public string Remarks { get; set; }
        public Status Status { get; set; }
        public IsDeleted IsDeleted { get; set; }
    }
}
