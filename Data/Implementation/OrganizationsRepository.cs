﻿using Data;
using Data.Models;
using Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Implementation
{
    public class OrganizationsRepository : IGenericRepository<Organization>, IOrganizationsRepository
    {
        private SigmaproIisContext context;
        public OrganizationsRepository(SigmaproIisContext _context) 
        {
            this.context = _context;
        }

        public void Add(Organization entity)
        {
            context.Organizations.Add(entity);
        }

        public void AddRange(IEnumerable<Organization> entities)
        {
            context.Organizations.AddRange(entities);
        }

        public IEnumerable<Organization> Find(Expression<Func<Organization, bool>> predicate)
        {
            return context.Organizations.Where(predicate);
        }

        public IEnumerable<Organization> GetAll()
        {
            return context.Organizations.ToList();
        }

        public Organization? GetById(int id)
        {
            return (Organization?)context.Organizations.Find(id);
        }
        public IEnumerable<Organization> GetOrganizationByJuridictionId(string jurdid)
        {
            var orgmodel = context.Organizations.Where(o => o.JuridictionId.ToString().Equals(jurdid));
            if (orgmodel != null)
            {
                return orgmodel;
            }
            return null;
        }

        public void Remove(Organization entity)
        {
            context.Remove(entity);
        }

        public void RemoveRange(IEnumerable<Organization> entities)
        {
            context.RemoveRange(entities);
        }
    }
}
