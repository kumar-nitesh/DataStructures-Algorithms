/********************************************************************************************
 * Count Different Palindromic Subsequences
 * 
 * Source     : LeetCode
 * Difficulty : Hard
 * Problem    : https://leetcode.com/problems/count-different-palindromic-subsequences
 * Solution   : 
 * 
 * Time Complexity  : O(n²)        
 * Space complexity : O(n²) 
 ********************************************************************************************/

using System.Runtime.Intrinsics.Arm;

namespace DataStructures.Algorithms.DynamicProgramming
{
    internal class CountPalindromicSubsequences : IExecute
    {
        private const int MOD = 1000000007;

        public void Execute()
        {
            Console.WriteLine("#################LeetCode#####################");
            Console.WriteLine("730.Count Different Palindromic Subsequences");

            Console.WriteLine("The closest number to zero is : " + Count("bccb"));
            //Console.WriteLine("The closest number to zero is : " + CountDebug("bccb"));
            //Console.WriteLine("The closest number to zero is : " + Count("abcdabcdabcdabcdabcdabcdabcdabcddcbadcbadcbadcbadcbadcbadcbadcba"));
        }

        public int Count(string s)
        {
            int n = s.Length;

            int[,] dp = new int[n, n];

            // Base case: single characters are palindromes
            for (int i = 0; i < n; i++)
            {
                dp[i, i] = 1;
            }

            // Fill DP table for increasing lengths
            for (int len = 2; len <= n; len++)
            {
                for (int i = 0; i <= n - len; i++)
                {
                    int j = i + len - 1;

                    // Case 1: Same character at both ends
                    if (s[i] == s[j])
                    {
                        int left = i + 1;
                        int right = j - 1;

                        // Find next occurrence of this character within the from left
                        while (left <= right && s[left] != s[i])
                            left++;

                        // Find previous of this character within the from right
                        while (left <= right && s[right] != s[j])
                            right--;

                        if (left > right)
                        {
                            // No same character in middle → both ends create 2 new palindromes
                            // Example: "aba" -> "a","b","aba"
                            dp[i, j] = (2 * dp[i + 1, j - 1] + 2);
                        }
                        else if (left == right)
                        {
                            // One same character in middle → add 1 extra palindrome
                            // Example: "aaa" -> "a","aa","aaa"
                            dp[i, j] = (2 * dp[i + 1, j - 1] + 1);
                        }
                        else
                        {
                            // Multiple same characters in middle
                            // Wrap middle palindromes, but subtract overlapping part
                            // Two or more same chars inside → remove duplicates between left and right
                            dp[i, j] = (2 * dp[i + 1, j - 1] - dp[left + 1, right - 1]);
                        }
                    }
                    else
                    {
                        // Case 2: Different characters at ends
                        // Combine left and right sides, but subtract overlap
                        dp[i, j] = (dp[i + 1, j] + dp[i, j - 1] - dp[i + 1, j - 1]);
                    }

                    // Modulo correction to prevent negatives
                    dp[i, j] = (dp[i, j] % MOD + MOD) % MOD;
                }
            }

            return dp[0, n - 1];
        }

