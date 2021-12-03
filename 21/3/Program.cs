var part = int.Parse(args[1]);

var hc = new HttpClient();
var inputUri = (args[0] == "i") ? "https://raw.githubusercontent.com/Aha43/ac21/main/21/3/input.txt" : "https://raw.githubusercontent.com/Aha43/ac21/main/21/3/example.txt";
var r = await hc.GetAsync(inputUri)
    .ConfigureAwait(continueOnCapturedContext: false);

var input = (await r.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false)).Split().Select(e => 
{
    var retVal = new int[5];
    for (int i = 0; i < 5; i++)
    {
        retVal[i] = e[i] == '0' ? 0 : 1; 
    }
    return retVal;
});

var sums = new int[]
{
    0, 0, 0, 0, 0
};

foreach (var e in input)
{
    for (var i = 0; i < 5; i++)
    {
        sums[i] += e[i];
    }
}

var n = input.Count();
var gammaRateBin = new int[5];
var epsilonRateBin = new int[5];
for (var i = 0; i < 5; i++)
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

Console.WriteLine($"Gamma rate bin: {gammaRateBin[0]}{gammaRateBin[1]}{gammaRateBin[2]}{gammaRateBin[3]}{gammaRateBin[4]}");
Console.WriteLine($"Epsilon rate bin: {epsilonRateBin[0]}{epsilonRateBin[1]}{epsilonRateBin[2]}{epsilonRateBin[3]}{epsilonRateBin[4]}");

var gammaRate = gammaRateBin[4] + 2*gammaRateBin[3] + 4*gammaRateBin[2] + 8*gammaRateBin[1] + 16*gammaRateBin[0];
var epsilonRate = epsilonRateBin[4] + 2*epsilonRateBin[3] + 4*epsilonRateBin[2] + 8*epsilonRateBin[1] + 16*epsilonRateBin[0]; 

Console.WriteLine($"Gamma rate: {gammaRate}, Epsilon rate: {epsilonRate}");

Console.WriteLine(gammaRate * epsilonRate);
