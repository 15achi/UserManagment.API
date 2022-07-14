using DataAL.Entities.Models;
using DataAL.Page;

namespace UserManagement.BLL.Models.Countries.ICountryServices
{
    public  interface ICountryService
    {
        PagedList<CountryDto> GetCountries(CountryParameters countryParameters);
        CountryDto GetCountry(string ContryName);
        bool AddCountry(string ContryName);
        bool DeleteCoutry(string ContryName);
        bool CountryExists(string CountryName);

    }
}
