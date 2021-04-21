/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:    码友网(https://codedefault.com)--专注.NET/.NET Core
 * DESCRIPTION:     角色信息实体类
 ******************************************/
using System;
using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.ViewModels.Rbac.DncTaskList
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskListJsonModel
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
    }
}
