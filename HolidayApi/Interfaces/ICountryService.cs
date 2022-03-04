﻿using HolidayApi.BLL.Dtos;

namespace HolidayApi.Interfaces
{
    public interface ICountryService
    {
        void AddCountriesToDb(IEnumerable<CountryDto> countriesDto);
        Task<IEnumerable<CountryDto>> GetCountries();
        IEnumerable<CountryDto> GetCountriesFromDb();
    }
}