EasySave
A robust C# file backup application with a Terminal interface for efficiently managing backup jobs.

Features
Multiple Backup Types: Support for full and differential backups
Job Management: Create, start, stop, and monitor backup jobs
Terminal Interface: User-friendly interface 
Progress Tracking: Real-time tracking of backup progress
Logging System: Comprehensive logging of backup activities and states
Multi-language Support: Support for multiple languages in the interface

Getting Started
Prerequisites
.NET 8.0 SDK or later
Windows operating system
Installation
Clone the repository:

git clone https://github.com/Nayxooo/secret_easysave.git
Navigate to the project directory:

cd better_saving
Build the application:

dotnet build
Run the application:

dotnet run
Usage
Launch the application
Create a new backup job by specifying:
Job name
Source directory (files to backup)
Target directory (backup destination)
Backup type (Full or Differential)
Start the job to begin the backup process
Monitor the progress through the Terminal GUI
View logs for detailed information about backup operations
Interface Screenshots
Below are screenshots of the EasySave console interface:

Main Dashboard
The main application interface showing job list and available actions Main Dashboard

Job Creation
Interface for creating a new backup job Job Creation

Jobs List
View of completed backup jobs and their results Jobs List

Job Details
Detailed view of a specific backup job, including progress and status Job Details

Backup Execution
A backup job in progress with real-time statistics Backup Execution

Language Selection
Application settings including language selection Language Selection

Main Dashboard in French
The main application interface in French Main Dashboard in French

Project Structure
Controllers/: Contains the main application controller
Models/: Contains data models for backup jobs, file hashing, and logging
Views/: Contains the Terminal user interface
UMLs/: Contains UML diagrams describing the application architecture
Documentation
For a comprehensive overview of the codebase, including detailed explanations of architecture, components, and implementation details, please see the Code Overview document.

Acknowledgments
Terminal.Gui - GUI toolkit used for the terminal interface
