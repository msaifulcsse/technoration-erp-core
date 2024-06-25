using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModels
{
    public class VmDTResponse
    {
        public int Draw { get; set; }
        public long RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
        public object[] Data { get; set; }
        public string Error { get; set; }
    }
}
