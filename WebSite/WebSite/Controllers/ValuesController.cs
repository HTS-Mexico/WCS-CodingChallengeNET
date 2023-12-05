using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;

namespace WebSite.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IHttpActionResult Get()
        {
            string value = Request.Content.ReadAsStringAsync().Result;
            string encodedValue = System.Web.HttpUtility.UrlEncode(value);
            var data = ReadTextFile("{path}");
            return Redirect("https://localhost:{port}/PrintValue.aspx?value=" + data);
        }

        // POST api/values
        [HttpPost]
        public IHttpActionResult Post()
        {
            string value = Request.Content.ReadAsStringAsync().Result;
            string encodedValue = System.Web.HttpUtility.UrlEncode(value);
            var data = ReadTextFile("{path}");
            return Redirect("https://localhost:{port}/PrintValue.aspx?value=" + data);
        }

        static string ReadTextFile(string filePath)
        {
            string content = string.Empty;

            // Check if the file exists
            if (File.Exists(filePath))
            {
                // Try to read the content of the file
                try
                {
                    content = File.ReadAllText(filePath);
                }
                catch (IOException e)
                {
                    // Handle any read error
                    throw new IOException($"Error reading the file: {e.Message}");
                }
            }
            else
            {
                // The file doesn't exist
                throw new FileNotFoundException($"File not found at the specified path: {filePath}");
            }

            return content;
        }
    }
}
