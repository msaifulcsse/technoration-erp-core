using System; 

namespace Entities.ViewModels
{
    //ErrorViewer
    public class VMErrorViewer
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId); 
    }
}
