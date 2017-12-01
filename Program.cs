using System;

public class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Day 1:" + Days.Day1().Replace("-", $"{Environment.NewLine}-"));

    Console.WriteLine("Day 2:" + Days.Day2().Replace("-", $"{Environment.NewLine}-"));
  }
}