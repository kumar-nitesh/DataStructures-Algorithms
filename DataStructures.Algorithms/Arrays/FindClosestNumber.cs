/********************************************************************************************
 * Find the closest number to zero
 * 
 * Source     : LeetCode
 * Difficulty : Easy
 * Problem    : https://leetcode.com/problems/find-closest-number-to-zero
 * Solution   : 
 * 
 * Time Complexity  : O(n)        
 * Space complexity : O(1) 
 ********************************************************************************************/

namespace DataStructures.Algorithms.Arrays
{
    internal class FindClosestNumber : IExecute
    {
        public void Execute()
        {
            Console.WriteLine("#################LeetCode#####################");
            Console.WriteLine("2239.Find the closest number to zero");

            int[] prices1 = [-4, -2, 1, 4, 8];
            Console.WriteLine("The closest number to zero is : " + Find(prices1));

            int[] prices2 = [-100000, -100000];
            Console.WriteLine("The closest number to zero is : " + Find(prices2));

            int[] prices3 = [2, -1, 1];
            Console.WriteLine("The closest number to zero is : " + Find(prices3));
        }

        public int Find(int[] nums)
        {
            int closestNumber = nums[0];
            int minDistanceToZero = Math.Abs(nums[0]);
            for (int i = 1; i < nums.Length;  i++)
            {
                int currentNumber = nums[i];
                int currentDistance = Math.Abs(currentNumber) - 0;

                if(minDistanceToZero > currentDistance || (currentDistance == minDistanceToZero && currentNumber > closestNumber))
                {
                    closestNumber = currentNumber;
                    minDistanceToZero = currentDistance;
                }
            }

            return closestNumber;
        }
    }
}
