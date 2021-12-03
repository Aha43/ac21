var part = int.Parse(args[1]);

var hc = new HttpClient();
var inputUri = (args[0] == "i") ? "https://raw.githubusercontent.com/Aha43/ac21/main/21/3/input.txt" : "https://raw.githubusercontent.com/Aha43/ac21/main/21/3/example.txt";
var r = await hc.GetAsync(inputUri)
    .ConfigureAwait(continueOnCapturedContext: false);

var input = (await r.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false)).Split();
var size = input[0].Length;

var numbers = input.Select(e => 
{
    var retVal = new int[size];
    for (int i = 0; i < size; i++)
    {
        retVal[i] = e[i] == '0' ? 0 : 1; 
    }
    return retVal;
});

var sums = new int[size];
foreach (var e in numbers)
{
    for (var i = 0; i < size; i++)
    {
        sums[i] += e[i];
    }
}

var n = input.Count();
var gammaRateBin = new int[size];
var epsilonRateBin = new int[size];
for (var i = 0; i < size; i++)
{
    var zeros = n - sums[i];
    if (zeros > sums[i])
    {
        epsilonRateBin[i] = 1;
    }
    else
    {
        gammaRateBin[i] = 1;
    }
}


var gammaRate = Convert.ToInt32(string.Join("", gammaRateBin), 2);
var epsilonRate = Convert.ToInt32(string.Join("", epsilonRateBin), 2);

Console.WriteLine(gammaRate * epsilonRate);
