var hc = new HttpClient();
var inputUri = (args[0] == "i") ? "https://raw.githubusercontent.com/Aha43/ac21/main/21/1/Input.txt" : "https://raw.githubusercontent.com/Aha43/ac21/main/21/1/example.txt";
var r = await hc.GetAsync(inputUri)
    .ConfigureAwait(continueOnCapturedContext: false);
if (r.IsSuccessStatusCode)
{
    var input = (await r.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false)).Split().Select(e => int.Parse(e)).ToArray();
    var n = input.Length;
    var c = 0;
    if (n > 1)
    {
        if (args[1] == "2")
        {
            input = input.Select((e, i) => 
            { 
                var sum = 0;
                var m = i + 3;
                m = (m < n) ? m : n;
                for (int j = i; j < m; j++) sum += input[j];
                return sum; 
            }).ToArray();
        }
        
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
