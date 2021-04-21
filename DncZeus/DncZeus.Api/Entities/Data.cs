using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DncZeus.Api.Entities
{
    public partial class Data
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
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public short? Dbid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public byte[] Sid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public short? Mode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? Status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? Status2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime Crdate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? Reserved { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? Category { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public byte Cmptlevel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Filename { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public short? Version { get; set; }
    }
}

