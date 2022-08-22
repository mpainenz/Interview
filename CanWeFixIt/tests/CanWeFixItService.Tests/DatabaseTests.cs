using System.Threading.Tasks;
using Xunit;
using System;

namespace CanWeFixItService.Tests
{

    public class DatabaseFixture : IDisposable
    {

        public DatabaseService DBService;

        public DatabaseFixture()
        {
            DBService = new DatabaseService();
            DBService.SetupDatabase();
        }

        public void Dispose()
        {
            // Clean up
        }

    }

    public class DatabaseTests : IClassFixture<DatabaseFixture>
    {
        private DatabaseFixture _dbFixture;

        public DatabaseTests(DatabaseFixture dbFixture)
        {
            this._dbFixture = dbFixture;
        }

        [Theory]
        [InlineData(1000)] // Crashes without semaphore
        // [InlineData(1000000)] // Interesting test case, but too slow for day-to-day testing
        public async Task TestParrelelConnections(int taskCount)
        {
            var tasks = new Task[taskCount];
            for (int i = 0; i < taskCount; i++)
            {
                tasks[i] = Task.Run(async () => 
                {
                    await this._dbFixture.DBService.Instruments();
                }
                );
            }
            await Task.WhenAll(tasks);
        }

        # region Instruments
        [Fact]
        public async Task withInstuments_ShouldReturnItems()
        {
            // Arrange

            // Act
            var instruments = await this._dbFixture.DBService.Instruments();

            // Assert
            Assert.NotEmpty(instruments);
        }

        [Fact]
        public async Task withInstuments_ShouldReturnOnlyActiveItems()
        {
            // Arrange

            // Act
            var instruments = await this._dbFixture.DBService.Instruments();

            // Assert
            foreach (var instrument in instruments)
            {
                Assert.True(instrument.Active);
            }
        }
        #endregion

        # region MarketData
        [Fact]
        public async Task withMarketData_ShouldReturnItems()
        {
            // Arrange

            // Act
            var marketData = await this._dbFixture.DBService.MarketData();

            // Assert
            Assert.NotEmpty(marketData);
        }

        [Fact]
        public async Task withMarketData_ShouldReturnOnlyActiveItems()
        {
            // Arrange

            // Act
            var marketData = await this._dbFixture.DBService.MarketData();

            // Assert
            foreach (var md in marketData)
            {
                Assert.True(md.Active);
            }
        }
        #endregion

        # region MarketValuation
        [Fact]
        public async Task withMarketValuation_ShouldReturnItems()
        {
            // Arrange

            // Act
            var marketValuation = await this._dbFixture.DBService.MarketValuation();

            // Assert
            Assert.NotEmpty(marketValuation);
        }


        [Fact]
        public async Task withMarketValuation_ShouldReturnOnlyOneItem()
        {
            // Arrange

            // Act
            var marketValuation = await this._dbFixture.DBService.MarketValuation();

            // Assert
            Assert.Single(marketValuation);
        }
        #endregion

    }
}