﻿using NeoSmart.ClassLibraries.Models;

namespace NeoSmart.ClassLibraries.Interfaces
{
    public interface IApiService
    {
        Task<Response<T>> GetAsync<T>(string servicePrefix, string controller);
    }
}