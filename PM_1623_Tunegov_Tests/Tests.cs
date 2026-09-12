using System;

namespace PM_1623_Tunegov_Tests
{
    // Структура для хранения данных теста
    struct TestCase
    {
        public string Name;
        public int[] Supply;
        public int[] Demand;
        public int[,] Cost;
        public int ExpectedCost;

        public TestCase(string name, int[] supply, int[] demand, int[,] cost, int expectedCost)
        {
            Name = name;
            Supply = supply;
            Demand = demand;
            Cost = cost;
            ExpectedCost = expectedCost;
        }
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine(" АВТОМАТИЧЕСКАЯ ПРОВЕРКА ТЕСТОВЫХ СЦЕНАРИЕВ ");

            // Набор тестовых данных (Таблицы 1-3 и краевые тесты)
            TestCase[] tests = new TestCase[]
            {
            // Основные расчёты для 3 таблиц
            new TestCase("Таблица 1 (Основной расчет)",
            new int[] { 20, 45, 24, 31, 30 },
            new int[] { 65, 44, 41 },
            new int[,] { { 5, 4, 6 }, { 7, 3, 3 }, { 9, 5, 2 }, { 3, 2, 5 }, { 4, 7, 1 } },
            594),

            new TestCase("Таблица 2 (Основной расчет)",
            new int[] { 40, 50, 30, 60 },
            new int[] { 70, 50, 60 },
            new int[,] { { 4, 2, 5 }, { 3, 6, 2 }, { 5, 1, 4 }, { 2, 4, 3 } },
            380),

            new TestCase("Таблица 3 (Основной расчет)",
            new int[] { 60, 80, 50 },
            new int[] { 40, 60, 50, 40 },
            new int[,] { { 6, 3, 2, 5 }, { 4, 5, 1, 3 }, { 2, 7, 4, 6 } },
            460),

            // Краевые тесты для Таблицы 1
            new TestCase("Таблица 1 (Тест 1x1: 1 поставщик/потребитель)",
            new int[] { 50 },
            new int[] { 50 },
            new int[,] { { 5 } },
            250),

            new TestCase("Таблица 1 (Тест: одинаковые тарифы = 2)",
            new int[] { 10, 20 },
            new int[] { 15, 15 },
            new int[,] { { 2, 2 }, { 2, 2 } },
            60),

            // Краевые тесты для Таблицы 2
            new TestCase("Таблица 2 (Тест 1x1)",
            new int[] { 100 },
            new int[] { 100 },
            new int[,] { { 4 } },
            400),

            new TestCase("Таблица 2 (Тест: одинаковые тарифы = 3)",
            new int[] { 40, 50, 30, 60 },
            new int[] { 70, 50, 60 },
            new int[,] { { 3, 3, 3 }, { 3, 3, 3 }, { 3, 3, 3 }, { 3, 3, 3 } },
            540),

            // Краевые тесты для Таблицы 3
            new TestCase("Таблица 3 (Тест 1x1)",
            new int[] { 30 },
            new int[] { 30 },
            new int[,] { { 10 } },
            300),

            new TestCase("Таблица 3 (Тест: одинаковые тарифы = 5)",
            new int[] { 60, 80, 50 },
            new int[] { 40, 60, 50, 40 },
            new int[,] { { 5, 5, 5, 5 }, { 5, 5, 5, 5 }, { 5, 5, 5, 5 } },
            950)
            };

            // Запуск прогона тестов
            int passedCount = 0;

            for (int t = 0; t < tests.Length; t++)
            {
                TestCase test = tests[t];
                int actualCost = CalculateMinCost(test.Supply, test.Demand, test.Cost);
                bool isPassed = actualCost == test.ExpectedCost;

                if (isPassed) passedCount++;

                Console.WriteLine($"[Тест {t + 1}] {test.Name}");
                Console.WriteLine($" Факт: {actualCost} | Ожидалось: {test.ExpectedCost} | Статус: {(isPassed ? "УСПЕШНО" : "ОШИБКА")}\n");
            }

            Console.WriteLine($"ИТОГ: Пройдено {passedCount} из {tests.Length} тестов.");
            Console.ReadLine();
        }

        // Логика расчета стоимости методом минимальных элементов
        static int CalculateMinCost(int[] supply, int[] demand, int[,] cost)
        {
            int m = supply.Length;
            int n = demand.Length;

            int[] curSupply = (int[])supply.Clone();
            int[] curDemand = (int[])demand.Clone();
            int totalCost = 0;

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
                totalCost += qty * cost[minI, minJ];

                curSupply[minI] -= qty;
                curDemand[minJ] -= qty;
            }

            return totalCost;
        }
    }
}