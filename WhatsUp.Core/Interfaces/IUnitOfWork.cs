using WhatsUp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bnan.Core.Models;

namespace WhatsUp.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IGenric<Company> Company { get; }
        public IGenric<Connect> Connect { get; }
        public IGenric<Renter> Renter { get; }
        public IGenric<Message> Message { get; }
        public IGenric<CrCasAccountReceipt> CrCasAccountReceipt { get; }
        public IGenric<CrMasSupPostRegion> CrMasSupPostRegion { get; }
        public IGenric<CrMasSupPostRegion_x> CrMasSupPostRegion_x { get; }
        //public IGenric<AccountWhatsUp> AccountWhatsUp { get; }


        int Complete();
        Task<int> CompleteAsync();
    }
}
