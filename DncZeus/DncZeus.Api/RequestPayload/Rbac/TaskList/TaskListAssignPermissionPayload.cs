/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:    码友网(https://codedefault.com)--专注.NET/.NET Core
 * 版权所有，请勿删除
 ******************************************/

using System.Collections.Generic;

namespace DncZeus.Api.RequestPayload.Rbac.TaskList
{
    /// <summary>
    /// 角色分配权限的请求载体类
    /// </summary>
    public class TaskListAssignPermissionPayload
    {
        /// <summary>
        /// 
        /// </summary>
        public TaskListAssignPermissionPayload()
        {
            Permissions = new List<string>();
        }
        /// <summary>
        /// 角色编码
        /// </summary>
        public string TaskListCode { get; set; }
        /// <summary>
        /// 权限列表
        /// </summary>
        public List<string> Permissions { get; set; }
    }
}
