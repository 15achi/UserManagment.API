
using DataAL.Services;
using UserManagement.BLL.Models.Countries.ICountryServices;
using DataAL.Entities.Models;
using DataAL.Page;

namespace UserManagement.BLL.Models.Countries.CountryServices
{
    public class CountryService : ICountryService
    {
        private readonly ICountryservce _countryservce;

        public CountryService(ICountryservce countryservce)
        {
            _countryservce = countryservce;
        }

        public bool AddCountry(string ContryName)
        {
          return  _countryservce.AddCountry(ContryName.Trim());
        }

        public bool CountryExists(string CountryName)
        {
            return _countryservce.CountryExists(CountryName.Trim());
        }

        public bool DeleteCoutry(string CountryName)
        {
            return _countryservce.DeleteCoutry(CountryName.Trim());
        }

        public PagedList<CountryDto> GetCountries(CountryParameters countryParameters)
        {
            return _countryservce.GetCountries(countryParameters);
        }

        public CountryDto GetCountry(string ContryName)
        {
            return _countryservce.GetCountry(ContryName.Trim());
        }
    }
}
