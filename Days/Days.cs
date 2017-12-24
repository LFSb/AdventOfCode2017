using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

public static partial class Days
{
  private const string Day1Input = "428122498997587283996116951397957933569136949848379417125362532269869461185743113733992331379856446362482129646556286611543756564275715359874924898113424472782974789464348626278532936228881786273586278886575828239366794429223317476722337424399239986153675275924113322561873814364451339186918813451685263192891627186769818128715595715444565444581514677521874935942913547121751851631373316122491471564697731298951989511917272684335463436218283261962158671266625299188764589814518793576375629163896349665312991285776595142146261792244475721782941364787968924537841698538288459355159783985638187254653851864874544584878999193242641611859756728634623853475638478923744471563845635468173824196684361934269459459124269196811512927442662761563824323621758785866391424778683599179447845595931928589255935953295111937431266815352781399967295389339626178664148415561175386725992469782888757942558362117938629369129439717427474416851628121191639355646394276451847131182652486561415942815818785884559193483878139351841633366398788657844396925423217662517356486193821341454889283266691224778723833397914224396722559593959125317175899594685524852419495793389481831354787287452367145661829287518771631939314683137722493531318181315216342994141683484111969476952946378314883421677952397588613562958741328987734565492378977396431481215983656814486518865642645612413945129485464979535991675776338786758997128124651311153182816188924935186361813797251997643992686294724699281969473142721116432968216434977684138184481963845141486793996476793954226225885432422654394439882842163295458549755137247614338991879966665925466545111899714943716571113326479432925939227996799951279485722836754457737668191845914566732285928453781818792236447816127492445993945894435692799839217467253986218213131249786833333936332257795191937942688668182629489191693154184177398186462481316834678733713614889439352976144726162214648922159719979143735815478633912633185334529484779322818611438194522292278787653763328944421516569181178517915745625295158611636365253948455727653672922299582352766484";

  private const string Day1TestInput = "91212129";

  private const string Day2Input = "Days/Input/Day2.txt";

  private const int Day3Input = 265149;

  private const int Day3TestInput = 1;

  private const string Day4Input = "Days/Input/Day4.txt";

  private const string Day5Input = "Days/Input/Day5.txt";

  private const string Day7TestInput = "Days/Input/Day7Test.txt";

  private const string Day7Input = "Days/Input/Day7.txt";

  private const string Day8Input = "Days/Input/Day8.txt";

  private const string Day9Input = "Days/Input/Day9.txt";

  private static byte[] Day10Input = new byte[] { 97, 167, 54, 178, 2, 11, 209, 174, 119, 248, 254, 0, 255, 1, 64, 190 };

  private const string Day10Padding = "17,31,73,47,23";

  private const string Day11Input = "Days/Input/Day11.txt";

  private static string[] Day12TestInput = new[]{"0 <-> 2",
"1 <-> 1",
"2 <-> 0, 3, 4",
"3 <-> 2, 4",
"4 <-> 2, 3, 6",
"5 <-> 6",
"6 <-> 4, 5"};

  private const string Day12Input = "Days/Input/Day12.txt";

  private static string[] Day13TestInput = new[]{
"0: 3",
"1: 2",
"4: 4",
"6: 4"
};

  private const string Day13Input = "Days/Input/Day13.txt";

  private const string Day16Input = "Days/Input/Day16.txt";

  private const string Day18Input = "Days/Input/Day18.txt";

  private const string Day19Input = "Days/Input/Day19.txt";

  private const string Day19TestInput = @"     |          
     |  +--+    
     A  |  C    
 F---|----E|--+ 
     |  |  |  D 
     +B-+  +--+ 
";

  private static string[] Day4TestInput = new string[]
  {
    "aa bb cc dd ee",
    "aa bb cc dd aa",
    "aa bb cc dd aaa"
  };

  private const string Day14Input = "ljoxqyyw";

  private static string OutputResult(string part1, string part2)
  {
    return $"{Environment.NewLine}- Part 1: {part1}{Environment.NewLine}- Part 2: {part2}";
  }

  public static string Day1()
  {
    var input = Day1Input.Select(x => int.Parse($"{x}")).ToArray();

    return OutputResult(
      CalculateSum(input, 1).ToString(),
      CalculateSum(input, input.Length / 2).ToString()
    );
  }

  private static int CalculateSum(int[] input, int offSet)
  {
    var output = 0;

    for (var i = 0; i < input.Length; i++)
    {
      if (input[i] == input[CalculateNextIndex(i, offSet, input.Length)])
      {
        output += input[i];
      }
    }

    return output;
  }

  private static int CalculateNextIndex(int currentIndex, int offSet, int maxLength)
  {
    return (currentIndex + offSet) % maxLength;
  }

  public static string Day2()
  {
    var content = File.ReadAllLines(Day2Input);

    var checkSum = 0;

    foreach (var line in content)
    {
      checkSum += CalculateDay2CheckSumPart1(line);
    }

    var checkSum2 = 0;

    foreach (var line in content)
    {
      checkSum2 += CalculateDay2CheckSumPart2(line);
    }

    return OutputResult(
      checkSum.ToString(),
      checkSum2.ToString()
    );
  }

