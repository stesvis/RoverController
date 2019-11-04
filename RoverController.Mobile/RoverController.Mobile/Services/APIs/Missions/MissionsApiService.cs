using RoverController.Lib;
using RoverController.Mobile.DTOs;
using RoverController.Mobile.Misc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoverController.Mobile.Services.APIs.Missions
{
    public class MissionsApiService : IMissionsApiService
    {
        /// <summary>
        /// Fetches one mission from the server
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Tuple<MissionDTO, string>> Get(int id)
        {
            if (!Helper.IsInternetAvailable())
            {
                return null;
            }

            var result = await ApiWrapper<MissionDTO>.Get(Api.Missions.Get.Replace("{id}", id.ToString()));

            return result;
        }

        /// <summary>
        /// Fetches all the missions performed
        /// </summary>
        /// <returns></returns>
        public async Task<Tuple<IEnumerable<MissionDTO>, string>> All()
        {
            if (!Helper.IsInternetAvailable())
            {
                return null;
            }

            var result = await ApiWrapper<IEnumerable<MissionDTO>>.Get(Api.Missions.All);

            return result;
        }

        /// <summary>
        /// Creates or updates a mission
        /// </summary>
        /// <param name="missionRequestDTO"></param>
        /// <returns></returns>
        public async Task<Tuple<MissionDTO, string>> Save(MissionRequestDTO missionRequestDTO)
        {
            if (missionRequestDTO.Id == 0)
            {
                return await Create(missionRequestDTO);
            }

            return await Update(missionRequestDTO.Id, missionRequestDTO);
        }

        /// <summary>
        /// Creates a new mission (deploys a new rover)
        /// </summary>
        /// <param name="missionDTO"></param>
        /// <returns></returns>
        public async Task<Tuple<MissionDTO, string>> Create(MissionRequestDTO missionRequestDTO)
        {
            if (!Helper.IsInternetAvailable())
            {
                return null;
            }

            var result = await ApiWrapper<MissionDTO>.Post(Api.Missions.Create, missionRequestDTO);

            return result;
        }

        /// <summary>
        /// Updates a mission (moves an existing rover)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="missionRequestDTO"></param>
        /// <returns></returns>
        public async Task<Tuple<MissionDTO, string>> Update(int id, MissionRequestDTO missionRequestDTO)
        {
            if (!Helper.IsInternetAvailable())
            {
                return null;
            }

            var result = await ApiWrapper<MissionDTO>.Post(Api.Missions.Move.Replace("{id}", id.ToString()), missionRequestDTO);

            return result;
        }

        public async Task<Tuple<MissionAttachmentDTO, string>> Upload(int id, byte[] ImageData)
        {
            if (!Helper.IsInternetAvailable())
            {
                return null;
            }

            var result = await ApiWrapper<MissionAttachmentDTO>.UploadImage(Api.Missions.Upload.Replace("{id}", id.ToString()), ImageData);

            return result;
        }
    }
}