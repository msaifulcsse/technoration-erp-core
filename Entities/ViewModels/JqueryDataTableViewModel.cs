using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModels
{
    public class JqueryDataTableViewModel
    {
        public int draw { get; set; }
        public int length { get; set; }
        public int start { get; set; }
        public string sSearch { get; set; }
    }
}
