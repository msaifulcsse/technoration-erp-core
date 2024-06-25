using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModels
{
    public class VmEmailAttactment
    {
        public string attachmentFileName { get; set; }
        public byte[] mailStreamAttachment { get; set; }
    }
}
