using System;
using System.Collections.Generic;
using DAL.Models;
using DAL.Repository;

namespace BLL.Services
{
    public class PregnancyProfileService
    {
        private readonly PregnancyProfileRepository _profileRepository;

        public PregnancyProfileService(PregnancyProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public List<PregnancyProfile> GetProfilesByUserId(int userId)
        {
            return _profileRepository.GetByUserId(userId);
        }

        public PregnancyProfile GetProfileById(int id)
        {
            return _profileRepository.GetById(id);
        }

        public void CreateProfile(string name, DateTime conceptionDate, DateTime dueDate, string status, int userId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Profile name cannot be empty");
            if (conceptionDate >= dueDate)
                throw new ArgumentException("Conception date must be before due date");
            if (string.IsNullOrWhiteSpace(status))
                throw new ArgumentException("Status cannot be empty");

            var profile = new PregnancyProfile
            {
                Name = name,
                ConceptionDate = conceptionDate,
                DueDate = dueDate,
                PregnancyStatus = status,
                UserId = userId,
                CreatedAt = DateTime.Now
            };

            _profileRepository.Add(profile);
        }

        public void UpdateProfile(int profileId, string name, DateTime conceptionDate, DateTime dueDate, string status)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Profile name cannot be empty");
            if (conceptionDate >= dueDate)
                throw new ArgumentException("Conception date must be before due date");
            if (string.IsNullOrWhiteSpace(status))
                throw new ArgumentException("Status cannot be empty");

            var profile = _profileRepository.GetById(profileId);
            if (profile == null)
                throw new ArgumentException("Profile not found");

            profile.Name = name;
            profile.ConceptionDate = conceptionDate;
            profile.DueDate = dueDate;
            profile.PregnancyStatus = status;

            _profileRepository.Update(profile);
        }

        public void DeleteProfile(int profileId, int userId)
        {
            var profile = _profileRepository.GetById(profileId);
            if (profile == null)
                throw new ArgumentException("Profile not found");
            if (profile.UserId != userId)
                throw new UnauthorizedAccessException("You are not authorized to delete this profile");

            _profileRepository.Delete(profile);
        }
    }
}