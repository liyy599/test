using System.Threading.Tasks;
using System.Linq;
using System;
using System.Linq.Expressions;
using ZC.Customer.IRepository;
using ZC.Customer.IServices;
using ZC.Customer.Model.Models;
using UMa.Merak.Infrastructure.Dto;
using UMa.Merak.Infrastructure.HttpContextUser;
using UMa.Merak.Infrastructure.Data.Specification;
using UMa.Merak.Infrastructure.AutoMap;
using System.Collections.Generic;
using ZC.Customer.Dto;
using UMa.Merak.Infrastructure.LogHelper;
using UMa.Merak.Infrastructure.Helper;
using Snowflake;
using ZC.Customer.Dto.Company;
using ZC.Customer.Model.Enums;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace ZC.Customer.FrameWork.Services
{
    /// <summary>
    /// 合同信息业务逻辑类
    /// </summary>
    public class CompanyInfoServices : ICompanyInfoServices
    {
        /// <summary>
        /// 公司信息存储器接口
        /// </summary>
        private readonly IComapnyInfoRepository comapnyInfoRepository;

        /// <summary>
        /// 员工信息存储器接口
        /// </summary>
        private readonly IStaffInfoRepository staffInfoRepository;

        /// <summary>
        /// 文件上传信息存储器接口
        /// </summary>
        private readonly IFileUploadsInfoRepository fileUploadsInfoRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="changeInfoRepository">合同信息存储器</param>
        public CompanyInfoServices(IComapnyInfoRepository comapnyInfoRepository, IStaffInfoRepository staffInfoRepository,
        IFileUploadsInfoRepository fileUploadsInfoRepository)
        {
            this.comapnyInfoRepository = comapnyInfoRepository;
            this.staffInfoRepository = staffInfoRepository;
            this.fileUploadsInfoRepository = fileUploadsInfoRepository;
        }

        /// <summary>
        /// 获取公司分页列表信息
        /// </summary>
        /// <param name="dto">查询dto</param>
        /// <returns></returns>
        public async Task<ResponseResult> GetCompanyInfoPageResult(CompanyInfoSearchDto dto)
        {
            var result = new ResponseResult(0, "获取成功");
            Expression<Func<CompanyInfo, bool>> filter = p => true;
            filter = filter.And(p => !p.IsDeleted);

            if (!string.IsNullOrEmpty(dto.CompanyName))
            {
                filter = filter.And(p => p.CompanyName.Contains(dto.CompanyName));
            }
            if (!string.IsNullOrEmpty(dto.Area))
            {
                filter = filter.And(p => (int)p.Area == Convert.ToInt32(dto.Area));
            }

            if (!string.IsNullOrEmpty(dto.Province.ToString()))
            {
                filter = filter.And(p => (int)p.Province == Convert.ToInt32(dto.Province));
            }


            var pageResult = await this.comapnyInfoRepository.QueryPageAsync(filter, dto.OrderBy, dto.PageSize, dto.PageIndex);
            var list = pageResult.List.MapTo<List<CompanyInfoItem>>();

            var data = new
            {
                pageResult.PageIndex,
                pageResult.PageSize,
                pageResult.RecordCount,
                pageResult.PageCount,
                List = list
            };

            result.Data = data;
            return result;
        }

        /// <summary>
        /// 新增或者修改公司信息
        /// </summary>
        /// <param name="jsonStr">Json格式公司信息dto</param>
        /// <returns></returns>
        public async Task<ResponseResult> AddOrUpdateCompanyInfo(JsonDto jsonStr)
        {
            var dto = JsonHelper.JsonStr2Obj<CompanyInfoDto>(jsonStr.JsonStr);

            var result = new ResponseResult(0, "保存成功");

            try
            {
                var worker = new IdWorker(1, 1);
                if (string.IsNullOrWhiteSpace(dto.Id))
                {
                    //创建公司基本信息及其对应档案信息
                    var modelBase = dto.MapTo<CompanyInfo>();
                    modelBase.Id = worker.NextId();
                    modelBase.CreationTime = DateTime.Now;
                    await this.comapnyInfoRepository.InsertAsync(modelBase);

                }
                else
                {
                    var companyInfo = await this.comapnyInfoRepository.GetByIdAsync(long.Parse(dto.Id));
                    if (companyInfo == null)
                    {
                        result.Code = -1;
                        result.Message = "当前公司信息不存在，请刷新后再试";
                        return result;
                    }
                    else
                    {
                        dto.MapTo(companyInfo);
                        companyInfo.LastModificationTime = DateTime.Now;
                        await this.comapnyInfoRepository.UpdateAsync(companyInfo);
                    }
                }

                this.comapnyInfoRepository.SaveChanged();
            }
            catch (Exception ex)
            {
                result.Code = -1;
                result.Message = ex.Message;
                LogLock.WriteLine("SystemLog", new[] { ex.ToString() });
            }

            return result;
        }

        /// <summary>
        /// 根据Id获取公司基本信息
        /// </summary>
        /// <param name="id">公司Id</param>
        /// <returns></returns>
        public async Task<ResponseResult> GetCompanyInfoById(string id)
        {
            var result = new ResponseResult(0, "获取成功");

            Expression<Func<CompanyInfo, bool>> filter = p => true;
            filter = filter.And(p => !p.IsDeleted);
            filter = filter.And(p => p.Id == long.Parse(id));

            var company = await this.comapnyInfoRepository.FirstOrDefaultAsync(filter);
            if (company != null)
            {
                var item = company.MapTo<CompanyInfoItem>();
                result.Data = item;
            }
            else
            {
                result.Code = -1;
                result.Message = "公司信息不存在，请刷新后再试";
            }

            return result;
        }

        /// <summary>
        /// 根据Id获取公司附件信息
        /// </summary>
        /// <param name="id">公司Id</param>
        /// <returns></returns>
        public async Task<ResponseResult> GetFileInfoDataById(string id)
        {
            var result = new ResponseResult(0, "获取成功");

            Expression<Func<FileUploadsInfo, bool>> filter = p => true;
            filter = filter.And(p => !p.IsDeleted);
            filter = filter.And(p => p.CompanyId.ToString() == id);

            var files = await this.fileUploadsInfoRepository.GetAllListAsync(filter);
            if (files != null)
            {
                var dataList = files.MapTo<List<FileUploadsInfoItem>>();
                result.Data = dataList;
            }
            else
            {
                result.Code = -1;
                result.Message = "附件信息不存在，请刷新后再试";
            }

            return result;
        }

        /// <summary>
        /// 根据Id删除取公司基本信息
        /// </summary>
        /// <param name="id">公司Id</param>
        /// <returns></returns>
        public async Task<ResponseResult> DeleteCompanyInfoById(string id)
        {
            var result = new ResponseResult(0, "删除成功");

            try
            {
                if (!string.IsNullOrWhiteSpace(id))
                {
                    var companyInfo = await this.comapnyInfoRepository.GetByIdAsync(long.Parse(id));
                    if (companyInfo == null)
                    {
                        result.Code = -1;
                        result.Message = "当前公司信息不存在，请刷新后再试";
                        return result;
                    }
                    else
                    {
                        companyInfo.IsDeleted = true;
                        await this.comapnyInfoRepository.UpdateAsync(companyInfo);
                    }
                }
                else
                {
                    result.Code = -1;
                    result.Message = "当前公司信息不存在，请刷新后再试";
                    return result;
                }

                this.comapnyInfoRepository.SaveChanged();
            }
            catch (Exception ex)
            {
                result.Code = -1;
                result.Message = ex.Message;
                LogLock.WriteLine("SystemLog", new[] { ex.ToString() });
            }
            return result;
        }

        /// <summary>
        /// 根据Id删除取公司附件信息
        /// </summary>
        /// <param name="id">公司Id</param>
        /// <returns></returns>
        public async Task<ResponseResult> DeleteUploadFileInfoById(string id)
        {
            var result = new ResponseResult(0, "删除成功");

            try
            {
                if (!string.IsNullOrWhiteSpace(id))
                {
                    var fileInfo = await this.fileUploadsInfoRepository.GetByIdAsync(long.Parse(id));
                    if (fileInfo == null)
                    {
                        result.Code = -1;
                        result.Message = "当前附件信息不存在，请刷新后再试";
                        return result;
                    }
                    else
                    {
                        //删除本地文件
                        if (File.Exists(fileInfo.FilePath))
                        {
                            File.Delete(fileInfo.FilePath);
                        }
                        //删除数据库信息
                        fileInfo.IsDeleted = true;
                        await this.fileUploadsInfoRepository.UpdateAsync(fileInfo);
                    }
                }
                else
                {
                    result.Code = -1;
                    result.Message = "当前附件信息不存在，请刷新后再试";
                    return result;
                }

                this.comapnyInfoRepository.SaveChanged();
            }
            catch (Exception ex)
            {
                result.Code = -1;
                result.Message = ex.Message;
                LogLock.WriteLine("SystemLog", new[] { ex.ToString() });
            }
            return result;
        }

        /// <summary>
        /// 上传公司附件
        /// </summary>
        /// <param name="file">文件</param>
        /// <returns></returns>
        public async Task<ResponseResult> uploadCompanyInfoFile(FileUploadsInfoDto dto)
        {
            var result = new ResponseResult(0, "上传成功");
            var file = dto.File;
            var Id = dto.Id;

            try
            {
                if (file != null)
                {
                    var typeList = ".jpg,.jpeg,.png,.gif,.pdf,.JPG,.JPEG,.PBG,.GIF,.PDF";
                    var fileNameSplit = file.FileName.Split(".");
                    var fileForm = fileNameSplit[fileNameSplit.Length - 1];
                    if (typeList.IndexOf(fileForm) != -1)
                    {
                        string dirName = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString();
                        var path = Directory.GetCurrentDirectory();
                        string dirPath = "/FileUpload/CompanyInfo/" + dto.Id+"/" + dirName;
                        var fullPath= path + "/wwwroot" + dirPath;
                        //文件名称
                        string projectFileName = file.FileName;
                        //判断是否上传同名文件
                        Expression<Func<FileUploadsInfo, bool>> filter = p => true;
                        filter = filter.And(p => !p.IsDeleted && p.FilePath== (dirPath + "/" + projectFileName));
                        var fileUploadsInfo = this.fileUploadsInfoRepository.GetAllListAsync(filter);
                        if(fileUploadsInfo.Result.Count > 0)
                        {
                            result.Code = -1;
                            result.Message = projectFileName+"重复上传";
                            return result;
                        }
                        else
                        {
                            if (!Directory.Exists(fullPath))
                            {
                                Directory.CreateDirectory(fullPath);
                            }
                            //上传的文件的路径
                            string filePath = fullPath + $@"\{projectFileName}";
                            using (FileStream fs = System.IO.File.Create(filePath))
                            {
                                file.CopyTo(fs);
                                fs.Flush();
                            }
                            var worker = new IdWorker(1, 1);
                            var fileInfo = new FileUploadsInfo();
                            fileInfo.Id = worker.NextId();
                            fileInfo.FileType = "CompanyInfo";
                            fileInfo.FilePath = dirPath + "/" + projectFileName;
                            fileInfo.CreationTime = DateTime.Now;
                            fileInfo.FileForm = fileForm;
                            fileInfo.FileName = projectFileName;
                            fileInfo.CompanyId = Convert.ToInt64(Id);
                            await this.fileUploadsInfoRepository.InsertAsync(fileInfo);
                            this.fileUploadsInfoRepository.SaveChanged();
                            return result;
                        }
                    }
                    else
                    {
                        result.Code = -1;
                        result.Message = "文件格式不正确，上传失败";

                    }
                }
            }
            catch (Exception ex)
            {
                result.Code = -1;
                result.Message = ex.Message;
                LogLock.WriteLine("SystemLog", new[] { ex.ToString() });
            }
            return result;
        }

        /// <summary>
        /// 获取客户分类下拉列表
        /// </summary>
        /// <returns></returns>
        public ResponseResult GetCustomerTypeCombobox()
        {
            var result = new ResponseResult(0, "获取成功");
            try
            {
                result.Data = EnumHelper.EnumListDictionary(typeof(CustomerType)).Select(m => new ComboboxItemDto()
                {
                    Value = m.Value,
                    DisplayText = m.Key
                }).ToList();
            }
            catch (Exception ex)
            {
                result.Code = -1;
                result.Message = ex.Message;
                result.Data = new List<ComboboxItemDto>();
            }
            return result;
        }

        /// <summary>
        /// 获取客户等级下拉列表
        /// </summary>
        /// <returns></returns>
        public ResponseResult GetCustomerLevelCombobox()
        {
            var result = new ResponseResult(0, "获取成功");
            try
            {
                result.Data = EnumHelper.EnumListDictionary(typeof(CustomerLevel)).Select(m => new ComboboxItemDto()
                {
                    Value = m.Value,
                    DisplayText = m.Key
                }).ToList();
            }
            catch (Exception ex)
            {
                result.Code = -1;
                result.Message = ex.Message;
                result.Data = new List<ComboboxItemDto>();
            }
            return result;
        }

        /// <summary>
        /// 获取区域下拉列表
        /// </summary>
        /// <returns></returns>
        public ResponseResult GetAreaCombobox()
        {
            var result = new ResponseResult(0, "获取成功");
            try
            {
                result.Data = EnumHelper.EnumListDictionary(typeof(Area)).Select(m => new ComboboxItemDto()
                {
                    Value = m.Value,
                    DisplayText = m.Key
                }).ToList();
            }
            catch (Exception ex)
            {
                result.Code = -1;
                result.Message = ex.Message;
                result.Data = new List<ComboboxItemDto>();
            }
            return result;
        }

        /// <summary>
        /// 获取省份下拉列表
        /// </summary>
        /// <returns></returns>
        public ResponseResult GetProvinceCombobox()
        {
            var result = new ResponseResult(0, "获取成功");
            try
            {
                result.Data = EnumHelper.EnumListDictionary(typeof(Province)).Select(m => new ComboboxItemDto()
                {
                    Value = m.Value,
                    DisplayText = m.Key
                }).ToList();
            }
            catch (Exception ex)
            {
                result.Code = -1;
                result.Message = ex.Message;
                result.Data = new List<ComboboxItemDto>();
            }
            return result;
        }

        /// <summary>
        /// 获取总监下拉列表
        /// </summary>
        /// <returns></returns>
        public ResponseResult GetDirectorCombobox()
        {
            var result = new ResponseResult(0, "获取成功");
            try
            {
                Expression<Func<StaffInfo, bool>> filter = p => true;
                filter = filter.And(p => !p.IsDeleted);
                filter = filter.And(p => p.Post == "1");

                var staffs = this.staffInfoRepository.GetAllListAsync(filter);
                var dataList = new List<object>();
                foreach (var staff in staffs.Result)
                {
                    var item = new
                    {
                        Value = staff.Id,
                        DisplayText = staff.StaffName,
                    };
                    dataList.Add(item);
                }
                result.Data = dataList;
            }
            catch (Exception ex)
            {
                result.Code = -1;
                result.Message = ex.Message;
                result.Data = new List<object>();
            }
            return result;
        }

        /// <summary>
        /// 获取业务员下拉列表
        /// </summary>
        /// <returns></returns>
        public ResponseResult GetSalesmanCombobox()
        {
            var result = new ResponseResult(0, "获取成功");
            try
            {
                Expression<Func<StaffInfo, bool>> filter = p => true;
                filter = filter.And(p => !p.IsDeleted);
                filter = filter.And(p => p.Post == "100");

                var staffs = this.staffInfoRepository.GetAllListAsync(filter);
                var dataList = new List<object>();
                foreach (var staff in staffs.Result)
                {
                    var item = new
                    {
                        Value = staff.Id,
                        DisplayText = staff.StaffName,
                    };
                    dataList.Add(item);
                }
                result.Data = dataList;
            }
            catch (Exception ex)
            {
                result.Code = -1;
                result.Message = ex.Message;
                result.Data = new List<object>();
            }
            return result;
        }

        /// <summary>
        /// 获取快递下拉列表
        /// </summary>
        /// <returns></returns>
        public ResponseResult GetExpressCombobox()
        {
            var result = new ResponseResult(0, "获取成功");
            try
            {
                result.Data = EnumHelper.EnumListDictionary(typeof(Express)).Select(m => new ComboboxItemDto()
                {
                    Value = m.Value,
                    DisplayText = m.Key
                }).ToList();
            }
            catch (Exception ex)
            {
                result.Code = -1;
                result.Message = ex.Message;
                result.Data = new List<ComboboxItemDto>();
            }
            return result;
        }

        /// <summary>
        /// 获取发票下拉列表
        /// </summary>
        /// <returns></returns>
        public ResponseResult GetInvoiceCombobox()
        {
            var result = new ResponseResult(0, "获取成功");
            try
            {
                result.Data = EnumHelper.EnumListDictionary(typeof(Invoice)).Select(m => new ComboboxItemDto()
                {
                    Value = m.Value,
                    DisplayText = m.Key
                }).ToList();
            }
            catch (Exception ex)
            {
                result.Code = -1;
                result.Message = ex.Message;
                result.Data = new List<ComboboxItemDto>();
            }
            return result;
        }

        /// <summary>
        /// 获取出库单模板下拉列表
        /// </summary>
        /// <returns></returns>
        public ResponseResult GetTemplateCombobox()
        {
            var result = new ResponseResult(0, "获取成功");
            try
            {
                result.Data = EnumHelper.EnumListDictionary(typeof(Template)).Select(m => new ComboboxItemDto()
                {
                    Value = m.Value,
                    DisplayText = m.Key
                }).ToList();
            }
            catch (Exception ex)
            {
                result.Code = -1;
                result.Message = ex.Message;
                result.Data = new List<ComboboxItemDto>();
            }
            return result;
        }

        /// <summary>
        /// 获取出库单联数下拉列表
        /// </summary>
        /// <returns></returns>
        public ResponseResult GetLinkNumCombobox()
        {
            var result = new ResponseResult(0, "获取成功");
            try
            {
                result.Data = EnumHelper.EnumListDictionary(typeof(LinkNum)).Select(m => new ComboboxItemDto()
                {
                    Value = m.Value,
                    DisplayText = m.Key
                }).ToList();
            }
            catch (Exception ex)
            {
                result.Code = -1;
                result.Message = ex.Message;
                result.Data = new List<ComboboxItemDto>();
            }
            return result;
        }

        /// <summary>
        /// 获取质检报告下拉列表
        /// </summary>
        /// <returns></returns>
        public ResponseResult GetQTCombobox()
        {
            var result = new ResponseResult(0, "获取成功");
            try
            {
                result.Data = EnumHelper.EnumListDictionary(typeof(QT)).Select(m => new ComboboxItemDto()
                {
                    Value = m.Value,
                    DisplayText = m.Key
                }).ToList();
            }
            catch (Exception ex)
            {
                result.Code = -1;
                result.Message = ex.Message;
                result.Data = new List<ComboboxItemDto>();
            }
            return result;
        }

        /// <summary>
        /// 获取冷链记录下拉列表
        /// </summary>
        /// <returns></returns>
        public ResponseResult GetColdChainCombobox()
        {
            var result = new ResponseResult(0, "获取成功");
            try
            {
                result.Data = EnumHelper.EnumListDictionary(typeof(ColdChain)).Select(m => new ComboboxItemDto()
                {
                    Value = m.Value,
                    DisplayText = m.Key
                }).ToList();
            }
            catch (Exception ex)
            {
                result.Code = -1;
                result.Message = ex.Message;
                result.Data = new List<ComboboxItemDto>();
            }
            return result;
        }

        /// <summary>
        /// 获取特殊产品名称下拉列表
        /// </summary>
        /// <returns></returns>
        public ResponseResult GetSpecialProductCombobox()
        {
            var result = new ResponseResult(0, "获取成功");
            try
            {
                result.Data = EnumHelper.EnumListDictionary(typeof(SpecialProduct)).Select(m => new ComboboxItemDto()
                {
                    Value = m.Value,
                    DisplayText = m.Key
                }).ToList();
            }
            catch (Exception ex)
            {
                result.Code = -1;
                result.Message = ex.Message;
                result.Data = new List<ComboboxItemDto>();
            }
            return result;
        }

        /// <summary>
        /// 获取特殊包装下拉列表
        /// </summary>
        /// <returns></returns>
        public ResponseResult GetExtraPackingCombobox()
        {
            var result = new ResponseResult(0, "获取成功");
            try
            {
                result.Data = EnumHelper.EnumListDictionary(typeof(ExtraPacking)).Select(m => new ComboboxItemDto()
                {
                    Value = m.Value,
                    DisplayText = m.Key
                }).ToList();
            }
            catch (Exception ex)
            {
                result.Code = -1;
                result.Message = ex.Message;
                result.Data = new List<ComboboxItemDto>();
            }
            return result;
        }

        /// <summary>
        /// 获取账期下拉列表
        /// </summary>
        /// <returns></returns>
        public ResponseResult GetAccountPeriodCombobox()
        {
            var result = new ResponseResult(0, "获取成功");
            try
            {
                result.Data = EnumHelper.EnumListDictionary(typeof(AccountPeriod)).Select(m => new ComboboxItemDto()
                {
                    Value = m.Value,
                    DisplayText = m.Key
                }).ToList();
            }
            catch (Exception ex)
            {
                result.Code = -1;
                result.Message = ex.Message;
                result.Data = new List<ComboboxItemDto>();
            }
            return result;
        }
    }
}