/********************************************************************************************
 * Sort Colors
 * 
 * Source     : LeetCode
 * Difficulty : Medium
 * Problem    : https://leetcode.com/problems/sort-colors/
 * Solution   : https://www.youtube.com/watch?v=J48aGjfjYTI
 * 
 * Time Complexity  : O(n)        
 * Space complexity : O(1) 
 ********************************************************************************************/

namespace DataStructures.Algorithms.Arrays
{
    public class DutchNationFlag : IExecute
    {
        public void Execute()
        {
            Console.WriteLine("#################LeetCode#####################");
            Console.WriteLine("75.Sort Colors");

            int[] array1DNF = { 2, 2, 2, 0, 0, 0, 1, 1 };
            int[] array2DNF = { 1, 2, 0 };

            Console.WriteLine("[{0}]", string.Join(", ", Sort(array1DNF)));
            Console.WriteLine("[{0}]", string.Join(", ", Sort(array2DNF)));
        }

        public static int[] Sort(int[] array)
        {
            int low = 0;
            int high = array.Length - 1;

            for (int mid = 0; mid <= high; mid++)
            {
                if (array[mid] == 0)
                {
                    Swap(array, low, mid);
                    low++;
                }

                if (array[mid] == 2)
                {
                    Swap(array, mid, high);
                    --high;
                    --mid;
                }
            }

            return array;
        }
        private static void Swap(int[] array, int x, int y)
        {            
            int temp = array[x];
            array[x] = array[y];
            array[y] = temp;
        }
    }
}
