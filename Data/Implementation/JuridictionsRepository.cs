using Data.Models;
using Data.Repository;
using System.Linq;
using System.Linq.Expressions;


namespace Data.Implementation
{
    public class JuridictionsRepository : IGenericRepository<Juridiction>, IJuridictionsRepository
    {
        private SigmaproIisContext context;
        public JuridictionsRepository(SigmaproIisContext _context) 
        {
            this.context = _context;
        }

        public void Add(Juridiction entity)
        {
            context.Juridictions.Add(entity);
        }


        public void AddRange(IEnumerable<Juridiction> entities)
        {
            context.Juridictions.AddRange(entities);
        }

        public IEnumerable<Juridiction> Find(Expression<Func<Juridiction, bool>> predicate)
        {
            return context.Juridictions.Where(predicate);
        }

        public IEnumerable<Juridiction> GetAll()
        {
            return context.Juridictions.ToList();
        }

        public Juridiction? GetById(int id)
        {
            return (Juridiction?)context.Juridictions.Find(id);
        }

        public IEnumerable<Juridiction> GetJuridictionsbyBusinessid(string businessid)
        {
            var jurdictionmodel = context.Juridictions.Where(j => j.AlternateId.ToString().Equals(businessid));
            if (jurdictionmodel != null)
            {
                return jurdictionmodel;
            }
            return null;
        }

        public void Remove(Juridiction entity)
        {
            context.Remove(entity);
        }

        public void RemoveRange(IEnumerable<Juridiction> entities)
        {
            context.RemoveRange(entities);
        }
    }
}
