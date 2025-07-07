using System;
using Controllers;

namespace Views
{
    public class MainView
    {
        private readonly BackupJobController backupJobController;
        private readonly LanguageController languageController;

        public MainView(LanguageController languageController)
        {
            this.languageController = languageController;
            backupJobController = new BackupJobController(languageController);
        }

        public void ShowMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                PrintAsciiLogo();
                Console.ResetColor();

                Console.WriteLine("╔═══════════════════════════════════════════════╗");
                Console.WriteLine($"║ 1. {languageController.T("menu.createJob"),-43}║");
                Console.WriteLine($"║ 2. {languageController.T("menu.listJobs"),-43}║");
                Console.WriteLine($"║ 3. {languageController.T("menu.runOneJob"),-43}║");
                Console.WriteLine($"║ 4. {languageController.T("menu.runAllJobs"),-43}║");
                Console.WriteLine($"║ 5. {languageController.T("menu.deleteJob"),-43}║");
                Console.WriteLine($"║ 6. {languageController.T("menu.changeLang"),-43}║");
                Console.WriteLine($"║ 0. {languageController.T("menu.exit"),-43}║");
                Console.WriteLine("╚═══════════════════════════════════════════════╝");
                Console.Write(languageController.T("menu.choice"));

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        backupJobController.CreateBackupJob();
                        break;
                    case "2":
                        backupJobController.ListBackupJobs();
                        break;
                    case "3":
                        Console.Write(languageController.T("menu.enterJobName"));
                        backupJobController.RunBackupJob(Console.ReadLine());
                        break;
                    case "4":
                        backupJobController.RunAllBackupJobs();
                        break;
                    case "5":
                        Console.Write(languageController.T("menu.enterJobName"));
                        backupJobController.DeleteBackupJob(Console.ReadLine());
                        break;
                    case "6":
                        ChangeLanguage();
                        break;
                    case "0":
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(languageController.T("menu.invalidChoice"));
                        Console.ResetColor();
                        break;
                }

                Console.WriteLine(languageController.T("menu.pressEnter"));
                Console.ReadLine();
            }
        }

        private void ChangeLanguage()
        {
            Console.Write("Choose language / Choisissez la langue (en/fr): ");
            string lang = Console.ReadLine()?.Trim().ToLower();
            if (lang != "fr" && lang != "en")
            {
                Console.WriteLine("Invalid language.");
                return;
            }

            languageController.SetLanguage(lang);
            Console.WriteLine("Language changed.");
        }

        private void PrintAsciiLogo()
        {
            Console.WriteLine(@"
___________                      _________                   
\_   _____/____    _________.__./   _____/____ ___  __ ____  
 |    __)_\__  \  /  ___<   |  |\_____  \\__  \\  \/ // __ \ 
 |        \/ __ \_\___ \ \___  |/        \/ __ \\   /\  ___/ 
/_______  (____  /____  >/ ____/_______  (____  /\_/  \___  >
        \/     \/     \/ \/            \/     \/          \/  
");
        }
    }
}
