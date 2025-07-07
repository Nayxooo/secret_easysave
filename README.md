# 🛡️ Backup Manager Console App

A multilingual C# console application for managing and executing file backup jobs. Supports full and differential backups, persistent job storage, and a clean, user-friendly interface.

---

## 📁 Project Structure

BackupManager/
├── Controllers/
│ ├── BackupJobController.cs
│ ├── LanguageController.cs
├── Models/
│ ├── BackupJob.cs
│ ├── BackupType.cs
├── Services/
│ ├── BackupService.cs
│ ├── LogService.cs
├── Views/
│ └── MainView.cs
├── Data/
│ └── backupJobs.json (auto-generated)
├── lang.en.json
├── lang.fr.json
├── Program.cs


---

## 🚀 Features

- ✅ Create, list, run, and delete backup jobs
- 💾 Backup types:
  - Full: copies all files
  - Differential: only changed files
- 🌍 Multilingual UI (English 🇺🇸 / French 🇫🇷)
- 📁 Persistent job data (JSON)
- 📝 Console output with logging system
- 👨‍💻 Easily extensible with new features

---

## 🧠 Key Concepts

### 🗂️ Backup Types

- **Full Backup**: Copies all files from the source to the destination directory.
- **Differential Backup**: Copies only the files that have changed since the last full backup.

---

## 🧭 Using the App

### ▶️ Running the Application

1. Clone the repository or copy the project files.
2. Open a terminal in the project directory.
3. Run:

```bash
dotnet run

📜 Menu Overview
Once launched, the main menu is displayed:

1. Create a new backup job
2. List existing backup jobs
3. Run a backup job
4. Run all backup jobs
5. Delete a backup job
6. Change language
0. Exit

🗂️ Managing Backup Jobs
✅ Creating a Backup Job
You'll be prompted to enter:

Job name

Source directory

Target directory

Backup type (Full or Differential)

Example:

Enter job name: MyDocuments
Enter source directory: C:\Users\Me\Documents
Enter target directory: D:\Backups\Docs
Enter backup type (full/differential): full

📋 Listing Jobs
Displays all saved backup jobs with their configuration.

▶️ Running a Job
Run a specific job by entering its name.

Or run all jobs at once via the corresponding menu option.

❌ Deleting a Job
Enter the name of the job to delete.

🌍 Multilingual Interface
Language selection is handled via the LanguageController. Translations are stored in JSON files:

🔤 Example JSON (lang.en.json)

```json
{
  "menu.createJob": "Create a new backup job",
  "menu.listJobs": "List existing backup jobs",
  "menu.runOneJob": "Run a backup job",
  "menu.runAllJobs": "Run all backup jobs",
  "menu.deleteJob": "Delete a backup job",
  "menu.exit": "Exit",
  "menu.choice": "Enter your choice: ",
  "menu.invalidChoice": "Invalid choice. Try again.",
  "menu.enterJobName": "Enter the job name: ",
  "menu.pressEnter": "Press Enter to continue...",
  "menu.changeLang": "Change language",
  "prompt.jobName": "Enter job name: ",
  "prompt.sourcePath": "Enter source directory: ",
  "prompt.targetPath": "Enter target directory: ",
  "prompt.type": "Enter backup type (full/differential): ",
  "message.jobCreated": "Backup job created successfully.",
  "message.jobExists": "A job with this name already exists.",
  "message.jobNotFound": "Backup job not found.",
  "message.jobDeleted": "Backup job deleted.",
  "message.jobListEmpty": "No backup jobs found.",
  "message.jobListHeader": "Registered Backup Jobs:"
}```

⚙️ Requirements
.NET 6.0 SDK or later

To check your installed version:

dotnet --version