  private static int[] ParseDay2Input(string line)
  {
    return line.Split(new[] { "  ", " ", "\t" }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse($"{x}")).OrderBy(x => x).ToArray();
  }

  private static int CalculateDay2CheckSumPart1(string line)
  {
    var input = ParseDay2Input(line);

    return input[input.Length - 1] - input[0];
  }

  private static int CalculateDay2CheckSumPart2(string line)
  {
    var input = ParseDay2Input(line);

    for (var i = 0; i < input.Length; i++)
    {
      var candidates = input.Where(x => x != input[i]).ToArray(); //Create a new array from all values that are not the current value. There's probably a better way to do this.

      foreach (var candidate in candidates)
      {
        if (input[i] % candidate == 0)
        {
          return input[i] / candidate;
        }
      }
    }

    return 0; //This shouldn't happen..right?
  }

  public static string Day3()
  {
    var gridSize = 600;

    var grid = new int[gridSize, gridSize];

    var p1 = WalkGrid(grid, gridSize, 265149, true);

    //We wanna reset the grid before going for P2.
    grid = new int[gridSize, gridSize];

    var p2 = WalkGrid(grid, gridSize, 265149);

    return OutputResult(p1.ToString(), p2.ToString());
  }

  public enum Direction
  {
    Right,
    Up,
    Left,
    Down
  }

  private static int WalkGrid(int[,] grid, int gridSize, int target, bool p1 = false)
  {
    var currentStepAmount = 1;
    var secondStep = false;
    var rotationCountdown = 1;
    var direction = Direction.Right;

    var startingCoord = (gridSize / 2) - 1; //Start from the middle of the grid.

    int x, y;
    x = y = startingCoord;

    int nextValue = 1;
    grid[x, y] = 1;

    while (nextValue < target)
    {
      if (rotationCountdown == 0)
      {
        if (secondStep)
        {
          ++currentStepAmount; //The amount of second steps we make directly impacts the amount of steps we should take until rotation.
        }

        secondStep = !secondStep;

        rotationCountdown = currentStepAmount;

        switch (direction)
        {
          case Direction.Right: direction = Direction.Up; break;
          case Direction.Up: direction = Direction.Left; break;
          case Direction.Left: direction = Direction.Down; break;
          case Direction.Down: direction = Direction.Right; break;
        }
      }

      switch (direction)
      {
        case Direction.Right: ++x; break;
        case Direction.Up: --y; break;
        case Direction.Left: --x; break;
        case Direction.Down: ++y; break;
      }

      --rotationCountdown;

      nextValue = CalculateNextValue(grid, x, y, nextValue, p1);

      grid[x, y] = nextValue;
    }

    return p1
      ? Math.Abs(x - startingCoord) + Math.Abs(y - startingCoord) //Manhattan Distance from one point to another is the sum of the absolute difference between the two coordinates.
      : nextValue;
  }

  //For part 1, just give us the next value. 
  //For part 2, add the sum of all surrounding values.
  private static int CalculateNextValue(int[,] grid, int x, int y, int nextValue, bool p1)
  {
    if (p1)
    {
      return ++nextValue;
    }

    nextValue = grid[x - 1, y - 1]
          + grid[x - 1, y]
          + grid[x - 1, y + 1]
          + grid[x, y - 1]
          + grid[x, y + 1]
          + grid[x + 1, y - 1]
          + grid[x + 1, y]
          + grid[x + 1, y + 1];

    return nextValue;
  }

  public static string Day4()
  {
    var input = File.ReadAllLines(Day4Input);

    var p1 = input.Count(f => f
      .Split(' ')
      .GroupBy(x => x)
      .ToDictionary(y => y, z => z.Count())
      .All(x => x.Value == 1)
    );

    var p2 = input.Count(f => f
      .Split(' ')
      .GroupBy(x => x.Length)
      .Where(x => x
        .Count() > 1)
        .SelectMany(x => x
          .Select(y => y
            .ToCharArray()
            .OrderBy(z => z
          )
        )
      )
      .GroupBy(y => string.Join(",", y))
      .All(y => y.Count() == 1)
    );

    return OutputResult(p1.ToString(), p2.ToString());
  }

  public static string Day5()
  {
    var input = File.ReadAllLines(Day5Input).Select(x => int.Parse(x)).ToArray();

    var outOfBounds = false;

    var currentIndex = 0;

    var p1 = 0;

    while (!outOfBounds)
    {
      outOfBounds = Jump(input, ref currentIndex);

      p1++;
    }

    currentIndex = 0;

    var p2 = 0;

    outOfBounds = false;

    input = File.ReadAllLines(Day5Input).Select(x => int.Parse(x)).ToArray();

    while (!outOfBounds)
    {
      outOfBounds = Jump(input, ref currentIndex, true);

      p2++;
    }

    return OutputResult(p1.ToString(), p2.ToString());
  }

