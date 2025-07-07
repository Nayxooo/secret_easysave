using System;

namespace Models
{
    public class BackupLogEntry
    {
        public string Name { get; set; }
        public string FileSource { get; set; }
        public string FileTarget { get; set; }
        public string DestPath { get; set; } = "";
        public long FileSize { get; set; }
        public double FileTransferTime { get; set; }
        public string Time { get; set; }

        public BackupLogEntry()
        {
            Time = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }
    }
}
