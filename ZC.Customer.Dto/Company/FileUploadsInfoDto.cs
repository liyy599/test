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
    public class FileUploadsInfoDto
    {

        /// <summary>
        /// 公司id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 文件
        /// </summary>
        public IFormFile File { get; set; }
        
    }
}
