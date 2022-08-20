using System.Threading.Tasks;
using Xunit;
using System;

namespace CanWeFixItService.Tests
{

    public class DatabaseTests : IDisposable
    {

        private DatabaseService _databaseService;

        public DatabaseTests()
        {
            _databaseService = new DatabaseService();
        }

        public void Dispose()
        {
            // _databaseService.Dispose();
        }

        [Fact]
        public async Task withInstuments_ShouldReturnItems()
        {
            // Arrange

            // Act
            var instruments = await _databaseService.Instruments();

            // Assert
            Assert.NotEmpty(instruments);
        }

        [Fact]
        public async Task withMarketData_ShouldReturnItems()
        {
            // Arrange

            // Act
            var marketData = await _databaseService.MarketData();

            // Assert
            Assert.NotEmpty(marketData);
        }


    }
}