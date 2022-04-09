using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace zadanie_10_drzewa
{
    class Tree
    {
        private TreeNode root;
        public void AddAnimal(string name)
        {
            TreeNode node = new TreeNode();
            node.Name = name;
            if (root == null)
            {
                root = node;
                return;
            }
            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);
            while (queue.Count != 0)
            {
                TreeNode currentnode = queue.Dequeue();
                if (currentnode.Name.Contains(","))
                {
                    if (currentnode.Left != null)
                    {
                        if (currentnode.Right != null)
                        {
                            if (currentnode.Up != null)
                            {
                                if (name.Length == currentnode.Up.Name.Length) currentnode.Name = name;
                            }
                            else if (currentnode.Left.Name.Length < name.Length || name.Length < currentnode.Right.Name.Length) currentnode.Name = name;
                            else if (name.Length <= currentnode.Left.Name.Length) queue.Enqueue(currentnode.Left);
                            else queue.Enqueue(currentnode.Right);
                        }
                        else if (currentnode.Up != null)
                        {
                            if (currentnode.Up.Name.Length == name.Length) currentnode.Name = name;
                            else if (currentnode.Up.Name.Length > name.Length) queue.Enqueue(currentnode.Left);
                            else currentnode.Right = node;
                        }
                        else if (currentnode.Left.Name.Length < name.Length) currentnode.Name = name;
                    }
                    else if (currentnode.Right != null)
                    {
                        if (currentnode.Up != null)
                        {
                            if (currentnode.Up.Name.Length == name.Length) currentnode.Name = name;
                            else if (currentnode.Up.Name.Length > name.Length) queue.Enqueue(currentnode.Right);
                            else currentnode.Left = node;
                        }
                        else if (currentnode.Right.Name.Length > name.Length) currentnode.Name = name;
                    }
                    else if (currentnode.Up != null)
                    {
                        if (currentnode.Up.Name.Length == name.Length) currentnode.Name = name;
                    }
                    else if (currentnode.Left == null && currentnode.Up == null && currentnode.Right == null) currentnode.Name = name;
                    continue;
                }
                if (name.Length > currentnode.Name.Length)
                {
                    if (currentnode.Right == null)
                    {
                        currentnode.Right = node;
                        break;
                    }
                    else
                    {
                        queue.Enqueue(currentnode.Right);
                    }
                }
                if (name.Length == currentnode.Name.Length)
                {
                    if (currentnode.Up == null)
                    {
                        currentnode.Up = node;
                        break;
                    }
                    else
                    {
                        queue.Enqueue(currentnode.Up);
                    }
                }
                if (name.Length < currentnode.Name.Length)
                {
                    if (currentnode.Left == null)
                    {
                        currentnode.Left = node;
                        break;
                    }
                    else
                    {
                        queue.Enqueue(currentnode.Left);
                    }
                }
            }
        }
        public bool CheckAnimal(string name)
        {
            if (root == null)
            {
                return false;
            }
            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);
            while (queue.Count != 0)
            {
                TreeNode currentnode = queue.Dequeue();
                if (name.Length > currentnode.Name.Length)
                {
                    if (currentnode.Right == null)
                    {
                        continue;
                    }
                    else
                    {
                        queue.Enqueue(currentnode.Right);
                    }
                }
                if (name.Length == currentnode.Name.Length)
                {
                    if(name == currentnode.Name)
                    {
                        return true;
                    }else if(currentnode.Up == null)
                    {
                        continue;
                    }
                    queue.Enqueue(currentnode.Up);
                }
                if (name.Length < currentnode.Name.Length)
                {
                    if (currentnode.Left == null)
                    {
                        continue;
                    }
                    else
                    {
                        queue.Enqueue(currentnode.Left);
                    }
                }
            }
            return false;
        }
        public void SaveAnimals()
        {
            List<string> animals = new List<string>();
            if (root == null)
            {
                return;
            }
            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);
            while (queue.Count != 0)
            {
                TreeNode currentNode = queue.Dequeue();
                //if (!currentNode.Name.Contains(","))
                //{
                    animals.Add(currentNode.Name);
                //}              
                if (currentNode.Right != null)
                {
                    queue.Enqueue(currentNode.Right);
                }
                if(currentNode.Up != null)
                {
                    queue.Enqueue(currentNode.Up);
                }
                if(currentNode.Left != null)
                {
                    queue.Enqueue(currentNode.Left);
                }
            }
            File.WriteAllLines("Animals.txt", animals);
        }
        public void RemoveAnimal(string name)
        {
            if (root == null)
            {
                Console.WriteLine("Zwierzę: " + name + " nie mieszka w tym ogrodzie");
            }
            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);
            int counter = 0;
            while (queue.Count != 0)
            {
                counter++;
                TreeNode currentnode = queue.Dequeue();
                if (name.Length > currentnode.Name.Length)
                {
                    if (currentnode.Right == null)
                    {
                        continue;
                    }
                    else
                    {
                        queue.Enqueue(currentnode.Right);
                    }
                }
                if (name.Length == currentnode.Name.Length)
                {
                    if (name == currentnode.Name)
                    {
                        /*string blank = "";
                        foreach(char el in name)
                        {
                            blank = blank + ",";
                        }
                        currentnode.Name = blank;*/
                        Console.WriteLine("Zwierzę " + name + " zostało usunięte pomyślnie.");
                        Stack<TreeNode> stack = new Stack<TreeNode>();
                        TreeNode lastnode = new TreeNode();
                        stack.Push(currentnode);
                        stack.Push(currentnode);
                        queue.Clear();
                        if(currentnode.Up != null)
                        {
                            queue.Enqueue(currentnode.Up);
                            while (queue.Count != 0)
                            {
                                lastnode = stack.Pop();
                                currentnode = queue.Dequeue();
                                if (currentnode.Up != null)
                                {
                                    queue.Enqueue(currentnode.Up);
                                    stack.Push(currentnode);
                                }
                                else
                                {
                                    TreeNode acnode = stack.Pop();
                                    acnode.Name = currentnode.Name;
                                    lastnode.Up = null;
                                    return;
                                }
                            }
                            return;
                        }
                        else if(currentnode.Right != null)
                        {
                            queue.Enqueue(currentnode.Right);
                            while (queue.Count != 0)
                            {
                                lastnode = stack.Pop();
                                currentnode = queue.Dequeue();
                                if (currentnode.Left != null)
                                {
                                    queue.Enqueue(currentnode.Left);
                                    stack.Push(currentnode);
                                }
                                else
                                {
                                    TreeNode acnode = stack.Pop();
                                    acnode.Name = currentnode.Name;
                                    lastnode.Left = null;
                                    return;
                                }
                            }
                            return;
                        }
                        else if (currentnode.Left != null)
                        {
                            queue.Enqueue(currentnode.Left);
                            while (queue.Count != 0)
                            {
                                lastnode = stack.Pop();
                                currentnode = queue.Dequeue();
                                if (currentnode.Right != null)
                                {
                                    queue.Enqueue(currentnode.Right);
                                    stack.Push(currentnode);
                                }
                                else
                                {
                                    TreeNode acnode = stack.Pop();
                                    acnode.Name = currentnode.Name;
                                    lastnode.Right = null;
                                    return;
                                }
                            }
                            return;
                        }

                    }
                    else if (currentnode.Up == null)
                    {
                        continue;
                    }
                    queue.Enqueue(currentnode.Up);
                }
                if (name.Length < currentnode.Name.Length)
                {
                    if (currentnode.Left == null)
                    {
                        continue;
                    }
                    else
                    {
                        queue.Enqueue(currentnode.Left);
                    }
                }
            }
            Console.WriteLine("Zwierzę: " + name + " nie mieszka w tym ogrodzie");
        }
    }
}
