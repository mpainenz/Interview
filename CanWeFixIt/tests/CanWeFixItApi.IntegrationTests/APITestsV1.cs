using System;
using System.Net.Http;

using Microsoft.AspNetCore.Mvc.Testing;

using Xunit;

using CanWeFixIt.Common;

namespace CanWeFixItApi.IntegrationTests.V1
{

    public class APITests : IClassFixture<WebApplicationFactory<CanWeFixItApi.Startup>>
    {

        private readonly WebApplicationFactory<CanWeFixItApi.Startup> _factory;

        public APITests(WebApplicationFactory<CanWeFixItApi.Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("v1/", HTTPVerbEnum.GET, System.Net.HttpStatusCode.NotFound)]
        [InlineData("v1/badendpoint/", HTTPVerbEnum.GET, System.Net.HttpStatusCode.NotFound)] // Test unknown endpoint

        [InlineData("v1/instruments/", HTTPVerbEnum.GET, System.Net.HttpStatusCode.OK)]
        [InlineData("v1/instruments/", HTTPVerbEnum.POST, System.Net.HttpStatusCode.MethodNotAllowed)] // Test unexpected http method

        [InlineData("v1/marketdata/", HTTPVerbEnum.GET, System.Net.HttpStatusCode.OK)]
        [InlineData("v1/marketdata/", HTTPVerbEnum.POST, System.Net.HttpStatusCode.MethodNotAllowed)] // Test unexpected http method

        [InlineData("v1/valuations/", HTTPVerbEnum.GET, System.Net.HttpStatusCode.OK)]
        [InlineData("v1/valuations/", HTTPVerbEnum.POST, System.Net.HttpStatusCode.MethodNotAllowed)] // Test unexpected http method
        
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