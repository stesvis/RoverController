using RoverController.Repositories.UnitOfWork;
using RoverController.Web.DTOs;
using RoverController.Web.Models;
using RoverController.Web.Services.Base;
using System;
using System.Collections.Generic;
using System.Web.WebPages;

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

        public PinPointDTO Create(PinPointDTO pinPointDTO, string currentUserId)
        {
            var pinPoint = AutoMapper.Mapper.Map<PinPoint>(pinPointDTO);

            // Mission
            pinPoint.CreatedByUserId = currentUserId;
            pinPoint.CreatedDate = DateTime.Now;

            Validate(pinPoint); // will thron an exception and terminate if it doesn't pass validation

            using (var unitOfWork = new UnitOfWork())
            {
                unitOfWork.PinPoints.Add(pinPoint);
                unitOfWork.SaveChanges();
                pinPointDTO = AutoMapper.Mapper.Map<PinPointDTO>(pinPoint);
            }

            return pinPointDTO;
        }

        private void Validate(PinPoint pinPoint)
        {
            if (pinPoint.MissionId == 0)
                throw new ArgumentNullException(nameof(pinPoint.MissionId));

            if (pinPoint.X == 0)
                throw new ArgumentNullException(nameof(pinPoint.X));

            if (pinPoint.Y == 0)
                throw new ArgumentNullException(nameof(pinPoint.Y));

            if (pinPoint.Direction.IsEmpty())
                throw new ArgumentNullException(nameof(pinPoint.Direction));
        }
    }
}