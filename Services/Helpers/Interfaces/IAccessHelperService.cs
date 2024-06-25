using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Helpers.Interfaces
{
    public interface IAccessHelperService
    {
        bool HasAccess();
        Task SeedCompanyInfoAsync();
        bool HasSubscription();
    }
}
