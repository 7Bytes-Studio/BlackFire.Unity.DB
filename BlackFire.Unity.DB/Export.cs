/*
--------------------------------------------------
| Copyright © 2008 Mr-Alan. All rights reserved. |
| Website: www.0x69h.com                         |
| Mail: mr.alan.china@gmail.com                  |
| QQ: 835988221                                  |
--------------------------------------------------
*/


using System.Diagnostics;

namespace BlackFire.Unity.DB
{
    /// <summary>
    /// BlackFireFramework.DB 的导出类。
    /// </summary>
    public sealed class Export : ExportedAssemblyBase
    {
        /// <summary>
        /// 导出接口事件(该事件会被BlackFire核心程序集反射执行)。
        /// </summary>
        /// <param name="ioc">BlackFireFramework内部的IOC容器。</param>
        protected override void OnExport(ISparrowIoC ioc)
        {
            ioc.RegisterType<SqliteModule>().As<ISqliteModule>();
#if DEVELOP
            Log.Info("ExportedAssembly::OnExport");
#endif

        }



        #region 程序集生命周期



        protected override void OnBorn()
        {
#if DEVELOP
          Log.Info("ExportedAssembly::OnBorn");
#endif
        }

        protected override void OnAct()
        {
#if DEVELOP
          Log.Info("ExportedAssembly::OnAct");
#endif
        }

        protected override void OnDie()
        {
#if DEVELOP
          Log.Info("ExportedAssembly::OnDie");
#endif
        }



        #endregion

    }
}
