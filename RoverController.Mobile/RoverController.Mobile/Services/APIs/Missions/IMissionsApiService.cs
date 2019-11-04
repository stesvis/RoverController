using RoverController.Mobile.DTOs;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RoverController.Mobile.Services.APIs.Missions
{
    public interface IMissionsApiService
    {
        Task<Tuple<IEnumerable<MissionDTO>, string>> All();

        Task<Tuple<MissionDTO, string>> Get(int id);

        Task<Tuple<MissionDTO, string>> Create(MissionRequestDTO missionRequestDTO);

        Task<HttpResponseMessage> Upload(int id, byte[] ImageData);
    }
}