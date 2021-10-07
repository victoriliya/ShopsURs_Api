using FakeItEasy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ShopsRUs.Controllers;
using ShopsRUs.Data;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShopsRUs.Testing
{
    public class CustomerControllerTests : IDisposable
    {

        protected TestServer _testServer;

        public CustomerControllerTests()
        {

            var webBuilder = new WebHostBuilder();
            webBuilder.UseStartup<Startup>();

            _testServer = new TestServer(webBuilder);
        }

        public void Dispose()
        {
            _testServer.Dispose();
        }


        [Fact]
        public async Task TestReadMethod()
        {
            // Arrange
            var response = await _testServer.CreateRequest("api/Customere").SendAsync("GET");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            response = await _testServer.CreateRequest("api/Customeresdvwe").SendAsync("GET");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

/*            response = await _testServer.CreateRequest("api/Customer/1").SendAsync("GET");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);*/


        }


/*        [Fact]
        public async Task CreateCustomers()
        {
            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/league");
            request.Content = new StringContent(JsonConvert.SerializeObject( new Dictionary<string, string> 
            {
                { "FirstName" , "Freddie"},
                { "LastName", "Krugger"}
            }), Encoding.Default, "application/json");

            var client = _testServer.CreateClient();

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Arrange

            // Act 
            var response = await client.SendAsync(request);


            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }*/
    }
}
