using BAL.Constant;
using BAL.Interfaces;
using BAL.Request;
using BAL.Responses;
using BAL.Services;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;


namespace BAL.Tests
{
    [TestFixture]
    public class FacilityServiceTests
    {

        private FacilityService _facilityService;
        private Mock<SigmaproIisContext> _dbContextMock;
        private Mock<SigmaproIisContextUdf> _dbContextUdfMock;
        private Mock<ILogger<FacilityService>> _loggerMock;

        [SetUp]
        public void Setup()
        {
            // Mock DbContext and ILogger
            _dbContextMock = new Mock<SigmaproIisContext>();
            _dbContextUdfMock = new Mock<SigmaproIisContextUdf>();
            _loggerMock = new Mock<ILogger<FacilityService>>();
            _facilityService = new FacilityService(_dbContextMock.Object, _loggerMock.Object, _dbContextUdfMock.Object);
        }

        [Test]
        public async Task FacilitySearch_ValidRequest_ReturnsSuccess()
        {
            // Arrange
            var validRequest = new FacilitySearchRequest
            {
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

            var facilitySearchData = new List<FacilitySearchResponse>
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
        },
       
    };

            var mockedDbSet = new Mock<DbSet<FacilitySearchResponse>>();
            mockedDbSet.As<IQueryable<FacilitySearchResponse>>().Setup(m => m.Provider).Returns(facilitySearchData.AsQueryable().Provider);
            mockedDbSet.As<IQueryable<FacilitySearchResponse>>().Setup(m => m.Expression).Returns(facilitySearchData.AsQueryable().Expression);
            mockedDbSet.As<IQueryable<FacilitySearchResponse>>().Setup(m => m.ElementType).Returns(facilitySearchData.AsQueryable().ElementType);
            mockedDbSet.As<IQueryable<FacilitySearchResponse>>().Setup(m => m.GetEnumerator()).Returns(facilitySearchData.AsQueryable().GetEnumerator());

            _dbContextUdfMock.SetupGet(d => d.FacilitySearch).Returns(mockedDbSet.Object);

            var _facilityServiceMock = new Mock<IFacilityService>();
            _facilityServiceMock.Setup(service => service.FacilitySearch(It.IsAny<FacilitySearchRequest>()))
                .ReturnsAsync(new PaginationModel<FacilitySearchResponse>
                {
                    TotalCount = facilitySearchData.Count,
                    TotalPages = 1,
                    CurrentPage = validRequest.pageNumber,
                    PagingDetails = $"Page {validRequest.pageNumber} of 1",
                    ShowingDetails = $"Showing {validRequest.pageSize} items on page {validRequest.pageNumber}",
                    Items = facilitySearchData
                });

            // Act
            var result = await _facilityServiceMock.Object.FacilitySearch(validRequest);

            // Assert
            Assert.That(result, Is.Not.Null);
            // Add more assertions based on the expected behavior
        }
        [Test]
        public void FacilitySearch_InvalidRequest_ThrowsException()
        {
            var invalidRequest = new FacilitySearchRequest
            {
                pageNumber = -1, 
            };

            var mockedDbSet = new Mock<DbSet<FacilitySearchResponse>>();
            mockedDbSet.As<IQueryable<FacilitySearchResponse>>().Setup(m => m.Provider).Returns(new List<FacilitySearchResponse>().AsQueryable().Provider);
            mockedDbSet.As<IQueryable<FacilitySearchResponse>>().Setup(m => m.Expression).Returns(new List<FacilitySearchResponse>().AsQueryable().Expression);
            mockedDbSet.As<IQueryable<FacilitySearchResponse>>().Setup(m => m.ElementType).Returns(new List<FacilitySearchResponse>().AsQueryable().ElementType);
            mockedDbSet.As<IQueryable<FacilitySearchResponse>>().Setup(m => m.GetEnumerator()).Returns(new List<FacilitySearchResponse>().AsQueryable().GetEnumerator());

            _dbContextUdfMock.Setup(d => d.FacilitySearch).Returns(mockedDbSet.Object);

            // Act and Assert
            var exception = Assert.ThrowsAsync<Exception>(async () => await _facilityService.FacilitySearch(invalidRequest));
            // Assert
            Assert.That(exception.Message, Does.Match("Invalid page number or page size. Both must be greater than zero"));

        }
        
