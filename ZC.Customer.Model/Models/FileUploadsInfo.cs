using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UMa.Merak.Infrastructure.Domain.Entities;
using ZC.Customer.Model.Enums;

namespace ZC.Customer.Model.Models
{
    /// <summary>
    /// 文件上传信息表
    /// </summary>
    public class FileUploadsInfo : IEntity<long>, ISoftDelete
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        /// <summary>
        /// 公司Id
        /// </summary>
        [Required]
        public long CompanyId { get; set; }

        /// <summary>
        /// 文件分类
        /// </summary>
        [Required]
        [StringLength(50)]
        public string FileType { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        [Required]
        [StringLength(50)]
        public string FileName { get; set; }

        /// <summary>
        /// 文件格式
        /// </summary>
        [Required]
        [StringLength(50)]
        public string FileForm { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        [Required]
        [StringLength(200)]
        public string FilePath { get; set; }

        /// <summary>
        /// 上传时间
        /// </summary>
        [Required]
        public DateTime CreationTime { get; set; }


        /// <summary>
        /// 是否逻辑删除
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
