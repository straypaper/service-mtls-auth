using System.Security.Cryptography.X509Certificates;

Console.WriteLine("Press ENTER to send the request...");
Console.ReadLine();

var handler = new HttpClientHandler();
handler.ClientCertificates.Add(new X509Certificate2(@"cert.pfx", "password"));

using var client = new HttpClient(handler);

var result = await client.GetAsync("https://localhost:7166/customers");

if (result.IsSuccessStatusCode)
    Console.WriteLine(await result.Content.ReadAsStringAsync());
else
    throw new Exception($"Status code: {result.StatusCode}");

Console.WriteLine("Done.");
Console.ReadLine();
