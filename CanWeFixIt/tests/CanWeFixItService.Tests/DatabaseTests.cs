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
        [InlineData(1000000)]
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
        public async Task withMarketData_ShouldReturnItems()
        {
            // Arrange

            // Act
            var marketData = await this._dbFixture.DBService.MarketData();

            // Assert
            Assert.NotEmpty(marketData);
        }


    }
}