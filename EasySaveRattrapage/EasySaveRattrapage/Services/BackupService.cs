using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using Models;
using System.Collections.Generic;

namespace Services
{
    public class BackupService
    {
        private readonly string logDirectory = Path.Combine(AppContext.BaseDirectory, "Logs");

        public void ExecuteBackup(BackupJob job)
        {
            if (!Directory.Exists(job.SourcePath))
            {
                Console.WriteLine($"Source directory  [ {job.SourcePath} ] does not exist. / Le chemin [ {job.SourcePath} ] n'est pas trouve.");
                return;
            }

            if (!Directory.Exists(job.TargetPath))
            {
                Directory.CreateDirectory(job.TargetPath);
            }

            string[] sourceFiles = Directory.GetFiles(job.SourcePath, "*", SearchOption.AllDirectories);
            int totalFiles = sourceFiles.Length;
            int currentFile = 0;

            List<object> logs = new List<object>();

            foreach (var fileSource in sourceFiles)
            {
                currentFile++;

                string relativePath = Path.GetRelativePath(job.SourcePath, fileSource);
                string fileTarget = Path.Combine(job.TargetPath, relativePath);
                string destPath = Path.GetDirectoryName(fileTarget);

                bool shouldCopy = false;

                if (job.Type == BackupType.Full)
                {
                    shouldCopy = true;
                }
                else if (job.Type == BackupType.Differential)
                {
                    if (!File.Exists(fileTarget))
                    {
                        shouldCopy = true;
                    }
                    else
                    {
                        var sourceInfo = new FileInfo(fileSource);
                        var targetInfo = new FileInfo(fileTarget);

                        if (sourceInfo.LastWriteTime > targetInfo.LastWriteTime || sourceInfo.Length != targetInfo.Length)
                        {
                            shouldCopy = true;
                        }
                    }
                }

                if (shouldCopy)
                {
                    try
                    {
                        if (!Directory.Exists(destPath))
                        {
                            Directory.CreateDirectory(destPath);
                        }

                        var stopwatch = Stopwatch.StartNew();
                        File.Copy(fileSource, fileTarget, true);
                        stopwatch.Stop();

                        long fileSize = new FileInfo(fileTarget).Length;
                        double transferTime = stopwatch.Elapsed.TotalMilliseconds;

                        logs.Add(new
                        {
                            Name = job.Name,
                            FileSource = Path.GetFullPath(fileSource),
                            FileTarget = Path.GetFullPath(fileTarget),
                            destPath = destPath,
                            FileSize = fileSize,
                            FileTransferTime = transferTime,
                            time = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")
                        });
                    }
                    catch (Exception ex)
                    {
                        logs.Add(new
                        {
                            Name = job.Name,
                            FileSource = Path.GetFullPath(fileSource),
                            FileTarget = Path.GetFullPath(fileTarget),
                            destPath = destPath,
                            FileSize = 0,
                            FileTransferTime = -1,
                            time = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                            Error = ex.Message
                        });
                    }
                }

                PrintProgressBar(currentFile, totalFiles);
            }

            SaveLog(logs);
        }

        private void SaveLog(List<object> logs)
        {
            if (!Directory.Exists(logDirectory))
                Directory.CreateDirectory(logDirectory);

            string logFileName = $"log_{DateTime.Now:yyyyMMdd}.json";
            string logFilePath = Path.Combine(logDirectory, logFileName);

            using StreamWriter writer = new StreamWriter(logFilePath, append: true);
            foreach (var logEntry in logs)
            {
                string json = JsonSerializer.Serialize(logEntry, new JsonSerializerOptions
                {
                    WriteIndented = false
                });
                writer.WriteLine(json); // Ligne par ligne pour lisibilité
            }
        }

        private void PrintProgressBar(int current, int total, int barLength = 40)
        {
            double percent = (double)current / total;
            int filledLength = (int)(barLength * percent);

            string bar = new string('#', filledLength).PadRight(barLength);
            string output = $"\rProgress: [{bar}] {percent:P0} ({current}/{total})";

            Console.Write(output);

            if (current == total)
                Console.WriteLine(); 
        }
    }
}
