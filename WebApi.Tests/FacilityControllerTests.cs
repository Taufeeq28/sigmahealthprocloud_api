using BAL.Constant;
using BAL.Interfaces;
using BAL.Request;
using BAL.Responses;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Web_API.Controllers;

namespace WebApi.Tests
{
    [TestFixture]
    public class FacilityControllerTests
    {
        private Mock<IFacilityService> _facilityServiceMock;
        private FacilityController _facilityController;

        [SetUp]
        public void Setup()
        {
            _facilityServiceMock = new Mock<IFacilityService>();
            _facilityController = new FacilityController(_facilityServiceMock.Object);
        }
        [Test]
        public async Task FacilitySearch_ValidRequest_ReturnsOk()
        {
            // Arrange
            var validRequest = new FacilitySearchRequest
            {
                identifier = "123",
                pageNumber = 1,
                pageSize = 10,
                sortBy = "facilityname",
                sortDirection = "desc",
                searchFacilityName = "Facility1",
                searchJurisdiction = "Jurisdiction1",
                searchOrganization = "Organization1",
                searchAddress = "Address1",
                searchCity = "City1",
                searchState = "State1",
                searchZipCode = "ZipCode1"
            };

            var expectedResult = new PaginationModel<FacilitySearchResponse>
            {
                TotalCount = 1,
                TotalPages = 1,
                CurrentPage = 1,
                Items = new List<FacilitySearchResponse>
            {
                new FacilitySearchResponse
                {
                    jurisdiction = "Jurisdiction1",
                    organization = "Organization1",
                    facilityName = "Facility1",
                    address = "Address1",
                    city = "City1",
                    state = "State1",
                    zipCode = "ZipCode1",
                    TotalRows = 10
                }
            }
            };

            _facilityServiceMock.Setup(service => service.FacilitySearch(It.IsAny<FacilitySearchRequest>()))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _facilityController.FacilitySearch(validRequest) as ObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200)); 
                                                             
        }
    


        [Test]
        public void FacilitySearch_InvalidRequest_ReturnsBadRequest()
        {
            // Arrange
            var invalidRequest = new FacilitySearchRequest
            {
                pageNumber = -1,
            };

            _facilityServiceMock.Setup(service => service.FacilitySearch(It.IsAny<FacilitySearchRequest>()))
                .ThrowsAsync(new Exception("Invalid page number or page size. Both must be greater than zero"));

            // Act and Assert
            var exception = Assert.ThrowsAsync<Exception>(async () => await _facilityController.FacilitySearch(invalidRequest));

            // Assert
            Assert.That(exception.Message, Does.Contain("Invalid page number or page size. Both must be greater than zero"));
        }

        [Test]
        public async Task DeleteFacility_ValidRequest_ReturnsSuccessResponse()
        {
            // Arrange
            var facilityId = Guid.NewGuid();
            var expectedResponse = ApiResponse<string>.Success(null, "Facility deleted successfully.");

            _facilityServiceMock.Setup(service => service.DeleteFacility(It.IsAny<Guid>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _facilityController.DeleteFacility(facilityId) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            // Additional assertions based on the expected behavior
            var apiResponse = result.Value as ApiResponse<string>;
            Assert.NotNull(apiResponse);
            Assert.AreEqual("Success", apiResponse.Status);
            Assert.AreEqual("Facility deleted successfully.", apiResponse.Message);
            Assert.Null(apiResponse.Data);
        }

        [Test]
        public async Task DeleteFacility_FailedRequest_ReturnsFailResponse()
        {
            // Arrange
            var facilityId=Guid.Empty;
            var expectedResponse = ApiResponse<string>.Fail("Facility with the given ID not found.");

            _facilityServiceMock.Setup(service => service.DeleteFacility(It.IsAny<Guid>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _facilityController.DeleteFacility(facilityId) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            
            var apiResponse = result.Value as ApiResponse<string>;
            Assert.NotNull(apiResponse);
            Assert.AreEqual("Fail", apiResponse.Status);
            Assert.AreEqual("Facility with the given ID not found.", apiResponse.Message);
            Assert.Null(apiResponse.Data);
        }

        [Test]
        public async Task CreateFacility_ValidRequest_ReturnsSuccessResponse()
        {
            // Arrange
            var createFacilityRequest = new CreateFacilityRequest
            {
                FacilityName = "New Facility",
                CreatedBy = "User1",
                UpdatedBy = "User1",
                OrganizationsId = Guid.NewGuid(),
                AddressId = Guid.NewGuid()
            };

            var expectedApiResponse = ApiResponse<string>.Success(null, "Facility created successfully.");

            _facilityServiceMock.Setup(service => service.CreateFacility(It.IsAny<CreateFacilityRequest>()))
                .ReturnsAsync(expectedApiResponse);

            // Act
            var result = await _facilityController.CreateFacility(createFacilityRequest) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            var apiResponse = result.Value as ApiResponse<string>;
            Assert.NotNull(apiResponse);
            Assert.AreEqual("Success", apiResponse.Status);
            Assert.AreEqual("Facility created successfully.", apiResponse.Message);
            Assert.Null(apiResponse.Data);
        }


        [Test]
        public async Task CreateFacility_FailureScenario_ReturnsFailResponse()
        {
            // Arrange
            var createFacilityRequest = new CreateFacilityRequest
            {
                FacilityName = "New Facility",
                CreatedBy = "User1",
                UpdatedBy = "User1",
                OrganizationsId = Guid.NewGuid(),
                AddressId = Guid.NewGuid()
            };

            var expectedApiResponse = ApiResponse<string>.Fail("An error occurred while creating the facility.");

            _facilityServiceMock.Setup(service => service.CreateFacility(It.IsAny<CreateFacilityRequest>()))
                .ReturnsAsync(expectedApiResponse);

            // Act
            var result = await _facilityController.CreateFacility(createFacilityRequest) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);

          
            var apiResponse = result.Value as ApiResponse<string>;
            Assert.NotNull(apiResponse);
            Assert.AreEqual("Fail", apiResponse.Status);
            Assert.AreEqual("An error occurred while creating the facility.", apiResponse.Message);
            Assert.Null(apiResponse.Data);
        }
        [Test]
        public async Task EditFacility_ValidRequest_ReturnsSuccessResponse()
        {
            // Arrange
            var editFacilityRequest = new EditFacilityRequest
            {
                Id = Guid.NewGuid(),
                FacilityName = "Updated Facility",
                AdministeredAtLocation = "Location1",
                SendingOrganization = "Org1",
                ResponsibleOrganization = "ResOrg1",
                UpdatedBy = "User1",
                OrganizationsId = Guid.NewGuid(),
                AddressId = Guid.NewGuid()
            };

            var expectedResponse = ApiResponse<string>.Success(null, "Facility record updated successfully.");

            _facilityServiceMock.Setup(service => service.EditFacility(It.IsAny<EditFacilityRequest>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _facilityController.EditFacility(editFacilityRequest) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            var apiResponse = result.Value as ApiResponse<string>;
            Assert.NotNull(apiResponse);
            Assert.AreEqual("Success", apiResponse.Status);
            Assert.AreEqual("Facility record updated successfully.", apiResponse.Message);
            Assert.Null(apiResponse.Data);
        }
        [Test]
        public async Task EditFacility_InvalidRequest_ReturnsFailResponse()
        {
            // Arrange
            var invalidRequest = new EditFacilityRequest
            {
                Id = Guid.Empty,
                FacilityName = "Updated Facility",
                AdministeredAtLocation = "Location1",
                SendingOrganization = "Org1",
                ResponsibleOrganization = "ResOrg1",
                UpdatedBy = "User1",
                OrganizationsId = Guid.Empty,
                AddressId = Guid.Empty
            };

            var expectedResponse = ApiResponse<string>.Fail("Invalid input. EditFacilityRequest object is null.");

            _facilityServiceMock.Setup(service => service.EditFacility(It.IsAny<EditFacilityRequest>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _facilityController.EditFacility(invalidRequest) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            // Additional assertions based on the expected behavior
            var apiResponse = result.Value as ApiResponse<string>;
            Assert.NotNull(apiResponse);
            Assert.AreEqual("Fail", apiResponse.Status);
            Assert.AreEqual("Invalid input. EditFacilityRequest object is null.", apiResponse.Message);
            Assert.Null(apiResponse.Data);
        }

        [Test]
        public async Task GetFacilityDetailsById_ReturnsOkResult_With_ValidData()
        {
            // Arrange
            var facilityId = Guid.NewGuid();
            var expectedFacilityDetails = new FacilityDetailsResponse
            {
                FacilityName = "Sample Facility",
                AdministeredAtLocation = "Location A",
                SendingOrganization = "Org A",
                ResponsibleOrganization = "Org B",
                UpdatedDate = DateTime.UtcNow,
                UpdatedBy = "Admin",
                OrganizationsId = Guid.NewGuid(),
                AddressId = Guid.NewGuid()
            };

            _facilityServiceMock
                .Setup(service => service.GetFacilityDetailsById(facilityId))
                .ReturnsAsync(ApiResponse<FacilityDetailsResponse>.Success(expectedFacilityDetails, "Facility details fetched successfully"));

            // Act
            var result = await _facilityController.GetFacilityDetailsById(facilityId) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            var apiResponse = result.Value as ApiResponse<FacilityDetailsResponse>;
            Assert.NotNull(apiResponse);
            Assert.AreEqual("Success", apiResponse.Status);
            Assert.AreEqual("Facility details fetched successfully", apiResponse.Message);

            var facilityDetails = apiResponse.Data;
            Assert.NotNull(facilityDetails);
            Assert.AreEqual("Sample Facility", facilityDetails.FacilityName);
        }

    

        [Test]
        public async Task GetFacilityDetailsById_Returns_NotFoundResult_When_FacilityNotFound()
        {
            // Arrange
            var facilityId = Guid.NewGuid(); // Set a facility ID that doesn't exist

            var expectedResponse = ApiResponse<FacilityDetailsResponse>.Fail("Facility not found.");

            _facilityServiceMock.Setup(service => service.GetFacilityDetailsById(It.IsAny<Guid>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _facilityController.GetFacilityDetailsById(facilityId) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode); // Expecting a 404 NotFound status code

            var apiResponse = result.Value as ApiResponse<FacilityDetailsResponse>;
            Assert.NotNull(apiResponse);
            Assert.AreEqual("Fail", apiResponse.Status);
            Assert.AreEqual("Facility not found.", apiResponse.Message);
            Assert.Null(apiResponse.Data);

            // Additional assertions based on your logic
        }


    }
}
