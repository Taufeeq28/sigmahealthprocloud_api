using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data;
using BAL.Repository;
using System.Threading.Tasks;
using Data.Models;
using BAL.Implementation;
using Microsoft.Extensions.Logging;
using BAL.RequestModels;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace BAL.Tests
{
    public class HelperDataModel
    {
        private SigmaproIisContext context;
        private ILogger<UnitOfWork> _logger;
       public static Guid Juridictionid = new Guid("cb0ae15f-6d32-4962-8927-53726d3778a7");
        public HelperDataModel(SigmaproIisContext _context, ILogger<UnitOfWork> logger)
        {
            context = _context;
            _logger = logger;
        }
        public static async Task<IEnumerable<Organization>> CreateOrganizationData()
        {
            
            // Create and return test data for your model
            // You can customize this based on your specific model and test requirements
            return new List<Organization>
            {
                new Organization{ Id = Guid.NewGuid(),JuridictionId = Juridictionid,OrganizationName = "TestOrganization1",OrganizationsId= "Org001",AddressId = Guid.NewGuid(), },
                new Organization{ Id = Guid.NewGuid(),JuridictionId = Guid.NewGuid(),OrganizationName = "TestOrganization2",OrganizationsId= "Org002",AddressId = Guid.NewGuid() },
                new Organization{ Id = Guid.NewGuid(),JuridictionId = Guid.NewGuid(),OrganizationName = "TestOrganization3",OrganizationsId= "Org003",AddressId = Guid.NewGuid() },
                new Organization{ Id = Guid.NewGuid(),JuridictionId = Guid.NewGuid(),OrganizationName = "TestOrganization4",OrganizationsId= "Org004",AddressId = Guid.NewGuid() },
                new Organization{ Id = Guid.NewGuid(),JuridictionId = Guid.NewGuid(),OrganizationName = "TestOrganization5",OrganizationsId= "Org005",AddressId = Guid.NewGuid() },
            }.AsEnumerable();
        }
        public static async Task<IEnumerable<OrganizationModel>> GetOrganizationModelData()
        {
            return new List<OrganizationModel>
            {
                new OrganizationModel{ Id = Guid.NewGuid(),JuridictionId = Guid.NewGuid(),OrganizationName = "TestOrganization1",OrganizationsId= "Org001",AddressId = Guid.NewGuid(), },
                new OrganizationModel{ Id = Guid.NewGuid(),JuridictionId = Guid.NewGuid(),OrganizationName = "TestOrganization2",OrganizationsId= "Org002",AddressId = Guid.NewGuid() },
                new OrganizationModel{ Id = Guid.NewGuid(),JuridictionId = Guid.NewGuid(),OrganizationName = "TestOrganization3",OrganizationsId= "Org003",AddressId = Guid.NewGuid() },
                new OrganizationModel{ Id = Guid.NewGuid(),JuridictionId = Guid.NewGuid(),OrganizationName = "TestOrganization4",OrganizationsId= "Org004",AddressId = Guid.NewGuid() },
                new OrganizationModel{ Id = Guid.NewGuid(),JuridictionId = Guid.NewGuid(),OrganizationName = "TestOrganization5",OrganizationsId= "Org005",AddressId = Guid.NewGuid() }
            };
        }
        public static OrganizationModel Data()
        {
            return new OrganizationModel()
            {
                Id = Guid.NewGuid(),
                OrganizationsId = "Organization1",
                OrganizationName = "TestOrg1",
                CreatedBy = "Ramya",
                UpdatedBy = "Ramya",
                JuridictionId = Guid.NewGuid(),
                AddressId = Guid.NewGuid()
            };
        }

    }

}
