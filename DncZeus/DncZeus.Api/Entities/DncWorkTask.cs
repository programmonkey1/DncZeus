﻿using System;
using System.Collections.Generic;
using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.Entities
{
    public partial class DncWorkTask
    {
        public int Id { get; set; }
        public string TaskTheme { get; set; }
        public string TaskContent { get; set; }
        public int? WorkType { get; set; }
        public string TaskPerson { get; set; }
        public string Telephone { get; set; }
        public DateTime? TaskTime { get; set; }
        public string CompletionTime { get; set; }
        public string TaskPlan { get; set; }
        public DateTime? ProgressDeviation { get; set; }
        public string InformationNote { get; set; }
        public string ThirdPartyCooperation { get; set; }
        public string MattersNeedingAttention { get; set; }
        public string ProjectManager { get; set; }
        public string Publisher { get; set; }
        public Status Status { get; set; }
        public IsDeleted IsDeleted { get; set; }
        public string Code { get; set; }
    }
}