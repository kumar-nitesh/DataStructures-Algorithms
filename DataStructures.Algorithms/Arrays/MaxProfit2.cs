/********************************************************************************************
 * Best Time to Buy and Sell Stock II
 * 
 * Source     : LeetCode
 * Difficulty : Medium
 * Problem    : https://leetcode.com/problems/best-time-to-buy-and-sell-stock-ii
 * 
 * Time Complexity  : O(n)        
 * Space complexity : O(1) 
 ********************************************************************************************/

namespace DataStructures.Algorithms.Arrays
{
    public class MaximumProfit2 : IExecute
    {
        public void Execute()
        {
            Console.WriteLine("#################LeetCode#####################");
            Console.WriteLine("122.Best Time to Buy and Sell Stock II");

            int[] sample1 = new int[] { 7, 1, 5, 3, 6, 4 };
            Console.WriteLine("[{0}]", string.Join(", ", MaxProfit(sample1)));

            int[] sample2 = new int[] { 7, 6, 4, 3, 1 };
            Console.WriteLine("[{0}]", string.Join(", ", MaxProfit(sample2)));
        }

        public static int MaxProfit(int[] prices)
        {
            int profit = 0;

            for (int i = 0; i < prices.Length; i++)
            {
                if (prices[i] > prices[i-1])
                {
                    profit  += prices[i] - prices[i-1];
                }
            }

            return profit;
        }

       
    }
}
