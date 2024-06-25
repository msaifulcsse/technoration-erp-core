using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Helpers.Interfaces;

namespace Web.Helpers
{
    public class DisplayMessageHelper : IDisplayMessageHelper
    {
        #region "Declared objects & Constructor => Get Errors"
        //public Controller controller;
        //public DisplayMessageHelper()
        //{
        //    this.controller = new Controller();
        //}
        #endregion

        public string SuccessMessageSetOrGet(Controller controller, bool isSet, string message = "")
        {
            if (isSet == true)
                controller.TempData["InfoMessage"] = message;
            else
                return controller.TempData["InfoMessage"] != null ? controller.TempData["InfoMessage"].ToString() : null;

            return null;
        }

        public string ErrorMessageSetOrGet(Controller controller, bool isSet, string message = "")
        {
            if (isSet == true)
                controller.TempData["ErrorMessage"] = message;
            else
                return controller.TempData["ErrorMessage"] != null ? controller.TempData["ErrorMessage"].ToString() : null;

            return null;
        }
    }
}
