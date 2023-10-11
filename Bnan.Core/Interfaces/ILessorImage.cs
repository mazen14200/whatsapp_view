using Bnan.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Core.Interfaces
{
    public interface ILessorImage
    {
        Task<bool> AddLessorImage(string LessorCode);
    }
}