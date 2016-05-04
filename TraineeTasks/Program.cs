using System;
using System.Collections.Generic;

namespace TraineeTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            // Первое задание
            BinaryTree tree = new BinaryTree();
            // Нахождение максимальной суммы в простом треугольнике
            tree.GetWeightsFromURL(BinaryTree.SimpleTriangleUri).Wait();
            Console.WriteLine(tree.FindMaxPath());

            // Нахождение максимальной суммы во втором треугольнике
            tree.GetWeightsFromURL(BinaryTree.TriangleUri).Wait();
            Console.WriteLine(tree.FindMaxPath());
            
            // Второе задание
            // Нахождение всех циклических простых чисел от 2 до 10^6
            List<int> primeNums = CircularPrime.FindCircularPrimeNumbers((int)Math.Pow(10, 6));
            Console.WriteLine(primeNums.Count);

            Console.ReadKey();
        }
    }
}