  private static bool Jump(int[] input, ref int currentIndex, bool p2 = false)
  {
    var jump = input[currentIndex];

    var nextIndex = currentIndex + jump;

    if (p2 && jump >= 3)
    {
      input[currentIndex]--;
    }
    else
    {
      input[currentIndex]++;
    }

    currentIndex = nextIndex;

    return currentIndex < 0 || currentIndex >= input.Length;
  }

  public static string Day6()
  {
    var banks = new int[] { 2, 8, 8, 5, 4, 2, 3, 1, 5, 5, 1, 2, 15, 13, 5, 14 };

    var savedStates = new Dictionary<string, int>();

    string currentState = string.Join("", banks);

    int cycles = 0;

    while (!savedStates.ContainsKey(currentState))
    {
      savedStates.Add(currentState, cycles); //Add the current state for checking later.

      var startingIndex = Array.IndexOf(banks, banks.Max()); //When a tie happens between max values, IndexOf always defaults to the first one.

      var savedValue = banks.Max();

      banks[startingIndex] = 0;

      //Now start cycling.
      for (var i = savedValue; i > 0; i--)
      {
        startingIndex = CalculateNextIndex(startingIndex, 1, banks.Count());
        banks[startingIndex]++;
      }

      cycles++;
      currentState = string.Join("", banks);
    }

    var p2 = cycles - savedStates[currentState];

    return OutputResult(cycles.ToString(), p2.ToString());
  }

  public static string Day7()
  {
    var nodes = File.ReadAllLines(Day7Input).Select(line => new Node(line)).ToList();

    foreach (var node in nodes.Where(n => n.ChildrenKeys != null && n.ChildrenKeys.Any()))
    {
      node.Children = nodes.Where(x => node.ChildrenKeys.Contains(x.Name)).ToList();

      foreach (var child in node.Children)
      {
        child.Parent = node;
      }
    }

    var p1 = nodes.First(x => x.Children.Any() && x.Parent == null);

    var towers = new List<Tuple<string, int>>();

    foreach (var child in p1.Children)
    {
      towers.Add(new Tuple<string, int>(child.Name, child.TotalWeight));
    }

    var exceptionNode = towers.GroupBy(x => x.Item2).First(x => x.Count() == 1).First();

    var difference = Math.Abs(towers.First(x => x.Item1 != exceptionNode.Item1).Item2 - exceptionNode.Item2);

    var current = nodes.First(x => x.Name == exceptionNode.Item1);

    var p2 = 0;

    while (true)
    {
      var exception = current.Children.GroupBy(x => x.TotalWeight).FirstOrDefault(x => x.Count() == 1);

      if (exception != null)
      {
        current = exception.First();
      }
      else
      {
        p2 = current.Weight - difference;
        break;
      }
    }

    return OutputResult(p1.Name, p2.ToString());
  }

  public static string Day8()
  {
    var input = File.ReadAllLines(Day8Input);

    var registers = input.Select(x => x.Split(' ')[0]).Distinct().ToDictionary(x => x, y => 0);

    var highestValue = 0;

    foreach (var line in input)
    {
      var split = line.Split(' ');

      var targetRegister = split[0];

      var operation = split[1];

      var additive = int.Parse(split[2]);

      var checkRegister = split[4];

      var op = split[5];

      var checkValue = split[6];

      switch (op)
      {
        case ">":
          {
            if (registers[checkRegister] > int.Parse(checkValue))
            {
              ModifyRegister(ref registers, targetRegister, operation, additive);
            }
          }
          break;
        case "<":
          {
            if (registers[checkRegister] < int.Parse(checkValue))
            {
              ModifyRegister(ref registers, targetRegister, operation, additive);
            }
          }
          break;
        case ">=":
          {
            if (registers[checkRegister] >= int.Parse(checkValue))
            {
              ModifyRegister(ref registers, targetRegister, operation, additive);
            }
          }
          break;
        case "==":
          {
            if (registers[checkRegister] == int.Parse(checkValue))
            {
              ModifyRegister(ref registers, targetRegister, operation, additive);
            }
          }
          break;
        case "<=":
          {
            if (registers[checkRegister] <= int.Parse(checkValue))
            {
              ModifyRegister(ref registers, targetRegister, operation, additive);
            }
          }
          break;
        case "!=":
          {
            if (registers[checkRegister] != int.Parse(checkValue))
            {
              ModifyRegister(ref registers, targetRegister, operation, additive);
            }
          }
          break;
      }

      if (registers[targetRegister] > highestValue)
      {
        highestValue = registers[targetRegister];
      }
    }

    return OutputResult(registers.Max(x => x.Value).ToString(), highestValue.ToString());
  }

  private static void ModifyRegister(ref Dictionary<string, int> registers, string targetRegister, string operation, int additive)
  {
    switch (operation)
    {
      case "inc":
        {
          registers[targetRegister] += additive;
        }
        break;
      case "dec":
        {
          registers[targetRegister] -= additive;
        }
        break;
    }
  }

