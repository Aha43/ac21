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
}).ToArray();

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

Console.WriteLine("Part one: " + (gammaRate * epsilonRate));

var gammaIteration = numbers.Where(e => e[0] == gammaRateBin[0]).ToArray();
var gi = 1;
while (gammaIteration.Count() > 1 && gi < size)
{
    var ones = sums[gi];
    var zeros = n - ones;
    var criteria = (ones >= zeros) ? 1 : 0;
    //gammaIteration = gammaIteration.Where(e => e[gi] == gammaRateBin[gi]).Select(e => e).ToArray();
    gammaIteration = gammaIteration.Where(e => e[gi] == criteria).Select(e => e).ToArray();
    gi++;
}

var epsilonIteration = numbers.Where(e => e[0] == epsilonRateBin[0]).ToArray();
var ei = 1;
while (epsilonIteration.Count() > 1 && ei < size)
{
    var ones = sums[ei];
    var zeros = n - ones;
    var criteria = (zeros <= ones) ? 0 : 1;
    //epsilonIteration = epsilonIteration.Where(e => e[ei] == epsilonRateBin[ei]).ToArray();
    epsilonIteration = epsilonIteration.Where(e => e[ei] == criteria).ToArray();
    ei++;
}

var oxRatingBin = gammaIteration.FirstOrDefault();
var co2RatingBin = epsilonIteration.FirstOrDefault();

if (oxRatingBin != null && co2RatingBin != null)
{
    var oxRating = Convert.ToInt32(string.Join("", oxRatingBin), 2);
    var co2Rating = Convert.ToInt32(string.Join("", co2RatingBin), 2);

    Console.WriteLine($"Part Two: {oxRating * co2Rating}");
}
