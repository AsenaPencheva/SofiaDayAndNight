using System;
using SofiaDayAndNight.Data.Models;

namespace SofiaDayAndNight.Data.Services.Contracts
{
    public interface IImageService
    {
        Image GetById(Guid id);
    }
}