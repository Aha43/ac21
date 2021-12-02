var part = int.Parse(args[1]);

var hc = new HttpClient();
var inputUri = (args[0] == "i") ? "https://raw.githubusercontent.com/Aha43/ac21/main/21/2/input.txt" : "https://raw.githubusercontent.com/Aha43/ac21/main/21/2/example.txt";
var r = await hc.GetAsync(inputUri)
    .ConfigureAwait(continueOnCapturedContext: false);
if (r.IsSuccessStatusCode)
{
    var input = (await r.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false)).Split();

    var aim = 0;
    var x = 0;
    var d = 0;

    var state = State.Start;
    foreach (var e in input)
    {
        var v = (state == State.Start) ? 0 : int.Parse(e);
        switch (state)
        {
            case State.Start:
                switch (e)
                {
                    case "forward":
                        state = State.Forward;
                    break;
                    case "down":
                        state = State.Down;
                    break;
                    case "up":
                        state = State.Up;
                    break;
                }
            break;
            case State.Forward:
                x += v;
                if (part == 2)
                {
                    d += (aim * v);
                }
                state = State.Start;
            break;
            case State.Down:
                
                switch (part)
                {
                    case 1:
                        d += v;
                    break;
                    case 2:
                        aim += v;
                    break;
                }
                state = State.Start;
            break;
            case State.Up:
                switch (part)
                {
                    case 1:
                        d -= v;
                    break;
                    case 2:
                        aim -= v;
                    break;
                }
                state = State.Start;
            break;
        }
    }

    Console.WriteLine(x * d);
}

enum State 
{
    Start,
    Forward,
    Down,
    Up
};
