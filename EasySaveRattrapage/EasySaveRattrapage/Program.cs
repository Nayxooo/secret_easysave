using System;
using Controllers;
using Views;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Langue par défaut : anglais
        LanguageController languageController = new LanguageController("en");
        MainView mainView = new MainView(languageController);

        mainView.ShowMenu();

        Console.WriteLine("Goodbye!");
    }
}
