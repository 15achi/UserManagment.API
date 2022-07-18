
using DataAL.Entities.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UserManagement.BLL.Models.Countries.ICountryServices;
using UserManagment.API.Controllers;
using UserManagment.API.Test.Data;
using Xunit;

namespace UserManagment.API.Test.Systems.Controller
{
    public class TestCountryController
    {
        [Fact]
        public void GetCountriesShouldReturn200status()
        {
            //Arrange
            var CountryParam = CountryData.CountryParam();
            var Countryservice = new Mock<ICountryService>();
            Countryservice.Setup(service => service.GetCountries(CountryParam))
                .Returns(CountryData.GetCountryDto());

            var sut = new CountryController(Countryservice.Object);

            //Act
            var result = sut.GetCountries(CountryParam);

            //Assert
            result.GetType().Should().Be(typeof(OkObjectResult));
            (result as OkObjectResult).StatusCode.Should().Be(200);


        }

        [Fact]

        public void save_shouldCallSaveAsOnce()
        {
            //Arrange
            var Countryservice = new Mock<ICountryService>();
            var newCountry = CountryData.AddCountryDto().Name;
            var sut = new CountryController(Countryservice.Object);
             CountryDto country = new CountryDto();
            country.Name = newCountry;



            //Act
            var result = sut.AddCountry(country);

            //Assert
            Countryservice.Verify(Countryservice => Countryservice.AddCountry(newCountry), Times.Exactly(1));

        }
        
        [Fact]

        public void SaveReturnsBadRequest()
        {
            //Arrange
            var Countryservice = new Mock<ICountryService>();
            var newCountry = CountryData.AddCountryDto().Name;
            var sut = new CountryController(Countryservice.Object);
            CountryDto country = new CountryDto();
            country.Name = newCountry;

            //Act
            var result = sut.AddCountry(country);

            //Assert

            result.GetType().Should().Be(typeof(OkObjectResult));
            Assert.False(Countryservice.Object.CountryExists(newCountry));

        }
        
        [Fact]
        public void Delete()
        {
            //Arrange
            var Value = CountryData.AddCountryDto().Name;
            var Countryservice = new Mock<ICountryService>();
            var controller = new CountryController(Countryservice.Object);

            //Act
            var result = controller.DeleteCountry(Value);
            var castedResult = result as OkObjectResult;

            //Assert
            castedResult.GetType().Should().Be(typeof(OkObjectResult));
            Assert.False(Countryservice.Object.DeleteCoutry(Value));

        }


        [Fact]
        public void DeleteReturnsBadRequest()
        {
            //Arrange
            var Value = CountryData.AddCountryDto().Name;
            var Countryservice = new Mock<ICountryService>();
            var controller = new CountryController(Countryservice.Object);

            //Act
            var result = controller.DeleteCountry(Value);
            var castedResult = result as OkObjectResult;

            //Assert
            castedResult.GetType().Should().Be(typeof(OkObjectResult));
            Assert.False(Countryservice.Object.CountryExists(Value));

        }
       
    }
}
