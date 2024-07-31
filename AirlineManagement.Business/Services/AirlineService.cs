﻿using AirlineManagement.Business.Common.MessageConstant;
using AirlineManagement.Business.Constants;
using AirlineManagement.Business.DTOs.AirlineDTOs;
using AirlineManagement.Data.Contracts;
using AirlineManagement.Domain.Entities;
using AirlineManagement.Domain.Results.Abstract;
using AirlineManagement.Domain.Results.ComplexType;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirlineManagement.Business.Services
{
    public class AirlineService : IAirlineService
    {
        private readonly IAirlineRepository _airlineRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AirlineService(IAirlineRepository airlineRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _airlineRepository = airlineRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<IEnumerable<AirlineDto>>> GetAirlinesAsync()
        {
            try
            {
                var airlines = await _airlineRepository.GetAllAsync();
                var airlineDtos = _mapper.Map<IEnumerable<AirlineDto>>(airlines);
                return new SuccessDataResult<IEnumerable<AirlineDto>>(airlineDtos, Messages.AirlinesRetrievedSuccessfully);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<IEnumerable<AirlineDto>>($"{Messages.AirlineUpdateFailed}: {ex.Message}");
            }
        }

        public async Task<IDataResult<AirlineDto>> GetAirlineDetailsAsync(string airlineId)
        {
            try
            {
                var airline = await _airlineRepository.GetAsync(a => a.AirlineId == airlineId);
                if (airline == null)
                {
                    return new ErrorDataResult<AirlineDto>(Messages.AirlineNotFound);
                }
                var airlineDto = _mapper.Map<AirlineDto>(airline);
                return new SuccessDataResult<AirlineDto>(airlineDto, Messages.AirlineDetailsRetrievedSuccessfully);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<AirlineDto>($"{Messages.AirlineUpdateFailed}: {ex.Message}");
            }
        }

        public async Task<IDataResult<AirlineDto>> CreateAirlineAsync(AirlineCreateDto airlineCreateDto)
        {
            try
            {
                var airline = _mapper.Map<Airline>(airlineCreateDto);
                await _airlineRepository.AddAsync(airline);
                await _unitOfWork.CommitAsync();
                var createdAirlineDto = _mapper.Map<AirlineDto>(airline);
                return new SuccessDataResult<AirlineDto>(createdAirlineDto, Messages.AirlineCreatedSuccessfully);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<AirlineDto>($"{Messages.AirlineCreationFailed}: {ex.Message}");
            }
        }

        public async Task<IDataResult<AirlineDto>> UpdateAirlineAsync(AirlineUpdateDto airlineUpdateDto)
        {
            try
            {
                var airline = await _airlineRepository.GetAsync(a => a.AirlineId == airlineUpdateDto.AirlineId);

                if (airline == null)
                {
                    return new ErrorDataResult<AirlineDto>(Messages.AirlineNotFound);
                }

                _mapper.Map(airlineUpdateDto, airline);
                await _airlineRepository.UpdateAsync(airline);
                await _unitOfWork.CommitAsync();

                var updatedAirlineDto = _mapper.Map<AirlineDto>(airline);
                return new SuccessDataResult<AirlineDto>(updatedAirlineDto, Messages.AirlineUpdatedSuccessfully);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<AirlineDto>($"{Messages.AirlineUpdateFailed}: {ex.Message}");
            }
        }

        public async Task<IResult> DeleteAirlineAsync(AirlineDeleteDto airlineDeleteDto)
        {
            try
            {
                var airline = await _airlineRepository.GetAsync(a => a.AirlineId == airlineDeleteDto.AirlineId);
                if (airline == null)
                {
                    return new ErrorResult(Messages.AirlineNotFound);
                }

                await _airlineRepository.DeleteAsync(airline);
                await _unitOfWork.CommitAsync();
                return new SuccessResult(Messages.AirlineDeletedSuccessfully);
            }
            catch (Exception ex)
            {
                return new ErrorResult($"{Messages.AirlineDeletionFailed}: {ex.Message}");
            }
        }
    }
}
