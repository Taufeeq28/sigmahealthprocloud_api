using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        ICitiesRepository Cities { get; }
        ICountiesRepository Counties { get; }
        ICountriesRepository Countries { get; }
        IFacilitiesRepository Facilities { get; }
        IOrganizationsRepository Organizations { get; }
        IJuridictionsRepository Juridictions { get; }
        IStatesRepository States { get; }
        IUsersRepository Users { get; }
        IUserTypesRepository UserTypes { get; }

        int Save();

    }
}
