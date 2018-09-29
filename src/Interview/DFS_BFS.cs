using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interview
{
    public class Point
    {
        public int x { get; set; }
        public int y { get; set; }
        public int dist { get; set; }
    }
    public class DFS_BFS
    {

        //79. Word Search
        //Given a 2D board and a word, find if the word exists in the grid.
        //The word can be constructed from letters of sequentially adjacent cell, where "adjacent" cells are 
        //those horizontally or vertically neighboring.The same letter cell may not be used more than once.
        //Example:board =
        //[
        //  ['A','B','C','E'],
        //  ['S','F','C','S'],
        //  ['A','D','E','E']
        //]
        //Given word = "ABCCED", return true.
        //Given word = "SEE", return true.
        //Given word = "ABCB", return false.
        public bool Exist(char[,] board, string word)
        {
            if (board == null || string.IsNullOrEmpty(word))
                return false;
            int row = board.GetLength(0);
            int col = board.GetLength(1);
            var visited = new bool[row, col];

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (existHelper(board, word, visited, 0, i, j))
                        return true;
                }
            }
            return false;
        }
        bool existHelper(char[,] board, string word, bool[,] visited, int wordIdx, int i, int j)
        {
            if (word.Length == wordIdx)
                return true;

            if (i >= board.GetLength(0) || j >= board.GetLength(1) || i < 0 || j < 0)
                return false;

            if (visited[i, j] || board[i, j] != word[wordIdx])
                return false;

            visited[i, j] = true;
            if (existHelper(board, word, visited, wordIdx + 1, i + 1, j) ||
            existHelper(board, word, visited, wordIdx + 1, i, j + 1) ||
            existHelper(board, word, visited, wordIdx + 1, i - 1, j) ||
            existHelper(board, word, visited, wordIdx + 1, i, j - 1))
                return true;

            visited[i, j] = false;
            return false;
        }


        //419. Battleships in a Board
        //Given an 2D board, count how many battleships are in it. The battleships are represented with 'X's, 
        //empty slots are represented with '.'s. You may assume the following rules:
        //  You receive a valid board, made of only battleships or empty slots.
        //  Battleships can only be placed horizontally or vertically.In other words, they can only be made of 
        //the shape 1xN (1 row, N columns) or Nx1(N rows, 1 column), where N can be of any size.
        //At least one horizontal or vertical cell separates between two battleships - there are no adjacent battleships.        
        public int CountBattleships(char[,] board)
        {
            if (board == null)
                return 0;
            int ret = 0;
            for (int x = 0; x < board.GetLength(0); x++)
            {
                for (int y = 0; y < board.GetLength(1); y++)
                {
                    if (board[x, y] == 'X')
                    {
                        ret++;
                        CountBattleshipsHelper(x, y, board);
                    }
                }
            }
            return ret;
        }
        void CountBattleshipsHelper(int x, int y, char[,] board)
        {
            if (x >= board.GetLength(0) || x < 0 || y >= board.GetLength(1) || y < 0 || board[x, y] == '.')
                return;
            board[x, y] = '.';
            CountBattleshipsHelper(x + 1, y, board);
            CountBattleshipsHelper(x, y + 1, board);
            CountBattleshipsHelper(x - 1, y, board);
            CountBattleshipsHelper(x, y - 1, board);
        }


        //ref Swap 
        public void refSwap(ref Point pt1, ref Point pt2)
        {
            Point temp = pt1;
            pt1 = pt2;
            pt2 = temp;

            Console.WriteLine(pt1.x + "," + pt1.y);
            Console.WriteLine(pt2.x + "," + pt2.y);
        }

        //NVIDIA Shortest path in a Binary Maze
        //Given a MxN matrix where each element can either be 0 or 1. We need to find the shortest 
        //path between a given source cell to a destination cell.
        //The path can only be created out of a cell if its value is 1.
        // Expected time complexity is O(MN).
        //Input:
        //        mat[ROW][COL]  = {{1, 0, 1, 1, 1, 1, 0, 1, 1, 1 },
        //                          {1, 0, 1, 0, 1, 1, 1, 0, 1, 1 },
        //                          {1, 1, 1, 0, 1, 1, 0, 1, 0, 1 },
        //                          {0, 0, 0, 0, 1, 0, 0, 0, 0, 1 },
        //                          {1, 1, 1, 0, 1, 1, 1, 0, 1, 0 },
        //                          {1, 1, 0, 0, 0, 0, 1, 0, 0, 1 }};
        //Source = {0, 0};
        //Destination = {3, 4};   Output: Shortest Path is 11 
        public int PathBinaryMaze(int[,] mat, Point source, Point dest)
        {
            if (mat == null || mat.GetLength(0) == 0 || mat.GetLength(1) == 0)
                return -1;

            Queue<Point> q = new Queue<Point>();
            q.Enqueue(source);

            bool[,] visited = new bool[mat.GetLength(0), mat.GetLength(1)];

            int[] adjRow = new int[4] { 1, 0, 0, -1 };
            int[] adjCol = new int[4] { 0, 1, -1, 0 };

            while (q.Count != 0)
            {
                Point cur = q.Dequeue();
                if (cur.x == dest.x && cur.y == dest.y)
                    return cur.dist;

                for (int i = 0; i < 4; i++)
                {
                    int adjX = cur.x + adjRow[i];
                    int adjY = cur.y + adjCol[i];
                    if (validPoint(mat, adjX, adjY) && !visited[adjX, adjY] && mat[adjX, adjY] == 1)
                    {
                        Point adjPoint = new Point() { x = adjX, y = adjY, dist = cur.dist + 1 };
                        visited[adjX, adjY] = true;
                        q.Enqueue(adjPoint);
                    }
                }
            }
            return -1;
        }
        bool validPoint(int[,] mat, int adjX, int adjY)
        {
            if (adjX >= 0 && adjX < mat.GetLength(0) && adjY >= 0 && adjY < mat.GetLength(1))
                return true;
            return false;
        }


        //139. Word Break
        //Given a non-empty string s and a dictionary wordDict containing a list of non-empty words, determine if s can be segmented into a space-separated sequence of one or more dictionary words. You may assume the dictionary does not contain duplicate words.
        //For example, given s = "leetcode", dict = ["leet", "code"].
        //Return true because "leetcode" can be segmented as "leet code".
        public bool WordBreak(string s, IList<string> wordDict)
        {
            if (wordDict == null || s == null)
                return false;
            if (s.Length == 0)
                return true;

            for (int i = 0; i < s.Length; i++)
            {
                string front = s.Substring(0, i);

                if (wordDict.Contains(front))
                {
                    if (WordBreak(s.Substring(i + 1), wordDict))
                        return true;

                    wordDict.Remove(front);
                }
            }
            return false;
        }


        //200. Number of Islands
        //Given a 2d grid map of '1's (land) and '0's (water), count the number of islands. 
        //An island is surrounded by water and is formed by connecting adjacent lands horizontally or vertically. 
        //You may assume all four edges of the grid are all surrounded by water.
        public int NumIslands(char[,] grid)
        {
            int ret = 0;
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] == '1')
                    {
                        drownIslandDfsHelper(grid, i, j);
                        ret++;
                    }
                }
            }
            return ret;
        }

        public void drownIslandDfsHelper(char[,] grid, int i, int j)
        {
            if (i >= grid.GetLength(0) || i < 0 || j >= grid.GetLength(1) || j < 0 || grid[i, j] == '0')
                return;
            grid[i, j] = '0';
            drownIslandDfsHelper(grid, i + 1, j);
            drownIslandDfsHelper(grid, i, j + 1);
            drownIslandDfsHelper(grid, i - 1, j);
            drownIslandDfsHelper(grid, i, j - 1);
        }


        //207. Course Schedule
        //There are a total of n courses you have to take, labeled from 0 to n - 1.
        //Some courses may have prerequisites, for example to take course 0 you have to first take course 1, which is expressed as a pair: [0,1]
        // Given the total number of courses and a list of prerequisite pairs, is it possible for you to finish all courses?        
        public bool CanFinish(int numCourses, int[,] prerequisites)
        {
            if (numCourses == 0 || prerequisites == null || prerequisites.GetLength(0) == 0)
                return true;

            var  visitedList = new int[numCourses];

            List<int>[] map = new List<int>[numCourses];
            for (int i = 0; i < numCourses; i++)
                map[i] = new List<int>();
            
            for (int i = 0; i < prerequisites.GetLength(0); i++)
                map[prerequisites[i, 0]].Add(prerequisites[i, 1]);
            
            for (int i = 0; i < numCourses; i++)
            {
                if (isCircle(map, i, visitedList))
                    return false;
            }
            return true;
        }
        bool isCircle(List<int>[] map, int key, int[] visitedList)
        {
            if (visitedList[key]==1)  //visiting in dfs stack
                return true;
            if (visitedList[key] == 2) // already visited, out of dfs stack
                return false;

            visitedList[key] = 1;

            foreach (var k in map[key])
            {
                if (isCircle(map, k, visitedList))
                    return true;
            }

            visitedList[key] = 2;
            return false;
        }


        //210 Course Schedule II
        //There are a total of n courses you have to take, labeled from 0 to n - 1.
        //Some courses may have prerequisites, for example to take course 0 you have to first take course 1, which is expressed as a pair: [0,1]
        //Given the total number of courses and a list of prerequisite pairs, return the ordering of courses you should take to finish all courses.
        //There may be multiple correct orders, you just need to return one of them.If it is impossible to finish all courses, return an empty array.
        // For example: 2, [[1,0]]
        //There are a total of 2 courses to take.To take course 1 you should have finished course 0. So the correct course order is [0,1]
        //4, [[1,0],[2,0],[3,1],[3,2]]
        //There are a total of 4 courses to take.To take course 3 you should have finished both courses 1 and 2. Both courses 1 and 2 should be 
        //taken after you finished course 0. So one correct course order is [0,1,2,3]. Another correct ordering is[0,2,1,3].
        //Note: The input prerequisites is a graph represented by a list of edges, not adjacency matrices.Read more about how a graph is represented.
        //You may assume that there are no duplicate edges in the input prerequisites.
        public int[] FindOrder(int numCourses, int[,] prerequisites)
        {
            if (numCourses == 0)
                return null;

            List<int> ret = new List<int>();
            //build graph
            List<int>[] graph = new List<int>[numCourses];
            for (int i = 0; i < numCourses; i++)
                graph[i] = new List<int>();

            for (int i = 0; i < prerequisites.GetLength(0); i++)
                graph[prerequisites[i, 0]].Add(prerequisites[i, 1]);

            var visit = new int[numCourses];
            
            for (int i = 0; i < numCourses; i++)
            {
                if (IsCourseCycle(graph, ret, visit, i))
                    return new int[] { };
            }
            return ret.ToArray();
        }
        bool IsCourseCycle(List<int>[] graph, List<int> ret, int[] visit, int idx)
        {
            if (visit[idx]==1)  //if visiting node 
                return true;

            if (visit[idx]==2)  //if visited node 
                return false;

            visit[idx] = 1;

            foreach (int x in graph[idx])
            {
                if (IsCourseCycle(graph, ret, visit, x))
                    return true;
            }

            visit[idx] = 2;  //already visited
            ret.Add(idx);
            return false;
        }






        //339. Nested List Weight Sum
        //Example 1: Given the list[[1, 1],2,[1,1]], return 10. (four 1's at depth 2, one 2 at depth 1)
        //Example 2: Given the list[1,[4,[6]]], return 27. (one 1 at depth 1, one 4 at depth 2, and one 6 at depth 3; 1 + 4*2 + 6*3 = 27)
        // This is the interface that allows for creating nested lists.
        public interface NestedInteger
        {     // @return true if this NestedInteger holds a single integer, rather than a nested list.
            bool IsInteger();
            // @return the single integer that this NestedInteger holds, if it holds a single integer
            // Return null if this NestedInteger holds a nested list
            int GetInteger();
            // @return the nested list that this NestedInteger holds, if it holds a nested list
            // Return null if this NestedInteger holds a single integer
            IList<NestedInteger> GetList();
        }
        public int DepthSum(IList<NestedInteger> nestedList)
        {
            if (nestedList == null)
                return 0;
            return DepthSumHelper(nestedList, 1);
        }
        int DepthSumHelper(IList<NestedInteger> nestedList, int depth)
        {
            int ret = 0;

            for (int i = 0; i < nestedList.Count; i++)
            {
                ret += nestedList[i].IsInteger() ? depth * nestedList[i].GetInteger() : DepthSumHelper(nestedList[i].GetList(), depth + 1);
            }
            return ret;
        }

        //364. Nested List Weight Sum II  (inverse weight)
        //Given a nested list of integers, return the sum of all integers in the list weighted by their depth.
        //Each element is either an integer, or a list -- whose elements may also be integers or other lists.
        //Different from the previous question where weight is increasing from root to leaf, now the weight is defined from bottom up.i.e., the leaf level integers have weight 1, and the root level integers have the largest weight.
        //Example 1: Given the list[[1, 1],2, [1,1]], return 8. (four 1's at depth 1, one 2 at depth 2)
        //Example 2:Given the list[1,[4,[6]]], return 17. (one 1 at depth 3, one 4 at depth 2, and one 6 at depth 1; 1*3 + 4*2 + 6*1 = 17)
        public int DepthSumInverse(IList<NestedInteger> nestedList)
        {
            if (nestedList == null || nestedList.Count == 0)
                return 0;

            int depth = depthHelper(nestedList);
            return sumHelper(nestedList, depth);
        }
        int sumHelper(IList<NestedInteger> nestedList, int depth)
        {
            if (nestedList == null || nestedList.Count == 0)
                return 0;
            int ret = 0;

            for (int i = 0; i < nestedList.Count(); i++)
            {
                if (nestedList[i].IsInteger())
                    ret += nestedList[i].GetInteger() * depth;
                else
                    ret += sumHelper(nestedList[i].GetList(), depth - 1);
            }
            return ret;
        }
        int depthHelper(IList<NestedInteger> nestedList)
        {
            if (nestedList == null || nestedList.Count == 0)
                return 0;
            int depth = 0;
            for (int i = 0; i < nestedList.Count(); i++)
            {
                if (nestedList[i].IsInteger())
                    depth = Math.Max(depth, 1);
                else
                    depth = Math.Max(depth, 1 + depthHelper(nestedList[i].GetList()));
            }
            return depth;
        }



        //582. Kill Process My SubmissionsBack To Contest
        //Given n processes, each process has a unique PID(process id) and its PPID(parent process id).
        //Input:  pid =  [1, 3, 10, 5]  ppid = [3, 0, 5, 3]   kill = 5   Output: [5,10]
        //Explanation:   Kill 5 will also kill 10.
        //      3
        //    /   \
        //   1     5
        //        /
        //      10
        public IList<int> KillProcess2(IList<int> pid, IList<int> ppid, int kill)
        {
            if (pid == null || ppid == null || pid.Count == 0 || ppid.Count == 0)
                return null;

            var ret = new HashSet<int>();

            dfs(pid, ppid, kill, ret);
            return ret.ToList();
        }
        void dfs(IList<int> pid, IList<int> ppid, int kill, HashSet<int> ret)
        {
            if (ppid.Contains(kill))
            {
                ret.Add(kill);
                int idx = -1;
                for (int i = 0; i < ppid.Count; i++)
                {
                    if (ppid[i] == kill)
                    {
                        idx = i;
                        dfs(pid, ppid, pid[idx], ret);
                    }
                }
            }
            else
                ret.Add(kill);
        }

        public IList<int> KillProcess(IList<int> pid, IList<int> ppid, int kill)
        {
            Dictionary<int, List<int>> map = new Dictionary<int, List<int>>();
            //save relationship in map
            for (int i = 0; i < ppid.Count; i++)
            {
                if (!map.ContainsKey(ppid[i]))
                    map.Add(ppid[i], new List<int>() { pid[i] });
                else
                    map[ppid[i]].Add(pid[i]);
            }

            List<int> ret = new List<int>();
            Queue<int> q = new Queue<int>();
            q.Enqueue(kill);

            while (q.Count != 0)
            {
                int parent = q.Dequeue();
                ret.Add(parent);
                if (map.ContainsKey(parent))
                {
                    foreach (var x in map[parent])
                        q.Enqueue(x);
                }
            }
            return ret;
        }


    }
}
