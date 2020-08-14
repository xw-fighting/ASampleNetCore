using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Xunit;

namespace ASample.NetCore.xUnitTest
{
    public class LeetCodeTest
    {
        [Fact]
        public void MinimumTotal()
        {
            var triangle = new List<IList<int>>();

            var list = new List<int>(){ 2 };
            var list1 = new List<int>(){ 3,4 };
            var list2 = new List<int>(){ 6,5,7 };
            var list3 = new List<int>(){ 4, 1, 8, 3 };

            triangle.Add(list);
            triangle.Add(list1);
            triangle.Add(list2);
            triangle.Add(list3);
            for (int i = 1; i < triangle.Count; i++)
            {
                var subArr = triangle[i];
                for (int j = 0; j < subArr.Count; j++)
                {
                    if (j == 0)
                    {
                        triangle[i][j] += triangle[i-1][j];
                    }
                    else if(j == subArr.Count-1)
                    {
                        triangle[i][j] += triangle[i - 1][j-1];
                    }
                    else
                    {
                        triangle[i][j] += Math.Min(triangle[i - 1][j - 1], triangle[i - 1][j]);
                    }
                }
            }
            var result = triangle.Last().Min();
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void MinimumTotal2()
        {
            var triangle = new List<IList<int>>();

            var list = new List<int>() { 2 };
            var list1 = new List<int>() { 3, 4 };
            var list2 = new List<int>() { 6, 5, 7 };
            var list3 = new List<int>() { 4, 1, 8, 3 };

            triangle.Add(list);
            triangle.Add(list1);
            triangle.Add(list2);
            triangle.Add(list3);
            for (int i = triangle.Count - 2; i >= 0 ; i--)
            {
                for (int j = 0; j < triangle[i].Count; j++)
                {
                    triangle[i][j] += Math.Min(triangle[i + 1][j], triangle[i + 1][j + 1]);
                }
            }

            var result = triangle[0][0];
        }

        /// <summary>
        /// Unique Binary Search Trees
        /// 不同的二叉搜索树
        /// </summary>
        [Fact]
        public void NumTrees()
        {
            var n = 4;
            var dp = new int[n + 1];
            dp[0] = 1; dp[1] = 1;
            for (var i = 2; i <= n; i++)
            {
                for (var j = 1; j <= i; j++)
                {
                    dp[i] = dp[i] + dp[j - 1] * dp[i - j];
                }
            }
            
            var result = dp[n];
        }

        [Fact]
        public bool IsValid()
        {
            string s = "([])";
            var sLength = s.Length;
            if (sLength == 1)
                return false;
            for (var i = 0; i < sLength; i++)
            {
                if (s[i] == '}' || s[i] == ']' || s[i] == ')')
                    return false;

                for (var j = i; j < sLength; j++)
                {
                    if (s[i] == '(' && s[j] == ')')
                    {
                        i = j;
                        break;
                    }

                    else if (s[i] == '{' && s[j] == '}')
                    {
                        i = j;
                        break;
                    }
                    else if (s[i] == '[' && s[j] == ']')
                    {
                        i = j;
                        break;
                    }
                    else if(j == sLength-1)
                        return false;
                    continue;
                }
            }
            return true;
        }
    }
}
