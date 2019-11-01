using RoverController.Repositories.UnitOfWork;
using RoverController.Web.DTOs;
using RoverController.Web.Models;
using RoverController.Web.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.WebPages;

namespace RoverController.Web.Services.Missions
{
    public class MissionsService : BaseService, IMissionsService
    {
        private string _directions = "NESW";

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

            // STEP 1: validate the minimum requirements
            ValidateMissionInput(mission);

            // if we get here it means that the mission is valid now let's calculate the route
            CreateRoute(ref mission); // this will also validate the pin-points

            ValidateMissionOutput(mission);

            // if we get here it means that all the pin-points in the route are valid we can persist
            using (var unitOfWork = new UnitOfWork())
            {
                unitOfWork.Missions.Add(mission);
                unitOfWork.SaveChanges();
                mission = unitOfWork.Missions.GetFull(mission.Id);
                missionDTO = AutoMapper.Mapper.Map<MissionDTO>(mission);
            }

            return missionDTO;
        }

        private void CreateRoute(ref Mission mission)
        {
            var pinPoint = new PinPoint
            {
                X = mission.InitialX,
                Y = mission.InitialY,
                Direction = mission.InitialDirection,
                CreatedDate = DateTime.Now,
                CreatedByUserId = mission.CreatedByUserId
            };
            mission.PinPoints.Add(pinPoint); // always store the initial pin-point too

            int currentIndex;
            int newIndex;

            // loop thru each single instructions
            foreach (var c in mission.Instructions)
            {
                switch (c)
                {
                    case 'L':
                        currentIndex = _directions.IndexOf(pinPoint.Direction);
                        newIndex = currentIndex - 1;

                        if (newIndex < 0) newIndex = _directions.Length - 1;
                        if (newIndex > _directions.Length - 1) newIndex = 0;

                        pinPoint = new PinPoint
                        {
                            X = pinPoint.X,
                            Y = pinPoint.Y,
                            Direction = _directions[newIndex].ToString()
                        };
                        break;

                    case 'R':
                        currentIndex = _directions.IndexOf(pinPoint.Direction);
                        newIndex = currentIndex + 1;

                        if (newIndex < 0) newIndex = _directions.Length - 1;
                        if (newIndex > _directions.Length - 1) newIndex = 0;

                        pinPoint = new PinPoint
                        {
                            X = pinPoint.X,
                            Y = pinPoint.Y,
                            Direction = _directions[newIndex].ToString()
                        };
                        break;

                    case 'M':
                        pinPoint = MoveForward(pinPoint);
                        break;

                    default:
                        break;
                }

                pinPoint.CreatedDate = DateTime.Now;
                pinPoint.CreatedByUserId = mission.CreatedByUserId;

                mission.PinPoints.Add(pinPoint);
            }

            ValidatePinPoints(mission);

            // set the final output for this mission
            mission.FinalX = mission.PinPoints.LastOrDefault().X;
            mission.FinalY = mission.PinPoints.LastOrDefault().Y;
            mission.FinalDirection = mission.PinPoints.LastOrDefault().Direction;
        }

        private PinPoint MoveForward(PinPoint currentPinPoint)
        {
            var newPinPoint = new PinPoint
            {
                Direction = currentPinPoint.Direction
            };

            switch (currentPinPoint.Direction)
            {
                case "N":
                    newPinPoint.X = currentPinPoint.X;
                    newPinPoint.Y = currentPinPoint.Y + 1;
                    break;

                case "E":
                    newPinPoint.X = currentPinPoint.X + 1;
                    newPinPoint.Y = currentPinPoint.Y;
                    break;

                case "S":
                    newPinPoint.X = currentPinPoint.X;
                    newPinPoint.Y = currentPinPoint.Y - 1;
                    break;

                case "W":
                    newPinPoint.X = currentPinPoint.X - 1;
                    newPinPoint.Y = currentPinPoint.Y;
                    break;

                default:
                    break;
            }

            return newPinPoint;
        }

        /// <summary>
        /// It throws an ArgumentNullException if some parameters are missing
        /// </summary>
        /// <param name="mission"></param>
        private void ValidateMissionInput(Mission mission)
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
        }

        /// <summary>
        /// It throws an ArgumentNullException if some parameters are missing
        /// </summary>
        /// <param name="mission"></param>
        private void ValidateMissionOutput(Mission mission)
        {
            if (mission.FinalX == 0)
                throw new ArgumentNullException(nameof(mission.FinalX));

            if (mission.FinalY == 0)
                throw new ArgumentNullException(nameof(mission.FinalY));

            if (mission.FinalDirection.IsEmpty())
                throw new ArgumentNullException(nameof(mission.FinalDirection));
        }

        /// <summary>
        /// Loops thru all the pin-points and checks if they are all within the mission perimeter
        /// </summary>
        /// <param name="mission"></param>
        private void ValidatePinPoints(Mission mission)
        {
            foreach (var pinPoint in mission.PinPoints)
            {
                if (pinPoint.X < 0 ||
                    pinPoint.X > mission.MaxX ||
                    pinPoint.Y < 0 ||
                    pinPoint.Y > mission.MaxY)
                {
                    throw new ArgumentException("These instructions lead the rover outside of the perimeter.");
                }
            }
        }
    }
}