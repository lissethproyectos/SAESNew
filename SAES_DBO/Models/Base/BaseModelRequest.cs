using SAES_DBA;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAES_DBO.Models
{
    public class BaseModelRequest 
    {
        public BaseModelRequest()
        {
        }        
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class SPParameterName : Attribute
    {
        private string name;
        private int index;
        public SPParameterName(string name, int index)
        {
            this.name = name;
            this.index = index;
        }
    }
}
