using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using BAL.Implementation;
using BAL.Repository;
using BAL.RequestModels;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework.Interfaces;
using SQLitePCL;
using BAL.Constant;
using Microsoft.AspNetCore.Mvc.Razor;
using BAL.Responses;

namespace BAL.Tests
{
    public class OrganizationRepositoryTest
    {
        private OrganizationsRepository _organizationsrepo;
        private Mock<SigmaproIisContext> _contextmock;
        private Mock<ILogger<UnitOfWork>> _loggermock;
        private Mock<IUnitOfWork> _unitOfWorkmock;
        [SetUp]
        public void Setup()
        {
            _contextmock=new Mock<SigmaproIisContext>();
            _loggermock=new Mock<ILogger<UnitOfWork>>();
            _organizationsrepo = new OrganizationsRepository(_contextmock.Object, _loggermock.Object);
            _unitOfWorkmock = new Mock<IUnitOfWork>();
        }
        public void GenerateService(IQueryable<Organization> documenten)
        {
            var mockDbSet = new Mock<DbSet<Organization>>();
            mockDbSet.As<IQueryable<Organization>>().Setup(x => x.Provider).Returns(documenten.Provider);
            mockDbSet.As<IQueryable<Organization>>().Setup(x => x.Expression).Returns(documenten.Expression);
            mockDbSet.As<IQueryable<Organization>>().Setup(x => x.ElementType).Returns(documenten.ElementType);
            mockDbSet.As<IQueryable<Organization>>().Setup(x => x.GetEnumerator()).Returns(documenten.GetEnumerator());

            var mockContext = new Mock<OrganizationsRepository>(_contextmock);
            _contextmock.Setup(x=>x.Organizations).Returns(mockDbSet.Object);

           
        }
        [Test]
        public async Task GetAllData_Organizations()
        {
            var organizationrepomock = new Mock<IOrganizationsRepository>();
            var testdata = HelperDataModel.CreateOrganizationData().Result;
            var mockDbSet = new Mock<DbSet<Organization>>();
            mockDbSet.As<IQueryable<Organization>>().Setup(x => x.Provider).Returns(testdata.AsQueryable().Provider);
            mockDbSet.As<IQueryable<Organization>>().Setup(x => x.Expression).Returns(testdata.AsQueryable().Expression);
            mockDbSet.As<IQueryable<Organization>>().Setup(x => x.ElementType).Returns(testdata.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<Organization>>().Setup(x => x.GetEnumerator()).Returns(testdata.AsQueryable().GetEnumerator());
           
            _unitOfWorkmock.Setup(i=>i.Organizations.GetAllAsync()).ReturnsAsync(new List<OrganizationModel>
            {
                new OrganizationModel{ Id = Guid.NewGuid(),JuridictionId = Guid.NewGuid(),OrganizationName = "TestOrganization1",OrganizationsId= "Org001",AddressId = Guid.NewGuid(), },
                new OrganizationModel{ Id = Guid.NewGuid(),JuridictionId = Guid.NewGuid(),OrganizationName = "TestOrganization2",OrganizationsId= "Org002",AddressId = Guid.NewGuid() },
                new OrganizationModel{ Id = Guid.NewGuid(),JuridictionId = Guid.NewGuid(),OrganizationName = "TestOrganization3",OrganizationsId= "Org003",AddressId = Guid.NewGuid() },
                new OrganizationModel{ Id = Guid.NewGuid(),JuridictionId = Guid.NewGuid(),OrganizationName = "TestOrganization4",OrganizationsId= "Org004",AddressId = Guid.NewGuid() },
                new OrganizationModel{ Id = Guid.NewGuid(),JuridictionId = Guid.NewGuid(),OrganizationName = "TestOrganization5",OrganizationsId= "Org005",AddressId = Guid.NewGuid() }
            });
            var result=await organizationrepomock.Object.GetAllAsync();
                Assert.That(result, Is.Not.Null);
                               
            
        }
        [Test]
        public async Task GetOrganizations_byJuridictionId_Returns_SuccessfulResponse()
        {
            var organizationrepomock = new Mock<IOrganizationsRepository>();
            var testdata = HelperDataModel.CreateOrganizationData().Result;
            var data = testdata.Where(s => s.JuridictionId.Equals(HelperDataModel.Juridictionid)).ToList();
            var orgList=new List<OrganizationModel>();
            foreach(var item in data)
            {
                orgList.Add(new OrganizationModel() { JuridictionId = item.JuridictionId, OrganizationName = item.OrganizationName });
            }
            var expResult = new ApiResponse<List<OrganizationModel>>() { Data = orgList,Status="200",Message="Success" };
            var mockDbSet = new Mock<DbSet<Organization>>();
            mockDbSet.As<IQueryable<Organization>>().Setup(x => x.Provider).Returns(testdata.AsQueryable().Provider);
            mockDbSet.As<IQueryable<Organization>>().Setup(x => x.Expression).Returns(testdata.AsQueryable().Expression);
            mockDbSet.As<IQueryable<Organization>>().Setup(x => x.ElementType).Returns(testdata.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<Organization>>().Setup(x => x.GetEnumerator()).Returns(testdata.AsQueryable().GetEnumerator());
            _contextmock.SetupGet(x => x.Organizations).Returns(mockDbSet.Object);
            organizationrepomock.Setup(i => i.GetOrganizationByJuridictionId(HelperDataModel.Juridictionid)).ReturnsAsync(expResult);
            var result = organizationrepomock.Object.GetOrganizationByJuridictionId(HelperDataModel.Juridictionid);
            Assert.That(result.Result,Is.EqualTo(expResult));
        }
        [Test]
        public async Task GetOrganizations_byJuridictionId_Returns_FailureResponse()
        {
            var organizationrepomock = new Mock<IOrganizationsRepository>();
            var juridiction = Guid.NewGuid(); // Set a facility ID that doesn't exist

            var expectedResponse = ApiResponse<string>.Fail("Organization not found.");

            _contextmock.Setup(d => d.Organizations.FindAsync(It.IsAny<Guid>())).ReturnsAsync((Organization)null);

            var result = organizationrepomock.Object.GetOrganizationByJuridictionId(juridiction);

            Assert.NotNull(result);
            Assert.IsNull(result.Result);
        }

        [Test]
        public async Task InsertOrganization_SuccessfullResponse()
        {
            var organizationrepomock = new Mock<IOrganizationsRepository>();
            var orgmod = HelperDataModel.Data();
            var mockedDbSet = new Mock<DbSet<Organization>>();
            _contextmock.Setup(d => d.Organizations).Returns(mockedDbSet.Object);
            _contextmock.Setup(d => d.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
            var result = await _organizationsrepo.InsertAsync(orgmod);
            Assert.That(result, Is.Not.Null);
            Assert.AreEqual("Success", result.Status);
            Assert.AreEqual("Organization created successfully.", result.Message);
           
        }

        [Test]
        public async Task InsertOrganization_FailureScenario_ReturnsFailResponse()
        {
            var organizationrepomock = new Mock<IOrganizationsRepository>();
            var orgmod = HelperDataModel.Data();
            _contextmock.Setup(db => db.Organizations.Add(It.IsAny<Organization>())).Throws(new Exception("Simulated database error"));
            var result = await _organizationsrepo.InsertAsync(orgmod);
            Assert.NotNull(result);
            Assert.AreEqual("Failure", result.Status);
            Assert.AreEqual("An error occurred while creating the organization.", result.Message);
            Assert.Null(result.Data);

        }

        [Test]
        public async Task UpdateOrganization_SuccessfullResponse()
        {
            var organizationrepomock = new Mock<IOrganizationsRepository>();
            var orgmod = HelperDataModel.Data();
            var updateorganization = new Organization()
            {
                Id = orgmod.Id,
                OrganizationsId = "Organization1",
                OrganizationName = "TestOrg1",
                CreatedBy = "Ramya",
                UpdatedBy = "Ramya",
                JuridictionId = Guid.NewGuid(),
                AddressId = Guid.NewGuid()
            };
            var mockedDbSet = new Mock<DbSet<Organization>>();
            var expectedResponse = ApiResponse<string>.Success(null, "Organization record updated successfully.");

            _contextmock.Setup(d => d.Organizations.FindAsync(It.IsAny<Guid>())).ReturnsAsync(updateorganization);
            _contextmock.Setup(d => d.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
            var result = await _organizationsrepo.UpdateAsync(orgmod);
            Assert.That(result, Is.Not.Null);
            Assert.AreEqual("Success", result.Status);
            Assert.AreEqual("Organization record updated successfully.", result.Message);

        }
        [Test]
        public async Task UpdateOrganization_InvalidRequest_ReturnsFailResponse()
        {
            var invaliddata = HelperDataModel.Data();
            var expectedResponse = ApiResponse<string>.Fail("Organization with the given ID not found.");

            _contextmock.Setup(d => d.Organizations.FindAsync(It.IsAny<Guid>())).ReturnsAsync((Organization)null);

            var result = await _organizationsrepo.UpdateAsync(invaliddata);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("Failure", result.Status);
            Assert.AreEqual("Organization with the given ID not found.", result.Message);
            Assert.Null(result.Data);
        }
        [Test]
        public async Task DeleteOrganization_SuccessfullResponse()
        {
            var organizationId = Guid.NewGuid();
            var organization = new Organization { Id = organizationId, Isdelete = false };
            var expectedResult = ApiResponse<string>.Success(null, "Organization deleted successfully.");
            _contextmock.Setup(d => d.Organizations.FindAsync(It.IsAny<Guid>())).ReturnsAsync(organization);
            var result = await _organizationsrepo.DeleteAsync(organizationId);
            Assert.That(result, Is.Not.Null);
            Assert.AreEqual(expectedResult.Status, result.Status);
            Assert.AreEqual("Organization deleted successfully.", result.Message);

        }

        [Test]
        public async Task DeleteOrganization_InvalidId_ReturnsFailure()
        {
            var invalidorganizationId = Guid.NewGuid();            
            var expectedResult = ApiResponse<string>.Fail("Organization with the given ID not found.");
            _contextmock.Setup(d => d.Organizations.FindAsync(It.IsAny<Guid>())).ReturnsAsync((Organization)null);
            var result = await _organizationsrepo.DeleteAsync(invalidorganizationId);
            Assert.That(result, Is.Not.Null);
            Assert.AreEqual(expectedResult.Status, result.Status);
            Assert.AreEqual(expectedResult.Message, result.Message);

        }

    }
    }
