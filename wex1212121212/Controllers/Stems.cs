using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace wex1212121212.Controllers
{
    [Route("")]
    [ApiController]
    public class StemsController : Controller
    {
        private static readonly HttpClient client = new HttpClient();

        private HttpResponseMessage result;

        private const string uri = "https://raw.githubusercontent.com/qualified/challenge-data/master/words_alpha.txt";
        private string[] listOfWords;
        public StemsController()
        {
            result = client.GetAsync(uri).Result;
            if (result.IsSuccessStatusCode)
            {
                listOfWords = result.Content.ReadAsStringAsync().Result.Split('\n');
            }
        }

        [HttpGet]
        public IActionResult Get(string stream)
        {
            if (result.IsSuccessStatusCode)
            {
                List<string> listOfstream = new List<string>();
                listOfstream = listOfWords.Where(x => x.StartsWith(stream)).ToList();
                return Ok(listOfstream);
            }

            return BadRequest();
        }
    }
}
