using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Models;
using DAL.Repository;

namespace BLL.Service
{
    public class FetalMeasurementService
    {
        private readonly FetalMeasurementRepository _measurementRepository;

        public FetalMeasurementService( )
        {
            _measurementRepository = new FetalMeasurementRepository();
        }

        public List<FetalMeasurement> GetAllMeasurements()
        {
            return _measurementRepository.GetAll();
        }

        public FetalMeasurement GetMeasurementById(int id)
        {
            return _measurementRepository.GetById(id);
        }

        public List<FetalMeasurement> GetMeasurementsByProfileId(int profileId)
        {
            return _measurementRepository.GetByProfileId(profileId);
        }

        public void CreateMeasurement(FetalMeasurement measurement)
        {
            _measurementRepository.Add(measurement);
        }

        public void UpdateMeasurement(FetalMeasurement measurement)
        {
            _measurementRepository.Update(measurement);
        }

        public void DeleteMeasurement(FetalMeasurement measurement)
        {
            _measurementRepository.Delete(measurement);
        }
    }
} 