using System;
using System.Collections.Generic;
using System.Text;
using UMa.Merak.Infrastructure.AutoMap;
using ZC.Customer.Model.Models;

namespace ZC.Customer.Dto.Staff
{
    [AutoMap(typeof(StaffInfo))]
    public class StaffInfoDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string StaffName { get; set; }

        /// <summary>
        /// 岗位标识
        /// </summary>        
        public string Post { get; set; }
    }
}
