using DataAL.Entities.Models;
using DataAL.Page;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UserManagement.BLL.Models.Countries.ICountryServices;

namespace UserManagment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryServices;

        public CountryController(ICountryService countryServices)
        {
            _countryServices = countryServices;
        }

        [HttpGet]
       // [Route("GetCountries")]
        //[Authorize(Roles = "Admin,User")]
        public IActionResult GetCountries([FromQuery] CountryParameters countryParameters)
        {
            var countryies = _countryServices.GetCountries(countryParameters);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var metadata = new
            {
                countryies.TotalCount,
                countryies.PageSize,
                countryies.CurrentPage,
                countryies.hasNext,
                countryies.HasPrevious
            };
            //Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(countryies);
        }


        [HttpGet("{CountryName}")]
        //[Authorize(Roles = "Admin,User")]
        public IActionResult GetCountry(string CountryName)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(!_countryServices.CountryExists(CountryName))
            {
                return Ok(new { message = "ამ დასახელების  ქვეყანა არ არის" });
            }

            var country = _countryServices.GetCountry(CountryName);
            return Ok(country);

        }

        [HttpPost]
       // [Authorize(Roles = "Admin")]
        public IActionResult AddCountry(CountryDto CountryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_countryServices.CountryExists(CountryDto.Name))
            {
                return Ok(new { message = "ამ დასახელების  ქვეყანა უკვე არსებობს" });
            }

            _countryServices.AddCountry(CountryDto.Name);
            return Ok(new { message = "დაემატა ქვეყანის დასახელება" });

        }



        [HttpDelete("{CountryName}")]
      //  [Authorize(Roles = "Admin")]
        public IActionResult DeleteCountry(string CountryName)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_countryServices.CountryExists(CountryName))
            {
                return Ok(new { message = "ამ დასახელების  ქვეყანა არ არსებობს" });
            }

            _countryServices.DeleteCoutry(CountryName);
            return Ok(new { message = "ქვეყნის სახელწოდება წაშლილია" });

        }

    }
}
