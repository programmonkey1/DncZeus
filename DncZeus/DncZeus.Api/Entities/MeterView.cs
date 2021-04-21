using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DncZeus.Api.Entities
{
    public partial class MeterView
    {
        
        /// <summary>
        /// 
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        [DefaultValue("newid()")]
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SaleRoomName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int AreaId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AreaName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int GprsCodeA { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int GprsCodeb { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int GprsCodec { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int UptownId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UptownName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int BuildId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BuildName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int UnitId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UnitName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UserAddr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RoomNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int MeterId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string MeterAddr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal Sys { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal Bys { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime Syrq { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime Byrq { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime Lrrq { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public byte Cbwc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DataName { get; set; }
    }
}
