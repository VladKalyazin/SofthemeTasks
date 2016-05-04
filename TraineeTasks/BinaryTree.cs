using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

// Класс бинарное дерево (для осуществления алгоритма поиска в дереве)
// Алгоритм основан на алгоритме Дейкстры
public class BinaryTree
{
    // Ссылки на источник данных
    public const string SimpleTriangleUri = "https://dl.dropboxusercontent.com/u/28873424/tasks/simple_triangle.txt";
    public const string TriangleUri = "https://dl.dropboxusercontent.com/u/28873424/tasks/triangle.txt";
    // Массив весов в дереве
    public List<List<int>> Weights { get; set; } = new List<List<int>>();
    // Массив максимальных длинн (суммы предыдущих элементов) в дереве
    private List<List<int>> MaxLengths { get; set; } = new List<List<int>>();

    // Метод для получения данных по Uri
    public async Task GetWeightsFromURL(string uri)
    {
        Weights.Clear();
        using (HttpClient client = new HttpClient())
        {
            string strWeights = await client.GetStringAsync(uri);
            string[] strRows = strWeights.Split('\n');
            foreach (string strRow in strRows)
            {
                if (strRow.Length == 0)
                    continue;
                List<int> numbers = new List<int>(); 
                string[] strNumbers = strRow.Split(' ');
                foreach (string strNumber in strNumbers)
                    numbers.Add(Convert.ToInt32(strNumber));
                Weights.Add(numbers);
            }
        }
    }

    // Алгоритм нахождения пути
    public int FindMaxPath()
    {
        // Заполняем массив длинн нулями
        MaxLengths.Clear();
        foreach (var row in Weights)
        {
            List<int> lengthsRow = new List<int>();
            for (int i = 0; i < row.Count; i++)
                lengthsRow.Add(0);
            MaxLengths.Add(lengthsRow);
        }

        MaxLengths[0][0] = Weights[0][0];

        // Цикл перебора элементов в массиве
        for (int i = 0; i < Weights.Count - 1; i++)
        {
            for (int j = 0; j < i + 1; j++)
            {
                // Находим максимальную сумму левого потомка
                int sumLeft = MaxLengths[i][j] + Weights[i + 1][j];
                if (MaxLengths[i + 1][j] < sumLeft)
                    MaxLengths[i + 1][j] = sumLeft;
                // Находим максимальную сумму правого потомка
                int sumRight = MaxLengths[i][j] + Weights[i + 1][j + 1];
                if (MaxLengths[i + 1][j + 1] < sumRight)
                    MaxLengths[i + 1][j + 1] = sumRight;
            }
        }

        // Находим в последней строке массива длинн максимальный результат
        int maxLength = 0;
        foreach (int length in MaxLengths.Last())
        {
            if (maxLength < length)
                maxLength = length;
        }

        return maxLength;
    }
}
