using System;
using System.Collections.Generic;
using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.Entities
{
    public partial class BaseTable
    {
        public string Btid { get; set; }
        public string BatchNumber { get; set; }

        public int BatchCount { get; set; }
        public string SteelSealNumberRange { get; set; }
        public string ParameterSpecification { get; set; }
        public string InstallationMethod { get; set; }
        public string CommonTraffic { get; set; }
        public string CommonFlowRatio { get; set; }
        public string InitialFlow { get; set; }
        public string MinimumReading { get; set; }
        public string MaximumDegree { get; set; }
        public string MaximumWorkingPressure { get; set; }
        public string MaximumOperatingTemperature { get; set; }
        public string WorkingEnvironmentTemperature { get; set; }
        public string WorkingEnvironmentHumidity { get; set; }
        public string ExecutiveStandard { get; set; }
        public DateTime? DateOfManufacture { get; set; }
        public string Remarks { get; set; }
        public Status Status { get; set; }
        public IsDeleted IsDeleted { get; set; }
    }
}
