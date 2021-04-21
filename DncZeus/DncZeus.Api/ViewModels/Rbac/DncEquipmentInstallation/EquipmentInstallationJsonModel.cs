using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.ViewModels.Rbac.DncEquipmentInstallation
{
    public class EquipmentInstallationJsonModel
    {
        public string Eiid { get; set; }
        public string Diid { get; set; }
        public string Eunumber { get; set; }
        public string Ipid { get; set; }
        public string PayNumber { get; set; }
        public DateTime? InstallationTime { get; set; }
        public string Remarks { get; set; }
        public Status Status { get; set; }
        public IsDeleted IsDeleted { get; set; }
    }
}
