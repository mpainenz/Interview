using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Dapper;
using Microsoft.Data.Sqlite;

namespace CanWeFixItService
{
    public class DatabaseService : IDatabaseService
    {
        // See SQLite In-Memory example:
        // https://github.com/dotnet/docs/blob/main/samples/snippets/standard/data/sqlite/InMemorySample/Program.cs
        
        // Using a name and a shared cache allows multiple connections to access the same
        // in-memory database
        
        private SqliteConnection _connection;
        private SemaphoreSlim _semaphore = new SemaphoreSlim(1);

        public DatabaseService()
        {
            // The in-memory database only persists while a connection is open to it. To manage
            // its lifetime, keep one open connection around for as long as you need it.

            // Note, that because the Integration Testing spins up concurrent, parrelell tests and each has it's own connection, it's not possible to share the same in-memory database between tests.
            var dataSourceGuid = Guid.NewGuid().ToString();
            var connectionString = $"Data Source=DatabaseService-{dataSourceGuid};Mode=Memory;Cache=Shared";

            _connection = new SqliteConnection(connectionString);
            _connection.Open();
        
        }
        
        public async Task<IEnumerable<Instrument>> Instruments()
        {
            await _semaphore.WaitAsync();
            try {
                return await _connection.QueryAsync<Instrument>("SELECT * FROM instrument WHERE Active = 0");
            } finally {
                _semaphore.Release();
            }
        }

        public async Task<IEnumerable<MarketData>> MarketData()
        {
            await _semaphore.WaitAsync();
            try {
                return await _connection.QueryAsync<MarketData>("SELECT Id, DataValue FROM marketdata WHERE Active = 0");
            } finally {
                _semaphore.Release();
            }
        }

        /// <summary>
        /// This is complete and will correctly load the test data.
        /// It is called during app startup 
        /// </summary>
        public void SetupDatabase()
        {
            const string createInstruments = @"
                CREATE TABLE instrument
                (
                    id     int,
                    sedol  text,
                    name   text,
                    active int
                );
                INSERT INTO instrument
                VALUES (1, 'Sedol1', 'Name1', 0),
                    (2, 'Sedol2', 'Name2', 1),
                    (3, 'Sedol3', 'Name3', 0),
                    (4, 'Sedol4', 'Name4', 1),
                    (5, 'Sedol5', 'Name5', 0),
                    (6, '', 'Name6', 1),
                    (7, 'Sedol7', 'Name7', 0),
                    (8, 'Sedol8', 'Name8', 1),
                    (9, 'Sedol9', 'Name9', 0)";

            _connection.Execute(createInstruments);
            
            const string createMarketData = @"
                CREATE TABLE marketdata
                (
                    id        int,
                    datavalue int,
                    sedol     text,
                    active    int
                );
                INSERT INTO marketdata

                VALUES (1, 1111, 'Sedol1', 0),
                    (2, 2222, 'Sedol2', 1),
                    (3, 3333, 'Sedol3', 0),
                    (4, 4444, 'Sedol4', 1),
                    (5, 5555, 'Sedol5', 0),
                    (6, 6666, 'Sedol6', 1)";

            _connection.Execute(createMarketData);
        }
    }
}