        public int CountDebug(string s)
        {
            int n = s.Length;
            int[,] dp = new int[n, n];

            Console.WriteLine($"==========================================");
            Console.WriteLine($"INPUT STRING: \"{s}\"");
            Console.WriteLine($"LENGTH: {n}");
            Console.WriteLine($"==========================================\n");

            // STEP 1: Base case
            Console.WriteLine("STEP 1: Initialize Base Cases (Single Characters)");
            Console.WriteLine("--------------------------------------------------");
            for (int i = 0; i < n; i++)
            {
                dp[i, i] = 1;
                Console.WriteLine($"dp[{i}][{i}] = 1  (substring \"{s[i]}\")");
            }
            Console.WriteLine();
            PrintDPTable(dp, n, s);
            Console.WriteLine();

            // STEP 2: Fill DP table
            for (int len = 2; len <= n; len++)
            {
                Console.WriteLine($"\n{'=' * 60}");
                Console.WriteLine($"PROCESSING LENGTH {len}");
                Console.WriteLine($"{'=' * 60}\n");

                for (int i = 0; i <= n - len; i++)
                {
                    int j = i + len - 1;
                    string substring = s.Substring(i, len);

                    Console.WriteLine($"┌─────────────────────────────────────────┐");
                    Console.WriteLine($"│ Computing dp[{i}][{j}] for \"{substring}\"");
                    Console.WriteLine($"│ s[{i}] = '{s[i]}', s[{j}] = '{s[j]}'");
                    Console.WriteLine($"└─────────────────────────────────────────┘");

                    if (s[i] == s[j])
                    {
                        Console.WriteLine($"✓ SAME character at both ends: '{s[i]}'");

                        int left = i + 1;
                        int right = j - 1;

                        Console.WriteLine($"  Searching for '{s[i]}' inside [{i + 1}...{j - 1}]");
                        Console.WriteLine($"  Initial: left={left}, right={right}");

                        // Find next occurrence from left
                        int originalLeft = left;
                        while (left <= right && s[left] != s[i])
                        {
                            left++;
                        }
                        if (left > originalLeft)
                            Console.WriteLine($"  Moved left pointer: {originalLeft} → {left}");

                        // Find previous occurrence from right
                        int originalRight = right;
                        while (left <= right && s[right] != s[j])
                        {
                            right--;
                        }
                        if (right < originalRight)
                            Console.WriteLine($"  Moved right pointer: {originalRight} → {right}");

                        Console.WriteLine($"  Final: left={left}, right={right}");

                        if (left > right)
                        {
                            Console.WriteLine($"  → SUBCASE 1: No '{s[i]}' in middle");
                            Console.WriteLine($"  Formula: 2 * dp[{i + 1}][{j - 1}] + 2");
                            Console.WriteLine($"         = 2 * {dp[i + 1, j - 1]} + 2");
                            dp[i, j] = (2 * dp[i + 1, j - 1] + 2) % MOD;
                            Console.WriteLine($"         = {dp[i, j]}");
                        }
                        else if (left == right)
                        {
                            Console.WriteLine($"  → SUBCASE 2: ONE '{s[i]}' at position {left}");
                            Console.WriteLine($"  Formula: 2 * dp[{i + 1}][{j - 1}] + 1");
                            Console.WriteLine($"         = 2 * {dp[i + 1, j - 1]} + 1");
                            dp[i, j] = (2 * dp[i + 1, j - 1] + 1) % MOD;
                            Console.WriteLine($"         = {dp[i, j]}");
                        }
                        else
                        {
                            Console.WriteLine($"  → SUBCASE 3: Multiple '{s[i]}' (left={left}, right={right})");
                            Console.WriteLine($"  Formula: 2 * dp[{i + 1}][{j - 1}] - dp[{left + 1}][{right - 1}]");
                            Console.WriteLine($"         = 2 * {dp[i + 1, j - 1]} - {dp[left + 1, right - 1]}");
                            dp[i, j] = (2 * dp[i + 1, j - 1] - dp[left + 1, right - 1] + MOD) % MOD;
                            Console.WriteLine($"         = {dp[i, j]}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"✗ DIFFERENT characters: '{s[i]}' ≠ '{s[j]}'");
                        Console.WriteLine($"  Formula: dp[{i + 1}][{j}] + dp[{i}][{j - 1}] - dp[{i + 1}][{j - 1}]");
                        Console.WriteLine($"         = {dp[i + 1, j]} + {dp[i, j - 1]} - {dp[i + 1, j - 1]}");
                        dp[i, j] = (dp[i + 1, j] + dp[i, j - 1] - dp[i + 1, j - 1] + MOD) % MOD;
                        Console.WriteLine($"         = {dp[i, j]}");
                    }

                    Console.WriteLine($"  ✓ dp[{i}][{j}] = {dp[i, j]}");
                    Console.WriteLine();
                }

                PrintDPTable(dp, n, s);
            }

            Console.WriteLine($"\n{'=' * 60}");
            Console.WriteLine($"FINAL ANSWER: dp[0][{n - 1}] = {dp[0, n - 1]}");
            Console.WriteLine($"{'=' * 60}\n");

            return dp[0, n - 1];
        }

        private void PrintDPTable(int[,] dp, int n, string s)
        {
            Console.WriteLine("Current DP Table:");
            Console.WriteLine("─────────────────");

            // Print header
            Console.Write("\t");
            for (int j = 0; j < n; j++)
            {
                Console.Write($"j={j}  ");
            }
            Console.WriteLine();

            Console.Write("\t");
            for (int j = 0; j < n; j++)
            {
                Console.Write($"'{s[j]}'  ");
            }
            Console.WriteLine();

            // Print table
            for (int i = 0; i < n; i++)
            {
                Console.Write($"i={i} '{s[i]}' ");
                for (int j = 0; j < n; j++)
                {
                    if (j < i)
                        Console.Write(" -   ");
                    else
                        Console.Write($"{dp[i, j],3}  ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
