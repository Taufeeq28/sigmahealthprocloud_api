using BAL.RequestModels;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Repository
{
    public interface IContactsRepository : IGenericRepository<Contact>
    {
        public Task<List<ContactsModel>> GetContactsbyContactid(Guid contactid);
    }
}
