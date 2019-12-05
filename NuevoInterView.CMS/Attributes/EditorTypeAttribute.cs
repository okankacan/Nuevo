using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NuevoInterView.CMS.Attributes
{
    public class EditorTypeAttribute : Attribute
    {
        public string Name { get; set; }
        public string Param { get; set; }

        public EditorTypeAttribute(string Name, string Param = null)
        {
            this.Name = Name;
            this.Param = Param;
        }
    }

}
