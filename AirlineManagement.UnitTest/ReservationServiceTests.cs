using AirlineManagement.Business.Common.MessageConstant;
using AirlineManagement.Business.Contracts;
using AirlineManagement.Business.DTOs.ReservationDTOs;
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
    public class ReservationServiceTests
    {
        private Mock<IReservationRepository> _reservationRepositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IMapper> _mapperMock;
        private IReservationService _reservationService;

        [SetUp]
        public void SetUp()
        {
            _reservationRepositoryMock = new Mock<IReservationRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _reservationService = new ReservationService(_reservationRepositoryMock.Object, _unitOfWorkMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task GetReservationsAsync_ShouldReturnReservations_WhenReservationsExist()
        {
            var reservations = new List<Reservation>
            {
                new Reservation { ReservationId = "R001", PassengerId = "P001", FlightNumber = "FL001", Seat = "12A" }
            };
            var reservationDtos = new List<ReservationDto>
            {
                new ReservationDto { ReservationId = "R001", PassengerId = "P001", FlightNumber = "FL001", Seat = "12A" }
            };

            _reservationRepositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<Expression<Func<Reservation, bool>>>())).ReturnsAsync(reservations);
            _mapperMock.Setup(m => m.Map<IEnumerable<ReservationDto>>(It.IsAny<IEnumerable<Reservation>>())).Returns(reservationDtos);

            var result = await _reservationService.GetReservationsAsync();

            Assert.IsInstanceOf<SuccessDataResult<IEnumerable<ReservationDto>>>(result);
            Assert.AreEqual(reservationDtos, result.Data);
        }

        [Test]
        public async Task GetReservationDetailsAsync_ShouldReturnReservationDetails_WhenReservationExists()
        {
            var reservationId = "R001";
            var reservation = new Reservation { ReservationId = reservationId, PassengerId = "P001", FlightNumber = "FL001", Seat = "12A" };
            var reservationDto = new ReservationDto { ReservationId = reservationId, PassengerId = "P001", FlightNumber = "FL001", Seat = "12A" };

            _reservationRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Reservation, bool>>>())).ReturnsAsync(reservation);
            _mapperMock.Setup(m => m.Map<ReservationDto>(It.IsAny<Reservation>())).Returns(reservationDto);

            var result = await _reservationService.GetReservationDetailsAsync(reservationId);

            Assert.IsInstanceOf<SuccessDataResult<ReservationDto>>(result);
            Assert.AreEqual(reservationDto, result.Data);
        }

        [Test]
        public async Task CreateReservationAsync_ShouldReturnSuccess_WhenReservationIsCreated()
        {
            var reservationCreateDto = new ReservationCreateDto { PassengerId = "P001", FlightNumber = "FL001", Seat = "12A" };
            var reservation = new Reservation { ReservationId = "R001", PassengerId = "P001", FlightNumber = "FL001", Seat = "12A" };
            var reservationDto = new ReservationDto { ReservationId = "R001", PassengerId = "P001", FlightNumber = "FL001", Seat = "12A" };

            _mapperMock.Setup(m => m.Map<Reservation>(It.IsAny<ReservationCreateDto>())).Returns(reservation);
            _reservationRepositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<Expression<Func<Reservation, bool>>>())).ReturnsAsync(new List<Reservation> { reservation });
            _reservationRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Reservation>())).Returns(Task.FromResult((Reservation)null));
            _unitOfWorkMock.Setup(uow => uow.CommitAsync()).Returns(Task.CompletedTask);
            _mapperMock.Setup(m => m.Map<ReservationDto>(It.IsAny<Reservation>())).Returns(reservationDto);

            var result = await _reservationService.CreateReservationAsync(reservationCreateDto);

            Assert.IsInstanceOf<SuccessDataResult<ReservationDto>>(result);
            Assert.AreEqual(reservationDto, result.Data);
        }

        [Test]
        public async Task UpdateReservationAsync_ShouldReturnSuccess_WhenReservationIsUpdated()
        {
            var reservationUpdateDto = new ReservationUpdateDto { ReservationId = "R001", PassengerId = "P001", FlightNumber = "FL001", Seat = "12A" };
            var reservation = new Reservation { ReservationId = "R001", PassengerId = "P001", FlightNumber = "FL001", Seat = "12A" };
            var reservationDto = new ReservationDto { ReservationId = "R001", PassengerId = "P001", FlightNumber = "FL001", Seat = "12A" };

            _reservationRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Reservation, bool>>>())).ReturnsAsync(reservation);
            _mapperMock.Setup(m => m.Map(reservationUpdateDto, reservation)).Returns(reservation);
            _reservationRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Reservation>())).Returns(Task.FromResult((Reservation)null));
            _unitOfWorkMock.Setup(uow => uow.CommitAsync()).Returns(Task.CompletedTask);
            _mapperMock.Setup(m => m.Map<ReservationDto>(It.IsAny<Reservation>())).Returns(reservationDto);

            var result = await _reservationService.UpdateReservationAsync(reservationUpdateDto);

            Assert.IsInstanceOf<SuccessDataResult<ReservationDto>>(result);
            Assert.AreEqual(reservationDto, result.Data);
        }

        [Test]
        public async Task DeleteReservationAsync_ShouldReturnSuccess_WhenReservationIsDeleted()
        {
            var reservationId = "R001";
            var reservation = new Reservation { ReservationId = reservationId, PassengerId = "P001", FlightNumber = "FL001", Seat = "12A" };

            _reservationRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Reservation, bool>>>())).ReturnsAsync(reservation);
            _reservationRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Reservation>())).Returns(Task.FromResult((Reservation)null));
            _unitOfWorkMock.Setup(uow => uow.CommitAsync()).Returns(Task.CompletedTask);

            var result = await _reservationService.DeleteReservationAsync(new ReservationDeleteDto { ReservationId = reservationId });

            Assert.IsInstanceOf<SuccessResult>(result);
        }
    }
}
