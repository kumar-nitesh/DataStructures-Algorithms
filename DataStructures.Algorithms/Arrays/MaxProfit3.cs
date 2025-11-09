/********************************************************************************************
 * Best Time to Buy and Sell Stock III
 * 
 * Source     : LeetCode
 * Difficulty : Medium
 * Problem    : https://leetcode.com/problems/best-time-to-buy-and-sell-stock-iii
 * 
 * Time Complexity  : O(n)        
 * Space complexity : O(1) 
 ********************************************************************************************/



namespace DataStructures.Algorithms.Arrays
{
    public class MaximumProfit3 : IExecute
    {
        public void Execute()
        {
            Console.WriteLine("#################LeetCode#####################");
            Console.WriteLine("123.Best Time to Buy and Sell Stock III");

            int[] sample1 = new int[] { 7, 1, 5, 3, 6, 4 };
            Console.WriteLine("[{0}]", string.Join(", ", MaxProfit(sample1)));

            int[] sample2 = new int[] { 7, 6, 4, 3, 1 };
            Console.WriteLine("[{0}]", string.Join(", ", MaxProfit(sample2)));
        }

        public static int MaxProfit(int[] prices)
        {
            if (prices == null || prices.Length == 0)
                return 0;

            int buy1 = int.MinValue;   // Max profit after first buy
            int sell1 = 0;             // Max profit after first sell
            int buy2 = int.MinValue;   // Max profit after second buy
            int sell2 = 0;             // Max profit after second sell

            foreach (int price in prices)
            {
                // Step-by-step transitions
                buy1 = Math.Max(buy1, -price);        // Best to buy first stock
                sell1 = Math.Max(sell1, buy1 + price); // Best to sell first stock
                buy2 = Math.Max(buy2, sell1 - price);  // Best to buy second stock using profit from first
                sell2 = Math.Max(sell2, buy2 + price); // Best to sell second stock
            }

            return sell2; // Final profit after at most 2 transactions
        }
    }
}
