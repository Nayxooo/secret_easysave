using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Models;

namespace Services
{
    public class LogService
    {
        private readonly string logDirectory = Path.Combine("Data", "logs");

        public LogService()
        {
            Directory.CreateDirectory(logDirectory);
        }

        public void WriteLogEntry(BackupLogEntry entry)
        {
            string logFileName = $"log_{DateTime.Now:yyyyMMdd}.json";
            string logFilePath = Path.Combine(logDirectory, logFileName);

            List<BackupLogEntry> entries;

            if (File.Exists(logFilePath))
            {
                try
                {
                    string existingJson = File.ReadAllText(logFilePath);
                    entries = JsonSerializer.Deserialize<List<BackupLogEntry>>(existingJson) ?? new List<BackupLogEntry>();
                }
                catch
                {
                    entries = new List<BackupLogEntry>();
                }
            }
            else
            {
                entries = new List<BackupLogEntry>();
            }

            entries.Add(entry);

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string newJson = JsonSerializer.Serialize(entries, options);
            File.WriteAllText(logFilePath, newJson);
        }
    }
}
