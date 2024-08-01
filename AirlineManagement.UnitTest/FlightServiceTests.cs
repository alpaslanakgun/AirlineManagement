using AirlineManagement.Business.Common.MessageConstant;
using AirlineManagement.Business.Contracts;
using AirlineManagement.Business.DTOs.FlightDTOs;
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
    public class FlightServiceTests
    {
        private Mock<IFlightRepository> _flightRepositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IMapper> _mapperMock;
        private IFlightService _flightService;

        [SetUp]
        public void SetUp()
        {
            _flightRepositoryMock = new Mock<IFlightRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _flightService = new FlightService(_flightRepositoryMock.Object, _unitOfWorkMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task GetFlightsAsync_ShouldReturnFlights_WhenFlightsExist()
        {
            var flights = new List<Flight>
            {
                new Flight { FlightNumber = "FL001", DepartureAirport = "JFK", ArrivalAirport = "LHR", DepartureTime = DateTime.Now, ArrivalTime = DateTime.Now.AddHours(7) }
            };
            var flightDtos = new List<FlightDto>
            {
                new FlightDto { FlightNumber = "FL001", DepartureAirport = "JFK", ArrivalAirport = "LHR", DepartureTime = DateTime.Now, ArrivalTime = DateTime.Now.AddHours(7) }
            };

            _flightRepositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<Expression<Func<Flight, bool>>>())).ReturnsAsync(flights);
            _mapperMock.Setup(m => m.Map<IEnumerable<FlightDto>>(It.IsAny<IEnumerable<Flight>>())).Returns(flightDtos);

            var result = await _flightService.GetFlightsAsync();

            Assert.IsInstanceOf<SuccessDataResult<IEnumerable<FlightDto>>>(result);
            Assert.AreEqual(flightDtos, result.Data);
        }

        [Test]
        public async Task GetFlightDetailsAsync_ShouldReturnFlightDetails_WhenFlightExists()
        {
            var flightNumber = "FL001";
            var flight = new Flight { FlightNumber = flightNumber, DepartureAirport = "JFK", ArrivalAirport = "LHR", DepartureTime = DateTime.Now, ArrivalTime = DateTime.Now.AddHours(7) };
            var flightDto = new FlightDto { FlightNumber = flightNumber, DepartureAirport = "JFK", ArrivalAirport = "LHR", DepartureTime = DateTime.Now, ArrivalTime = DateTime.Now.AddHours(7) };

            _flightRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Flight, bool>>>())).ReturnsAsync(flight);
            _mapperMock.Setup(m => m.Map<FlightDto>(It.IsAny<Flight>())).Returns(flightDto);

            var result = await _flightService.GetFlightDetailsAsync(flightNumber);

            Assert.IsInstanceOf<SuccessDataResult<FlightDto>>(result);
            Assert.AreEqual(flightDto, result.Data);
        }

        [Test]
        public async Task CreateFlightAsync_ShouldReturnSuccess_WhenFlightIsCreated()
        {
            var flightCreateDto = new FlightCreateDto { DepartureAirport = "JFK", ArrivalAirport = "LHR", DepartureTime = DateTime.Now, ArrivalTime = DateTime.Now.AddHours(7) };
            var flight = new Flight { FlightNumber = "FL001", DepartureAirport = "JFK", ArrivalAirport = "LHR", DepartureTime = DateTime.Now, ArrivalTime = DateTime.Now.AddHours(7) };
            var flightDto = new FlightDto { FlightNumber = "FL001", DepartureAirport = "JFK", ArrivalAirport = "LHR", DepartureTime = DateTime.Now, ArrivalTime = DateTime.Now.AddHours(7) };

            _mapperMock.Setup(m => m.Map<Flight>(It.IsAny<FlightCreateDto>())).Returns(flight);
            _flightRepositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<Expression<Func<Flight, bool>>>())).ReturnsAsync(new List<Flight> { flight });
            _flightRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Flight>())).Returns(Task.FromResult((Flight)null));
            _unitOfWorkMock.Setup(uow => uow.CommitAsync()).Returns(Task.CompletedTask);
            _mapperMock.Setup(m => m.Map<FlightDto>(It.IsAny<Flight>())).Returns(flightDto);

            var result = await _flightService.CreateFlightAsync(flightCreateDto);

            Assert.IsInstanceOf<SuccessDataResult<FlightDto>>(result);
            Assert.AreEqual(flightDto, result.Data);
        }

        [Test]
        public async Task UpdateFlightAsync_ShouldReturnSuccess_WhenFlightIsUpdated()
        {
            var flightUpdateDto = new FlightUpdateDto { FlightNumber = "FL001", DepartureAirport = "JFK", ArrivalAirport = "LHR", DepartureTime = DateTime.Now, ArrivalTime = DateTime.Now.AddHours(7) };
            var flight = new Flight { FlightNumber = "FL001", DepartureAirport = "JFK", ArrivalAirport = "LHR", DepartureTime = DateTime.Now, ArrivalTime = DateTime.Now.AddHours(7) };
            var flightDto = new FlightDto { FlightNumber = "FL001", DepartureAirport = "JFK", ArrivalAirport = "LHR", DepartureTime = DateTime.Now, ArrivalTime = DateTime.Now.AddHours(7) };

            _flightRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Flight, bool>>>())).ReturnsAsync(flight);
            _mapperMock.Setup(m => m.Map(flightUpdateDto, flight)).Returns(flight);
            _flightRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Flight>())).Returns(Task.FromResult((Flight)null));
            _unitOfWorkMock.Setup(uow => uow.CommitAsync()).Returns(Task.CompletedTask);
            _mapperMock.Setup(m => m.Map<FlightDto>(It.IsAny<Flight>())).Returns(flightDto);

            var result = await _flightService.UpdateFlightAsync(flightUpdateDto);

            Assert.IsInstanceOf<SuccessDataResult<FlightDto>>(result);
            Assert.AreEqual(flightDto, result.Data);
        }

        [Test]
        public async Task DeleteFlightAsync_ShouldReturnSuccess_WhenFlightIsDeleted()
        {
            var flightNumber = "FL001";
            var flight = new Flight { FlightNumber = flightNumber, DepartureAirport = "JFK", ArrivalAirport = "LHR", DepartureTime = DateTime.Now, ArrivalTime = DateTime.Now.AddHours(7) };

            _flightRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Flight, bool>>>())).ReturnsAsync(flight);
            _flightRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Flight>())).Returns(Task.FromResult((Flight)null));
            _unitOfWorkMock.Setup(uow => uow.CommitAsync()).Returns(Task.CompletedTask);

            var result = await _flightService.DeleteFlightAsync(new FlightDeleteDto { FlightNumber = flightNumber });

            Assert.IsInstanceOf<SuccessResult>(result);
        }
    }
}
