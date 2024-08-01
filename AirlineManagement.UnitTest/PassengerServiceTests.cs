using AirlineManagement.Business.Common.MessageConstant;
using AirlineManagement.Business.Contracts;
using AirlineManagement.Business.DTOs.PassengerDTOs;
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
    public class PassengerServiceTests
    {
        private Mock<IPassengerRepository> _passengerRepositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IMapper> _mapperMock;
        private IPassengerService _passengerService;

        [SetUp]
        public void SetUp()
        {
            _passengerRepositoryMock = new Mock<IPassengerRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _passengerService = new PassengerService(_passengerRepositoryMock.Object, _unitOfWorkMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task GetPassengersAsync_ShouldReturnPassengers_WhenPassengersExist()
        {
            var passengers = new List<Passenger>
            {
                new Passenger { PassengerId = "P001", Name = "Alice Smith" }
            };
            var passengerDtos = new List<PassengerDto>
            {
                new PassengerDto { PassengerId = "P001", Name = "Alice Smith" }
            };

            _passengerRepositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<Expression<Func<Passenger, bool>>>())).ReturnsAsync(passengers);
            _mapperMock.Setup(m => m.Map<IEnumerable<PassengerDto>>(It.IsAny<IEnumerable<Passenger>>())).Returns(passengerDtos);

            var result = await _passengerService.GetPassengersAsync();

            Assert.IsInstanceOf<SuccessDataResult<IEnumerable<PassengerDto>>>(result);
            Assert.AreEqual(passengerDtos, result.Data);
        }

        [Test]
        public async Task GetPassengerDetailsAsync_ShouldReturnPassengerDetails_WhenPassengerExists()
        {
            var passengerId = "P001";
            var passenger = new Passenger { PassengerId = passengerId, Name = "Alice Smith" };
            var passengerDto = new PassengerDto { PassengerId = passengerId, Name = "Alice Smith" };

            _passengerRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Passenger, bool>>>())).ReturnsAsync(passenger);
            _mapperMock.Setup(m => m.Map<PassengerDto>(It.IsAny<Passenger>())).Returns(passengerDto);

            var result = await _passengerService.GetPassengerDetailsAsync(passengerId);

            Assert.IsInstanceOf<SuccessDataResult<PassengerDto>>(result);
            Assert.AreEqual(passengerDto, result.Data);
        }

        [Test]
        public async Task CreatePassengerAsync_ShouldReturnSuccess_WhenPassengerIsCreated()
        {
            var passengerCreateDto = new PassengerCreateDto { Name = "Alice Smith" };
            var passenger = new Passenger { PassengerId = "P001", Name = "Alice Smith" };
            var passengerDto = new PassengerDto { PassengerId = "P001", Name = "Alice Smith" };

            _mapperMock.Setup(m => m.Map<Passenger>(It.IsAny<PassengerCreateDto>())).Returns(passenger);
            _passengerRepositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<Expression<Func<Passenger, bool>>>())).ReturnsAsync(new List<Passenger> { passenger });
            _passengerRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Passenger>())).Returns(Task.FromResult((Passenger)null));
            _unitOfWorkMock.Setup(uow => uow.CommitAsync()).Returns(Task.CompletedTask);
            _mapperMock.Setup(m => m.Map<PassengerDto>(It.IsAny<Passenger>())).Returns(passengerDto);

            var result = await _passengerService.CreatePassengerAsync(passengerCreateDto);

            Assert.IsInstanceOf<SuccessDataResult<PassengerDto>>(result);
            Assert.AreEqual(passengerDto, result.Data);
        }

        [Test]
        public async Task UpdatePassengerAsync_ShouldReturnSuccess_WhenPassengerIsUpdated()
        {
            var passengerUpdateDto = new PassengerUpdateDto { PassengerId = "P001", Name = "Alice Smith" };
            var passenger = new Passenger { PassengerId = "P001", Name = "Alice Smith" };
            var passengerDto = new PassengerDto { PassengerId = "P001", Name = "Alice Smith" };

            _passengerRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Passenger, bool>>>())).ReturnsAsync(passenger);
            _mapperMock.Setup(m => m.Map(passengerUpdateDto, passenger)).Returns(passenger);
            _passengerRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Passenger>())).Returns(Task.FromResult((Passenger)null));
            _unitOfWorkMock.Setup(uow => uow.CommitAsync()).Returns(Task.CompletedTask);
            _mapperMock.Setup(m => m.Map<PassengerDto>(It.IsAny<Passenger>())).Returns(passengerDto);

            var result = await _passengerService.UpdatePassengerAsync(passengerUpdateDto);

            Assert.IsInstanceOf<SuccessDataResult<PassengerDto>>(result);
            Assert.AreEqual(passengerDto, result.Data);
        }

        [Test]
        public async Task DeletePassengerAsync_ShouldReturnSuccess_WhenPassengerIsDeleted()
        {
            var passengerId = "P001";
            var passenger = new Passenger { PassengerId = passengerId, Name = "Alice Smith" };

            _passengerRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Passenger, bool>>>())).ReturnsAsync(passenger);
            _passengerRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Passenger>())).Returns(Task.FromResult((Passenger)null));
            _unitOfWorkMock.Setup(uow => uow.CommitAsync()).Returns(Task.CompletedTask);

            var result = await _passengerService.DeletePassengerAsync(new PassengerDeleteDto { PassengerId = passengerId });

            Assert.IsInstanceOf<SuccessResult>(result);
        }
    }
}
