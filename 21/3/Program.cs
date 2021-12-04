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

var oxIteration = numbers.ToArray();
var done = false;
var ii = 0;
while (!done)
{
    var N = oxIteration.Length;
    var ones = 0;
    for (var k = 0; k < N; k++) ones += oxIteration[k][ii];
    var zeros = N - ones;
    var criteria = (ones >= zeros) ? 1 : 0;
    oxIteration = oxIteration.Where(e => e[ii] == criteria).Select(e => e).ToArray();
    ii++;
    done = (ii == N) || (oxIteration.Length < 2);
}

var co2Iteration = numbers.ToArray();
done = false;
ii = 0;
while (!done)
{
    var N = co2Iteration.Length;
    var ones = 0;
    for (var k = 0; k < N; k++) ones += co2Iteration[k][ii];
    var zeros = N - ones;
    var criteria = (zeros <= ones) ? 0 : 1;
    co2Iteration = co2Iteration.Where(e => e[ii] == criteria).Select(e => e).ToArray();
    ii++;
    done = (ii == N) || (co2Iteration.Length < 2);
}

var oxRatingBin = oxIteration.FirstOrDefault();
var co2RatingBin = co2Iteration.FirstOrDefault();

if (oxRatingBin != null && co2RatingBin != null)
{
    var oxRating = Convert.ToInt32(string.Join("", oxRatingBin), 2);
    var co2Rating = Convert.ToInt32(string.Join("", co2RatingBin), 2);

    Console.WriteLine($"Part Two: {oxRating * co2Rating}");
}
