using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.ViewModels.Rbac.DncWorkTask
{
    public class WorkTaskCreateViewModel
    {
        public int Id { get; set; }
        public string TaskTheme { get; set; }
        public string TaskContent { get; set; }
        public int? WorkType { get; set; }
        public string TaskPerson { get; set; }
        public string Telephone { get; set; }
        public string TaskTime { get; set; }
        public string CompletionFirstTime { get; set; }
        public string CompletionEndTime { get; set; }
        public string CompletionTime { get; set; }
        public string TaskPlan { get; set; }
        public string PlanList { get; set; }
        public int? No1 { get; set; }
        public int? No2 { get; set; }
        public int? No3 { get; set; }
        public int? No4 { get; set; }
        public int? No5 { get; set; }
        public int? No6 { get; set; }
        public int? No7 { get; set; }
        public int? No8 { get; set; }
        public int? No9 { get; set; }
        public int? No10 { get; set; }
        public int? No11 { get; set; }
        public int? No12 { get; set; }
        public int? No13 { get; set; }
        public int? No14 { get; set; }
        public int? No15 { get; set; }
        public int? No16 { get; set; }
        public int? No17 { get; set; }
        public int? No18 { get; set; }
        public int? No19 { get; set; }
        public int? No20 { get; set; }
        public int? No21 { get; set; }
        public int? No22 { get; set; }
        public int? No23 { get; set; }
        public int? No24 { get; set; }
        public int? No25 { get; set; }
        public int? No26 { get; set; }
        public int? No27 { get; set; }
        public int? No28 { get; set; }
        public int? No29 { get; set; }
        public int? No30 { get; set; }
        public int? No31 { get; set; }
        public string ProgressDeviation { get; set; }
        public string InformationNote { get; set; }
        public string ThirdPartyCooperation { get; set; }
        public string MattersNeedingAttention { get; set; }
        public string ProjectManager { get; set; }
        public string Publisher { get; set; }
        public int? IsFinished { get; set; }
        public Status Status { get; set; }
        public IsDeleted IsDeleted { get; set; }
        public string Code { get; set; }
    }
}
