using Azzuha.DataViewModels.Common;
using Azzuha.DataViewModels.Response.AdminResponse;
using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.Services.Interface.admin
{
    public interface IAdminListingService
    {

        GenralReportListRequest<ContactUsResponseModel> GetConatctusReports(GenralReportListRequest<ContactUsResponseModel> page, DateTime? str, DateTime? endd);
        GenralReportListRequest<AdminResponseViewModel> GetAdmintionReports(GenralReportListRequest<AdminResponseViewModel> page, DateTime? str, DateTime? endd);

    }
}