  public static string Day9()
  {
    var input = File.ReadAllText(Day9Input).ToCharArray();

    var rinsed = Rinse(input);

    var p1 = Score(rinsed);

    return OutputResult(p1.ToString(), rinsed.Count(x => x == '\0').ToString());
  }

  private static int Score(char[] input)
  {
    var groups = 0;

    var groupStarted = new Queue<Boolean>();

    var score = 1;

    for (var i = 0; i < input.Length; i++)
    {
      switch (input[i])
      {
        case '{':
          {
            if (groupStarted.FirstOrDefault())
            {
              score++;
            }

            groupStarted.Enqueue(true);
          }
          break;

        case '}':
          {
            if (groupStarted.FirstOrDefault())
            {
              groups += score;
              score--;
              groupStarted.Dequeue();
            }
          }
          break;
      }
    }

    return groups;
  }

  private static char[] Rinse(char[] input)
  {
    //In here, it's garbage day.

    while (input.Contains('<'))
    {
      var start = Array.IndexOf(input, '<');
      var end = 0;

      for (var i = start + 1; i < input.Length; i++)
      {
        switch (input[i])
        {
          case '!':
            {
              input[i + 1] = '\0';
            }
            break;
          case '>':
            {

              end = i;
              i = input.Length;
            }
            break;
        }
      }

      var ignoreNext = false;

      input[start] = '0';
      input[end] = '0';

      for (var i = start + 1; i < end; i++)
      {
        switch (input[i])
        {
          case '!':
            {
              input[i] = '0';
              ignoreNext = true;
            }
            break;
          default:
            {
              if (ignoreNext)
              {
                input[i] = '0';
                ignoreNext = false;
              }
              else
              {
                input[i] = '\0';
              }
            }
            break;
        }
      }
    }

    return input;
  }

  public static string Day10()
  {
    var inputList = Enumerable.Range(0, 256).Select(x => (byte)x).ToArray();

    var input = Day10Input;

    var p2input = System.Text.Encoding.ASCII.GetBytes(string.Join(",", input)).ToList();

    var inputPosition = 0;
    var skipSize = 0;

    KnotHash(inputList, input, ref inputPosition, ref skipSize);

    var p1 = (inputList[0] * inputList[1]).ToString();

    //For part 2: we're going to want to reset everthing that we're reusing here.
    inputList = Enumerable.Range(0, 256).Select(x => (byte)x).ToArray();
    inputPosition = 0;
    skipSize = 0;

    var p2Numeric = p2input.Select(x => (byte)x).ToList();
    p2Numeric.AddRange(Day10Padding.Split(',').Select(x => byte.Parse($"{x}")));

    for (var round = 0; round < 64; round++)
    {
      KnotHash(inputList, p2Numeric.ToArray(), ref inputPosition, ref skipSize);
    }

    var denseHash = CalculateDenseHash(inputList);

    var p2 = string.Empty;

    foreach (var value in denseHash)
    {
      p2 += value.ToString("x").PadLeft(2, '0');
    }

    return OutputResult(p1, p2);
  }

  public static byte[] CalculateDenseHash(byte[] sparseHash)
  {
    int hashSize = 16;

    byte[] denseHash = new byte[hashSize];

    for (var x = 0; x < sparseHash.Length / hashSize; x++)
    {
      var block = sparseHash.Skip(x * hashSize).Take(hashSize).ToArray();
      byte blockValue = block[0];

      for (var y = 1; y < hashSize; y++)
      {
        blockValue ^= block[y];
      }

      denseHash[x] = blockValue;
    }

    return denseHash;
  }

  private static void KnotHash(byte[] inputList, byte[] lengths, ref int inputPosition, ref int skipSize)
  {
    for (var i = 0; i < lengths.Length; i++)
    {
      var length = lengths[i];

      var toReverse = new byte[length];

      var start = inputPosition;

      for (var j = 0; j < length; j++)
      {
        toReverse[j] = inputList[start];
        start = CalculateNextIndex(start, 1, inputList.Length);
      }

      start = inputPosition;

      toReverse = toReverse.Reverse().ToArray();

      for (var j = 0; j < length; j++)
      {
        inputList[start] = toReverse[j];
        start = CalculateNextIndex(start, 1, inputList.Length);
      }

      inputPosition = CalculateNextIndex(inputPosition, length + skipSize, inputList.Length);
      skipSize++;
    }
  }

  public static string Day11()
  {
    var input = File.ReadAllText(Day11Input).Split(',');

    int x = 0, y = 0, z = 0; //Hex grids have three axes: X, Y and Z (diagonal)

    int p2 = 0, p1 = 0;

    foreach (var direction in input)
    {
      switch (direction)
      {
        case "n":
          {
            y++;
            z--;
          }
          break;
        case "s":
          {
            y--;
            z++;
          }
          break;
        case "ne":
          {
            x++;
            z--;
          }
          break;
        case "nw":
          {
            x--;
            y++;
          }
          break;
        case "se":
          {
            x++;
            y--;
          }
          break;
        case "sw":
          {
            x--;
            z++;
          }
          break;
      }

      p1 = (Math.Abs(x) + Math.Abs(y) + Math.Abs(z)) / 2; //Calculating the manhattan distance of a hex grid is the same as calculating the manhattan distance of a cube (abs(x) + abs(y) + abs(z)) only divided by 2 as there are only 2 dimensions

      if (p1 > p2)
      {
        p2 = p1;
      }
    }

    return OutputResult(p1.ToString(), p2.ToString());
  }

