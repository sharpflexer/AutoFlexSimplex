using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AF_RobotService.Models
{
    public class Request
    {
        public static string Send(string uri, string requestMethod, string jsonData = "")
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(uri);
            builder.Append(jsonData);
            WebRequest request = WebRequest.Create(builder.ToString());
            request.Method = requestMethod;
            WebResponse response = request.GetResponse();

            String json = null;
            using (Stream dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Display the content.
                json = responseFromServer;
            }
            // Close the response.
            response.Close();
            return json;
        }
    }
}

