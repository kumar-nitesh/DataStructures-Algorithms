﻿namespace DataStructures.Algorithms.LinkedList
{
    public partial class LinkedList
    {
        /// <summary>
        /// Remove first and add last.
        /// </summary>
        /// Time Complexity: O(n) 
        /// Auxiliary Space: O(1)
        public Node RemoveDuplicatesSorted(LinkedList list)
        {
            Node current = list.Head;
            Node nextNext = null;

            if (current == null || current.Next == null)
            {
                return null;
            }

            while (current?.Next != null)
            {
                if (current.Data == current.Next.Data)
                {
                    nextNext = current.Next.Next;
                    current.Next = null;
                    current.Next = nextNext;
                }
                else
                {
                    current = current.Next;
                }
            }

            return list.Head;
        }
    }
}
