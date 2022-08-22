using System;
using System.Net.Http;

using Microsoft.AspNetCore.Mvc.Testing;

using Xunit;

using CanWeFixIt.Common;

namespace CanWeFixItApi.IntegrationTests.V2
{

    public class APITestsV2 : IClassFixture<WebApplicationFactory<CanWeFixItApi.Startup>>
    {

        private readonly WebApplicationFactory<CanWeFixItApi.Startup> _factory;

        public APITestsV2(WebApplicationFactory<CanWeFixItApi.Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("v2/", HTTPVerbEnum.GET, System.Net.HttpStatusCode.NotFound)]
        [InlineData("v2/badendpoint/", HTTPVerbEnum.GET, System.Net.HttpStatusCode.NotFound)] // Test unknown endpoint

        [InlineData("v2/marketdata/", HTTPVerbEnum.GET, System.Net.HttpStatusCode.OK)]
        [InlineData("v2/marketdata/", HTTPVerbEnum.POST, System.Net.HttpStatusCode.MethodNotAllowed)] // Test unexpected http method

        public async void Test_API_Endpoint_Connectivity(string endpoint, HTTPVerbEnum httpVerb, System.Net.HttpStatusCode expectedStatusCode, HttpContent content = null)
        {
            //Arrange
            var context = new TestContext(_factory);

            //Act
            var response = httpVerb switch
            {
                HTTPVerbEnum.GET => await context.Client.GetAsync(endpoint),
                HTTPVerbEnum.POST => await context.Client.PostAsync(endpoint, content),
                HTTPVerbEnum.PUT => await context.Client.PutAsync(endpoint, content),
                HTTPVerbEnum.DELETE => await context.Client.DeleteAsync(endpoint),
                _ => throw new ArgumentException("Invalid HTTP verb", nameof(httpVerb))
            };

            //Assert
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }
    }

}