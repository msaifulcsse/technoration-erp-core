using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Helpers.Interfaces
{
    public interface IDisplayMessageHelper
    {
        string SuccessMessageSetOrGet(Controller controller, bool isSet, string message = "");
        string ErrorMessageSetOrGet(Controller controller, bool isSet, string message = "");
    }
}
