using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.ViewModels.Rbac.DncWorkTask
{
    public class WorkTasktimeModel
    {
        public DateTime firstTime { get; set; }
        public DateTime endTime { get; set; }
    }
}
