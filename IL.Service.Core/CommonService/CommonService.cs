﻿using AutoMapper;
using IL.DTO.Core.CommonDTO;
using IL.Service.Core.LoggerService;
using IM.Data.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace IL.Service.Core.CommonService
{
    public class CommonService : ICommonService
    {

        private db_InventoryEntities _dbEntities
        {
            get { return new db_InventoryEntities(); }
        }
        private readonly IMapper _mapper;
        private readonly ILoggerService _logger;
        public CommonService(IMapper mapper, ILoggerService logger)
        {
            _mapper = mapper;
            _logger = logger;
        }
        public List<DTO.Core.CommonDTO.CommonDTO> GetOutlets()
        {
            return this._dbEntities.outlets.Where(p => p.deleteflag == false).AsEnumerable().Select(opt => this._mapper.Map<CommonDTO>(opt)).ToList();
        }
        public List<DTO.Core.CommonDTO.CommonDTO> GetRoles()
        {
            return this._dbEntities.UserRoles.Where(p => p.deleteflag == false).AsEnumerable().Select(opt => this._mapper.Map<CommonDTO>(opt)).ToList();
        }
        public List<CommonDTO> GetMovementType() =>
             this._dbEntities.movementTypes.Where(p => p.deleteflag == false).AsEnumerable().Select(opt => this._mapper.Map<CommonDTO>(opt)).ToList();

        public List<CommonDTO> GetUnits() =>
            this._dbEntities.units.Where(p => p.deleteflag == false).AsEnumerable().OrderBy(p => p.normalizeName).Select(opt => this._mapper.Map<CommonDTO>(opt)).ToList();

        public List<CommonDTO> GetStatus() =>
            this._dbEntities.Status.Where(p => p.deleteflag == false).AsEnumerable().OrderBy(p => p.name).Select(opt => this._mapper.Map<CommonDTO>(opt)).ToList();

        public List<CommonDTO> UserSpecificOutlet(int userId)
        {
            return this._dbEntities.OutletMappings.Where(p => p.userId == userId).Select(opt => new CommonDTO
            {
                Id = opt.outlet.id,
                NormalizeName = opt.outlet.NormalizeName
            }).ToList();
        }
        public bool ImportNewItemsFromCreation(int outletId)
        {
            try
            {
                this._dbEntities.IMPORT_NEW_ITEM_TOSTORE(outletId);
                return Convert.ToBoolean(true);
            }
            catch (Exception ex)
            {
                this._logger.LogException(ex);
                return false;
            }
        }
    }
}
