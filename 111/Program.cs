using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using TaskTrackerWORK.Core.Migration;
using TaskTrackerWORK.Core.Models;
using System.IO;


internal class Program
{
    private static void Main(string[] args)
    {
        var path = "tasks.json";
        Console.WriteLine($"Поиск файла: {path}");

        if (!File.Exists(path))
        {
            File.WriteAllText(path, "[]");
            Console.WriteLine("Файл tasks.json создан с пустым массивом задач.");
        }
        else
        {
            Console.WriteLine("Файл tasks.json найден.");
        }

        try
        {
            var json = File.ReadAllText(path);
            Console.WriteLine($"Прочитано JSON: {json}");

            var tasks = JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
            Console.WriteLine($"Загружено задач: {tasks.Count}");

            // Дополнительная проверка на пустой массив
            if (tasks.Count == 0)
            {
                Console.WriteLine("В файле нет задач для обработки. Добавьте данные в tasks.json.");
                return; // Завершаем выполнение, если задач нет
            }

            int changedCount = 0;
            foreach (var t in tasks)
            {
                if (TaskNormalizer.Normalize(t))
                    changedCount++;
            }

            if (changedCount > 0)
            {
                File.WriteAllText(path, JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true }));
                Console.WriteLine($"Миграция применена. Обновлено задач: {changedCount}");
            }
            else
            {
                Console.WriteLine("Миграция не требуется. Все задачи уже нормализованы.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при работе с файлом: {ex.Message}");
        }
    }
}