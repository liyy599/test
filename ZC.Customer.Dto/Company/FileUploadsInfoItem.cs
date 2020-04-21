using System;
using System.Collections.Generic;
using System.Text;
using ZC.Customer.Model.Enums;
using ZC.Customer.Model.Models;
using UMa.Merak.Infrastructure.AutoMap;
using Microsoft.AspNetCore.Http;

namespace ZC.Customer.Dto.Company
{
    [AutoMap(typeof(FileUploadsInfo))]
    public class FileUploadsInfoItem
    {

        /// <summary>
        /// id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///公司id
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public string CreationTime
        {
            get;set;
        }

        /// <summary>
        /// 文件格式
        /// </summary>
        public string FileForm { get; set; }
    }
}