  public static string Day12()
  {
    var pipes = new List<Pipe>();

    foreach (var line in File.ReadAllLines(Day12Input))
    {
      var pipe = new Pipe(line);

      pipes.Add(pipe);
    }

    //Direct connections are already in place, we only want the indirect connections.

    var zero = pipes.First(x => x.ID == 0);

    foreach (var pipe in pipes)
    {
      pipe.ConnectPipes(pipes);
    }

    var p1 = zero.ReturnConnections().Count.ToString();

    var p2 = 0;

    while (pipes.Any())
    {
      var group = pipes.First().ReturnConnections();
      pipes.RemoveAll(x => group.Contains(x));
      p2++;
    }

    return OutputResult(p1, p2.ToString());
  }

  private class Pipe
  {
    public Pipe(string input)
    {
      var split = input.Split(' ');

      ID = int.Parse(split[0]);

      Connected = split.Skip(2).Select(x => int.Parse(x.Trim(','))).ToList();

      ConnectedPipes = new List<Pipe>();
    }

    public int ID { get; set; }

    public List<int> Connected;

    public List<Pipe> ConnectedPipes;

    internal void ConnectPipes(List<Pipe> pipes)
    {
      for (var i = 0; i < Connected.Count; i++)
      {
        var pipe = pipes.First(x => x.ID == Connected[i]);

        if (!ConnectedPipes.Contains(pipe))
        {
          ConnectedPipes.Add(pipe);
        }

        if (!pipe.ConnectedPipes.Contains(this))
        {
          pipe.ConnectedPipes.Add(this);
        }
      }
    }

    //In here, recursively map all the pipes that can be mapped to the current pipe.
    internal List<Pipe> ReturnConnections()
    {
      var con = new List<Pipe>();
      MapConnections(con);
      return con;
    }

    internal void MapConnections(List<Pipe> pipes)
    {
      pipes.Add(this);

      foreach (var connection in ConnectedPipes)
      {
        if (!pipes.Contains(connection))
        {
          connection.MapConnections(pipes);
        }
      }
    }
  }

  public static string Day13()
  {
    var fw = new FireWall(File.ReadAllLines(Day13Input));

    //fw.PrintFireWall();

    var p1 = 0;

    for (var picoSecond = 0; picoSecond < fw.Layers.Length; picoSecond++)
    {
      if (fw.Layers[picoSecond].Length == 0)
      {
        continue;
      }


      if (picoSecond % ((fw.Layers[picoSecond].Length - 1) * 2) == 0)
      {
        p1 += (fw.Layers[picoSecond].Length * picoSecond);
      }
    }

    var p2 = 0;

    while (true)
    {
      var caught = false;

      for (var picoSecond = 0; picoSecond < fw.Layers.Length; picoSecond++)
      {
        if (fw.Layers[picoSecond].Length == 0)
        {
          continue;
        }

        if ((picoSecond + p2) % ((fw.Layers[picoSecond].Length - 1) * 2) == 0)
        {
          caught = true;
          // We're caught, so we need to try again with a bigger delay.
          break;
        }
      }

      if (!caught)
      {
        break;
      }
      else
      {
        p2++;
      }
    }

    return OutputResult(p1.ToString(), p2.ToString());
  }

  public class FireWall
  {
    public FireWall(string[] input)
    {
      var grouped = input.GroupBy(x => int.Parse(x.Split(' ')[0].Trim(':'))).ToDictionary(x => x.Key, y => int.Parse(y.First().Split(' ')[1]));

      var depth = grouped.Last().Key;
      var layers = new List<int[]>();

      for (var i = 0; i <= depth; i++)
      {
        if (grouped.ContainsKey(i))
        {
          layers.Add(new int[grouped[i]]);
        }
        else
        {
          layers.Add(new int[0]);
        }
      }

      Layers = layers.ToArray();
    }

    public int[][] Layers { get; set; }

    public int ScanDepth { get; set; }

    public void PrintFireWall()
    {
      var header = string.Empty;

      for (var i = 0; i < Layers.Length; i++)
      {
        header += $"{i}".PadLeft(2, ' ').PadRight(3, ' ');
      }

      System.Console.WriteLine(header);

      for (var i = 0; i < Layers.Max(x => x.Length); i++)
      {
        System.Console.WriteLine(string.Join("", Layers.Select(x => x.Length > i).Select(x => x ? $"[ ]" : "   ")));
      }
    }
  }

