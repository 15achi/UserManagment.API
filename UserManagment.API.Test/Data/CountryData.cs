using DataAL.Entities.Models;
using DataAL.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagment.API.Test.Data
{
    public  class CountryData
    {
        
        public static PagedList<CountryDto> GetCountryDto()
        {
            var item1 = CountryData.CountryParam();
           
            return PagedList<CountryDto>.ToPagedList(ListOfCountry(), item1.PageNamber, item1.PageSize);
        }
        public static List<CountryDto> EmptyCountryDto()
        {
            return new List<CountryDto>();
        }

        public static CountryDto AddCountryDto()
        {
            return new CountryDto
            {
                Name = "Japan"
            };
        }

        public static CountryParameters CountryParam()
        {
            return new CountryParameters();
           
        }

        public static IQueryable<CountryDto> ListOfCountry()
        {
            var item = new List<CountryDto>
            {
                new CountryDto
                {
                    Id=1,
                    Name="India",
                },
                  new CountryDto
                {
                    Id=2,
                    Name="Georgia"
                },
                  new CountryDto
                {
                   Id=3,
                    Name="Usa"
                }
            };

            var ListOfCountry = item.AsQueryable();
            return ListOfCountry;
        }
    }
}
