var hc = new HttpClient();
var r = await hc.GetAsync("https://raw.githubusercontent.com/Aha43/ac21/main/21/1/Input.txt")
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
