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
    public class BTree<T> where T : IComparable<T>
    {
        public class Node
        {
            public int N { get; set; }

            public bool Leaf { get; set; }

            public T?[] Keys { get; }

            public Node?[] Children { get; }

            public Node(int minimumDegree)
            {
                Keys = new T[2*minimumDegree - 1];
                Children = new Node[2*minimumDegree];
                N = 0;
                Leaf = true;
            }
        }

        public Node Root { get; private set; }

        private readonly int _minDegree; // t

        public BTree(int t)
        {
            _minDegree = t;
            Root = new Node(_minDegree);
        }

        public T? Search(T value)
        {
            return Search(value, Root);
        }

        private static T? Search(T key, Node n)
        {
            var i = 0;

            while (i < n.N && key.CompareTo(n.Keys[i]) > 0)
            {
                i++;
            }

            if (i < n.N && key.CompareTo(n.Keys[i]) == 0)
            {
                return n.Keys[i];
            }

            // ReSharper disable once TailRecursiveCall
            // If we are at a leaf and didn't found the key, return null-ish values
            // otherwise continue search in child where key might be
            return n.Leaf ? default : Search(key, n.Children[i]!);
        }

        /// <summary>
        /// Split an overfull child (at position <c>childIndex</c>) in two.
        /// The idea is: n has 2*t - 1 keys (meaning its full)
        /// We copy the upper half to a (newly created) sibling. The middle becomes
        /// a new key in <c>n</c>. This is possible since by construction of our algorithm
        /// n is non-full at this point.
        /// </summary>
        /// <param name="n">A node with an overfull child at position childIndex</param>
        /// <param name="childIndex">Index of the overfull child</param>
        private void SplitChild(Node n, int childIndex)
        {
            var childNode = n.Children[childIndex]!; // by construction, this is non-null
            Node sibling = new(_minDegree);

            sibling.Leaf = childNode.Leaf; // if childNode was a leaf, its sibling must also be one
            sibling.N = _minDegree - 1; // childNode is full, meaning is has 2t - 1 keys
                                        // => t - 1 remains in childNode, t - 1 are going to sibling and one key moves up
                                        // to n

            // copy the upper half of the keys from childNode to its sibling
            for (var j = 0; j < _minDegree - 1; j++)
            {
                sibling.Keys[j] = childNode.Keys[j + _minDegree];
                childNode.Keys[j + _minDegree] = default; // release reference
            }
            // childNode was an internal node, copy also its children pointer
            if (!childNode.Leaf)
            {
                for (var j = 0; j < _minDegree; j++)
                {
                    sibling.Children[j] = childNode.Children[j + _minDegree];
                    childNode.Children[j + _minDegree] = default; // release reference
                }
            }

            childNode.N = _minDegree - 1; // same argument as an sibling.N = ...

            // shift all children right of childIndex in parent node to the right
            for (var j = n.N + 1; j > childIndex + 1; j--)
            {
                n.Children[j] = n.Children[j - 1];
            }
            n.Children[childIndex + 1] = sibling;
            // shift all keys right of childIndex in parent node to the right
            for (var j = n.N; j > childIndex; j--)
            {
                n.Keys[j] = n.Keys[j - 1];
            }
            n.Keys[childIndex] = childNode.Keys[_minDegree - 1];
            n.N++;
            childNode.Keys[_minDegree - 1] = default; // erase key from child which was moved upwards
        }
    
        public void Insert(T key)
        {
            if (Root.N == 2 * _minDegree - 1) // root is full
            {
                var newRoot = SplitRoot();
                InsertNonFull(newRoot, key);
            }
            else
            {
                InsertNonFull(Root, key);
            }
        }

        private Node SplitRoot()
        {
            Node newRoot = new(_minDegree);
            newRoot.Leaf = false;
            newRoot.Children[0] = Root;

            Root = newRoot;
            SplitChild(newRoot, 0);

            return newRoot;
        }

        private void InsertNonFull(Node n, T key)
        {
            var i = n.N - 1;
            if (n.Leaf) // insert node in leaf
            {
                // shift keys to create space for a new key
                while (i >= 0 && key.CompareTo(n.Keys[i]) < 0)
                {
                    n.Keys[i + 1] = n.Keys[i];
                    i--;
                }
                n.Keys[i + 1] = key;
                n.N++;
            }
            else // internal node
            {
                while (i >= 0 && key.CompareTo(n.Keys[i]) < 0)
                {
                    i--;
                }
                i++;

                if (n.Children[i]!.N == 2 * _minDegree - 1) // child is full. By construction the ith child is non-null
                {
                    SplitChild(n, i);
                    if (key.CompareTo(n.Keys[i]) > 0) // should key go into left or right subtree
                    {                                 // since we bubbled on child key up, we don't know the value in advance
                        i++;
                    }
                }

                // ReSharper disable once TailRecursiveCall
                InsertNonFull(n.Children[i]!, key);
            }
        }

        public void Delete(T key)
        {
            Delete(Root, key);
        }

        private void Delete(Node n, T key)
        {
            // case 1
            if (n.Leaf)
            {
                DeleteInLeaf(n, key);

                return;
            }

            var i = n.N - 1;
            while (i > 0 && key.CompareTo(n.Keys[i]) < 0)
            {
                i--;
            }

            // case 2
            if (key.CompareTo(n.Keys[i]) == 0)
            {
                DeleteInternalAt(n, key, i);
            }
            else // case 3
            {
                DeleteInternalSubtree(n, key, i);
            }
        }

        /// <summary>
        /// Case 2 in deleting a key
        /// </summary>
        /// <param name="node">The node where the key resides</param>
        /// <param name="key">The key we want to delete</param>
        /// <param name="position">The position of the key within the key list</param>
        private void DeleteInternalAt(Node node, T key, int position)
        {
            // case 2a. Children[i] has t keys (one more than minimum)
            if (node.Children[position]!.N >= _minDegree)
            {
                // replace current position by predecessor
                var predecessor = FindPredecessor(node.Children[position]!);
                Delete(node.Children[position]!, predecessor);
                node.Keys[position] = predecessor;
            }
            // case 2b. Children[i + 1] has t keys
            else if (node.Children[position + 1]!.N >= _minDegree)
            {
                // replace current position by successor
                var successor = FindSuccessor(node.Children[position + 1]!);
                Delete(node.Children[position + 1]!, successor);
                node.Keys[position] = successor;
            }
            // case 2c
            // this operation could leave the root node empty
            else
            {
                // neither Children[i] nor Children[i + 1] has enough keys
                // meaning just t - 1 keys
                // merge all of them, ie copy k and its sibling to Children[i]
                // Then move all keys by one position in node to the left
                // then delete k in Children[i] subtree
                Merge(node.Children[position]!, key, node.Children[position + 1]!);
                MoveToLeftAndShrink(node, position, true); // keeping freshly merged child
                Delete(node.Children[position]!, key);

                CheckAndFixEmptyRoot();
            }
        }

        private void CheckAndFixEmptyRoot()
        {
            if (Root.N != 0)
            {
                return;
            }

            // root can only have one child now
            // make this one the new root
            Root = Root.Children[0]!;
        }

        /// <summary>
        /// Merge <c>n1</c>, <c>key</c> and <c>n2</c>
        /// by appending <c>key</c> followed by <c>n2</c>
        /// to <c>n1</c>.
        /// <remarks>Note that <c>key</c> and <c>n2</c> will not get deleted in this method</remarks>
        /// </summary>
        /// <param name="n1">Node where appending happens</param>
        /// <param name="key">Key from parent node forming the median of new node</param>
        /// <param name="n2">Node which gets appended to <c>n1</c></param>
        private static void Merge(Node n1, T key, Node n2)
        {
            n1.Keys[n1.N] = key;
            for (var i = 0; i < n2.N; i++)
            {
                n1.Keys[n1.N + 1 + i] = n2.Keys[i];
                n1.Children[n1.N + 1 + i] = n2.Children[i];
            }
            n1.Children[n1.N + 1 + n2.N] = n2.Children[n2.N]; // remaining last child pointer
            n1.N = n1.N + 1 + n2.N; // this should be 2*t - 1
        }

        /// <summary>
        /// Case 3 in deleting a key
        /// </summary>
        /// <param name="node">The node where we are searching for key</param>
        /// <param name="key">The key we want to delete</param>
        /// <param name="position">The position where our key search stopped</param>
        private void DeleteInternalSubtree(Node node, T key, int position)
        {
            // key must either be in the i or i + 1 subtree
            Node subTree;
            int subTreeIndex;
            if (key.CompareTo(node.Keys[position]) < 0)
            {
                subTree = node.Children[position]!;
                subTreeIndex = position;
            }
            else
            {
                subTree = node.Children[position + 1]!;
                subTreeIndex = position + 1;
            }

            // subtree has not enough keys, fix situation with 3a or 3b
            if (subTree.N == _minDegree - 1)
            {
                DeleteInternalFixUnderfullSubtrees(node, position, subTree, subTreeIndex);
            }

            // in any case: delete key within the subtree
            Delete(subTree, key);
        }

        /// <summary>
        /// Fix <c>subTree</c> where the deletion of <c>key></c> will be executed afterwards.
        /// This <c>subTree</c> has only t-1 keys and might go underfull when removing <c>key</c>
        /// This will execute case 3a/3b in the deletion process.
        /// </summary>
        /// <param name="node">The node where a child has not enough keys</param>
        /// <param name="positionInNodeKeys">The position of <c>subTree</c> in <c>node</c>'s child list</param>
        /// <param name="subTree">The sub tree with just t - 1 keys</param>
        /// <param name="subTreePosition">The index where <c>subTree</c> resides in <c>node</c></param>
        /// <exception cref="NotImplementedException"></exception>
        private void DeleteInternalFixUnderfullSubtrees(Node node, int positionInNodeKeys, Node subTree, int subTreePosition)
        {
            // case 3a. Left sibling has enough keys
            if (subTreePosition > 0 && node.Children[subTreePosition - 1]!.N >= _minDegree)
            {
                MoveKeyFromLeftSibling(node, positionInNodeKeys, subTree, subTreePosition);
            }
            // case 3a. Right sibling has enough keys
            else if (subTreePosition < node.N && node.Children[subTreePosition + 1]!.N >= _minDegree)
            {
                MoveKeyFromRightSibling(node, positionInNodeKeys, subTree, subTreePosition);
            }
            // case 3b. Left sibling has just t -1 keys. Merge with subTree
            // this operation could leave the root node empty
            else if (subTreePosition > 0 && node.Children[subTreePosition - 1]!.N == _minDegree - 1)
            {
                MergeWithLeftSibling(node, positionInNodeKeys, subTree, subTreePosition);
                CheckAndFixEmptyRoot();
            }
            // case 3b. Right sibling has just t -1 keys. Merge with subTree
            // this operation could leave the root node empty
            else if (subTreePosition < node.N && node.Children[subTreePosition + 1]!.N == _minDegree - 1)
            {
                MergeWithRightSibling(node, positionInNodeKeys, subTree, subTreePosition);
                CheckAndFixEmptyRoot();
            }
        }

        /// <summary>
        /// Merging <c>subTree</c> with its right sibling by appending it to <c>subTree</c>
        /// </summary>
        /// <param name="node">Parent node which children get merged</param>
        /// <param name="positionInNodeKeys">Position in <c>node</c> keys where operations happen</param>
        /// <param name="subTree">Subtree which gets merged</param>
        /// <param name="subTreePosition">Position of subtree in <c>node</c> children list</param>
        private static void MergeWithRightSibling(Node node, int positionInNodeKeys, Node subTree, int subTreePosition)
        {
            var rightSibling = node.Children[subTreePosition + 1]!;
            Merge(subTree, node.Keys[positionInNodeKeys]!, rightSibling);
            // node such that the moving works
            node.Children[subTreePosition + 1] = node.Children[subTreePosition];
            // shift everything in node by one position, but move subTrees position in children list of
            // if subTree is the right child of node, we need to keep Children[positionInNodeKeys] child pointer
            MoveToLeftAndShrink(node, positionInNodeKeys, positionInNodeKeys == subTreePosition);
        }

        /// <summary>
        /// Merging <c>subTree</c> with its left sibling by appending it to sibling
        /// </summary>
        /// <param name="node">Parent node which children get merged</param>
        /// <param name="positionInNodeKeys">Position in <c>node</c> keys where operations happen</param>
        /// <param name="subTree">Subtree which gets merged</param>
        /// <param name="subTreePosition">Position of subtree in <c>node</c> children list</param>
        private static void MergeWithLeftSibling(Node node, int positionInNodeKeys, Node subTree, int subTreePosition)
        {
            var leftSibling = node.Children[subTreePosition - 1]!;
            Merge(leftSibling, node.Keys[positionInNodeKeys]!, subTree);
            // shift all everything in node by one position
            // if subTree is the right child of node, we need to keep Children[positionInNodeKeys] child pointer
            MoveToLeftAndShrink(node, positionInNodeKeys, positionInNodeKeys == subTreePosition);
        }

        /// <summary>
        /// Move one key (together with corresponding child) from the right sibling of <c>subTree</c> to <c>node</c>
        /// and move key from <c>node</c> down to <c>subTree</c>
        /// </summary>
        /// <param name="node">Node which involves swapping of keys</param>
        /// <param name="positionInNodeKeys">Position in <c>node</c> where the swap happens</param>
        /// <param name="subTree">Reference to node which gets an additional key</param>
        /// <param name="subTreePosition">Reference to sibling which loses one key</param>
        private static void MoveKeyFromRightSibling(Node node, int positionInNodeKeys, Node subTree, int subTreePosition)
        {
            var subTreeRightSibling = node.Children[subTreePosition + 1]!;

            // move element down from node
            subTree.Keys[subTree.N] = node.Keys[positionInNodeKeys];
            // left right first key replaces old key in node
            node.Keys[positionInNodeKeys] = subTreeRightSibling.Keys[0];

            // right siblings first child becomes subTrees last child
            subTree.Children[subTree.N + 1] = subTreeRightSibling.Children[0]!;
            MoveToLeftAndShrink(subTreeRightSibling, 0);
            subTreeRightSibling.Children[subTreeRightSibling.N + 1] = default; // release reference
            subTree.N++;
        }

        /// <summary>
        /// Move one key (together with corresponding child) from the left sibling of <c>subTree</c> to <c>node</c>
        /// and move key from <c>node</c> down to <c>subTree</c>
        /// </summary>
        /// <param name="node">Node which involves swapping of keys</param>
        /// <param name="positionInNodeKeys">Position in <c>node</c> where the swap happens</param>
        /// <param name="subTree">Reference to node which gets an additional key</param>
        /// <param name="subTreePosition">Reference to sibling which loses one key</param>
        private static void MoveKeyFromLeftSibling(Node node, int positionInNodeKeys, Node subTree, int subTreePosition)
        {
            var subTreeLeftSibling = node.Children[subTreePosition - 1]!;
            MoveToRightAndGrow(subTree, 0); // increase size by one

            // move element down from node
            subTree.Keys[0] = node.Keys[positionInNodeKeys];
            // left siblings last key replaces old key in node
            node.Keys[positionInNodeKeys] = subTreeLeftSibling.Keys[subTreeLeftSibling.N - 1];

            // left siblings last child becomes subTrees first child
            subTree.Children[0] = subTreeLeftSibling.Children[subTreeLeftSibling.N]!;
            subTreeLeftSibling.Children[subTreeLeftSibling.N] = default; // release reference
            subTreeLeftSibling.N--;
        }

        private static void DeleteInLeaf(Node n, T key)
        {
            var i = n.N - 1;
            while (i > 0 && key.CompareTo(n.Keys[i]) < 0)
            {
                i--;
            }

            // we are at a leaf but did not found our key
            // do nothing in this case
            if (key.CompareTo(n.Keys[i]) != 0)
            {
                return;
            }

            MoveToLeftAndShrink(n, i);
        }

        private static T FindPredecessor(Node n)
        {
            // we are already within a subtree. Now go to right until
            // a leaf is reached
            while (!n.Leaf)
            {
                n = n.Children[n.N]!; // by construction this is non-null
            }

            return n.Keys[n.N - 1]!;
        }

        private static T FindSuccessor(Node n)
        {
            // we are already within a subtree. Now go to left until
            // a leaf is reached
            while (!n.Leaf)
            {
                n = n.Children[0]!; // by construction this is non-null
            }

            return n.Keys[0]!;
        }

        /// <summary>
        /// Moves all keys/child pointer in the range of start + 1 .. node.N one position to the left
        /// </summary>
        /// <param name="node"></param>
        /// <param name="start"></param>
        /// <param name="keepLeftMostChildPointer">If true the pointer at <c>node.Children[start]</c> is left untouched</param>
        private static void MoveToLeftAndShrink(Node node, int start, bool keepLeftMostChildPointer = false)
        {
            for (var i = start; i < node.N - 1; i++)
            {
                node.Keys[i] = node.Keys[i + 1];
            }

            var startChildren = keepLeftMostChildPointer == false ? start : start + 1;
            for (var i = startChildren; i < node.N; i++)
            {
                node.Children[i] = node.Children[i + 1];
            }

            node.Keys[node.N - 1] = default;
            node.Children[node.N] = default;

            node.N--;
        }

        /// <summary>
        /// Moves all keys/child pointer in the range of start + 1 .. node.N one position to the right
        /// </summary>
        /// <param name="node"></param>
        /// <param name="start"></param>
        private static void MoveToRightAndGrow(Node node, int start)
        {
            for (var i = node.N; i > start; i--)
            {
                node.Keys[i] = node.Keys[i - 1];
            }
            for (var i = node.N + 1; i > start; i--)
            {
                node.Children[i] = node.Children[i - 1];
            }

            node.Keys[start] = default;
            node.Children[start + 1] = default;

            node.N++;
        }
    }
}