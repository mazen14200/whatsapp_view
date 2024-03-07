using Bnan.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Core.Interfaces
{
    public interface IUserLoginsService
    {
        Task SaveTracing(string userCode, string operationAr, string operationEn, string mainTaskCode, string subTaskCode,
            string mainTaskAr, string subTaskAr, string mainTaskEn, string subTaskEn, string systemCode, string systemAr, string systemEn);

        Task SaveTracing(string userCode, string RecordAr, string RecordEn, string operationAr, string operationEn, string mainTaskCode, string subTaskCode,
            string mainTaskAr, string subTaskAr, string mainTaskEn, string subTaskEn, string systemCode, string systemAr, string systemEn);
    }
}
