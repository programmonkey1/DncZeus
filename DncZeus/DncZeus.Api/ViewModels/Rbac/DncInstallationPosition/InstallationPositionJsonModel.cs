using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.ViewModels.Rbac.DncInstallationPosition
{
    public class InstallationPositionJsonModel
    {

        public string Ipid { get; set; }
        public string AreaId { get; set; }
        public string AreaName { get; set; }
        public string UptownId { get; set; }
        public string UptownName { get; set; }
        public string UptownAddr { get; set; }
        public string BuildId { get; set; }
        public string BuildName { get; set; }
        public string UnitId { get; set; }
        public string UnitName { get; set; }
        public string PayNumber { get; set; }
        public DateTime? InstallationTime { get; set; }
        public string Remarks { get; set; }
        public Status Status { get; set; }
        public IsDeleted IsDeleted { get; set; }

    }
}
