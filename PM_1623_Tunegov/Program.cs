using System;

namespace PM_1623_Tunegov
{
    class Program
    {
        static void Main()
        {

            Console.WriteLine("РАСЧЕТ ТРАНСПОРТНОЙ ЗАДАЧИ ДЛЯ ТАБЛИЦ 1, 2 И 3");

            // --- ТАБЛИЦА 1
            int[] supply1 = { 20, 45, 24, 31, 30 };
            int[] demand1 = { 65, 44, 41 };
            int[,] cost1 = {
                { 5, 4, 6 },
                { 7, 3, 3 },
                { 9, 5, 2 },
                { 3, 2, 5 },
                { 4, 7, 1 }
            };
            SolveTable("ТАБЛИЦА 1", supply1, demand1, cost1);

            Console.WriteLine("\n--------------------------------------------------\n");

            // --- ТАБЛИЦА 2
            int[] supply2 = { 40, 50, 30, 60 };
            int[] demand2 = { 70, 50, 60 };
            int[,] cost2 = {
                { 4, 2, 5 },
                { 3, 6, 2 },
                { 5, 1, 4 },
                { 2, 4, 3 }
            };
            SolveTable("ТАБЛИЦА 2", supply2, demand2, cost2);

            Console.WriteLine("\n--------------------------------------------------\n");

            // --- ТАБЛИЦА 3
            int[] supply3 = { 60, 80, 50 };
            int[] demand3 = { 40, 60, 50, 40 };
            int[,] cost3 = {
                { 6, 3, 2, 5 },
                { 4, 5, 1, 3 },
                { 2, 7, 4, 6 }
            };
            SolveTable("ТАБЛИЦА 3", supply3, demand3, cost3);

            Console.WriteLine("\nНажмите Enter для выхода...");
            Console.ReadLine();
        }

        static void SolveTable(string title, int[] supply, int[] demand, int[,] cost)
        {
            int m = supply.Length;
            int n = demand.Length;
            int[,] plan = new int[m, n];

            int[] curSupply = (int[])supply.Clone();
            int[] curDemand = (int[])demand.Clone();

            // Поиск ячеек с минимальным тарифом
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
            Console.WriteLine($"--- {title} ---");
            Console.WriteLine("Матрица опорного плана:");

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
        }
    }
}