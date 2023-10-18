using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Inferastructure.Repository
{
    public class DocumentsMaintainanceCar : IDocumentsMaintainanceCar
    {
        private IUnitOfWork _unitOfWork;

        public DocumentsMaintainanceCar(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddDocumentCar(string serialNumber, string lessorCode, string branchCode, int currentMeter)
        {
            var lessorMechanisms = _unitOfWork.CrCasLessorMechanism.FindAll(l => l.CrCasLessorMechanismCode == lessorCode && l.CrCasLessorMechanismProceduresClassification == "12");
            try
            {
                if (lessorMechanisms != null)
                {
                    foreach (var item in lessorMechanisms)
                    {
                        CrCasCarDocumentsMaintenance crCasCarDocumentsMaintenance = new CrCasCarDocumentsMaintenance
                        {
                            CrCasCarDocumentsMaintenanceSerailNo = serialNumber,
                            CrCasCarDocumentsMaintenanceProcedures = item.CrCasLessorMechanismProcedures,
                            CrCasCarDocumentsMaintenanceProceduresClassification = item.CrCasLessorMechanismProceduresClassification,
                            CrCasCarDocumentsMaintenanceIsActivation = item.CrCasLessorMechanismActivate,
                            CrCasCarDocumentsMaintenanceLessor = item.CrCasLessorMechanismCode,
                            CrCasCarDocumentsMaintenanceBranch = branchCode,
                            CrCasCarDocumentsMaintenanceCurrentMeter = currentMeter,
                            CrCasCarDocumentsMaintenanceStatus = "N",
                        };

                        await _unitOfWork.CrCasCarDocumentsMaintenance.AddAsync(crCasCarDocumentsMaintenance);
                    }
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> AddMaintainaceCar(string serialNumber,string lessorCode,string branchCode, int currentMeter)
        {
            var lessorMechanisms = _unitOfWork.CrCasLessorMechanism.FindAll(l => l.CrCasLessorMechanismCode == lessorCode && l.CrCasLessorMechanismProceduresClassification == "13");
            try
            {
                if (lessorMechanisms != null)
                {
                    foreach (var item in lessorMechanisms)
                    {
                        CrCasCarDocumentsMaintenance crCasCarDocumentsMaintenance = new CrCasCarDocumentsMaintenance
                        {
                            CrCasCarDocumentsMaintenanceSerailNo = serialNumber,
                            CrCasCarDocumentsMaintenanceProcedures = item.CrCasLessorMechanismProcedures,
                            CrCasCarDocumentsMaintenanceProceduresClassification = item.CrCasLessorMechanismProceduresClassification,
                            CrCasCarDocumentsMaintenanceIsActivation = item.CrCasLessorMechanismActivate,
                            CrCasCarDocumentsMaintenanceLessor = item.CrCasLessorMechanismCode,
                            CrCasCarDocumentsMaintenanceBranch = branchCode,
                            CrCasCarDocumentsMaintenanceCurrentMeter =currentMeter,
                            CrCasCarDocumentsMaintenanceStatus = "N",
                        };

                        await _unitOfWork.CrCasCarDocumentsMaintenance.AddAsync(crCasCarDocumentsMaintenance);
                    }
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
