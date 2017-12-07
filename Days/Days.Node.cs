using System.Linq;
using System.Collections.Generic;

public static partial class Days
{
  public class Node
  {
    public Node(string input)
    {
      var split = input.Split(' ');
      this.Name = split[0];
      this.Weight = int.Parse(split[1].Trim(new[] { '(', ')' }));

      if (split.Length > 2)
      {
        ChildrenKeys = split.Skip(3).Select(x => x.Trim(',')).ToList();
      }

      Children = new List<Node>();
    }

    public Node Parent { get; set; }

    public string Name { get; set; }

    public int Weight { get; set; }

    public int TotalWeight
    {
      get
      {
        return Weight + Children.Sum(child => child.TotalWeight);
      }
    }

    public List<string> ChildrenKeys { get; set; }

    public List<Node> Children { get; set; }
  }
}