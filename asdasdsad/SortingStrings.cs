using System.Text;

namespace asdasdsad
{
    public class SortingStrings
    {

    }
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

                QuickSorts(arr, left, pivotIndex - 1); // Рекурсивная сортировка левой части
                QuickSorts(arr, pivotIndex + 1, right); // Рекурсивная сортировка правой части
            }
        }

        static int Partition(char[] arr, int left, int right)
        {
            char pivotValue = arr[right]; // Выбор опорного элемента (последний элемент в текущем подмассиве)
            int pivotIndex = left;

            for (int i = left; i < right; i++)
            {
                if (arr[i] < pivotValue) // Если элемент меньше опорного, перемещаем его влево
                {
                    Swap(arr, i, pivotIndex);
                    pivotIndex++;
                }
            }

            Swap(arr, pivotIndex, right); // Помещаем опорный элемент на его правильное место
            return pivotIndex; // Возвращаем индекс опорного элемента
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
        public char Value; // Данные узла (символ)
        public Node Left; // Левый дочерний узел
        public Node Right; // Правый дочерний узел

        public Node(char value)
        {
            Value = value;
            Left = null;
            Right = null;
        }
    }
    // Класс для бинарного дерева поиска
    class BinaryTree
    {
        private Node root; // Корневой узел дерева

        public BinaryTree()
        {
            root = null;
        }
        // Метод для вставки символа в дерево
        public void Insert(char value)
        {
            root = InsertRec(root, value);
        }
        // Рекурсивный метод для вставки символа в дерево
        private Node InsertRec(Node root, char value)
        {
            if (root == null)
            {
                root = new Node(value); // Создаем новый узел, если текущий узел пуст
                return root;
            }

            if (value < root.Value)
            {
                root.Left = InsertRec(root.Left, value); // Рекурсивная вставка в левое поддерево
            }
            else if (value > root.Value)
            {
                root.Right = InsertRec(root.Right, value); // Рекурсивная вставка в правое поддерево
            }

            return root;
        }
        public string GetSortedString()
        {
            StringBuilder sortedString = new StringBuilder();
            InOrderTraversal(root, sortedString);
            return sortedString.ToString();
        }
        // Метод для обхода дерева в порядке возрастания
        private void InOrderTraversal(Node root, StringBuilder sortedString)
        {
            if (root != null)
            {
                InOrderTraversal(root.Left, sortedString);
                sortedString.Append(root.Value); // Добавляем значение к строке
                InOrderTraversal(root.Right, sortedString);
            }
        }
        // Рекурсивный метод для обхода дерева в порядке возрастания
        private void InOrderTraversal(Node root)
        {
            if (root != null)
            {
                InOrderTraversal(root.Left); // Рекурсивный обход левого поддерева
                Console.Write(root.Value); // Выводим данные узла
                InOrderTraversal(root.Right); // Рекурсивный обход правого поддерева
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
