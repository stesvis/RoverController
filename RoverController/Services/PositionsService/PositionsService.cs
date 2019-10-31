using RoverController.DTOs;
using RoverController.Repositories.UnitOfWork;
using RoverController.Web.Services.Base;
using System.Collections.Generic;

namespace RoverController.Web.Services.PositionsService
{
    public class PositionsService : BaseService, IPositionsService
    {
        public PositionsService() : base()
        {
        }

        public IEnumerable<PositionDTO> All()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var positions = unitOfWork.Positions.GetAll();

                var positionDTOs = AutoMapper.Mapper.Map<IEnumerable<PositionDTO>>(positions);

                return positionDTOs;
            }
        }
    }
}