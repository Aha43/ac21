var hc = new HttpClient();
var inputUri = (args.Length == 0 || args[0] != "e") ? "https://raw.githubusercontent.com/Aha43/ac21/main/21/1/Input.txt" : "https://raw.githubusercontent.com/Aha43/ac21/main/21/1/example.txt";
var r = await hc.GetAsync(inputUri)
    .ConfigureAwait(continueOnCapturedContext: false);
if (r.IsSuccessStatusCode)
{
    var input = (await r.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false)).Split().Select(e => int.Parse(e)).ToArray();
    var n = input.Length;
    var c = 0;
    if (n > 1)
    {
        for (var i = 1; i < n; i++)
        {
            if (input[i] > input[i-1]) c++;
        }
    }
    Console.WriteLine(c);
}
else
{
    Console.WriteLine("Input read failed");
    Console.WriteLine(r.StatusCode);
}
