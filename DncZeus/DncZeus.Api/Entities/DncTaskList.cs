using System;
using System.Collections.Generic;
using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.Entities
{
    public partial class DncTaskList
    {
        public int Id { get; set; }
        public string KeyWork { get; set; }
        public int ImportanceOfWork { get; set; }
        public string MajorIinitiatives { get; set; }
        public string ImplementationProgress { get; set; }
        public int ProgressStatus { get; set; }
        public string PersonLiable { get; set; }
        public string Remarks { get; set; }
        public DateTime CreationTime { get; set; }
        public int IsComplete { get; set; }
        public string Code { get; set; }
    }
}
