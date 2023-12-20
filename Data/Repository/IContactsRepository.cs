using Data.Models;
using Data.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IContactsRepository: IGenericRepository<Contact>
    {
        public Task<List<ContactsModel>> GetContactsbyContactid(string contactid);
    }
}
