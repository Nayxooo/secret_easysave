using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Models;
using Services;

namespace Controllers
{
    public class BackupJobController
    {
        private const int MaxBackupJobs = 5;
        private readonly string jobsFilePath = Path.Combine("Data", "backupJobs.json");
        private List<BackupJob> backupJobs;
        private readonly BackupService backupService;
        private readonly LogService logService;

        public BackupJobController()
        {
            backupService = new BackupService();
            logService = new LogService();
            LoadJobs();
        }

        public void CreateBackupJob()
        {
            Console.Clear();
            Console.WriteLine("=== Create a new Backup Job ===");

            Console.Write("Enter job name: ");
            string name = Console.ReadLine();

            Console.Write("Enter source directory: ");
            string source = Console.ReadLine();

            Console.Write("Enter target directory: ");
            string target = Console.ReadLine();

            BackupType type;
            while (true)
            {
                Console.Write("Enter backup type (Full / Differential): ");
                string typeInput = Console.ReadLine()?.Trim().ToLower();

                if (typeInput == "full")
                {
                    type = BackupType.Full;
                    break;
                }
                else if (typeInput == "differential")
                {
                    type = BackupType.Differential;
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid type. Please enter 'Full' or 'Differential'.");
                }
            }

            BackupJob job = new BackupJob
            {
                Name = name,
                SourcePath = source,
                TargetPath = target,
                Type = type
            };

            backupJobs.Add(job);
            Console.WriteLine("Backup job created successfully!");
        }

        public void ListBackupJobs()
        {
            if (backupJobs.Count == 0)
            {
                Console.WriteLine("No backup jobs found.");
                return;
            }

            Console.WriteLine("List of backup jobs:");
            foreach (var job in backupJobs)
            {
                Console.WriteLine($"- {job.Name} | Source: {job.SourcePath} | Target: {job.TargetPath} | Type: {job.Type}");
            }
        }

        public void RunBackupJob(string jobName)
        {
            var job = backupJobs.FirstOrDefault(j => j.Name.Equals(jobName, StringComparison.OrdinalIgnoreCase));
            if (job == null)
            {
                Console.WriteLine($"Backup job '{jobName}' not found.");
                return;
            }

            Console.WriteLine($"Running backup job '{job.Name}'...");
            backupService.ExecuteBackup(job);
        }

        public void RunAllBackupJobs()
        {
            if (backupJobs.Count == 0)
            {
                Console.WriteLine("No backup jobs to run.");
                return;
            }

            foreach (var job in backupJobs)
            {
                Console.WriteLine($"Running backup job '{job.Name}'...");
                backupService.ExecuteBackup(job);
            }
        }

        public void DeleteBackupJob(string jobName)
        {
            var job = backupJobs.FirstOrDefault(j => j.Name.Equals(jobName, StringComparison.OrdinalIgnoreCase));
            if (job == null)
            {
                Console.WriteLine($"Backup job '{jobName}' not found.");
                return;
            }

            backupJobs.Remove(job);
            SaveJobs();

            Console.WriteLine($"Backup job '{jobName}' deleted.");
        }

        private void LoadJobs()
        {
            try
            {
                if (File.Exists(jobsFilePath))
                {
                    string json = File.ReadAllText(jobsFilePath);
                    backupJobs = JsonSerializer.Deserialize<List<BackupJob>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }) ?? new List<BackupJob>();
                }
                else
                {
                    backupJobs = new List<BackupJob>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading backup jobs: {ex.Message}");
                backupJobs = new List<BackupJob>();
            }
        }

        private void SaveJobs()
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

                string json = JsonSerializer.Serialize(backupJobs, options);
                Directory.CreateDirectory(Path.GetDirectoryName(jobsFilePath));
                File.WriteAllText(jobsFilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving backup jobs: {ex.Message}");
            }
        }
    }
}
