/********************************************************************************************
 * Backspace String Compare
 * 
 * Source     : LeetCode
 * Difficulty : Easy
 * Problem    : https://leetcode.com/problems/backspace-string-compare/
 * Solution   : 
 * 
 * Time Complexity  : O(m+n)        
 * Space complexity : O(m+n) 
 ********************************************************************************************/

using System.Text;

namespace DataStructures.Algorithms.Strings
{
    internal class BackspaceCompare : IExecute
    {
        public void Execute()
        {
            Console.WriteLine("#################LeetCode#####################");
            Console.WriteLine("844.Backspace String Compare");

            Console.WriteLine(CompareUsingStack("ab#c", "ad#c"));
            Console.WriteLine(CompareUsingStack("ab##", "c#d#"));
            Console.WriteLine(CompareUsingStack("a#c", "b"));
            Console.WriteLine(CompareUsingStack("xywrrmp", "xywrrmu#p"));
            Console.WriteLine(CompareUsingStack("y#fo##f", "y#f#o##f"));
        }

        /// <summary>
        /// Time complexity: O(N + M) 
        /// Space complexity: O(N + M)
        /// </summary>
        public bool CompareUsingStack(string s, string t)
        {
            return RemoveBackspace(s).Equals(RemoveBackspace(t));
        }

        private string RemoveBackspace(string s)
        {
            Stack<char> stack = new Stack<char>();

            foreach (char c in s)
            {
                if (c == '#')
                {
                    if(stack.Count > 0)
                        stack.Pop();
                }
                else
                {
                    stack.Push(c);
                }
            }

            return new string(stack.ToArray());
        }
    }
}
