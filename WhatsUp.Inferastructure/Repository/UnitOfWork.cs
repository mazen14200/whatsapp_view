using WhatsUp.Core.Interfaces;
using WhatsUp.Core.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bnan.Core.Models;

namespace WhatsUp.Inferastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WhatsUpContext _context;

        public IGenric<Company> Company { get; private set; }

        public IGenric<Connect> Connect { get; private set; }

        public IGenric<Renter> Renter { get; private set; }
        public IGenric<Message> Message { get; private set; }
        public IGenric<CrCasAccountReceipt> CrCasAccountReceipt { get; private set; }
        public IGenric<CrMasSupPostRegion> CrMasSupPostRegion { get; private set; }
        public IGenric<CrMasSupPostRegion_x> CrMasSupPostRegion_x { get; private set; }
        
        //public IGenric<AccountWhatsUp> AccountWhatsUp { get; private set; }

        public UnitOfWork(WhatsUpContext context)
        {
            _context = context;
            Company = new BaseRepository<Company>(_context);
            Connect = new BaseRepository<Connect>(_context);
            Renter = new BaseRepository<Renter>(_context);
            Message = new BaseRepository<Message>(_context);
            CrCasAccountReceipt = new BaseRepository<CrCasAccountReceipt>(_context);
            CrMasSupPostRegion = new BaseRepository<CrMasSupPostRegion>(_context);
            CrMasSupPostRegion_x = new BaseRepository<CrMasSupPostRegion_x>(_context);

            //AccountWhatsUp = new BaseRepository<AccountWhatsUp>(_context);

        }
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
