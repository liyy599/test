using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using AutoMapper;
using ZC.Customer.AOP;
using ZC.Customer.AuthHelper;
using ZC.Customer.Filter;
using ZC.Customer.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using StackExchange.Profiling.Storage;
using Swashbuckle.AspNetCore.Swagger;
using static ZC.Customer.SwaggerHelper.CustomApiVersion;
using log4net.Repository;
using log4net;
using log4net.Config;
using UMa.Merak.Infrastructure.MemoryCache;
using UMa.Merak.Infrastructure.Redis;
using UMa.Merak.Infrastructure.LogHelper;
using UMa.Merak.Infrastructure.HttpContextUser;
using UMa.Merak.Infrastructure.Authorization;
using UMa.Merak.Infrastructure.Helper;
using UMa.Merak.Infrastructure.Hubs;
using ZC.Customer.Repository;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using UMa.Merak.Infrastructure.AutoMap;
using Polly;
using Microsoft.AspNetCore.Http.Features;

namespace ZC.Customer
{
    public class Startup
    {

        /// <summary>
        /// log4net 仓储库
        /// </summary>
        public static ILoggerRepository repository { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //log4net
            repository = LogManager.CreateRepository(Configuration["Logging:Log4Net:Name"]);
            //指定配置文件，如果这里你遇到问题，应该是使用了InProcess模式，请查看ZC.Customer.csproj,并删之
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));

        }

        public IConfiguration Configuration { get; }
        private const string ApiName = "ZC.Customer";

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            #region 配置DbContextOption
            var dbConnectionString = Configuration["AppSettings:SqlServer:SqlServerConnection"];
            if (Configuration["AppSettings:Sqlite:Enabled"].ObjToBool())
            {
                dbConnectionString = Configuration["AppSettings:Sqlite:SqliteConnection"];
            }
            else if (Configuration["AppSettings:SqlServer:Enabled"].ObjToBool())
            {
                dbConnectionString = Configuration["AppSettings:SqlServer:SqlServerConnection"];
            }
            else if (Configuration["AppSettings:MySql:Enabled"].ObjToBool())
            {
                dbConnectionString = Configuration["AppSettings:MySql:MySqlConnection"];
            }
            else if (Configuration["AppSettings:Oracle:Enabled"].ObjToBool())
            {
                dbConnectionString = Configuration["AppSettings:Oracle:OracleConnection"];
            }

            services.AddDbContext<MyDbContext>(ops =>
            {
                var loggerFactory = new LoggerFactory();
                loggerFactory.AddProvider(new EFLoggerProvider());
                //打开延迟加载代理的创建。 便于加载导航属性
                ops.UseLazyLoadingProxies();
                ops.UseSqlServer(dbConnectionString).UseLoggerFactory(loggerFactory);
            });
            services.Add(new ServiceDescriptor(typeof(DbContext), typeof(MyDbContext), ServiceLifetime.Scoped));

            #endregion

            #region 部分服务注入-netcore自带方法
            // 缓存注入
            services.AddScoped<ICaching, MemoryCaching>();
            services.AddSingleton<IMemoryCache>(factory =>
            {
                var cache = new MemoryCache(new MemoryCacheOptions());
                return cache;
            });
            // Redis注入
            services.AddSingleton<IRedisCacheManager, RedisCacheManager>();
            // log日志注入
            services.AddSingleton<ILoggerHelper, LogHelper>();
            #endregion

            #region Automapper
            services.AddAutoMapper(typeof(Startup));
            #endregion

            #region CORS  跨域
            //跨域第二种方法，声明策略，记得下边app中配置
            services.AddCors(c =>
            {
                #region 全开放的处理
                c.AddPolicy("AllRequests", policy =>
                {
                    policy
                    .AllowAnyOrigin()//允许任何源
                    .AllowAnyMethod()//允许任何方式
                    .AllowAnyHeader()//允许任何头
                    .AllowCredentials();//允许cookie
                });
                #endregion


                #region 指定域名ip
                c.AddPolicy("LimitRequests", policy =>
                {
                    // 支持多个域名端口，注意端口号后不要带/斜杆：比如localhost:80/，是错的
                    // 注意，http://127.0.0.1:80 和 http://localhost:80 是不一样的，尽量写两个
                    policy
                    .WithOrigins("http://127.0.0.1:4002", "http://localhost:4002", "http://127.0.0.1:4002", "http://localhost:4002", "http://192.168.1.42:4002", "http://10.119.1.219:4002")
                    .AllowAnyHeader()// 确保策略允许任何标头
                    .AllowAnyMethod();
                });
                #endregion
            });

            //跨域第一种办法，注意下边 Configure 中进行配置
            //services.AddCors();
            #endregion

            #region MiniProfiler 性能分析

            services.AddMiniProfiler(options =>
                {
                    options.RouteBasePath = "/profiler";
                    (options.Storage as MemoryCacheStorage).CacheDuration = TimeSpan.FromMinutes(10);

                }
            );

            #endregion

            #region Swagger UI Service api文档配置

            var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
            services.AddSwaggerGen(c =>
            {
                //遍历出全部的版本，做文档信息展示
                typeof(ApiVersions).GetEnumNames().ToList().ForEach(version =>
                {
                    c.SwaggerDoc(version, new Info
                    {
                        // {ApiName} 定义成全局变量，方便修改
                        Version = version,
                        Title = $"{ApiName} 接口文档",
                        Description = $"{ApiName} HTTP API " + version,
                        TermsOfService = "None",
                        Contact = new Contact { Name = "ZC.Customer", Email = "ZC.Customer@xxx.com", Url = "http://127.0.0.1" }
                    });
                    // 按相对路径排序
                    c.OrderActionsBy(o => o.RelativePath);
                });


                //就是这里
                var xmlPath = Path.Combine(basePath, "ZC.Customer.xml");//这个就是刚刚配置的xml文件名
                c.IncludeXmlComments(xmlPath, true);//默认的第二个参数是false，这个是controller的注释，记得修改

                var xmlModelPath = Path.Combine(basePath, "ZC.Customer.Model.xml");//这个就是Model层的xml文件名
                c.IncludeXmlComments(xmlModelPath);

                #region Token绑定到ConfigureServices

                //添加header验证信息
                //c.OperationFilter<SwaggerHeader>();

                // 发行人
                var IssuerName = (Configuration.GetSection("Audience"))["Issuer"];
                var security = new Dictionary<string, IEnumerable<string>> { { IssuerName, new string[] { } }, };
                c.AddSecurityRequirement(security);

                //方案名称“ZC.Customer”可自定义，上下一致即可
                c.AddSecurityDefinition(IssuerName, new ApiKeyScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = "header",//jwt默认存放Authorization信息的位置(请求头中)
                    Type = "apiKey"
                });
                #endregion
            });

            #endregion

            #region MVC + GlobalExceptions

            //注入全局异常捕获
            services.AddMvc(o =>
            {
                // 全局异常过滤
                o.Filters.Add(typeof(GlobalExceptionsFilter));
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            // 取消默认驼峰
            .AddJsonOptions(options => { options.SerializerSettings.ContractResolver = new DefaultContractResolver(); });


            #endregion

            #region Httpcontext

            // Httpcontext 注入
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();

            #endregion

            #region 设置HttpClient

            services.AddHttpClient("IMS", client =>
            {
                client.BaseAddress = new Uri(Configuration["SystemUrl:ImsUrl"]);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            }).AddTransientHttpErrorPolicy(b => b.WaitAndRetryAsync(new[] {
                // 该策略将处理典型的瞬态故障，如果需要，会最多重试 3 次 Http 请求。
                // 这个策略将在第一次重试前延迟 1 秒，第二次重试前 5 秒，在第三次重试前延迟 10 秒
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(5),
                TimeSpan.FromSeconds(10)
            }));

            services.AddHttpClient("CRM", client =>
            {
                client.BaseAddress = new Uri(Configuration["SystemUrl:CrmUrl"]);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            }).AddTransientHttpErrorPolicy(b => b.WaitAndRetryAsync(new[] {
                // 该策略将处理典型的瞬态故障，如果需要，会最多重试 3 次 Http 请求。
                // 这个策略将在第一次重试前延迟 1 秒，第二次重试前 5 秒，在第三次重试前延迟 10 秒
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(5),
                TimeSpan.FromSeconds(10)
            }));

            services.AddHttpClient("UMa.User", client =>
            {
                client.BaseAddress = new Uri(Configuration["SystemUrl:UmaUserUrl"]);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            }).AddTransientHttpErrorPolicy(b => b.WaitAndRetryAsync(new[] {
                // 该策略将处理典型的瞬态故障，如果需要，会最多重试 3 次 Http 请求。
                // 这个策略将在第一次重试前延迟 1 秒，第二次重试前 5 秒，在第三次重试前延迟 10 秒
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(5),
                TimeSpan.FromSeconds(10)
            }));

            #endregion 设置HttpClient


            services.AddSignalR();

            #region Authorize权限设置三种情况

            //使用说明：
            //如果你只是简单的基于角色授权的，第一步：【1/2 简单角色授权】，第二步：配置【统一认证】，第三步：开启中间件app.UseMiddleware<JwtTokenAuth>()不能验证过期，或者 app.UseAuthentication();可以验证过期时间
            //如果你是用的复杂的策略授权，配置权限在数据库，第一步：【3复杂策略授权】，第二步：配置【统一认证】，第三步：开启中间件app.UseAuthentication();
            //综上所述，设置权限，必须要三步走，涉及授权策略 + 配置认证 + 开启授权中间件，只不过自定义的中间件不能验证过期时间，所以我都是用官方的。

            #region 【1/2、简单角色授权】
            #region 1、基于角色的API授权 

            // 1【授权】、这个很简单，其他什么都不用做，
            // 无需配置服务，只需要在API层的controller上边，增加特性即可，注意，只能是角色的:
            // [Authorize(Roles = "Admin")]

            // 2【认证】、然后在下边的configure里，配置中间件即可:app.UseMiddleware<JwtTokenAuth>();但是这个方法，无法验证过期时间，所以如果需要验证过期时间，还是需要下边的第三种方法，官方认证

            #endregion

            #region 2、基于策略的授权（简单版）

            // 1【授权】、这个和上边的异曲同工，好处就是不用在controller中，写多个 roles 。
            // 然后这么写 [Authorize(Policy = "Admin")]
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Client", policy => policy.RequireRole("Client").Build());
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin").Build());
                options.AddPolicy("SystemOrAdmin", policy => policy.RequireRole("Admin", "System"));
            });


            // 2【认证】、然后在下边的configure里，配置中间件即可:app.UseMiddleware<JwtTokenAuth>();但是这个方法，无法验证过期时间，所以如果需要验证过期时间，还是需要下边的第三种方法，官方认证
            #endregion 
            #endregion


            #region 【3、复杂策略授权】

            #region 参数
            //读取配置文件
            var audienceConfig = Configuration.GetSection("Audience");
            var symmetricKeyAsBase64 = audienceConfig["Secret"];
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);


            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            // 如果要数据库动态绑定，这里先留个空，后边处理器里动态赋值
            var permission = new List<PermissionItem>();

            // 角色与接口的权限要求参数
            var permissionRequirement = new PermissionRequirement(
                "/api/denied",// 拒绝授权的跳转地址（目前无用）
                permission,
                ClaimTypes.Role,//基于角色的授权
                audienceConfig["Issuer"],//发行人
                audienceConfig["Audience"],//听众
                signingCredentials,//签名凭据
                expiration: TimeSpan.FromSeconds(2 * 60 * 60)//接口的过期时间
                );
            #endregion

            //【授权】
            services.AddAuthorization(options =>
            {
                options.AddPolicy(PermissionNames.Permission,
                         policy => policy.Requirements.Add(permissionRequirement));
            });


            #endregion


            #region 【统一认证】
            // 令牌验证参数
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = audienceConfig["Issuer"],//发行人
                ValidateAudience = true,
                ValidAudience = audienceConfig["Audience"],//订阅人
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromSeconds(30),
                RequireExpirationTime = true,
            };

            //2.1【认证】、core自带官方JWT认证
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(o =>
             {
                 o.TokenValidationParameters = tokenValidationParameters;
                 o.Events = new JwtBearerEvents
                 {
                     OnAuthenticationFailed = context =>
                     {
                         // 如果过期，则把<是否过期>添加到，返回头信息中
                         if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                         {
                             context.Response.Headers.Add("Token-Expired", "true");
                         }
                         return Task.CompletedTask;
                     }
                 };
             });


            //2.2【认证】、IdentityServer4 认证 (暂时忽略)
            //services.AddAuthentication("Bearer")
            //  .AddIdentityServerAuthentication(options =>
            //  {
            //      options.Authority = "http://localhost:5002";
            //      options.RequireHttpsMetadata = false;
            //      options.ApiName = "vcan_ims.api";
            //  });
            // 注入权限处理器

            services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
            services.AddSingleton(permissionRequirement);
            #endregion

            #endregion


            #region AutoFac DI
            //实例化 AutoFac  容器   
            var builder = new ContainerBuilder();
            //注册要通过反射创建的组件
            //builder.RegisterType<AdvertisementServices>().As<IAdvertisementServices>();
            builder.RegisterType<GlobalCacheAOP>();//可以直接替换其他拦截器
            builder.RegisterType<GlobalRedisCacheAOP>();//可以直接替换其他拦截器
            builder.RegisterType<GlobalLogAOP>();//这样可以注入第二个

            #region 带有接口层的服务注入

            try
            {
                #region Service.dll 注入，有对应接口

                //获取项目绝对路径，请注意，这个是实现类的dll文件，不是接口 IService.dll ，注入容器当然是Activatore
                var servicesDllFile = Path.Combine(basePath, "ZC.Customer.Services.dll");
                var assemblysServices = Assembly.LoadFrom(servicesDllFile);//直接采用加载文件的方法 

                //builder.RegisterAssemblyTypes(assemblysServices).AsImplementedInterfaces();//指定已扫描程序集中的类型注册为提供所有其实现的接口。


                // AOP 开关，如果想要打开指定的功能，只需要在 appsettigns.json 对应对应 true 就行。
                var cacheType = new List<Type>();
                if (Appsettings.app(new string[] { "AppSettings", "RedisCaching", "Enabled" }).ObjToBool())
                {
                    cacheType.Add(typeof(GlobalRedisCacheAOP));
                }
                if (Appsettings.app(new string[] { "AppSettings", "MemoryCachingAOP", "Enabled" }).ObjToBool())
                {
                    cacheType.Add(typeof(GlobalCacheAOP));
                }
                if (Appsettings.app(new string[] { "AppSettings", "LogAOP", "Enabled" }).ObjToBool())
                {
                    cacheType.Add(typeof(GlobalLogAOP));
                }

                builder.RegisterAssemblyTypes(assemblysServices)
                          .AsImplementedInterfaces()
                          .InstancePerLifetimeScope()
                          .EnableInterfaceInterceptors()//引用Autofac.Extras.DynamicProxy;
                                                        // 如果你想注入两个，就这么写  InterceptedBy(typeof(GlobalCacheAOP), typeof(GlobalLogAOP));
                                                        //允许将拦截器服务的列表分配给注册。
                          .InterceptedBy(cacheType.ToArray());
                #endregion

                #region Repository.dll 注入，有对应接口

                var repositoryDllFile = Path.Combine(basePath, "ZC.Customer.Repository.dll");
                var assemblysRepository = Assembly.LoadFrom(repositoryDllFile);
                builder.RegisterAssemblyTypes(assemblysRepository).AsImplementedInterfaces();

                #endregion
            }
            catch (Exception)
            {
                throw new Exception("※※★※※ 如果你是第一次下载项目，请先对整个解决方案dotnet build（F6编译），然后再对api层 dotnet run（F5执行），\n因为解耦了，如果你是发布的模式，请检查bin文件夹是否存在Repository.dll和service.dll ※※★※※");
            }


            #endregion

            //将services填充到Autofac容器生成器中
            builder.Populate(services);

            //使用已进行的组件登记创建新容器
            var ApplicationContainer = builder.Build();

            #endregion


            #region 设置上传文件大小
            services.Configure<FormOptions>(x => {
                x.MultipartBodyLengthLimit = 300_000_000;//不到300M
            });
            #endregion


            return new AutofacServiceProvider(ApplicationContainer);//第三方IOC接管 core内置DI容器

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            #region Environment
            if (env.IsDevelopment())
            {
                // 在开发环境中，使用异常页面，这样可以暴露错误堆栈信息，所以不要放在生产环境。
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // 在非开发环境中，使用HTTP严格安全传输(or HSTS) 对于保护web安全是非常重要的。
                // 强制实施 HTTPS 在 ASP.NET Core，配合 app.UseHttpsRedirection
                //app.UseHsts();

            }
            #endregion


            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                //根据版本名称倒序 遍历展示
                typeof(ApiVersions).GetEnumNames().OrderByDescending(e => e).ToList().ForEach(version =>
                {
                    c.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"{ApiName} {version}");
                });
                // 将swagger首页，设置成我们自定义的页面，记得这个字符串的写法：解决方案名.index.html
                c.IndexStream = () => GetType().GetTypeInfo().Assembly.GetManifestResourceStream("ZC.Customer.index.html");//这里是配合MiniProfiler进行性能监控的，《文章：完美基于AOP的接口性能分析》，如果你不需要，可以暂时先注释掉，不影响大局。
                c.RoutePrefix = ""; //路径配置，设置为空，表示直接在根域名（localhost:8001）访问该文件,注意localhost:8001/swagger是访问不到的，去launchSettings.json把launchUrl去掉
            });
            #endregion

            #region MiniProfiler
            app.UseMiniProfiler();
            #endregion

            #region Authen

            //此授权认证方法已经放弃，请使用下边的官方验证方法。但是如果你还想传User的全局变量，还是可以继续使用中间件，第二种写法//app.UseMiddleware<JwtTokenAuth>(); 
            //app.UseJwtTokenAuth(); 

            //如果你想使用官方认证，必须在上边ConfigureService 中，配置JWT的认证服务 (.AddAuthentication 和 .AddJwtBearer 二者缺一不可)
            app.UseAuthentication();
            #endregion

            #region CORS
            //跨域第二种方法，使用策略，详细策略信息在ConfigureService中
            app.UseCors("LimitRequests");//将 CORS 中间件添加到 web 应用程序管线中, 以允许跨域请求。


            #region 跨域第一种版本
            //跨域第一种版本，请要ConfigureService中配置服务 services.AddCors();
            //    app.UseCors(options => options.WithOrigins("http://localhost:8021").AllowAnyHeader()
            //.AllowAnyMethod());  
            #endregion

            #endregion

            // 跳转https
            // app.UseHttpsRedirection();
            // 使用静态文件
            app.UseStaticFiles();
            // 使用cookie
            app.UseCookiePolicy();
            // 返回错误码
            app.UseStatusCodePages();//把错误码返回前台，比如是404

            AutoMapperUtil.Register("ZC.Customer.Dto");

            app.UseMvc();

            // 用于访问本地资源，上传的文件，文件需放在wwwroot文件夹中，浏览器访问时，不需加上wwwroot
            app.UseStaticFiles();


            app.UseSignalR(routes =>
            {
                //这里要说下，为啥地址要写 /api/xxx 
                //因为我前后端分离了，而且使用的是代理模式，所以如果你不用/api/xxx的这个规则的话，会出现跨域问题，毕竟这个不是我的controller的路由，而且自己定义的路由
                routes.MapHub<ChatHub>("/api2/chatHub");
            });
        }

    }
}
