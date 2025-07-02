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
        private readonly LanguageController languageController;

        public BackupJobController(LanguageController languageController)
        {
            this.languageController = languageController;
            backupService = new BackupService();
            logService = new LogService();
            LoadJobs();
        }

        private string T(string key) => languageController.T(key);

        public void CreateBackupJob()
        {
            Console.Clear();
            Console.WriteLine("=== " + T("menu.createJob") + " ===");

            if (backupJobs.Count >= MaxBackupJobs)
            {
                Console.WriteLine(string.Format(T("message.maxJobsReached"), MaxBackupJobs));
                return;
            }

            string name;
            do
            {
                Console.Write(T("prompt.jobName"));
                name = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(name))
                    Console.WriteLine(T("message.jobNameEmpty"));
            } while (string.IsNullOrWhiteSpace(name));

            Console.Write(T("prompt.sourcePath"));
            string source = Console.ReadLine()?.Trim();

            Console.Write(T("prompt.targetPath"));
            string target = Console.ReadLine()?.Trim();

            BackupType type;
            while (true)
            {
                Console.Write(T("prompt.type"));
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
                    Console.WriteLine(T("message.invalidType"));
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
            SaveJobs();
            Console.WriteLine(T("message.jobCreated"));
        }

        public void ListBackupJobs()
        {
            if (backupJobs.Count == 0)
            {
                Console.WriteLine(T("message.jobListEmpty"));
                return;
            }

            Console.WriteLine(T("message.jobListHeader"));
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
                Console.WriteLine(T("message.jobNotFound"));
                return;
            }

            Console.WriteLine(string.Format(T("message.jobRunning"), job.Name));
            backupService.ExecuteBackup(job);
        }

        public void RunAllBackupJobs()
        {
            if (backupJobs.Count == 0)
            {
                Console.WriteLine(T("message.jobListEmpty"));
                return;
            }

            foreach (var job in backupJobs)
            {
                Console.WriteLine(string.Format(T("message.jobRunning"), job.Name));
                backupService.ExecuteBackup(job);
            }
        }

        public void DeleteBackupJob(string jobName)
        {
            var job = backupJobs.FirstOrDefault(j => j.Name.Equals(jobName, StringComparison.OrdinalIgnoreCase));
            if (job == null)
            {
                Console.WriteLine(T("message.jobNotFound"));
                return;
            }

            backupJobs.Remove(job);
            SaveJobs();
            Console.WriteLine(T("message.jobDeleted"));
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
                Console.WriteLine($"{T("error.loadJobs")} {ex.Message}");
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
                Console.WriteLine($"{T("error.saveJobs")} {ex.Message}");
            }
        }
    }
}
