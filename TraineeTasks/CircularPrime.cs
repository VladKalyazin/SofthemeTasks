using System;
using System.Collections.Generic;
using System.Linq;

public static class CircularPrime
{
    // Нахождение простых чисел в некотором диапазоне
    // Алгоритм "Решето Эратосфена"
    public static List<int> FindPrimeNumbers(int maxValue)
    {
        List<int> result = new List<int>();
        bool[] isPrime = Enumerable.Repeat(true, maxValue).ToArray();

        double sqrtOfMaxValue = Math.Sqrt(maxValue);
        for (int i = 2; i < maxValue; i++)
        {
            if (isPrime[i])
            {
                result.Add(i);
                if (i < sqrtOfMaxValue)
                {
                    for (int j = i * i; j < maxValue; j += i)
                        isPrime[j] = false;
                }
            }
        }

        return result;
    }

    // Метод нахождения разрядности числа
    private static int GetDigitCapacity(int number)
    {
        int k = 0;
        while (number > 0)
        {
            number /= 10;
            k++;
        }
        return k;
    }

    // Метод возведения 10 в заданную степень
    private static int PowOfTen(int index)
    {
        int result = 1;
        for (int i = 0; i < index; i++)
            result *= 10;
        return result;
    }

    // Нахождение циклических простых чисел
    public static List<int> FindCircularPrimeNumbers(int maxValue)
    {
        List<int> result = new List<int>();
        // Находим простые числа
        List<int> primeNums = FindPrimeNumbers(maxValue);

        // Перебор простых чисел
        foreach (int number in primeNums)
        {
            bool isCircularPrime = true;
            int capacity = GetDigitCapacity(number);
            // Генератор циклических вариаций числа
            for (int i = 1; i < capacity && isCircularPrime; i++)
            {
                int circularValue = (number % PowOfTen(i)) * PowOfTen(capacity - i) + number / PowOfTen(i);

                double sqrtOfCurrentValue = Math.Sqrt(circularValue);
                // Проверка является ли циклическая вариация числа простым числом
                foreach (int j in primeNums)
                {
                    if (j > sqrtOfCurrentValue)
                        break;
                    if (circularValue % j == 0)
                    {
                        isCircularPrime = false;
                        break;
                    }
                }
            }

            if (isCircularPrime)
                result.Add(number);
        }

        return result; 
    }
}
