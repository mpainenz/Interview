using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;

using CanWeFixItApi;

namespace CanWeFixItApi.IntegrationTests {

    public class TestContext {
        
        public HttpClient Client { get; private set; }

        public TestContext(WebApplicationFactory<CanWeFixItApi.Startup> factory) {
            Client = factory.CreateClient();
            Client.BaseAddress = new Uri("https://localhost:5010");
        }

    }
}
