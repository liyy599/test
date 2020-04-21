using Castle.DynamicProxy;
using Microsoft.AspNetCore.SignalR;
using StackExchange.Profiling;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using UMa.Merak.Infrastructure.Hubs;
using UMa.Merak.Infrastructure.Helper;
using UMa.Merak.Infrastructure.LogHelper;
using UMa.Merak.Infrastructure.HttpContextUser;

namespace ZC.Customer.AOP
{
    /// <summary>
    /// GlobalLogAOP 继承IInterceptor接口
    /// </summary>
    public class GlobalLogAOP : IInterceptor
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IUser _user;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hubContext"></param>
        /// <param name="user"></param>
        public GlobalLogAOP(IHubContext<ChatHub> hubContext, IUser user)
        {
            _hubContext = hubContext;
            _user = user;
        }


        /// <summary>
        /// 实例化IInterceptor唯一方法 
        /// </summary>
        /// <param name="invocation">包含被拦截方法的信息</param>
        public void Intercept(IInvocation invocation)
        {
            //记录被拦截方法信息的日志信息
            var dataIntercept = new StringBuilder();
            dataIntercept.Append($"【请求人信息】：{ _user.CurrentUser().Id},{_user.CurrentUser().Name} \r\n");
            dataIntercept.Append($"【当前执行方法】：{ invocation.Method.Name} \r\n");
           
            var arguments = invocation.Arguments.Select(a =>a==null?"":JsonHelper.Obj2JsonStr(a)).ToArray();
            dataIntercept.Append($"【携带的参数有】： {string.Join(", ", arguments)} \r\n");

            try
            {
                MiniProfiler.Current.Step($"执行Service方法：{invocation.Method.Name}() -> ");
                //在被拦截的方法执行完毕后 继续执行当前方法，注意是被拦截的是异步的
                invocation.Proceed();
            }
            catch (Exception e)
            {
                //执行的 service 中，收录异常
                MiniProfiler.Current.CustomTiming("Errors：", e.Message);
                //执行的 service 中，捕获异常
                dataIntercept.Append($"方法执行中出现异常：{e.Message + e.InnerException}\r\n");
            }

            dataIntercept.Append($"【执行完成结果】：{invocation.ReturnValue}");

            Parallel.For(0, 1, e =>
            {
                LogLock log = new LogLock();
                LogLock.WriteLine("AOPLog", new string[] { dataIntercept.ToString() });
            });

            _hubContext.Clients.All.SendAsync("ReceiveUpdate", LogLock.GetLogData()).Wait();
        }

    }
}