  private static string CalculateDenseHash(string input)
  {
    var result = Enumerable.Range(0, 256).Select(x => (byte)x).ToArray();

    var inputPosition = 0;

    var skipSize = 0;

    var key = System.Text.Encoding.ASCII.GetBytes(input).ToList();

    key.AddRange(Day10Padding.Split(',').Select(x => byte.Parse($"{x}")));

    for (var round = 0; round < 64; round++)
    {
      KnotHash(result, key.ToArray(), ref inputPosition, ref skipSize);
    }

    return string.Join("", CalculateDenseHash(result).Select(x => x.ToString("x2")));
  }

  public static string Day14()
  {
    var gridSize = 128;

    var grid = new bool[gridSize, gridSize];

    var input = "ljoxqyyw";

    var rows = new List<string>();

    for (var line = 0; line < gridSize; line++)
    {
      var dense = CalculateDenseHash($"{input}-{line}");

      var binary = string.Join("", dense.Select(y => Convert.ToString(
          Convert.ToInt32($"{y}", 16), 2).PadLeft(4, '0'))
      );

      for (var column = 0; column < binary.Length; column++)
      {
        grid[line, column] = binary[column] == '1';
      }

      var row = string.Join("", binary.Select(x => x == '1' ? '#' : '.'));
      rows.Add(row);
    }

    var p1 = 0;

    foreach (var square in grid)
    {
      if (square)
      {
        p1++;
      }
    }


    //p2

    grid = new bool[gridSize, gridSize];

    for (var line = 0; line < gridSize; line++)
    {
      var dense = CalculateDenseHash($"{input}-{line}");

      var binary = string.Join("", dense.Select(y => Convert.ToString(
          Convert.ToInt32($"{y}", 16), 2).PadLeft(4, '0'))
      );

      for (var column = 0; column < binary.Length; column++)
      {
        grid[line, column] = binary[column] == '1';
      }
    }

    var visitedGrid = new bool[gridSize, gridSize];

    var regionCount = 0;

    for (var row = 0; row < gridSize; row++)
    {
      for (var col = 0; col < gridSize; col++)
      {
        if (grid[row, col] && !visitedGrid[row, col])
        {
          VisitRegion(row, col, grid, ref visitedGrid);
          regionCount++;
        }
      }
    }

    return OutputResult(p1.ToString(), regionCount.ToString());
  }

  public static void VisitRegion(int x, int y, bool[,] grid, ref bool[,] coords)
  {
    var visits = new List<Tuple<int, int>>();

    visits.Add(new Tuple<int, int>(x, y)); //First, add the current position to the list of visits.

    int position = 0;

    while (position < visits.Count) //Then, from the current position, check what adjacent coords are true. If they are true, add them to the list of visits.
    {
      x = visits.ElementAt(position).Item1; y = visits.ElementAt(position).Item2;

      if (x - 1 > -1 && grid[x - 1, y] && !coords[x - 1, y]) //To the left
      {
        visits.Add(new Tuple<int, int>(x - 1, y));
      }
      if (x + 1 < 128 && grid[x + 1, y] && !coords[x + 1, y]) //To the right
      {
        visits.Add(new Tuple<int, int>(x + 1, y));
      }
      if (y + 1 < 128 && grid[x, y + 1] && !coords[x, y + 1]) //Up
      {
        visits.Add(new Tuple<int, int>(x, y + 1));
      }
      if (y - 1 > -1 && grid[x, y - 1] && !coords[x, y - 1]) //Down
      {
        visits.Add(new Tuple<int, int>(x, y - 1));
      }

      position++; //Afterwards, go to the next position, and perform the same check.
    }

    foreach (var location in visits) //Mark the visited locations.
    {
      coords[location.Item1, location.Item2] = true;
    }
  }

  public static string Day15()
  {
    long genAValue = 516;

    long genBValue = 190;

    long genAFactor = 16807;

    long genBFactor = 48271;

    long division = 2147483647;

    var pairCountp1 = 40000000;

    var p1 = 0;

    var sw = new Stopwatch();

    sw.Start();

    for (var i = 0; i < pairCountp1; i++)
    {
      genAValue = ((genAValue * genAFactor) % division);
      genBValue = ((genBValue * genBFactor) % division);

      var bina = Convert.ToString(genAValue, 2).PadLeft(32, '0').Substring(16);
      var binb = Convert.ToString(genBValue, 2).PadLeft(32, '0').Substring(16);

      if (bina == binb)
      {
        p1++;
      }
    }

    sw.Stop();

    System.Console.WriteLine($"p1: {sw.ElapsedMilliseconds}ms");

    var pairCountp2 = 5000000;

    genAValue = 516;
    genBValue = 190;

    var aQueue = new Queue<long>();
    var bQueue = new Queue<long>();

    sw = new Stopwatch();
    sw.Start();

    while (aQueue.Count != pairCountp2 || bQueue.Count != pairCountp2)
    {
      genAValue = ((genAValue * genAFactor) % division);
      genBValue = ((genBValue * genBFactor) % division);

      if (genAValue % 4 == 0 && aQueue.Count != pairCountp2)
      {
        aQueue.Enqueue(genAValue);
      }

      if (genBValue % 8 == 0 && bQueue.Count != pairCountp2)
      {
        bQueue.Enqueue(genBValue);
      }
    }

    var p2 = 0;

    while (pairCountp2 > 0)
    {
      if (Convert.ToString(aQueue.Dequeue(), 2).PadLeft(32, '0').Substring(16) == Convert.ToString(bQueue.Dequeue(), 2).PadLeft(32, '0').Substring(16))
      {
        p2++;
      }

      pairCountp2--;
    }

    sw.Stop();

    System.Console.WriteLine($"p2: {sw.ElapsedMilliseconds}ms");

    return OutputResult(p1.ToString(), p2.ToString());
  }

