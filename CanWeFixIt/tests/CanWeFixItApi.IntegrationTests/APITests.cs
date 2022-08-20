using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;

using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace CanWeFixItApi.IntegrationTests
{

    public class APITests : IClassFixture<WebApplicationFactory<CanWeFixItApi.Startup>>
    {

        private readonly WebApplicationFactory<CanWeFixItApi.Startup> _factory;

        public APITests(WebApplicationFactory<CanWeFixItApi.Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        // Invalid
        [InlineData("v1/", System.Net.HttpStatusCode.NotFound)]
        [InlineData("v1/badendpoint/", System.Net.HttpStatusCode.NotFound)]
        // Valid
        [InlineData("v1/instruments/", System.Net.HttpStatusCode.OK)]
        //TODO - Fix this test
        // [InlineData("v1/marketdata/", System.Net.HttpStatusCode.OK)]

        public async void Test_API_Endpoint_Connectivity(string endpoint, System.Net.HttpStatusCode expectedStatusCode)
        {
            {
                //Arrange
                var context = new TestContext(_factory);

                //Act
                var response = await context.Client.GetAsync(endpoint);

                //Assert
                Assert.Equal(expectedStatusCode, response.StatusCode);
            }

        }
    }

}