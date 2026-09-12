using System;

namespace PM_1623_Tunegov
{
    class Program
    {
        static void Main()
        {
            // Исходные данные Таблицы 1
            int[] supply = { 20, 45, 24, 31, 30 };
            int[] demand = { 65, 44, 41 };
            int[,] cost = {
                { 5, 4, 6 },
                { 7, 3, 3 },
                { 9, 5, 2 },
                { 3, 2, 5 },
                { 4, 7, 1 }
            };

            int m = supply.Length;
            int n = demand.Length;
            int[,] plan = new int[m, n];

            int[] curSupply = (int[])supply.Clone();
            int[] curDemand = (int[])demand.Clone();

            // Расчет методом минимальных элементов
            while (true)
            {
                int minCost = int.MaxValue;
                int minI = -1, minJ = -1;

                for (int i = 0; i < m; i++)
                {
                    if (curSupply[i] == 0) continue;
                    for (int j = 0; j < n; j++)
                    {
                        if (curDemand[j] == 0) continue;
                        if (cost[i, j] < minCost)
                        {
                            minCost = cost[i, j];
                            minI = i;
                            minJ = j;
                        }
                    }
                }

                if (minI == -1 || minJ == -1) break;

                int qty = Math.Min(curSupply[minI], curDemand[minJ]);
                plan[minI, minJ] = qty;
                curSupply[minI] -= qty;
                curDemand[minJ] -= qty;
            }

            // Вывод результатов
            int totalCost = 0;
            Console.WriteLine("Опорный план для Таблицы 1");
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write($"{plan[i, j],4} ");
                    totalCost += plan[i, j] * cost[i, j];
                }
                Console.WriteLine();
            }

            Console.WriteLine($"\nИтоговая стоимость грузоперевозки = {totalCost}");
            Console.ReadLine();
        }
    }
}