  public static string Day16()
  {
    var constant = "abcdefghijklmnop";

    var chars = "abcdefghijklmnop".ToCharArray();

    var dances = File.ReadAllText(Day16Input).Split(',').ToArray();

    var shortCut = 0;

    var p1 = string.Empty;

    var i = 0;

    while (shortCut == 0)
    {
      i++;

      Dance(dances, chars);

      if (string.IsNullOrEmpty(p1))
      {
        p1 = string.Join("", chars);
      }

      if (Enumerable.SequenceEqual(constant, chars)) //After a certain amount of loops, the sequence will reset itself. The amount of loops it takes is the modulo we can apply to 1 billion rounds.
      {
        System.Console.WriteLine($"After {i} rounds we've looped back around. Shortcut?");
        shortCut = i;
      }
    }

    chars = constant.ToCharArray(); //Reset the loop before reprocessing.

    for (var iteration = 0; iteration < 1000000000 % shortCut; iteration++)
    {
      Dance(dances, chars);
    }

    return OutputResult(p1, string.Join("", chars));
  }

  private static void Dance(string[] dances, char[] chars)
  {
    foreach (var line in dances)
    {
      switch (line[0])
      {
        case 's':
          {
            //Spin. Take the last element of the array, move it to the front, then append the rest.

            for (var spins = 0; spins < int.Parse(line.Substring(1)); spins++)
            {
              var len = chars.Length - 1;
              var temp = chars.Take(len).ToArray();
              chars[0] = chars[len];

              for (var remainder = 1; remainder <= len; remainder++)
              {
                chars[remainder] = temp[remainder - 1];
              }
            }
          }
          break;
        case 'x':
          {
            //Exchange
            var split = line.Split('/');
            SwapChars(int.Parse($"{split[0].Substring(1)}"), int.Parse($"{split[1]}"), ref chars);
          }
          break;
        case 'p':
          {
            //Partner
            SwapChars(Array.IndexOf(chars, line[1]), Array.IndexOf(chars, line[3]), ref chars);
          }
          break;
      }
    }
  }

  public static void SwapChars(int index1, int index2, ref char[] target)
  {
    char temp = target[index1];
    target[index1] = target[index2];
    target[index2] = temp;
  }

  public static string Day17()
  {
    var buffer = new LinkedList<int>();

    var steps = 371;

    buffer.AddFirst(0); //Initial state

    var currentNode = buffer.Find(0);

    SpinLock(steps, 2017, currentNode, ref buffer);

    currentNode = buffer.Find(2017);

    var p1 = currentNode.Next.Value.ToString();

    buffer = new LinkedList<int>();

    buffer.AddFirst(0);

    currentNode = buffer.Find(0); //Reset the buffer, and everything else.

    SpinLock(steps, 50000000, currentNode, ref buffer);

    currentNode = buffer.Find(0);

    var p2 = currentNode.Next.Value.ToString();

    return OutputResult(p1, p2);
  }

  private static void SpinLock(int steps, int values, LinkedListNode<int> currentNode, ref LinkedList<int> buffer)
  {
    for (var i = 1; i <= values; i++)
    {
      for (var step = 0; step < (steps % buffer.Count); step++)
      {
        currentNode = currentNode.Next;
        if (currentNode == null)
        {
          currentNode = buffer.First; //We've looped around.
        }
      }

      buffer.AddAfter(currentNode, i);
      currentNode = currentNode.Next; //Set the current node to the node just insterted.
    }
  }

  public static string Day18()
  {
    var input = File.ReadAllLines(Day18Input);

    var registers = Enumerable.Range('a', 26).Select(x => (char)x).ToDictionary(x => $"{x}", y => (long)0);

    long snd = 0;

    long p1 = 0;

    for (long line = 0; line < input.Length && line > -1;)
    {
      long val;

      var split = input[line].Split(' ');

      switch (split[0])
      {
        case "snd":
          {
            if (!long.TryParse(split[1], out val))
            {
              val = registers[split[1]];
            }

            snd = val;

            line++;
          }
          break;
        case "set":
          {
            if (!long.TryParse(split[2], out val))
            {
              val = registers[split[2]];
            }

            registers[split[1]] = val;

            line++;
          }
          break;
        case "add":
          {
            if (!long.TryParse(split[2], out val))
            {
              val = registers[split[2]];
            }

            registers[split[1]] += val;

            line++;
          }
          break;
        case "mul":
          {
            if (!long.TryParse(split[2], out val))
            {
              val = registers[split[2]];
            }

            registers[split[1]] *= val;

            line++;
          }
          break;
        case "mod":
          {
            if (!long.TryParse(split[2], out val))
            {
              val = registers[split[2]];
            }

            registers[split[1]] = registers[split[1]] % val;

            line++;
          }
          break;
        case "rcv":
          {
            if (!long.TryParse(split[1], out val))
            {
              val = registers[split[1]];
            }

            if (val != 0)
            {
              p1 = snd;
              line = input.Length; //We're done!
            }
            else
            {
              line++;
            }
          }
          break;
        case "jgz":
          {
            if (!long.TryParse(split[1], out val))
            {
              val = registers[split[1]];
            }

            if (val > 0)
            {
              if (!long.TryParse(split[2], out long val2))
              {
                val2 = registers[split[2]];
              }

              line += val2;
            }
            else
            {
              line++;
            }
          }
          break;
      }
    }

    return OutputResult(p1.ToString(), "");
  }

