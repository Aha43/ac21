// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var hc = new HttpClient();
var r = await hc.GetAsync("https://adventofcode.com/2021/day/1/input")
    .ConfigureAwait(continueOnCapturedContext: false);
if (r.IsSuccessStatusCode)
{
    var input = r.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false);
}
else
{
    Console.WriteLine("Input read failed");
    Console.WriteLine(r.StatusCode);
}
