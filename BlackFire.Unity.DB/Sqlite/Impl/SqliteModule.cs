/*
--------------------------------------------------
| Copyright © 2008 Mr-Alan. All rights reserved. |
| Website: www.0x69h.com                         |
| Mail: mr.alan.china@gmail.com                  |
| QQ: 835988221                                  |
--------------------------------------------------
*/

using System;
using System.Collections.Generic;
using System.IO;

namespace BlackFire.Unity.DB
{
    /// <summary>
    /// Sqlite模块。
    /// </summary>
    public sealed class SqliteModule:ModuleBase,ISqliteModule
    {
        private Dictionary<string, IConnection> m_ConnectionDictionary = new Dictionary<string, IConnection>();

        /// <summary>
        /// 连接类工厂回调。
        /// </summary>
        public Func<string,string,IConnection> ConnectionFactory { get; set; }

        /// <summary>
        /// 连接或创建数据库并返回数据服务
        /// </summary>
        /// <param name="databaseName">数据库相对于StreamingAssets的路径</param>
        /// <returns>Connection</returns>
        public IConnection CreateConnection(string connectionAlias,string databasePath)
        {
            if (string.IsNullOrEmpty(connectionAlias))
            {
                throw new Exception(string.Format("参数'connectionAlias'不能为空或者Null"));
            }

            if (!m_ConnectionDictionary.ContainsKey(databasePath))
            {
                var connectionIpml = null != ConnectionFactory ? ConnectionFactory.Invoke(connectionAlias,databasePath) : null;
                if (null!=connectionIpml)
                {
                    m_ConnectionDictionary.Add(databasePath,connectionIpml);
                }
            }
            
            return m_ConnectionDictionary[databasePath];
        }

        /// <summary>
        /// 获取数据库连接服务类。
        /// </summary>
        /// <param name="connectionAlias">服务别名。</param>
        /// <returns>数据库连接服务类实例。</returns>
        public IConnection AcquireConnection(string connectionAlias)
        {
            foreach (var item in m_ConnectionDictionary.Values)
            {
                if (item.Alias==connectionAlias)
                {                   
                    return item;
                }
            }
            return null;
        }

        /// <summary>
        /// 关闭服务。
        /// </summary>
        /// <param name="connectionAlias">服务别名。</param>
        public void CloseConnection(string connectionAlias)
        {
            if (m_ConnectionDictionary.ContainsKey(connectionAlias))
            {
                m_ConnectionDictionary[connectionAlias].Close();
            }
        }

        protected override void OnDie()
        {
            base.OnDie();
            foreach (var item in m_ConnectionDictionary.Values)
            {
                item.Close();
            }   
        }

    }

}
