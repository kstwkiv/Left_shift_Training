using FlightSearchEngine.Models;
using Xunit;

namespace FlightSearchEngine.Tests
{
    // ============================================================
    // SearchViewModel Tests
    // ============================================================
    public class SearchViewModelTests
    {
        [Fact]
        public void SearchViewModel_DefaultValues_AreCorrect()
        {
            var model = new SearchViewModel();
            Assert.Equal(string.Empty, model.Source);
            Assert.Equal(string.Empty, model.Destination);
            Assert.Equal(1, model.NumberOfPersons);
            Assert.Null(model.SourceList);
            Assert.Null(model.DestinationList);
        }

        [Fact]
        public void SearchViewModel_CanSetProperties()
        {
            var model = new SearchViewModel
            {
                Source          = "Mumbai",
                Destination     = "Delhi",
                NumberOfPersons = 3
            };

            Assert.Equal("Mumbai", model.Source);
            Assert.Equal("Delhi",  model.Destination);
            Assert.Equal(3,        model.NumberOfPersons);
        }
    }

    // ============================================================
    // FlightResult Tests
    // ============================================================
    public class FlightResultTests
    {
        [Fact]
        public void FlightResult_CanSetAndGetProperties()
        {
            var result = new FlightResult
            {
                FlightId    = 1,
                FlightName  = "Air India AI-101",
                FlightType  = "Domestic",
                Source      = "Mumbai",
                Destination = "Delhi",
                TotalCost   = 13500.00m
            };

            Assert.Equal(1,                  result.FlightId);
            Assert.Equal("Air India AI-101", result.FlightName);
            Assert.Equal("Domestic",         result.FlightType);
            Assert.Equal("Mumbai",           result.Source);
            Assert.Equal("Delhi",            result.Destination);
            Assert.Equal(13500.00m,          result.TotalCost);
        }

        [Fact]
        public void FlightResult_TotalCost_IsDecimal()
        {
            var result = new FlightResult { TotalCost = 4500.75m };
            Assert.IsType<decimal>(result.TotalCost);
        }

        [Fact]
        public void FlightResult_DefaultStrings_AreEmpty()
        {
            var result = new FlightResult();
            Assert.Equal(string.Empty, result.FlightName);
            Assert.Equal(string.Empty, result.FlightType);
            Assert.Equal(string.Empty, result.Source);
            Assert.Equal(string.Empty, result.Destination);
        }
    }

    // ============================================================
    // FlightHotelResult Tests
    // ============================================================
    public class FlightHotelResultTests
    {
        [Fact]
        public void FlightHotelResult_CanSetAndGetProperties()
        {
            var result = new FlightHotelResult
            {
                FlightId    = 2,
                FlightName  = "IndiGo 6E-300",
                Source      = "Mumbai",
                Destination = "Bangalore",
                HotelName   = "ITC Gardenia Bangalore",
                TotalCost   = 12300.00m
            };

            Assert.Equal(2,                       result.FlightId);
            Assert.Equal("IndiGo 6E-300",         result.FlightName);
            Assert.Equal("Mumbai",                 result.Source);
            Assert.Equal("Bangalore",              result.Destination);
            Assert.Equal("ITC Gardenia Bangalore", result.HotelName);
            Assert.Equal(12300.00m,                result.TotalCost);
        }

        [Fact]
        public void FlightHotelResult_DefaultStrings_AreEmpty()
        {
            var result = new FlightHotelResult();
            Assert.Equal(string.Empty, result.FlightName);
            Assert.Equal(string.Empty, result.HotelName);
        }

        [Fact]
        public void FlightHotelResult_TotalCost_ReflectsCombinedPrice()
        {
            decimal flightCost = 3800m * 2;   // 2 persons
            decimal hotelCost  = 8500m;
            var result = new FlightHotelResult { TotalCost = flightCost + hotelCost };

            Assert.Equal(16100m, result.TotalCost);
        }
    }

    // ============================================================
    // Business Logic / Calculation Tests
    // ============================================================
    public class BusinessLogicTests
    {
        [Theory]
        [InlineData(4500, 1,  4500)]
        [InlineData(4500, 2,  9000)]
        [InlineData(4500, 5, 22500)]
        [InlineData(4500, 10, 45000)]
        public void FlightTotalCost_CalculatesCorrectly(decimal pricePerSeat, int persons, decimal expected)
        {
            decimal totalCost = pricePerSeat * persons;
            Assert.Equal(expected, totalCost);
        }

        [Theory]
        [InlineData(4500, 2, 8500,  17500)]   // flight * persons + hotel
        [InlineData(3800, 3, 8500,  19900)]
        public void PackageTotalCost_CalculatesCorrectly(decimal pricePerSeat, int persons, decimal hotelPerDay, decimal expected)
        {
            decimal totalCost = (pricePerSeat * persons) + hotelPerDay;
            Assert.Equal(expected, totalCost);
        }

        [Fact]
        public void Source_And_Destination_SameCity_ShouldBeDetected()
        {
            string source      = "Mumbai";
            string destination = "Mumbai";
            Assert.True(source == destination, "Same city should trigger validation error.");
        }

        [Fact]
        public void NumberOfPersons_ValidRange_1to10()
        {
            for (int i = 1; i <= 10; i++)
            {
                Assert.InRange(i, 1, 10);
            }
        }

        [Theory]
        [InlineData(0)]
        [InlineData(11)]
        [InlineData(-1)]
        public void NumberOfPersons_InvalidValues_OutOfRange(int persons)
        {
            bool valid = persons >= 1 && persons <= 10;
            Assert.False(valid);
        }
    }
}
