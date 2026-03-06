using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAES_DBO.Models
{
    public class BaseModelResponse
    {
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class SPName : System.Attribute
    {
        private string name;
        public SPName(string name)
        {
            this.name = name;
        }
    }
    [AttributeUsage(AttributeTargets.Class)]
    public class QueryString : System.Attribute
    {
        private string Query;
        public QueryString(string Query)
        {
            this.Query = Query;
        }
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class SPResponseColumnName : System.Attribute
    {
        private string name;
        public SPResponseColumnName(string name)
        {
            this.name = name;
        }
    }
    public class SPResponseCursorColumnName : System.Attribute
    {
        private string name;
        public SPResponseCursorColumnName(string name)
        {
            this.name = name;
        }
    }
}
