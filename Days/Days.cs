using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;

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

  private static string[] Day4TestInput = new string[]
  {
    "aa bb cc dd ee",
    "aa bb cc dd aa",
    "aa bb cc dd aaa"
  };

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
}