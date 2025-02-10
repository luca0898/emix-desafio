﻿using eMix.ConsultaCEP.Models;
using Refit;

namespace eMix.ConsultaCEP.Contracts
{
    [Headers("accept: application/json")]
    public interface IViaCepHttpService
    {
        [Get("/ws/{zipCode}/json")]
        Task<ViaCepRequestResult> getAddressByZipCode(string zipCode);
    }
}
