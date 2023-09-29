using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace NWR_Middleware
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string ipAddress = "10.238.9.211"; // Replace with DRS IP address
            int port = 8206; // Replace with DRS port
            string hostIp = ConfigurationManager.AppSettings["IP"];
            int hostPort = int.Parse(ConfigurationManager.AppSettings["Port"]);
            string prefix = $"http://{hostIp}:{hostPort}/";


            // Start listening for UGS requests
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add(prefix); // Replace with your desired URL
            listener.Start();

            Console.WriteLine("Middleware is running. Listening for requests...");
            Console.WriteLine(prefix);

            while (true)
            {
                try
                {
                    // Wait for a UGS request
                    HttpListenerContext context = await listener.GetContextAsync();

                    // Create a new thread to handle the client
                    Thread clientThread = new Thread(() => HandleRequest(context, ipAddress, port));
                    clientThread.Start();
                }
                catch (SocketException ex)
                {
                    Console.WriteLine($"SocketException: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                }
            }
        }


        static async Task HandleRequest(HttpListenerContext context, string ipAddress, int port)
        {
            try
            {
                // Read the UGS request as bytes 
                using (BinaryReader reader = new BinaryReader(context.Request.InputStream))
                {
                    byte[] ugsRequestBytes = new byte[context.Request.ContentLength64];
                    reader.Read(ugsRequestBytes, 0, (int)context.Request.ContentLength64);

                    // Decode the request as a string for logging
                    string ugsRequestString = Encoding.UTF8.GetString(ugsRequestBytes);
                    Console.WriteLine("XML: " + ugsRequestString);
                    // check for error / rejection
                    XmlDocument xmldoc = new XmlDocument();
                    // * xmldoc.LoadXml(strResponse)
                    xmldoc.LoadXml(ugsRequestString);

                    XmlNode xmlnode;
                    XmlNode fullRspNode = xmldoc.SelectSingleNode("//fullRsp");
                    XmlNode uidNode = xmldoc.SelectSingleNode("//uid");
                    XmlNode upwNode = xmldoc.SelectSingleNode("//upw");

                    if (fullRspNode != null && uidNode != null && upwNode != null)
                    {
                        string fullRsp = fullRspNode.InnerText;
                        string uid = uidNode.InnerText;
                        string upw = upwNode.InnerText;
                        Console.WriteLine($"fullRsp: {fullRsp}, uid: {uid}, upw: {upw}");
                        //Console.WriteLine($"uid: {uid}, upw: {upw}");
                    }
                    else
                    {
                        Console.WriteLine("uid and/or upw elements not found in the XML.");
                    }

                    // Forward the original byte request to DRS
                    string drsResponseString = "hihihi";
                    //await ForwardToDRS(ugsRequestBytes, ipAddress, port);

                    //string drsResponseString = "";
                    if (drsResponseString != null || !drsResponseString.Equals(""))
                    {
                        Console.WriteLine("Response from server: "+drsResponseString);
                        // Convert the DRS response to a string for logging
                        //drsResponseString = Encoding.UTF8.GetString(drsResponseBytes);
                    }
                    else
                    {
                        Console.WriteLine("No response from server");
                    }

                    // Log the communication
                    LogRequestResponse(ugsRequestString, drsResponseString);

                    byte[] b = Encoding.UTF8.GetBytes(drsResponseString);
                    Console.WriteLine("byte: " + b );
                    // Send the DRS response back to UGS as bytes
                    context.Response.OutputStream.Write(b, 0, b.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error handling request: {ex.Message}");
                // Handle errors and log them
            }
        }

        static async Task<string> ForwardToDRS(byte[] request, string ipAddress, int port)
        {
            //convert bytes to request xml
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    UriBuilder ub = new UriBuilder("https", ipAddress, port, "DRS");
                    string drsUrl = $"http://{ipAddress}:{port}/DRS"; // Adjust the URL https://10.238.9.187:8080/DRS

                    HttpWebRequest hwrDRSRequest = (HttpWebRequest)HttpWebRequest.Create(ub.Uri);
                    hwrDRSRequest.Method = "POST";
                    hwrDRSRequest.KeepAlive = true;

                    string strCertPath = ConfigurationManager.AppSettings["Path"];
                    string strCertPass = ConfigurationManager.AppSettings["Pass"];
                    // Attach client certificate
                    hwrDRSRequest.ClientCertificates.Add(new X509Certificate2(strCertPath, strCertPass));

                    // Set the content type of the data being posted
                    hwrDRSRequest.ContentType = "application/x-www-form-urlencoded";

                    // Set the content length of the string being posted
                    hwrDRSRequest.ContentLength = request.Length;

                    Stream newStream = hwrDRSRequest.GetRequestStream();
                    newStream.Write(request, 0, request.Length);

                    // Sends the request and waits for a response
                    HttpWebResponse hwrDRSResponse = (HttpWebResponse)hwrDRSRequest.GetResponse();
                    //var content = new ByteArrayContent(request);
                    //HttpResponseMessage response = await httpClient.PostAsync(drsUrl, content);

                    // Get HTTP response
                    if (hwrDRSResponse.StatusCode != HttpStatusCode.OK)
                        throw new ApplicationException("Invalid response status code. Encountered: " + hwrDRSResponse.StatusCode.ToString());

                    //if (response.IsSuccessStatusCode)
                    //{
                    //    return await response.Content.ReadAsByteArrayAsync();
                    //}
                    //else
                    //{
                    //    Console.WriteLine($"DRS request failed with status code: {response.StatusCode}");
                    //    return null;
                    //}
                    // Get the stream associated with the response.
                    Stream receiveStream = hwrDRSResponse.GetResponseStream();
                    //byte[] b = receiveStream;
                    Encoding encoding = Encoding.UTF8;
                    // Pipes the stream to a higher level stream reader with the required encoding format. 
                    StreamReader readStream = new StreamReader(receiveStream, ASCIIEncoding.UTF8);
                    string strResponse = readStream.ReadToEnd();
                    readStream.Close();
                    return strResponse;
                }
            }
            catch (ApplicationException ex)
            {
                Console.WriteLine($"Error forwarding request to DRS: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error forwarding request to DRS: {ex.Message}");
                return null;
            }
        }

        static void LogRequestResponse(string request, string response)
        {
            // Implement your logging logic here, e.g., write to a file
            //File.WriteAllText("log.txt", $"Request:\n{request}\nResponse:\n{response}\n\n");
            Console.WriteLine("log.txt", $"Request:\n{request}\nResponse:\n{response}\n\n");
        }
    }
}
