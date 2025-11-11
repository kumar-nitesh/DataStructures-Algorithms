/********************************************************************************************
 * Trapping Rain Water
 * 
 * Source     : LeetCode, Geeks for Geeks - Must Do
 * Difficulty : Hard
 * Problem    : https://leetcode.com/problems/trapping-rain-water
 * Solution   : https://www.youtube.com/watch?v=C8UjlJZsHBw
 * 
 * Time Complexity  : O(n)        
 * Space complexity : O(1) 
 ********************************************************************************************/

namespace DataStructures.Algorithms.Arrays
{
    public class TrapRainWater : IExecute
    {
        public void Execute()
        {
            Console.WriteLine("#################LeetCode#####################");
            Console.WriteLine("42.Trapping Rain Water");

            int[] height = new int[] { 0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1 };
            Console.WriteLine(TrapWater(height));
        }

        /// <summary>
        /// 0. First and Last block can never trap water.
        /// 1. Find Water Level = min(max[Left],max[Right])
        /// 2. Trapped Water = (Water Level - Height of current block) * width
        /// 3. Calculate the Min and Max Window.
        /// 4. Two Pointers - left and right should move towards each other.
        /// </summary>
        public static int TrapWater(int[] height)
        {
            if (height == null || height.Count() == 0)
            {
                return 0;
            }
            int left = 0; int right = height.Count() - 1; // Pointers to both ends of the array.
            int maxLeft = 0; int maxRight = 0;

            int totalWater = 0;
            while (left < right)
            {
                // Water could, potentially, fill everything from left to right, if there is nothing in between.
                if (height[left] < height[right])
                {
                    // If the current elevation is greater than the previous maximum, water cannot occupy that point at all.
                    // However, we do know that everything from maxLeft to the current index, has been optimally filled, as we've
                    // been adding water to the brim of the last maxLeft.
                    if (height[left] >= maxLeft)
                    {
                        // So, we say we've found a new maximum, and look to see how much water we can fill from this point on.
                        maxLeft = height[left];
                        // If we've yet to find a maximum, we know that we can fill the current point with water up to the previous
                        // maximum, as any more will overflow it. We also subtract the current height, as that is the elevation the
                        // ground will be at.
                    }
                    else
                    {
                        totalWater += maxLeft - height[left];
                    }
                    // Increment left, we'll now look at the next point.
                    left++;
                    // If the height at the left is NOT greater than height at the right, we cannot fill from left to right without over-
                    // flowing; however, we do know that we could potentially fill from right to left, if there is nothing in between.
                }
                else
                {
                    // Similarly to above, we see that we've found a height greater than the max, and cannot fill it whatsoever, but
                    // everything before is optimally filled
                    if (height[right] >= maxRight)
                    {
                        // We can say we've found a new maximum and move on.  
                        maxRight = height[right];
                        // If we haven't found a greater elevation, we can fill the current elevation with maxRight - height[right]
                        // water.
                    }
                    else
                    {
                        totalWater += maxRight - height[right];
                    }
                    // Decrement left, we'll look at the next point.
                    right--;
                }
            }
            // Return the sum we've been adding to.
            return totalWater;
        }
    }
}
