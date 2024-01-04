using BAL.Implementation;
using BAL.Interfaces;
using BAL.Repository;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_API.Controllers;
using Data.Models;
using Microsoft.Extensions.Configuration;
using BAL.RequestModels;
using Microsoft.EntityFrameworkCore;
using BAL.Tests;
using Microsoft.AspNetCore.Mvc;
using BAL.Constant;
using BAL.Request;



namespace WebApi.Tests
{
    [TestFixture]
    public class OrganizationControllerTest
    {
        
        private Mock<IConfiguration> _config;
        private Mock<ILogger<OrganizationController>> _loggermock;
        private Mock<IUnitOfWork> _unitOfWork;      
        private OrganizationController _organizationController;
        [SetUp]
        public void Setup()
        {
            _config = new Mock<IConfiguration>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _loggermock = new Mock<ILogger<OrganizationController>>();
            _organizationController = new OrganizationController(_unitOfWork.Object,_config.Object,_loggermock.Object);

        }

        [Test]
        public async Task Getallorganizations()
        {

            var organizationrepomock = new Mock<OrganizationController>();
            var testdata = HelperDataModel.GetOrganizationModelData();
            _unitOfWork.Setup(i => i.Organizations.GetAllAsync()).ReturnsAsync(new List<OrganizationModel>
            {
                new OrganizationModel{ Id = Guid.NewGuid(),JuridictionId = Guid.NewGuid(),OrganizationName = "TestOrganization1",OrganizationsId= "Org001",AddressId = Guid.NewGuid(), },
                new OrganizationModel{ Id = Guid.NewGuid(),JuridictionId = Guid.NewGuid(),OrganizationName = "TestOrganization2",OrganizationsId= "Org002",AddressId = Guid.NewGuid() },
                new OrganizationModel{ Id = Guid.NewGuid(),JuridictionId = Guid.NewGuid(),OrganizationName = "TestOrganization3",OrganizationsId= "Org003",AddressId = Guid.NewGuid() },
                new OrganizationModel{ Id = Guid.NewGuid(),JuridictionId = Guid.NewGuid(),OrganizationName = "TestOrganization4",OrganizationsId= "Org004",AddressId = Guid.NewGuid() },
                new OrganizationModel{ Id = Guid.NewGuid(),JuridictionId = Guid.NewGuid(),OrganizationName = "TestOrganization5",OrganizationsId= "Org005",AddressId = Guid.NewGuid() }
            });
            var result = await _organizationController.GetAllOrganizations() as ObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.That(result.Value,Is.Not.Null);


        }
        [Test]
        public async Task GetallorganizationsbyJuridictionid()
        {

            var organizationrepomock = new Mock<OrganizationController>();
            var testdata = HelperDataModel.CreateOrganizationData().Result;
            var data = testdata.Where(s => s.JuridictionId.Equals(HelperDataModel.Juridictionid)).ToList();
            var orgList = new List<OrganizationModel>();
            foreach (var item in data)
            {
                orgList.Add(new OrganizationModel() { JuridictionId = item.JuridictionId, OrganizationName = item.OrganizationName });
            }
            var expResult = new ApiResponse<List<OrganizationModel>>() { Data = orgList, Status = "200", Message = "Success" };
            _unitOfWork.Setup(i => i.Organizations.GetOrganizationByJuridictionId(HelperDataModel.Juridictionid)).ReturnsAsync(expResult);
            var result = await _organizationController.GetOrganizationByJurdiction(HelperDataModel.Juridictionid) as ObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.That(result.Value, Is.Not.Null);
            Assert.AreEqual(result.Value, expResult);

        }
        [Test]
        public async Task GetOrganizations_byJuridictionId_Returns_FailureResponse()
        {
            var juridictionid = Guid.NewGuid(); // Set a facility ID that doesn't exist

            var expectedResponse = ApiResponse<List<OrganizationModel>>.Fail("Organization not found.");
            _unitOfWork.Setup(i => i.Organizations.GetOrganizationByJuridictionId(HelperDataModel.Juridictionid)).ReturnsAsync(expectedResponse);
            var result= await _organizationController.GetOrganizationByJurdiction(HelperDataModel.Juridictionid) as ObjectResult;
            Assert.NotNull(result);
            Assert.That(result.Value, Is.Not.Null);
            Assert.AreEqual(result.Value, expectedResponse);
            
        }
        [Test]
        public async Task InsertOrganization_Successfullresponse()
        {
            var organizationrepomock = new Mock<OrganizationController>();
            var orgmod = HelperDataModel.Data();
            var expectedApiResponse = ApiResponse<string>.Success(null, "Organization created successfully.");

            _unitOfWork.Setup(i => i.Organizations.InsertAsync(orgmod)).ReturnsAsync(expectedApiResponse);

            var result = await _organizationController.CreateOrganization(orgmod) as ObjectResult;
            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            var apiResponse = result.Value as ApiResponse<string>;
            Assert.NotNull(apiResponse);
            Assert.AreEqual("Success", apiResponse.Status);
            Assert.AreEqual("Organization created successfully.", apiResponse.Message);
            Assert.Null(apiResponse.Data);

        }
        [Test]
        public async Task InsertOrganization_FailureScenario_ReturnsFailResponse()
        {
            var orgmod = HelperDataModel.Data();
            var expectedApiResponse = ApiResponse<string>.Fail("An error occurred while creating the organization.");

            _unitOfWork.Setup(i => i.Organizations.InsertAsync(orgmod)).ReturnsAsync(expectedApiResponse);

            // Act
            var result = await _organizationController.CreateOrganization(orgmod) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            var apiResponse = result.Value as ApiResponse<string>;
            Assert.NotNull(apiResponse);
            Assert.AreEqual("Failure", apiResponse.Status);
            Assert.AreEqual("An error occurred while creating the organization.", apiResponse.Message);
            Assert.Null(apiResponse.Data);
        }
        [Test]
        public async Task UpdateOrganization_SuccessfullResponse()
        {
            var orgmod = HelperDataModel.Data();
            
            var expectedResponse = ApiResponse<string>.Success(null, "Organization record updated successfully.");

            _unitOfWork.Setup(i => i.Organizations.UpdateAsync(orgmod)).ReturnsAsync(expectedResponse);

            // Act
            var result = await _organizationController.EditOrganization(orgmod) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            var apiResponse = result.Value as ApiResponse<string>;
            Assert.NotNull(apiResponse);
            Assert.AreEqual("Success", apiResponse.Status);
            Assert.AreEqual("Organization record updated successfully.", apiResponse.Message);
            Assert.Null(apiResponse.Data);
        }
        [Test]
        public async Task UpdateOrganization_InvalidRequest_ReturnsFailResponse()
        {
            var invaliddata = HelperDataModel.Data();
            var expectedResponse = ApiResponse<string>.Fail("Invalid input. Organization object is null.");

            _unitOfWork.Setup(i => i.Organizations.UpdateAsync(invaliddata)).ReturnsAsync(expectedResponse);

            // Act
            var result = await _organizationController.EditOrganization(invaliddata) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            // Additional assertions based on the expected behavior
            var apiResponse = result.Value as ApiResponse<string>;
            Assert.NotNull(apiResponse);
            Assert.AreEqual("Failure", apiResponse.Status);
            Assert.AreEqual("Invalid input. Organization object is null.", apiResponse.Message);
            Assert.Null(apiResponse.Data);
        }
        [Test]
        public async Task DeleteOrganization_SuccessfullResponse()
        {
           
            var organizationid = Guid.NewGuid();
            var expectedResponse = ApiResponse<string>.Success(null, "Organization deleted successfully.");

            _unitOfWork.Setup(i => i.Organizations.DeleteAsync(organizationid)).ReturnsAsync(expectedResponse);

            // Act
            var result = await _organizationController.DeleteOrganization(organizationid) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            // Additional assertions based on the expected behavior
            var apiResponse = result.Value as ApiResponse<string>;
            Assert.NotNull(apiResponse);
            Assert.AreEqual("Success", apiResponse.Status);
            Assert.AreEqual("Organization deleted successfully.", apiResponse.Message);
            Assert.Null(apiResponse.Data);
        }
        [Test]
        public async Task DeleteOrganization_InvalidId_ReturnsFailure()
        {
            var organizationid = Guid.Empty;
            var expectedResponse = ApiResponse<string>.Fail("Organization with the given ID not found.");

            _unitOfWork.Setup(i => i.Organizations.DeleteAsync(organizationid)).ReturnsAsync(expectedResponse);

            // Act
            var result = await _organizationController.DeleteOrganization(organizationid) as ObjectResult;

            // Assert
            Assert.NotNull(result);

            var apiResponse = result.Value as ApiResponse<string>;
            Assert.NotNull(apiResponse);
            Assert.AreEqual("Failure", apiResponse.Status);
            Assert.AreEqual("Organization with the given ID not found.", apiResponse.Message);
            Assert.Null(apiResponse.Data);
        }
    }
}
