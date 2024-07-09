using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateManagement.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        ICandidateRepository CandidateRepository { get; }

        Task CompleteAsync();
    }
}
