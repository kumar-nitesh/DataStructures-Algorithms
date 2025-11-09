/********************************************************************************************
 * Daily Temperatures
 * 
 * Source     : LeetCode
 * Difficulty : Medium
 * Problem    : https://leetcode.com/problems/daily-temperatures
 * Solution   : Use Monotonic Stack pattern to find next smaller element to the right.
 * 
 * Time Complexity  : O(n)        
 * Space complexity : O(n) 
 ********************************************************************************************/

namespace DataStructures.Algorithms.Stacks
{
    public class DailyTemperatures : IExecute
    {
        public void Execute()
        {
            Console.WriteLine("#################LeetCode#####################");
            Console.WriteLine("739.Daily Temperatures");

            int[] temperatures = [73, 74, 75, 71, 69, 72, 76, 73];
            Console.WriteLine("No. of Days :  " + string.Join(", ", RightToLeft(temperatures)));
        }


        /// <summary>
        /// More Intuitive
        /// Stack stores future warmer days
        /// </summary>
        public int[] RightToLeft(int[] temperatures)
        {
            int length = temperatures.Length;

            // Monotonic Decreasing Stack: Temperatures in stack are always in decreasing order (from bottom to top).
            Stack<int> stack = new Stack<int>();
            int[] result = new int[length];

            for (int i = length - 1; i >= 0; i--)
            {
                Console.WriteLine($"Day {i}: temp = {temperatures[i]}");
                Console.WriteLine($"Stack before: [{string.Join(", ", stack)}]");

                while (stack.Count > 0 && temperatures[i] >= temperatures[stack.Peek()])
                {
                    stack.Pop();
                }

                result[i] = stack.Count > 0 ? stack.Peek() - i : 0;
                stack.Push(i);
            }

            return result;
        }

        /// <summary>
        /// Stack stores Past colder days
        /// </summary>
        public int[] LeftToRight(int[] temperatures)
        {
            int length = temperatures.Length;

            // Monotonic Decreasing Stack: Temperatures in stack are always in decreasing order (from bottom to top).
            Stack<int> stack = new Stack<int>();
            int[] result = new int[length];

            for (int i = length - 1; i >= 0; i--)
            {
                Console.WriteLine($"Day {i}: temp = {temperatures[i]}");
                Console.WriteLine($"Stack before: [{string.Join(", ", stack)}]");

                // While current temp is warmer than stack top
                while (stack.Count > 0 && temperatures[i] > temperatures[stack.Peek()])
                {
                    int prevIndex = stack.Pop();
                    result[prevIndex] = i - prevIndex;

                    Console.WriteLine($"  ✓ Day {prevIndex} (temp={temperatures[prevIndex]}) found warmer day!");
                    Console.WriteLine($"    Wait time: {i} - {prevIndex} = {result[prevIndex]} days");
                }

                // Add current day to stack (waiting for warmer day)
                stack.Push(i);
                Console.WriteLine($"  Push day {i} to stack (waiting for warmer)");
                Console.WriteLine($"Stack after: [{string.Join(", ", stack)}]");
                Console.WriteLine();
            }

            return result;
        }
    }
}