        [Test]
        public async Task DeleteFacility_ValidId_ReturnsSuccess()
        {
            // Arrange
            var facilityId = Guid.NewGuid();
            var facility = new Facility { Id = facilityId, Isdelete = false }; 
            var expectedResult = ApiResponse<string>.Success(null, "Facility deleted successfully.");

            _dbContextMock.Setup(d => d.Facilities.FindAsync(It.IsAny<Guid>())).ReturnsAsync(facility);

            var facilityService = new FacilityService(_dbContextMock.Object, _loggerMock.Object, _dbContextUdfMock.Object);

            // Act
            var result = await facilityService.DeleteFacility(facilityId);

            // Assert
            Assert.AreEqual(expectedResult.Status, result.Status);
            Assert.AreEqual(expectedResult.Message, result.Message);
        }
        [Test]
        public async Task DeleteFacility_InvalidId_ReturnsFailure()
        {
            // Arrange
            var invalidFacilityId = Guid.NewGuid();
            var expectedResult = ApiResponse<string>.Fail("Facility with the given ID not found.");

           
            _dbContextMock.Setup(d => d.Facilities.FindAsync(It.IsAny<Guid>())).ReturnsAsync((Facility)null);

            
            var facilityService = new FacilityService(_dbContextMock.Object, _loggerMock.Object, _dbContextUdfMock.Object);

            // Act
            var result = await facilityService.DeleteFacility(invalidFacilityId);

            // Assert
            Assert.AreEqual(expectedResult.Status, result.Status);
            Assert.AreEqual(expectedResult.Message, result.Message);
            // Add more assertions based on the expected behavior
        }

        [Test]
        public async Task CreateFacility_ValidRequest_ReturnsSuccess()
        {
            // Arrange
            var createFacilityRequest = new CreateFacilityRequest
            {
                FacilityName = "New Facility",
                CreatedBy = "User1",
                UpdatedBy = "User1",
                OrganizationsId = Guid.NewGuid(),
            };

            var mockedDbSet = new Mock<DbSet<Facility>>();
            _dbContextMock.Setup(d => d.Facilities).Returns(mockedDbSet.Object);
            _dbContextMock.Setup(d => d.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var facilityService = new FacilityService(_dbContextMock.Object, _loggerMock.Object, _dbContextUdfMock.Object);

            // Act
            var result = await facilityService.CreateFacility(createFacilityRequest);

            // Assert
            Assert.AreEqual("Success", result.Status);
            Assert.AreEqual("Facility created successfully.", result.Message);
           
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
                OrganizationsId = Guid.NewGuid()
               
            };

            _dbContextMock.Setup(db => db.Facilities.Add(It.IsAny<Facility>())).Throws(new Exception("Simulated database error"));

            // Act
            var result = await _facilityService.CreateFacility(createFacilityRequest);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("Fail", result.Status);
            Assert.AreEqual("An error occurred while creating the facility.", result.Message);
            Assert.Null(result.Data);
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
                OrganizationsId = Guid.Empty
            };

            var expectedResponse = ApiResponse<string>.Fail("Facility with the given ID not found.");

            _dbContextMock.Setup(d => d.Facilities.FindAsync(It.IsAny<Guid>())).ReturnsAsync((Facility)null);

