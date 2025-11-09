using System;
using System.Text;

namespace DataStructures.Algorithms.Strings
{
    internal class ReorganizeString : IExecute
    {
        public void Execute()
        {
            Console.WriteLine("#################LeetCode#####################");
            Console.WriteLine("767.Reorganize String");

            string inputString = "aab";
            Console.WriteLine(ReorganizeUsingPriorityQueue(inputString));
            Console.WriteLine(ReorganizeUsingArrayPlacement("bfrbs"));
            string inputAnotherString = "aaab";
            Console.WriteLine(ReorganizeUsingPriorityQueue(inputAnotherString));
            Console.WriteLine(ReorganizeUsingArrayPlacement(inputAnotherString));
        }

        /// <summary>
        /// Time Complexity: O(n log k) where n is the length of the string and k is the number of unique characters.
        /// Space Complexity: O(k) for the priority queue.
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        private string ReorganizeUsingPriorityQueue(string inputString)
        {
            // Step 1: Count frequency of each character
            Dictionary<char, int> charCount = new Dictionary<char, int>();

            foreach (char c in inputString)
            {
                if (charCount.ContainsKey(c))
                {
                    charCount[c]++;
                }
                else
                {
                    charCount[c] = 1;
                }
            }

            // Step 2: Check if rearrangement is possible
            // If any character appears more than (n+1)/2 times, impossible
            // If any character appears more than half the time(rounded up), it's impossible to rearrange the string
            int maxFrequency = charCount.Values.Max();

            if (maxFrequency > (inputString.Length + 1) / 2)
            {
                return string.Empty;
            }

            // Step 3: Create a max-heap (priority queue) based on character frequency
            // Time Complexity: O(n log k) where n is the length of the string and k is the number of unique characters.
            // Space Complexity: O(k) for the priority queue.
            PriorityQueue<char, int> priorityQueue = new PriorityQueue<char, int>();
            foreach (var kvp in charCount)
            {
                priorityQueue.Enqueue(kvp.Key, -kvp.Value); // Negate frequency for max-heap behavior
            }

            // Step 4: Build the result
            StringBuilder result = new StringBuilder();
            int prevCount = 0;
            char prevChar = '\0';
            while (priorityQueue.Count > 0)
            {
                var currentChar = priorityQueue.Dequeue(); // Gets the most frequent character
                int currentCharCount = charCount[currentChar];
                result.Append(currentChar);
                currentCharCount--;

                if (prevCount > 0)
                {
                    priorityQueue.Enqueue(prevChar, -prevCount); // Key Point: The "Holding Pattern"
                }

                prevChar = currentChar; // Store the current character as previous for the next iteration
                prevCount = currentCharCount;
                charCount[currentChar] = currentCharCount;
            }

            return result.ToString();
        }

        private string ReorganizeUsingArrayPlacement(string s)
        {
            // Count frequency of each character
            int[] freq = new int[26];
            foreach (char c in s)
            {
                freq[c - 'a']++;
            }

            // Find the character with max frequency
            int maxFreq = 0;
            int maxChar = 0;
            for (int i = 0; i < 26; i++)
            {
                if (freq[i] > maxFreq)
                {
                    maxFreq = freq[i];
                    maxChar = i;
                }
            }

            // Check if rearrangement is possible
            if (maxFreq > (s.Length + 1) / 2)
                return "";

            // Create result array
            char[] result = new char[s.Length];
            int index = 0;

            // Place the most frequent character first at even positions (0, 2, 4...)
            while (freq[maxChar] > 0)
            {
                result[index] = (char)(maxChar + 'a');
                index += 2;
                freq[maxChar]--;
            }

            // Place remaining characters
            for (int i = 0; i < 26; i++)
            {
                while (freq[i] > 0)
                {
                    // If we've filled all even positions, start filling odd positions
                    if (index >= s.Length)
                    {
                        index = 1;
                    }

                    result[index] = (char)(i + 'a');
                    index += 2;
                    freq[i]--;
                }
            }

            return new string(result);
        }
    }
}
