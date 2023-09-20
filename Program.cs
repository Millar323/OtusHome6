using System;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;

class Program
{
    public async Task Main()
    {
        string folderPath = @"D:\Prog"; // Замените на нужный путь

        Stopwatch stopwatch = Stopwatch.StartNew();
        int totalSpaces = await CalculateTotalSpacesAsync(folderPath);
        stopwatch.Stop();

        Console.WriteLine("Количество пробелов: " + totalSpaces);
        Console.WriteLine("Время выполнения: " + stopwatch.Elapsed.TotalSeconds + "сек");
    }

    public async Task<int> CalculateTotalSpacesAsync(string folderPath)
    {
        string[] filePaths = Directory.GetFiles(folderPath);

        Task<int>[] tasks = new Task<int>[filePaths.Length];

        for (int i = 0; i < filePaths.Length; i++)
        {
            tasks[i] = CountSpacesAs(filePaths[i]);
        }

        int[] results = await Task.WhenAll(tasks);

        int totalSpaces = 0;
        foreach (int result in results)
        {
            totalSpaces += result;
        }

        return totalSpaces;
    }

    async Task<int> CountSpacesAs(string filePath)
    {
        int totalSpaces = 0;

        using (StreamReader reader = new StreamReader(filePath))
        {
            await reader.ReadToEndAsync();

            string content = await reader.ReadToEndAsync();
            foreach (var a in content)
            {
                if (a == ' ')
                {
                    totalSpaces++;
                }
            }
        }

        return totalSpaces;
    }
}
