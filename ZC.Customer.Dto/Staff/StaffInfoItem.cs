using System;
using System.Collections.Generic;
using System.Text;
using ZC.Customer.Model.Enums;
using ZC.Customer.Model.Models;
using UMa.Merak.Infrastructure.AutoMap;
using UMa.Merak.Infrastructure.Domain.Enums;
using UMa.Merak.Infrastructure.Helper;

namespace ZC.Customer.Dto.Staff
{
    [AutoMapFrom(typeof(StaffInfo))]
    public class StaffInfoItem
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string StaffName { get; set; }

        /// <summary>
        /// 长者来源
        /// </summary>
        public string Post { get; set; }

        /// <summary>
        /// 长者来源
        /// </summary>
        public string SourceTypeStr
        {
            get
            {
                var postNames = string.Empty;
                var posts = this.Post.Split(',');
                foreach (var item in posts)
                {
                    var name = EnumHelper.GetDescriptionByValue(typeof(Post), (Convert.ToInt32(item)).ToString());
                    if (postNames == null || postNames == "")
                    {
                        postNames = name;
                    }
                    else
                    {
                        postNames = postNames + ",";
                    }
                }
                return postNames;
            }
        }
    }
}