  public static string Day19()
  {
     var input = File.ReadAllText(Day19Input)
      // var input = Day19TestInput
    .Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
.Select(line => line.ToCharArray())
.ToArray();

    var finish = input.SelectMany(x => x).Where(x => char.IsLetter(x)).Count();

    var start = Array.IndexOf(input[0], input[0].First(x => x != ' '));

    var currentPosition = new Tuple<int, int>(0, start);

    var letterQueue = new Queue<char>();

    var previousDirection = GridDirection.Down;

    char current = ' ';

    var notAllowed = new char[] { ' ', '+' }; //from a decision space ('+') we can't end up on either a space, or a different decision space.

    var directions = Enum.GetValues(typeof(GridDirection)).Cast<GridDirection>().ToArray();

    var p2 = 0;

    while (letterQueue.Count != finish)
    {
      current = input[currentPosition.Item1][currentPosition.Item2];
      
      switch (current)
      {
        case '-':
        case '|':
          {
            currentPosition = CalculateNextPosition(currentPosition, input, previousDirection);
          }
          break;
        case '+':
          {
            var directionsToSearch = directions.Where(direction => direction != Opposite(previousDirection)).ToArray();
            Tuple<int, int> nextPosition;

            foreach (var direction in directionsToSearch)
            {
              nextPosition = CalculateNextPosition(currentPosition, input, direction);

              if (nextPosition != null)
              {
                if (!notAllowed.Contains(input[nextPosition.Item1][nextPosition.Item2]))
                {
                  if(
                    IsHorizontal(direction) && input[nextPosition.Item1][nextPosition.Item2] != '|'
                    || IsVertical(direction) && input[nextPosition.Item1][nextPosition.Item2] != '-'
                    )
                  {
                    previousDirection = direction;
                    currentPosition = nextPosition;
                  }
                  break;
                }
              }
            }
          }
          break;
        default:
          {
            letterQueue.Enqueue(current); //We should keep going in the same direction.
            System.Console.WriteLine($"Came across '{current}'");
            currentPosition = CalculateNextPosition(currentPosition, input, previousDirection);
          }
          break;
      }

      p2++;
    }

    return OutputResult(string.Join("", letterQueue.Select(x => x)), p2.ToString());
  }

  private static bool IsHorizontal(GridDirection direction) => direction == GridDirection.Left || direction == GridDirection.Right;

  private static bool IsVertical(GridDirection direction) => direction == GridDirection.Up || direction == GridDirection.Down;

  private static GridDirection Opposite(GridDirection direction)
  {
    switch (direction)
    {
      case GridDirection.Up:
        {
          return GridDirection.Down;
        }
      case GridDirection.Right:
        {
          return GridDirection.Left;
        }
      case GridDirection.Left:
        {
          return GridDirection.Right;
        }
      case GridDirection.Down:
        {
          return GridDirection.Up;
        }
      default:
        {
          throw new NotImplementedException();
        }
    }
  }

  private static Tuple<int, int> CalculateNextPosition(Tuple<int, int> currentPosition, char[][] input, GridDirection previousDirection)
  {
    switch (previousDirection)
    {
      case GridDirection.Down:
        {
          return currentPosition.Item1 + 1 < input.Length ? new Tuple<int, int>(currentPosition.Item1 + 1, currentPosition.Item2) : null;
        }
      case GridDirection.Up:
        {
          return currentPosition.Item1 - 1 > -1 ? new Tuple<int, int>(currentPosition.Item1 - 1, currentPosition.Item2) : null;
        }
      case GridDirection.Left:
        {
          return currentPosition.Item2 - 1 > -1 ? new Tuple<int, int>(currentPosition.Item1, currentPosition.Item2 - 1) : null;
        }
      case GridDirection.Right:
        {
          return currentPosition.Item2 + 1 < input.First().Length ? new Tuple<int, int>(currentPosition.Item1, currentPosition.Item2 + 1) : null;
        }
      default:
        {
          return null;
        }
    }
  }

  private enum GridDirection
  {
    Up,
    Down,
    Left,
    Right
  }
}