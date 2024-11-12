using System.IO;
using lab3.Logic;

namespace lab3.Utilities;

public class FileHandlerTree
    {
        private readonly string filePath = "inputTree.txt";
        private readonly Action<string> outputHandler;

        public FileHandlerTree(Action<string> outputHandler)
        {
            this.outputHandler = outputHandler;
        }

        public TreeNode ReadTreeFromFile()
        {
            if (!File.Exists(filePath))
            {
                outputHandler($"Файл {filePath} не найден.");
                return null;
            }

            try
            {
                string[] lines = File.ReadAllLines(filePath);

                if (lines.Length == 0 || (lines.Length == 1 && string.IsNullOrWhiteSpace(lines[0])))
                {
                    outputHandler($"Файл {filePath} пуст.");
                    return null;
                }

                Queue<string> nodeQueue = new Queue<string>();
                foreach (var line in lines)
                {
                    var elements = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    foreach (var element in elements)
                    {
                        if (!IsValidNodeData(element))
                        {
                            outputHandler($"Ошибка: некорректный символ '{element}' в узле. Разрешены только заглавные буквы английского алфавита и '*'.");
                            return null;
                        }
                        nodeQueue.Enqueue(element);
                    }
                }

                TreeNode root = BuildTree(nodeQueue);
                return root;
            }
            catch (Exception ex)
            {
                outputHandler($"Ошибка при чтении файла: {ex.Message}");
                return null;
            }
        }

        private bool IsValidNodeData(string data)
        {
            // Проверка на допустимые символы (заглавные буквы и '*')
            return data == "*" || (data.Length == 1 && char.IsUpper(data[0]));
        }

        private TreeNode BuildTree(Queue<string> nodeQueue)
        {
            if (nodeQueue.Count == 0)
            {
                return null;
            }

            string data = nodeQueue.Dequeue();
            if (data == "*")
            {
                return null;
            }

            TreeNode node = new TreeNode(data);
            node.Left = BuildTree(nodeQueue);
            node.Right = BuildTree(nodeQueue);
            return node;
        }

        public void OverwriteFile(string newContent)
        {
            try
            {
                File.WriteAllText(filePath, newContent);
                outputHandler($"Файл {filePath} был успешно перезаписан.");
            }
            catch (Exception ex)
            {
                outputHandler($"Ошибка при перезаписи файла: {ex.Message}");
            }
        }
    }
