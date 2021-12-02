using System;
using System.IO;
using System.Linq;

namespace aoc21
{
    class Program
    {
        static void Main(string[] args)
        {
            Q1();
            Q2();
            Q3();
            Q4();
        }

        // Day 1

        private static void Q1()
        {
            var m = Input1();
            var count = 0;
            for (int last = m[0], i = 0; i < m.Length; last = m[i++])
                if (m[i] > last)
                    count++;

            Console.WriteLine($"Q1: {count}");
        }
        private static void Q2()
        {
            var m = Input1();
            var count = 0;
            for (int mm = m[0] + m[1] + m[2], last = mm, i = 2; i < m.Length; last = mm, mm = m[i - 1] + m[i] + m[++i])
            {
                if (mm > last)
                    count++;

                if (i + 1 == m.Length)
                    break;
            }
            Console.WriteLine($"Q2: {count}");
        }

        private static int[] Input1()
        {
            return File.ReadAllLines("./input1.txt").Select(line => int.Parse(line)).ToArray();
        }

        // Day 2

        private static void Q3()
        {
            var d = Input2();
            int x = 0, y = 0;
            for (var i = 0;  i < d.Length; x += d[i].x, y += d[i].y, i++) { }
            Console.WriteLine($"Q3: {x * y}");
        }

        private static void Q4()
        {
            var d = Input2();
            int x = 0, depth = 0, aim = 0;
            for (var i = 0; i < d.Length; i++)
            {
                aim += d[i].y;
                if (d[i].x > 0)
                {
                    x += d[i].x;
                    depth += d[i].x * aim;
                }
            }
            Console.WriteLine($"Q4: {x * depth}");
        }

        private static (int x, int y)[] Input2()
        {
            return File.ReadAllLines("./input2.txt").Select(line => {
                var movement = line.Split(' ');
                var amount = int.Parse(movement[1]);
                return movement[0] == "forward" ? (x: amount, y: 0) : (x: 0, y: amount * (movement[0] == "down" ? 1 : -1));
            }).ToArray();
        }
    }
}
