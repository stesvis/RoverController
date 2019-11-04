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

        /// <summary>
        /// Returns all missions
        /// </summary>
        /// <returns></returns>
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
                mission = unitOfWork.Missions.GetFull(id);
            }
            var missionDTO = AutoMapper.Mapper.Map<MissionDTO>(mission);
            // make sure the pinpoints are ordered by DESC
            missionDTO.PinPoints = missionDTO.PinPoints.OrderByDescending(x => x.Id).ToList();

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
            var pinPointsRoute = CreateRoute(ref mission); // this will also validate the pin-points

            mission.PinPoints = pinPointsRoute;

            ValidateMissionOutput(mission);

            // if we get here it means that all the pin-points in the route are valid we can persist
            using (var unitOfWork = new UnitOfWork())
            {
                var roversOnTheSameSpot = unitOfWork.Missions.Find(x => x.FinalX == mission.FinalX && x.FinalY == mission.FinalY);
                if (roversOnTheSameSpot != null && roversOnTheSameSpot.Count() > 0)
                {
                    throw new Exception($"Houston we have a problem!{Environment.NewLine}This rover would collide with another rover in the same final spot!{Environment.NewLine}Mission aborted!");
                }
                unitOfWork.Missions.Add(mission);
                unitOfWork.SaveChanges();
                mission = unitOfWork.Missions.GetFull(mission.Id);
                missionDTO = AutoMapper.Mapper.Map<MissionDTO>(mission);
            }

            return missionDTO;
        }

        public MissionDTO Move(int id, MissionDTO missionDTO, string currentUserId)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var mission = unitOfWork.Missions.GetFull(id);
                mission.MoveInstructions = missionDTO.Instructions;

                // STEP 1: validate the minimum requirements
                ValidateMissionInput(mission);

                // if we get here it means that the mission is valid now let's calculate the route
                var pinPointsRoute = CreateRoute(ref mission); // this will also validate the pin-points
                mission.Instructions += missionDTO.Instructions; // add the new instructions

                ValidateMissionOutput(mission);

                // if we get here it means that all the pin-points in the route are valid we can persist
                var roversOnTheSameSpot = unitOfWork.Missions
                    .Find(x =>
                        x.Id != id &&
                        x.FinalX == mission.FinalX &&
                        x.FinalY == mission.FinalY);
                if (roversOnTheSameSpot != null && roversOnTheSameSpot.Count() > 0)
                {
                    throw new Exception($"Houston we have a problem!{Environment.NewLine}This rover would collide with another rover in the same final spot!{Environment.NewLine}Mission aborted!");
                }

                unitOfWork.Missions.Update(mission);
                unitOfWork.PinPoints.AddRange(pinPointsRoute);
                unitOfWork.SaveChanges();
                mission = unitOfWork.Missions.GetFull(mission.Id);
                missionDTO = AutoMapper.Mapper.Map<MissionDTO>(mission);
            }

            return missionDTO;
        }

        /// <summary>
        /// Attaches a file to a mission
        /// </summary>
        /// <param name="id"></param>
        /// <param name="attachmentDTO"></param>
        /// <param name="currentUserId"></param>
        /// <returns></returns>
        public MissionAttachmentDTO Attach(int id, MissionAttachmentDTO attachmentDTO, string currentUserId)
        {
            if (attachmentDTO.MissionId != id)
            {
                throw new ArgumentException("Trying to attach a file to a different mission.", nameof(attachmentDTO.Mission));
            }

            var attachment = AutoMapper.Mapper.Map<MissionAttachment>(attachmentDTO);

            // Mission
            attachment.CreatedByUserId = currentUserId;
            attachment.CreatedDate = DateTime.Now;

            ValidateAttachment(attachment);

            using (var unitOfWork = new UnitOfWork())
            {
                // hard-delete any existing attachments for this mission
                var existingAttachments = unitOfWork.MissionAttachments.Find(x => x.MissionId == id);
                if (existingAttachments != null)
                {
                    unitOfWork.MissionAttachments.HardDeleteRange(existingAttachments);
                }
                unitOfWork.MissionAttachments.Add(attachment);
                unitOfWork.SaveChanges();
                attachment = unitOfWork.MissionAttachments.GetFull(attachment.Id);
                attachmentDTO = AutoMapper.Mapper.Map<MissionAttachmentDTO>(attachment);
            }

            return attachmentDTO;
        }

        private List<PinPoint> CreateRoute(ref Mission mission)
        {
            var pinPointsRoute = new List<PinPoint>();
            var pinPoint = new PinPoint();

            if (mission.Id == 0)
            {
                // do this only the first time. Next times the initial position will be the previous
                // final position already
                pinPoint = new PinPoint
                {
                    MissionId = mission.Id,
                    X = mission.InitialX,
                    Y = mission.InitialY,
                    Direction = mission.InitialDirection,
                    CreatedDate = DateTime.Now,
                    CreatedByUserId = mission.CreatedByUserId
                };
                pinPointsRoute.Add(pinPoint); // always store the initial pin-point too
            }
            else
            {
                pinPoint = mission.PinPoints.LastOrDefault();
            }

            int currentIndex;
            int newIndex;

            // if we are just moving an existing rover we have to use the "MoveInstructions"
            var instructions = mission.Id > 0 ? mission.MoveInstructions : mission.Instructions;

            // loop thru each single instructions
            foreach (var c in instructions)
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

                pinPoint.MissionId = mission.Id;
                pinPoint.CreatedDate = DateTime.Now;
                pinPoint.CreatedByUserId = mission.CreatedByUserId;

                pinPointsRoute.Add(pinPoint);
            }

            ValidatePinPoints(mission, pinPointsRoute);

            // set the final output for this mission
            mission.FinalX = pinPointsRoute.LastOrDefault().X;
            mission.FinalY = pinPointsRoute.LastOrDefault().Y;
            mission.FinalDirection = pinPointsRoute.LastOrDefault().Direction;

            return pinPointsRoute;
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

            if (mission.InitialX < 0 || mission.InitialX > mission.MaxX)
                throw new ArgumentNullException(nameof(mission.InitialX));

            if (mission.InitialY < 0 || mission.InitialY > mission.MaxY)
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
            if (mission.FinalX < 0)
                throw new ArgumentException("Argument cannot be negative.", nameof(mission.FinalX));

            if (mission.FinalY < 0)
                throw new ArgumentException("Argument cannot be negative.", nameof(mission.FinalY));

            if (mission.FinalDirection.IsEmpty())
                throw new ArgumentNullException(nameof(mission.FinalDirection));
        }

        /// <summary>
        /// Loops thru all the pin-points and checks if they are all within the mission perimeter
        /// </summary>
        /// <param name="mission"></param>
        private void ValidatePinPoints(Mission mission, List<PinPoint> pinPoints)
        {
            foreach (var pinPoint in pinPoints)
            {
                if (pinPoint.X < 0 ||
                    pinPoint.X > mission.MaxX ||
                    pinPoint.Y < 0 ||
                    pinPoint.Y > mission.MaxY)
                {
                    throw new ArgumentException($"Houston we have a problem!{Environment.NewLine}These instructions lead the rover outside of the perimeter.{Environment.NewLine}Mission aborted!");
                }
            }
        }

        private void ValidateAttachment(MissionAttachment attachment)
        {
            if (attachment.MissionId == 0)
                throw new ArgumentNullException(nameof(attachment.MissionId));

            if (attachment.FileType.IsEmpty())
                throw new ArgumentNullException(nameof(attachment.FileType));

            if (attachment.FileName.IsEmpty())
                throw new ArgumentNullException(nameof(attachment.FileName));

            if (attachment.OriginalFilename.IsEmpty())
                throw new ArgumentNullException(nameof(attachment.OriginalFilename));
        }
    }
}