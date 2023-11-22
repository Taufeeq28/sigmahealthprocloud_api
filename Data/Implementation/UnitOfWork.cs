using Data.Models;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SigmaproIisContext _context;
        public UnitOfWork(SigmaproIisContext context)
        {
            _context = context;
            Cities = new CitiesRepository(_context);
            Counties = new CountiesRepository(_context);
            Countries = new CountriesRepository(_context);
            States = new StatesRepository(_context);
            Facilities = new FacilitiesRepository(_context);
            Juridictions = new JuridictionsRepository(_context);
            Organizations = new OrganizationsRepository(_context);
            Users = new UsersRepository(_context);
            
            Contacts= new ContactsRepository(_context);
            
            lOVTypeMaster=new LovTypeMasterRepository(_context);
            Addresss=new AddressesRepository(_context);
            BusinessConfiguration = new BusinessConfigurationRepository(_context);
        }
        public ICitiesRepository Cities { get; private set; }
        public ICountiesRepository Counties { get; private set; }
        public ICountriesRepository Countries { get; private set; }
        public IStatesRepository States { get; private set; }
        public IFacilitiesRepository Facilities { get; private set; }
        public IJuridictionsRepository Juridictions { get; private set; }
        public IOrganizationsRepository Organizations { get; private set; }
        public IUsersRepository Users { get; private set; }
        
        public IContactsRepository Contacts { get; private set; }
        
        public ILOVTypeMasterRepository lOVTypeMaster { get; private set; }
        public IAddressesRepository Addresss { get; private set; }

        public IBusinessConfigurationRepository BusinessConfiguration {  get; private set; }




        public int Save()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
