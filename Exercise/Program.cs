using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Tuple<int, int>> Data = new List<Tuple<int, int>>();
            Data.Add(Tuple.Create(1, 2));
            Data.Add(Tuple.Create(1, 3));
            Data.Add(Tuple.Create(2, 4));
            Data.Add(Tuple.Create(3, 4));
            Data.Add(Tuple.Create(4, 5));
            Data.Add(Tuple.Create(4, 6));
            var res = GraphResolver.ConnectingPaths(Data, 1, 4);

            string output = "";
            foreach(var r in res)
            {
                output = output + string.Join(" ", r) + "\n";

            }
            Console.WriteLine(output);
            Console.ReadLine();
        }
    }

    class GraphResolver
    {
        public static List<List<int>> ConnectingPaths(List<Tuple<int, int>> graph, int node1, int node2)
        {
            List<List<int>> results = new List<List<int>>();
            results.Add(new List<int> { node1 });
            int matchCnt = 0;
            ForEachBreak:
            foreach (var r in results)
            {
                matchCnt = graph.Where(u => u.Item1 == r.Last()).Count();
                if (matchCnt == 1)
                {
                    Tuple<int, int> res = graph.Where(u => u.Item1 == r.Last()).Single();
                    r.Add(res.Item2);
                    graph.Remove(res);
                    goto ForEachBreak;
                }
                else if (matchCnt > 1)
                {

                    List<Tuple<int, int>> res = graph.Where(u => u.Item1 == r.Last()).ToList();
                    Tuple<int, int> first = res.First();
                    r.Add(first.Item2);
                    graph.Remove(first);
                    res.Remove(first);
                    foreach (var remainder in res)
                    {
                        results.Add(new List<int> { remainder.Item1, remainder.Item2 });
                        graph.Remove(remainder);
                    }
                    goto ForEachBreak;
                }
            }

            ForEachBreak2:
            foreach (var item in results)
            {
                if (item[0] != node1)
                {
                    results.Remove(item);
                    goto ForEachBreak2;
                }
                else if (item[item.Count() - 1] != node2)
                {
                    int node2Pos = item.IndexOf(node2);
                    item.RemoveRange(node2Pos+1, item.Count - node2Pos-1);
                }
            }

            return results;
        }
    }
}
