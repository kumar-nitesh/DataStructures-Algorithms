/********************************************************************************************
 * Evaluate Reverse Polish Notation
 * 
 * Source     : LeetCode
 * Difficulty : Medium
 * Problem    : https://leetcode.com/problems/evaluate-reverse-polish-notation
 * Solution   : 
 * 
 * Time Complexity  : O(n)        
 * Space complexity : O(n) 
 ********************************************************************************************/

namespace DataStructures.Algorithms.Stacks
{
    public class PolishNotations : IExecute
    {
        public void Execute()
        {
            Console.WriteLine("#################LeetCode#####################");
            Console.WriteLine("150.Evaluate Reverse Polish Notation");

            string[] tokens = ["2", "1", "+", "3", "*"];
            Console.WriteLine("Output :  " + string.Join(", ", EvalRPN(tokens)));
        }
        
        public int EvalRPN(string[] tokens)
        {
            Stack<int> stack = new Stack<int>();

            foreach(var token in tokens)
            {
                if (token == "+" || token == "-" || token == "*" || token == "/")
                {
                    int b = stack.Pop();
                    int a = stack.Pop();

                    int result = 0;

                    switch (token)
                    {
                        case "+": result = a + b; break;
                        case "-": result = a - b; break;
                        case "*": result = a * b; break;
                        case "/": result = a / b; break;
                    }

                    stack.Push(result);
                }
                else
                {
                    stack.Push(Convert.ToInt32(token));
                }
            }

            return stack.Pop();
        }
    }
}
