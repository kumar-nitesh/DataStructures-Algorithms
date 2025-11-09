/********************************************************************************************
 * Final Prices With a Special Discount in a Shop.
 * 
 * Source     : LeetCode
 * Difficulty : Easy
 * Problem    : https://leetcode.com/problems/final-prices-with-a-special-discount-in-a-shop/
 * Solution   : Use Monotonic Stack pattern to find next smaller element to the right.
 * 
 * Time Complexity  : O(n)        
 * Space complexity : O(n) 
 ********************************************************************************************/

namespace DataStructures.Algorithms.Stacks
{
    public class FinalPrice : IExecute
    {
        public void Execute()
        {
            Console.WriteLine("#################LeetCode#####################");
            Console.WriteLine("1475.Final Prices With a Special Discount in a Shop");

            int[] prices = [8, 4, 6, 2, 3];
            Console.WriteLine("Final Prices With a Special Discount in a Shop " + string.Join(", ", FinalPrices(prices)));
        }

        public int[] FinalPrices(int[] prices)
        {
            int length = prices.Length;
            int[] finalPrices = new int[length];

            // Monotonic Stack pattern - a common technique for next smaller/greater element problems.
            // Right → Left Find next smaller/ greater element to the right e.g. This problem
            // Left → Right Find previous smaller/ greater element to the left e.g. Histogram problems
            Stack<int> stack = new Stack<int>();

            // We traverse right-to-left because we're looking for the next smaller element to the right of each element.
            // By processing from right to left, we've already seen all the elements to the right when we reach the current element.
            // The stack maintains elements in increasing order - it acts like a catalog of potential 'next smaller elements' for future iterations.
            // When we reach an element, the top of the stack tells us the next smaller element immediately, without having to search.
            // This gives us O(n) time because each element is pushed and popped from the stack exactly once.
            for (int i = length - 1; i >= 0; i--)
            {
                while (stack.Count > 0 && stack.Peek() > prices[i])
                {
                    stack.Pop();
                }

                int discount = stack.Count > 0 ? stack.Peek() : 0;
                finalPrices[i] = prices[i] - discount;
                stack.Push(prices[i]);
            }

            return finalPrices;
        }
    }
}
