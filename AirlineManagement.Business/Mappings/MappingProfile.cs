using AirlineManagement.Business.DTOs.AirlineDTOs;
using AirlineManagement.Business.DTOs.CheckInDTOs;
using AirlineManagement.Business.DTOs.FlightDTOs;
using AirlineManagement.Business.DTOs.PassengerDTOs;
using AirlineManagement.Business.DTOs.ReservationDTOs;
using AirlineManagement.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement.Business.Mappings
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            // Flight Mapping
            CreateMap<Flight, FlightDto>().ReverseMap();
            CreateMap<Flight, FlightCreateDto>().ReverseMap();
            CreateMap<Flight, FlightUpdateDto>().ReverseMap();
 

            // Reservation Mapping
            CreateMap<Reservation, ReservationDto>().ReverseMap();
            CreateMap<Reservation, ReservationCreateDto>().ReverseMap();
            CreateMap<Reservation, ReservationUpdateDto>().ReverseMap();
            

            // CheckIn Mapping
            CreateMap<CheckIn, CheckInDto>().ReverseMap();
            CreateMap<CheckIn, CheckInCreateDto>().ReverseMap();
            CreateMap<CheckIn, CheckInUpdateDto>().ReverseMap();

            // Airline Mapping
            CreateMap<Airline, AirlineDto>().ReverseMap();
            CreateMap<Airline, AirlineCreateDto>().ReverseMap();
            CreateMap<Airline, AirlineUpdateDto>().ReverseMap();
            CreateMap<Airline, AirlineDeleteDto>().ReverseMap();

            // Passenger mapping
            CreateMap<Passenger, PassengerDto>().ReverseMap();
            CreateMap<Passenger, PassengerCreateDto>().ReverseMap();
            CreateMap<Passenger, PassengerUpdateDto>().ReverseMap();
            CreateMap<Passenger, PassengerDeleteDto>().ReverseMap();

        }
    }
}
