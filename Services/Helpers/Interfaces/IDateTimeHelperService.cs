using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Services.Helpers.Interfaces
{
    public interface IDateTimeHelperService
    {
        string ConvertBDDateStringToDateTimeObject(string dateStringBDFormat);
        string ConvertUSDateStringToDateTimeObject(string dateStringUKFormat);
        DateTime Now();
        DateTime Today();
        DateTime FirstDayOfCurrentMonth();
        DateTime LastDayOfCurrentMonth();
        int CurrentMonthNoOfDays();
        int GetNoOfDaysInMonthForGivenDate(DateTime date);
    }
}
