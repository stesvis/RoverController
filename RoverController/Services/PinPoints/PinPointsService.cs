using RoverController.Repositories.UnitOfWork;
using RoverController.Web.DTOs;
using RoverController.Web.Models;
using RoverController.Web.Services.Base;
using System.Collections.Generic;

namespace RoverController.Web.Services.PinPoints
{
    public class PinPointsService : BaseService, IPinPointsService
    {
        public PinPointsService() : base()
        {
        }

        public IEnumerable<PinPointDTO> All()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var pinPoints = unitOfWork.PinPoints.GetAll();

                var pinPointDTOs = AutoMapper.Mapper.Map<IEnumerable<PinPointDTO>>(pinPoints);

                return pinPointDTOs;
            }
        }

        /// <summary>
        /// Returns a pin-point
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PinPointDTO Get(int id)
        {
            var mission = new PinPoint();
            using (var unitOfWork = new UnitOfWork())
            {
                mission = unitOfWork.PinPoints.Get(id);
            }
            var missionDTO = AutoMapper.Mapper.Map<PinPointDTO>(mission);

            return missionDTO;
        }
    }
}