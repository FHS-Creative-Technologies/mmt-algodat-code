// AlgoDat Implementation
// Copyright (C) 2024  Fachhochschule Salzburg / Department Creative Technologies / Andreas Bilke

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace FHS.CT.AlgoDat.DataStructures
{
    public class Tree<T>
    {
        public class Node
        {
            public T Key { get; }

            public Node? Left { get; set; }
            public Node? Right { get; set; }

            public Node(T key)
            {
                Key = key;
            }
        }

        public Node? Root { get; set; }

        public void InorderTreeWalk(Node? node)
        {
            if (node == null)
            {
                return;
            }

            InorderTreeWalk(node.Left);
            Console.WriteLine(node.Key);
            InorderTreeWalk(node.Right);
        }
    }
}
