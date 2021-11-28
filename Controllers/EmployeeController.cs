using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<EmployeeController> logger;

        public EmployeeController(IHttpClientFactory httpClientFactory,ILogger<EmployeeController> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<Employee>> GetEmployee()
        {
            var client = new HttpClient();
            client.BaseAddress = new System.Uri("http://40.88.216.141");

            //using var httpClient = httpClientFactory.CreateClient("Employee");
            //var fullAddress = httpClient.BaseAddress + "api/Employee";
            //logger.LogInformation(fullAddress);
            var response=await client.GetAsync(client.BaseAddress + "api/Employee");
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Employee>>(stream);
        }
    }

    public class Employee
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string OfficeLocation { get; set; }

        public int ZipCode { get; set; }
    }
}
