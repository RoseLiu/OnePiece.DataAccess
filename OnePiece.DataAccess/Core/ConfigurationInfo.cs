using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.DataAccess.Core
{
    using System.Configuration;

    /// <summary>
    /// 数据库配置信息
    /// </summary>
    internal class ConfigurationInfo
    {
        public static readonly string ConnectionStringQuery
            = ConfigurationManager.ConnectionStrings["ConnectionStringQuery"].ConnectionString;

        public static readonly string ConnectionStringCommand
            = ConfigurationManager.ConnectionStrings["ConnectionStringCommand"].ConnectionString;
    }


}
