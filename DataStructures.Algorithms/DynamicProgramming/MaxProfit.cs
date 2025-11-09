/********************************************************************************************
 * Best Time to Buy and Sell Stock IV
 * 
 * Source     : LeetCode
 * Difficulty : Hard
 * Problem    : https://leetcode.com/problems/best-time-to-buy-and-sell-stock-iv/
 * Solution   : https://www.youtube.com/watch?v=3YILP-PdEJA
 * 
 * Time Complexity  : O(k * n)        
 * Space complexity : O(1) 
 ********************************************************************************************/

namespace DataStructures.Algorithms.DynamicProgramming
{
    internal class MaximumProfit : IExecute
    {
        public void Execute()
        {
            Console.WriteLine("#################LeetCode#####################");
            Console.WriteLine("188.Best Time to Buy and Sell Stock IV");

            int[] sample1 = new int[] { 2, 4, 1 };
            Console.WriteLine("[{0}]", string.Join(", ", MaxProfit(2, sample1)));

            int[] sample2 = new int[] { 3, 2, 6, 5, 0, 3 };
            Console.WriteLine("[{0}]", string.Join(", ", MaxProfit(2, sample2)));
        }

        public static int MaxProfit(int k, int[] prices)
        {
            int n = prices.Length;
            if (n == 0 || k == 0) return 0;

            // If k >= n/2, it's equivalent to unlimited transactions
            if (k >= n / 2)
            {
                int profit = 0;
                for (int i = 1; i < n; i++)
                {
                    if (prices[i] > prices[i - 1])
                        profit += prices[i] - prices[i - 1];
                }
                return profit;
            }

            int[,] dp = new int[k + 1, n];

            for (int t = 1; t <= k; t++)
            {
                int maxDiff = -prices[0];
                for (int d = 1; d < n; d++)
                {
                    dp[t, d] = Math.Max(dp[t, d - 1], // Skip selling today
                                        prices[d] + maxDiff); // Sell today
                    maxDiff = Math.Max(maxDiff, dp[t - 1, d] - prices[d]); // best buy so far for previous transaction
                }
            }

            return dp[k, n - 1];
        }
    }
}
