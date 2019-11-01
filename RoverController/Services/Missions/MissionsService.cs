using RoverController.DTOs;
using RoverController.Models;
using RoverController.Repositories.UnitOfWork;
using RoverController.Web.Services.Base;
using System;
using System.Collections.Generic;
using System.Web.WebPages;

namespace RoverController.Web.Services.Missions
{
    public class MissionsService : BaseService, IMissionsService
    {
        public MissionsService() : base()
        {
        }

        public IEnumerable<MissionDTO> All()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var missions = unitOfWork.Missions.GetAll();

                var missionDTOs = AutoMapper.Mapper.Map<IEnumerable<MissionDTO>>(missions);

                return missionDTOs;
            }
        }

        /// <summary>
        /// Returns a mission
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MissionDTO Get(int id)
        {
            var mission = new Mission();
            using (var unitOfWork = new UnitOfWork())
            {
                mission = unitOfWork.Missions.Get(id);
            }
            var missionDTO = AutoMapper.Mapper.Map<MissionDTO>(mission);

            return missionDTO;
        }

        /// <summary>
        /// Inserts a new mission into the database
        /// </summary>
        /// <param name="missionDTO"></param>
        /// <param name="currentUserId"></param>
        /// <returns></returns>
        public MissionDTO Create(MissionDTO missionDTO, string currentUserId)
        {
            var mission = AutoMapper.Mapper.Map<Mission>(missionDTO);

            // Mission
            mission.CreatedByUserId = currentUserId;
            mission.CreatedDate = DateTime.Now;

            Validate(mission); // will thron an exception and terminate if it doesn't pass validation

            using (var unitOfWork = new UnitOfWork())
            {
                unitOfWork.Missions.Add(mission);
                unitOfWork.SaveChanges();
                missionDTO = AutoMapper.Mapper.Map<MissionDTO>(mission);
            }

            return missionDTO;
        }

        private void Validate(Mission mission)
        {
            if (mission.MaxX == 0)
                throw new ArgumentNullException(nameof(mission.MaxX));

            if (mission.MaxY == 0)
                throw new ArgumentNullException(nameof(mission.MaxY));

            if (mission.InitialX == 0)
                throw new ArgumentNullException(nameof(mission.InitialX));

            if (mission.InitialY == 0)
                throw new ArgumentNullException(nameof(mission.InitialY));

            if (mission.InitialDirection.IsEmpty())
                throw new ArgumentNullException(nameof(mission.InitialDirection));

            if (mission.FinalX == 0)
                throw new ArgumentNullException(nameof(mission.FinalX));

            if (mission.FinalY == 0)
                throw new ArgumentNullException(nameof(mission.FinalY));

            if (mission.FinalDirection.IsEmpty())
                throw new ArgumentNullException(nameof(mission.FinalDirection));
        }
    }
}