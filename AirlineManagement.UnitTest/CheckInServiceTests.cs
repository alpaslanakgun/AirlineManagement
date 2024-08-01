using AirlineManagement.Business.Common.MessageConstant;
using AirlineManagement.Business.Contracts;
using AirlineManagement.Business.DTOs.CheckInDTOs;
using AirlineManagement.Business.Services;
using AirlineManagement.Data.Contracts;
using AirlineManagement.Domain.Entities;
using AirlineManagement.Domain.Enums;
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
    public class CheckInServiceTests
    {
        private Mock<ICheckInRepository> _checkInRepositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IReservationRepository> _reservationRepositoryMock;
        private ICheckInService _checkInService;

        [SetUp]
        public void SetUp()
        {
            _checkInRepositoryMock = new Mock<ICheckInRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _reservationRepositoryMock = new Mock<IReservationRepository>();
            _checkInService = new CheckInService(_checkInRepositoryMock.Object, _unitOfWorkMock.Object, _mapperMock.Object, _reservationRepositoryMock.Object);
        }

        [Test]
        public async Task GetCheckInsAsync_ShouldReturnCheckIns_WhenCheckInsExist()
        {
          
            var checkIns = new List<CheckIn>
            {
                new CheckIn { CheckInId = "CI001", ReservationId = "R001", Status = CheckInStatus.Completed, BaggageCount = 2, BoardingTime = DateTime.Now }
            };
            var checkInDtos = new List<CheckInDto>
            {
                new CheckInDto { CheckInId = "CI001", ReservationId = "R001", Status = CheckInStatus.Completed, BaggageCount = 2, BoardingTime = DateTime.Now }
            };

            _checkInRepositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<Expression<Func<CheckIn, bool>>>())).ReturnsAsync(checkIns);
            _mapperMock.Setup(m => m.Map<IEnumerable<CheckInDto>>(It.IsAny<IEnumerable<CheckIn>>())).Returns(checkInDtos);

       
            var result = await _checkInService.GetCheckInsAsync();

           
            Assert.IsInstanceOf<SuccessDataResult<IEnumerable<CheckInDto>>>(result);
            Assert.AreEqual(checkInDtos, result.Data);
        }

        [Test]
        public async Task GetCheckInDetailsAsync_ShouldReturnCheckIn_WhenCheckInExists()
        {
         
            var checkInId = "CI001";
            var checkIn = new CheckIn { CheckInId = checkInId, ReservationId = "R001", Status = CheckInStatus.Completed, BaggageCount = 2, BoardingTime = DateTime.Now };
            var checkInDto = new CheckInDto { CheckInId = checkInId, ReservationId = "R001", Status = CheckInStatus.Completed, BaggageCount = 2, BoardingTime = DateTime.Now };

            _checkInRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<CheckIn, bool>>>())).ReturnsAsync(checkIn);
            _mapperMock.Setup(m => m.Map<CheckInDto>(It.IsAny<CheckIn>())).Returns(checkInDto);

            var result = await _checkInService.GetCheckInDetailsAsync(checkInId);

            Assert.IsInstanceOf<SuccessDataResult<CheckInDto>>(result);
            Assert.AreEqual(checkInDto, result.Data);
        }

        [Test]
        public async Task CreateCheckInAsync_ShouldReturnSuccess_WhenCheckInIsCreated()
        {
           
            var checkInCreateDto = new CheckInCreateDto { ReservationId = "R001", BaggageCount = 2 };
            var reservation = new Reservation { ReservationId = "R001", Seat = "12A" };
            var checkIn = new CheckIn { CheckInId = "CI001", ReservationId = "R001", Status = CheckInStatus.InProgress, BaggageCount = 2, BoardingTime = DateTime.Now };
            var checkInDto = new CheckInDto { CheckInId = "CI001", ReservationId = "R001", Status = CheckInStatus.Completed, BaggageCount = 2, BoardingTime = DateTime.Now };

            _reservationRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Reservation, bool>>>())).ReturnsAsync(reservation);
            _mapperMock.Setup(m => m.Map<CheckIn>(It.IsAny<CheckInCreateDto>())).Returns(checkIn);
            _checkInRepositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<Expression<Func<CheckIn, bool>>>())).ReturnsAsync(new List<CheckIn> { checkIn });
            _checkInRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<CheckIn>())).Returns(Task.FromResult((CheckIn)null));
            _unitOfWorkMock.Setup(uow => uow.CommitAsync()).Returns(Task.CompletedTask);
            _checkInRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<CheckIn>())).Returns(Task.FromResult((CheckIn)null));
            _mapperMock.Setup(m => m.Map<CheckInDto>(It.IsAny<CheckIn>())).Returns(checkInDto);

           
            var result = await _checkInService.CreateCheckInAsync(checkInCreateDto);

           
            Assert.IsInstanceOf<SuccessDataResult<CheckInDto>>(result);
            Assert.AreEqual(checkInDto, result.Data);
        }
        [Test]
        public async Task UpdateCheckInAsync_ShouldReturnSuccess_WhenCheckInIsUpdated()
        {
            
            var checkInUpdateDto = new CheckInUpdateDto { CheckInId = "CI001", BaggageCount = 2, Status = CheckInStatus.Completed };
            var checkIn = new CheckIn { CheckInId = "CI001", ReservationId = "R001", Status = CheckInStatus.InProgress, BaggageCount = 2, BoardingTime = DateTime.Now };
            var checkInDto = new CheckInDto { CheckInId = "CI001", ReservationId = "R001", Status = CheckInStatus.Completed, BaggageCount = 2, BoardingTime = DateTime.Now };

            _checkInRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<CheckIn, bool>>>())).ReturnsAsync(checkIn);
            _checkInRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<CheckIn>())).Returns(Task.FromResult(checkIn));
            _unitOfWorkMock.Setup(uow => uow.CommitAsync()).Returns(Task.CompletedTask);
            _mapperMock.Setup(m => m.Map<CheckInDto>(It.IsAny<CheckIn>())).Returns(checkInDto);

           
            var result = await _checkInService.UpdateCheckInAsync(checkInUpdateDto);

         
            Assert.IsInstanceOf<SuccessDataResult<CheckInDto>>(result);
            Assert.AreEqual(checkInDto, result.Data);
        }

        [Test]
        public async Task DeleteCheckInAsync_ShouldReturnSuccess_WhenCheckInIsDeleted()
        {
           
            var checkInId = "CI001";
            var checkIn = new CheckIn { CheckInId = checkInId, ReservationId = "R001", Status = CheckInStatus.Completed, BaggageCount = 2, BoardingTime = DateTime.Now };

            _checkInRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<CheckIn, bool>>>())).ReturnsAsync(checkIn);
            _checkInRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<CheckIn>())).Returns(Task.FromResult(checkIn));
            _unitOfWorkMock.Setup(uow => uow.CommitAsync()).Returns(Task.CompletedTask);

            var result = await _checkInService.DeleteCheckInAsync(new CheckInDeleteDto { CheckInId = checkInId });

            Assert.IsInstanceOf<SuccessResult>(result);
        }
    }
}
