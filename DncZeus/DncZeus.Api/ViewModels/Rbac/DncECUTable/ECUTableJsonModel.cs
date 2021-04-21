using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.ViewModels.Rbac.DncECUTable
{
    public class ECUTableJsonModel
    {
        public string Ecuid { get; set; }
        public string ElectronicUnitNumber { get; set; }
        public string CommunicationMode { get; set; }
        public string AverageWorkingCurrent { get; set; }
        public string ProtectionLevel { get; set; }
        public DateTime? DateOfManufacture { get; set; }
        public string ProgramVersion { get; set; }
        public string Imsi { get; set; }
        public string Imei { get; set; }
        public string ProductModel { get; set; }
        public string SignalIntensity { get; set; }
        public string MagneticInterferenceStatus { get; set; }
        public string VoltageState { get; set; }
        public string Voltage { get; set; }
        public string BatteryModel { get; set; }
        public string Remarks { get; set; }
        public Status Status { get; set; }
        public IsDeleted IsDeleted { get; set; }
    }
}
