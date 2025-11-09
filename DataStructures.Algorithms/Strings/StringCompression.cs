namespace DataStructures.Algorithms.Strings
{
    internal class StringCompression : IExecute
    {
        public int Compression(char[] chars)
        {
            int read = 0;
            int write = 0;

            while (read < chars.Length)
            {
                char currentChar = chars[read];
                int count = 0;

                while(read < chars.Length && chars[read] == currentChar)
                {
                    read++;
                    count++;
                }

                chars[write++] = currentChar;

                if(count > 1)
                {
                    foreach(char c in count.ToString())
                    {
                        chars[write++] = c;
                    }
                }
            }

            return write;
        }

        public void Execute()
        {
            Console.WriteLine("#################LeetCode#####################");
            Console.WriteLine("443.String Compression");

            char[] chars = ['a','a','b','b','c','c','c'];

            Console.WriteLine(Compression(chars));
        }
    }
}
