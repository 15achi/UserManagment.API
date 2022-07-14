using DataAL.Entities.Models;
using DataAL.Page;
using DataAL.Services;


namespace DataAL.Repository
{
    public class Countryservce : ICountryservce
    {
        private readonly DataContext _Context;

        public Countryservce(DataContext Context)
        {
            _Context = Context;
        }

        public bool AddCountry(string ContryName)
        {
            var country = new Country();
            country.Name = ContryName;

            _Context.Countries.Add(country);
            return Save();
        }

        public bool CountryExists(string CountryName)
        {
            return _Context.Countries.Any(x => x.Name.ToLower() == CountryName.Trim().ToLower());
        }

        public bool DeleteCoutry(string ContryName)
        {
            var country = _Context.Countries.First(x=>x.Name.ToLower()== ContryName.Trim().ToLower());

            _Context.Countries.Remove(country);
            return Save();
        }

        public PagedList<CountryDto> GetCountries(CountryParameters countryParameters)
        {

            return PagedList<CountryDto>.ToPagedList(ListOfCountry(),countryParameters.PageNamber, countryParameters.PageSize);
        }

        public CountryDto GetCountry(string ContryName)
        {
            return ListOfCountry().First(x => x.Name.ToLower() == ContryName.Trim().ToLower());
        }

        public IQueryable<CountryDto> ListOfCountry()
        {
            var ListOfCountry = (from C in _Context.Countries                            
                                  where C.Active == 1
                                  select new
                               {
                                   id=C.Id,
                                   name = C.Name
                               }).Select(x => new CountryDto
                               {                                 
                                    Id=x.id,
                                    Name=x.name
                               });
            return ListOfCountry;
        }

        public bool Save()
        {
            var saved = _Context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
