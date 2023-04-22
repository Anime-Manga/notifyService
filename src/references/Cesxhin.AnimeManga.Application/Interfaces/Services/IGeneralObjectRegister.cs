﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cesxhin.AnimeManga.Application.Interfaces.Services
{
    public interface IGeneralObjectRegister<TObjectRegisterDTO>
    {
        //get
        Task<TObjectRegisterDTO> GetObjectRegisterByObjectId(string id);

        //insert
        Task<TObjectRegisterDTO> InsertObjectRegisterAsync(TObjectRegisterDTO objectGeneralRegister);
        Task<IEnumerable<TObjectRegisterDTO>> InsertObjectsRegistersAsync(List<TObjectRegisterDTO> objectGeneralRegister);

        //put
        Task<TObjectRegisterDTO> UpdateObjectRegisterAsync(TObjectRegisterDTO objectGeneralRegister);
    }
}
