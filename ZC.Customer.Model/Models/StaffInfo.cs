using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UMa.Merak.Infrastructure.Domain.Entities;

namespace ZC.Customer.Model.Models
{
    /// <summary>
    /// 员工信息表
    /// </summary>
    public class StaffInfo : IEntity<long>, ISoftDelete
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        /// <summary>
        /// 员工姓名
        /// </summary>
        [Required]
        [StringLength(50)]
        public string StaffName { get; set; }

        /// <summary>
        /// 岗位标识
        /// </summary>
        [Required]
        public string Post { get; set; }


        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }


        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModificationTime { get; set; }


        /// <summary>
        /// 是否逻辑删除
        /// </summary>
        public bool IsDeleted { get; set; }
        
    }
}
