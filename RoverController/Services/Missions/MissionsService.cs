using RoverController.DTOs;
using RoverController.Models;
using RoverController.Repositories.UnitOfWork;
using RoverController.Web.Services.Base;
using System.Collections.Generic;

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
    }
}