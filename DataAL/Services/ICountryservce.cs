using DataAL.Entities.Models;
using DataAL.Page;

namespace DataAL.Services
{
    public interface ICountryservce
    {
        PagedList<CountryDto> GetCountries(CountryParameters countryParameters);
        CountryDto GetCountry(string ContryName);
        bool AddCountry(string ContryName);
        bool DeleteCoutry(string ContryName);
        bool CountryExists(string CountryName);
        bool Save();
    }
}
