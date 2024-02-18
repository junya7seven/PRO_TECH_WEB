using System.Text;

namespace asdasdsad.StringOper.Sorting
{
    public class QuickSort
    {
        public static string QuickSortPrint(char[] arr)
        {
            QuickSorts(arr, 0, arr.Length - 1);
            string sortedString = new string(arr);
            return sortedString;
        }

        public static void QuickSorts(char[] arr, int left, int right)
        {
            if (left < right)
            {
                int pivotIndex = Partition(arr, left, right); // Опорный элемент

                QuickSorts(arr, left, pivotIndex - 1);
                QuickSorts(arr, pivotIndex + 1, right);
            }
        }

        static int Partition(char[] arr, int left, int right)
        {
            char pivotValue = arr[right];
            int pivotIndex = left;

            for (int i = left; i < right; i++)
            {
                if (arr[i] < pivotValue)
                {
                    Swap(arr, i, pivotIndex);
                    pivotIndex++;
                }
            }

            Swap(arr, pivotIndex, right);
            return pivotIndex;
        }

        static void Swap(char[] arr, int i, int j)
        {
            char temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }
    }
    /////////// Сортировка деревом
    class Node
    {
        public char Value;
        public Node Left;
        public Node Right;

        public Node(char value)
        {
            Value = value;
            Left = null;
            Right = null;
        }
    }
    class BinaryTree
    {
        private Node root;

        public BinaryTree()
        {
            root = null;
        }
        public void Insert(char value)
        {
            root = InsertRec(root, value);
        }

        private Node InsertRec(Node root, char value)
        {
            if (root == null)
            {
                root = new Node(value);
                return root;
            }

            if (value < root.Value)
            {
                root.Left = InsertRec(root.Left, value);
            }
            else if (value > root.Value)
            {
                root.Right = InsertRec(root.Right, value);
            }

            return root;
        }
        public string GetSortedString()
        {
            StringBuilder sortedString = new StringBuilder();
            InOrderTraversal(root, sortedString);
            return sortedString.ToString();
        }

        private void InOrderTraversal(Node root, StringBuilder sortedString)
        {
            if (root != null)
            {
                InOrderTraversal(root.Left, sortedString);
                sortedString.Append(root.Value);
                InOrderTraversal(root.Right, sortedString);
            }
        }

        private void InOrderTraversal(Node root)
        {
            if (root != null)
            {
                InOrderTraversal(root.Left);
                Console.Write(root.Value);
                InOrderTraversal(root.Right);
            }
        }
        public string TreeSortPrint(string inputString)
        {
            BinaryTree tree = new BinaryTree();
            foreach (char c in inputString)
            {
                tree.Insert(c);
            }
            return inputString = tree.GetSortedString();
        }
    }
}
