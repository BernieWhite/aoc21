using System;
using System.Collections.Generic;
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
            Q5();
            Q6();
            Q7();
            Q8();
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

        // Day 3

        private static void Q5()
        {
            var d = Input3();
            var size = 12;
            var sig = new int[size];
            for (var i = 0; i < d.Length; i++)
            {
                for (var s = 0; s < size; s++)
                    sig[s] += (d[i] >> size - 1 - s) & 1;
            }
            var gamma = 0;
            for (var i = 0; i < sig.Length; i++)
                gamma |= sig[i] >= d.Length / 2 ? 1 << size - 1 - i : 0;

            var epsilon = ~gamma & 4095;
            Console.WriteLine($"Q5: {gamma * epsilon}");
        }

        private static void Q6()
        {
            var d = Input3();
            var size = 12;

            var set = d;
            for (int x = 0, count = 0; x < size && set.Count() > 1; x++, count = 0)
            {
                foreach (var sig in set)
                    count += (sig >> size - 1 - x) & 1;

                set = set.Where(entry => ((entry >> size - 1 - x) & 1) ==(count * 2 >= set.Count() ? 1 : 0)).ToArray();
            }
            var ox = set.FirstOrDefault();

            set = d;
            for (int x = 0, count = 0; x < size && set.Count() > 1; x++, count = 0)
            {
                foreach (var sig in set)
                    count += (sig >> size - 1 - x) & 1;

                set = set.Where(entry => ((entry >> size - 1 - x) & 1) != (count * 2 >= set.Count() ? 1 : 0)).ToArray();
            }
            var co = set.FirstOrDefault();

            Console.WriteLine($"Q6: {ox * co}");
        }

        private static int[] Input3()
        {
            return File.ReadAllLines("./input3.txt").Select(line =>
            {
                int b = 0;
                for (var i = 0; i < line.Length; i++)
                    b += line[i] == '1' ? 1 << line.Length - 1 - i : 0;

                return b;
            }).ToArray();
        }

        // Day 4

        private static void Q7()
        {
            var numbers = Input4Numbers();
            var winDistance = int.MaxValue;
            var winScore = 0;
            foreach (var board in Input4())
            {
                Score(numbers, board, out int distance, out int score);
                if (distance < winDistance)
                {
                    winDistance = distance;
                    winScore = score;
                }
            }
            Console.WriteLine($"Q7: {winScore}");
        }

        

        private static void Q8()
        {
            var numbers = Input4Numbers();
            var winDistance = 0;
            var winScore = 0;
            foreach (var board in Input4())
            {
                Score(numbers, board, out int distance, out int score);
                if (distance > winDistance)
                {
                    winDistance = distance;
                    winScore = score;
                }
            }
            Console.WriteLine($"Q8: {winScore}");
        }

        private static void Score(int[] numbers, int[] board, out int distance, out int score)
        {
            distance = int.MaxValue;
            score = 0;
            var grid = new int[]
            {
                31,
                31 << 5,
                31 << 10,
                31 << 15,
                31 << 20,
                1082401,
                1082401 << 1,
                1082401 << 2,
                1082401 << 3,
                1082401 << 4
            };
            uint map = 0;
            var sum = board.Sum();
            for (var n = 0; n < numbers.Length; n++)
            {
                var index = Array.IndexOf(board, numbers[n]);
                if (index >= 0)
                {
                    map |= (uint)(1 << index);
                    sum -= numbers[n];
                }
                if (n >= 4 && grid.Any(g => (g & map) == g))
                {
                    score = sum * numbers[n];
                    distance = n + 1;
                    break;
                }
            }
        }

        private static int[] Input4Numbers()
        {
            var lines = File.ReadAllLines("./input4.txt");
            return lines[0].Split(',', StringSplitOptions.RemoveEmptyEntries).Select(n => int.Parse(n)).ToArray();
        }

        private static IEnumerable<int[]> Input4(int size = 5)
        {
            var lines = File.ReadAllLines("./input4.txt");
            for (int i = 1; i < lines.Length; i++)
            {
                if (lines[i] == "")
                    continue;

                var pos = 0;
                var board = new int[size * size];
                while (i < lines.Length && lines[i] != "")
                {
                    foreach (var n in lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(n => int.Parse(n)))
                    {
                        board[pos++] = n;
                    }
                    i++;
                }
                yield return board;
            }
        }
    }
}
