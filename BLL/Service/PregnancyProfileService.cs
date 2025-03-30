using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Models;
using DAL.Repository;

namespace BLL.Service
{
    public class PregnancyProfileService
    {
        private readonly PregnancyProfileRepository _profileRepository;

        public PregnancyProfileService( )
        {
            _profileRepository = new PregnancyProfileRepository();
        }

        public List<PregnancyProfile> GetAllProfiles()
        {
            return _profileRepository.GetAll();
        }

        public PregnancyProfile GetProfileById(int id)
        {
            return _profileRepository.GetById(id);
        }

        public List<PregnancyProfile> GetProfilesByUserId(int userId)
        {
            return _profileRepository.GetByUserId(userId);
        }

        public void CreateProfile(PregnancyProfile profile)
        {
            _profileRepository.Add(profile);
        }

        public void UpdateProfile(PregnancyProfile profile)
        {
            _profileRepository.Update(profile);
        }

        public void DeleteProfile(PregnancyProfile profile)
        {
            _profileRepository.Delete(profile);
        }
    }
} 