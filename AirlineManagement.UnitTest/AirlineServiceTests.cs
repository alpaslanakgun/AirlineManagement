using AirlineManagement.Business.Common.MessageConstant;
using AirlineManagement.Business.Contracts;
using AirlineManagement.Business.DTOs.AirlineDTOs;
using AirlineManagement.Business.Services;
using AirlineManagement.Data.Contracts;
using AirlineManagement.Domain.Entities;
using AirlineManagement.Domain.Results.ComplexType;
using AutoMapper;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AirlineManagement.UnitTest
{
    [TestFixture]
    public class AirlineServiceTests
    {
        private Mock<IAirlineRepository> _airlineRepositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IMapper> _mapperMock;
        private IAirlineService _airlineService;

        [SetUp]
        public void SetUp()
        {
            _airlineRepositoryMock = new Mock<IAirlineRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _airlineService = new AirlineService(_airlineRepositoryMock.Object, _unitOfWorkMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task GetAirlinesAsync_ShouldReturnAirlines_WhenAirlinesExist()
        {
            var airlines = new List<Airline>
            {
                new Airline { AirlineId = "AL001", Name = "Global Air", Country = "USA" }
            };
            var airlineDtos = new List<AirlineDto>
            {
                new AirlineDto { AirlineId = "AL001", Name = "Global Air", Country = "USA" }
            };

            _airlineRepositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<Expression<Func<Airline, bool>>>())).ReturnsAsync(airlines);
            _mapperMock.Setup(m => m.Map<IEnumerable<AirlineDto>>(It.IsAny<IEnumerable<Airline>>())).Returns(airlineDtos);

            var result = await _airlineService.GetAirlinesAsync();

            Assert.IsInstanceOf<SuccessDataResult<IEnumerable<AirlineDto>>>(result);
            Assert.AreEqual(airlineDtos, result.Data);
        }

        [Test]
        public async Task GetAirlineDetailsAsync_ShouldReturnAirlineDetails_WhenAirlineExists()
        {
            var airlineId = "AL001";
            var airline = new Airline { AirlineId = airlineId, Name = "Global Air", Country = "USA" };
            var airlineDto = new AirlineDto { AirlineId = airlineId, Name = "Global Air", Country = "USA" };

            _airlineRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Airline, bool>>>())).ReturnsAsync(airline);
            _mapperMock.Setup(m => m.Map<AirlineDto>(It.IsAny<Airline>())).Returns(airlineDto);

            var result = await _airlineService.GetAirlineDetailsAsync(airlineId);

            Assert.IsInstanceOf<SuccessDataResult<AirlineDto>>(result);
            Assert.AreEqual(airlineDto, result.Data);
        }

        [Test]
        public async Task CreateAirlineAsync_ShouldReturnSuccess_WhenAirlineIsCreated()
        {
            var airlineCreateDto = new AirlineCreateDto { Name = "Global Air", Country = "USA" };
            var airline = new Airline { AirlineId = "AL001", Name = "Global Air", Country = "USA" };
            var airlineDto = new AirlineDto { AirlineId = "AL001", Name = "Global Air", Country = "USA" };

            _mapperMock.Setup(m => m.Map<Airline>(It.IsAny<AirlineCreateDto>())).Returns(airline);
            _airlineRepositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<Expression<Func<Airline, bool>>>())).ReturnsAsync(new List<Airline> { airline });
            _airlineRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Airline>())).Returns(Task.FromResult((Airline)null));
            _unitOfWorkMock.Setup(uow => uow.CommitAsync()).Returns(Task.CompletedTask);
            _mapperMock.Setup(m => m.Map<AirlineDto>(It.IsAny<Airline>())).Returns(airlineDto);

            var result = await _airlineService.CreateAirlineAsync(airlineCreateDto);

            Assert.IsInstanceOf<SuccessDataResult<AirlineDto>>(result);
            Assert.AreEqual(airlineDto, result.Data);
        }

        [Test]
        public async Task UpdateAirlineAsync_ShouldReturnSuccess_WhenAirlineIsUpdated()
        {
            var airlineUpdateDto = new AirlineUpdateDto { AirlineId = "AL001", Name = "Global Air", Country = "USA" };
            var airline = new Airline { AirlineId = "AL001", Name = "Global Air", Country = "USA" };
            var airlineDto = new AirlineDto { AirlineId = "AL001", Name = "Global Air", Country = "USA" };

            _airlineRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Airline, bool>>>())).ReturnsAsync(airline);
            _mapperMock.Setup(m => m.Map(airlineUpdateDto, airline)).Returns(airline);
            _airlineRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Airline>())).Returns(Task.FromResult((Airline)null));
            _unitOfWorkMock.Setup(uow => uow.CommitAsync()).Returns(Task.CompletedTask);
            _mapperMock.Setup(m => m.Map<AirlineDto>(It.IsAny<Airline>())).Returns(airlineDto);

            var result = await _airlineService.UpdateAirlineAsync(airlineUpdateDto);

            Assert.IsInstanceOf<SuccessDataResult<AirlineDto>>(result);
            Assert.AreEqual(airlineDto, result.Data);
        }

        [Test]
        public async Task DeleteAirlineAsync_ShouldReturnSuccess_WhenAirlineIsDeleted()
        {
            var airlineId = "AL001";
            var airline = new Airline { AirlineId = airlineId, Name = "Global Air", Country = "USA" };

            _airlineRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Airline, bool>>>())).ReturnsAsync(airline);
            _airlineRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Airline>())).Returns(Task.FromResult((Airline)null));
            _unitOfWorkMock.Setup(uow => uow.CommitAsync()).Returns(Task.CompletedTask);

            var result = await _airlineService.DeleteAirlineAsync(new AirlineDeleteDto { AirlineId = airlineId });

            Assert.IsInstanceOf<SuccessResult>(result);
        }
    }
}