            // Act
            var result = await _facilityService.EditFacility(invalidRequest);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("Fail", result.Status);
            Assert.AreEqual("Facility with the given ID not found.", result.Message);
            Assert.Null(result.Data);
        }

        [Test]
        public async Task EditFacility_ValidRequest_ReturnsSuccessResponse()
        {
            // Arrange
            var validRequest = new EditFacilityRequest
            {
                Id = Guid.NewGuid(), // Existing Id for a valid facility
                FacilityName = "UpdatedFacilityName",
                AdministeredAtLocation = "UpdatedLocation",
                SendingOrganization = "UpdatedSendingOrg",
                ResponsibleOrganization = "UpdatedResponsibleOrg",
                UpdatedBy = "UpdatedUser",
                OrganizationsId = Guid.NewGuid()
                
            };

            var existingFacility = new Facility
            {
                Id =validRequest.Id??Guid.Empty,
                FacilityName = "ExistingFacility",
                AdministeredAtLocation = "ExistingLocation",
                SendingOrganization = "ExistingSendingOrg",
                ResponsibleOrganization = "ExistingResponsibleOrg",
                UpdatedBy = "ExistingUser",
                OrganizationsId = validRequest.OrganizationsId
               
            };

            var expectedResponse = ApiResponse<string>.Success(null, "Facility record updated successfully.");

            _dbContextMock.Setup(d => d.Facilities.FindAsync(It.IsAny<Guid>())).ReturnsAsync(existingFacility);
            _dbContextMock.Setup(d => d.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            // Act
            var result = await _facilityService.EditFacility(validRequest);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("Success", result.Status);
            Assert.AreEqual("Facility record updated successfully.", result.Message);
            Assert.Null(result.Data);

            // Additional assertions based on your logic
            Assert.AreEqual("UpdatedFacilityName", existingFacility.FacilityName);
            Assert.AreEqual("UpdatedLocation", existingFacility.AdministeredAtLocation);
            // Assert other properties are updated accordingly
        }

        [Test]
        public async Task GetFacilityDetailsById_Returns_SuccessfulResponse()
        {
            // Arrange
            var facilityId = Guid.NewGuid(); // Set a valid facility ID
            var existingFacility = new Facility
            {
                Id = facilityId,
                FacilityName = "Sample Facility",
                AdministeredAtLocation = "Location A",
                SendingOrganization = "Org A",
                ResponsibleOrganization = "Org B",
                UpdatedDate = DateTime.UtcNow,
                UpdatedBy = "Admin",
                OrganizationsId = Guid.NewGuid()
            };

            var expectedResponse = ApiResponse<FacilityDetailsResponse>.Success(
                FacilityDetailsResponse.FromFacilityEntity(existingFacility),
                "Facility details fetched successfully."
            );

            _dbContextMock.Setup(d => d.Facilities.FindAsync(It.IsAny<Guid>())).ReturnsAsync(existingFacility);

            // Act
            var result = await _facilityService.GetFacilityDetailsById(facilityId);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("Success", result.Status);
            Assert.AreEqual("Facility details fetched successfully.", result.Message);
            Assert.NotNull(result.Data);

        }

        [Test]
        public async Task GetFacilityDetailsById_Returns_NotFoundResult_When_FacilityNotFound()
        {
            // Arrange
            var facilityId = Guid.NewGuid(); // Set a facility ID that doesn't exist

            var expectedResponse = ApiResponse<FacilityDetailsResponse>.Fail("Facility not found.");

            _dbContextMock.Setup(d => d.Facilities.FindAsync(It.IsAny<Guid>())).ReturnsAsync((Facility)null);

            // Act
            var result = await _facilityService.GetFacilityDetailsById(facilityId);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("Fail", result.Status);
            Assert.AreEqual("Facility not found.", result.Message);
            Assert.Null(result.Data);

        }

        [Test]
        public async Task GetFacilityDetailsById_Returns_ErrorResponse_OnException()
        {
            // Arrange
            var facilityId = Guid.NewGuid(); // Set a valid facility ID

            _dbContextMock.Setup(d => d.Facilities.FindAsync(It.IsAny<Guid>())).ThrowsAsync(new Exception("Simulated error"));

            // Act
            var result = await _facilityService.GetFacilityDetailsById(facilityId);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("Fail", result.Status);
            Assert.AreEqual("An error occurred while fetching facility details.", result.Message);
            Assert.Null(result.Data);

        }




    }
}
