using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZC.Customer.Repository.Mappings;
using UMa.Merak.Infrastructure.Domain.Entities;
using UMa.Merak.Infrastructure.Dto;
using UMa.Merak.Infrastructure.Helper;
using UMa.Merak.Infrastructure.HttpContextUser;
using UMa.Merak.Infrastructure.LogHelper;

namespace ZC.Customer.Repository
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() : base()
        {
        }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }

        private readonly IUser _user;
        public MyDbContext(DbContextOptions<MyDbContext> options, IUser user) : base(options)
        {
            this._user = user;
        }

        public override int SaveChanges()
        {
            SetTrackInfo();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            SetTrackInfo();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void SetTrackInfo()
        {
            ChangeTracker.DetectChanges();

            var entitys = this.ChangeTracker.Entries()
                .Select(t => new { t.State, t.Entity, t.CurrentValues })
                .ToArray();

            var entityState = "";
            foreach (var entity in entitys)
            {
                switch (entity.State)
                {
                    case EntityState.Added:
                        entityState = "Added";
                        break;

                    case EntityState.Modified:
                        entityState = "Modified";
                        break;
                    case EntityState.Deleted:
                        entityState = "Deleted";
                        var trackDeleted = entity.Entity as ISoftDelete;
                        trackDeleted.IsDeleted = true;
                        break;
                }

                // 只有新增，修改，删除 才会记录实体类当前值
                if (!string.IsNullOrWhiteSpace(entityState))
                {
                    var propertys = PrintPropertyValues(entity.CurrentValues, entity.CurrentValues.Properties.Select(p => p.Name).ToList());
                    JObject jObject = new JObject();

                    foreach (var pro in propertys)
                    {
                        jObject[pro.DisplayText] = pro.Value == null ? "" : pro.Value.ToString();
                    }

                    var jsonStr = JsonHelper.Obj2JsonStr<JObject>(jObject);
                    LogLock.WriteLine("实体类当前值", new string[] {
                     "操作类型："+entityState,
                     "实体类名称："+entity.CurrentValues.EntityType.Name,
                     "实体类当前值："+jsonStr
                 });
                }
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // 放在这里会导致日志记录在Repository项目根目录中，所以转移到startup中
            //var loggerFactory = new LoggerFactory();
            //loggerFactory.AddProvider(new EFLoggerProvider());
            //optionsBuilder.UseLoggerFactory(loggerFactory);
            //base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CompanyInfoMapping());
            modelBuilder.ApplyConfiguration(new ProductInfoMapping());
            modelBuilder.ApplyConfiguration(new StaffInfoMapping());
            modelBuilder.ApplyConfiguration(new FileUploadsInfoMapping());
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// 记录实体类当前值
        /// </summary>
        private List<ComboboxItemDto> PrintPropertyValues(PropertyValues values, IEnumerable<string> propertiesToPrint, int indent = 1)
        {
            var list = new List<ComboboxItemDto>();
            foreach (var propertyName in propertiesToPrint)
            {
                var value = values[propertyName];
                if (value is PropertyValues complexPropertyValues)
                {
                    var items = PrintPropertyValues(complexPropertyValues, complexPropertyValues.Properties.Select(s => s.Name).ToList(), indent + 1);
                    list.AddRange(items);
                }
                else
                {
                    list.Add(new ComboboxItemDto { Value = values[propertyName], DisplayText = propertyName });
                }
            }

            return list;
        }
    }
}
