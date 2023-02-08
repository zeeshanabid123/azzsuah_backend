using Azzuha.DataViewModels.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Azzuha.Services.Interface.v1
{
  public  interface IAdmissionService
    {

       Task<bool> SaveAmission(AdmissionRequest request);
    }
}
