using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modul16HW
{

    class FileManager
    {
        static void Main()
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("\nSimple File Manager");
                Console.WriteLine("1. View Directory Contents");
                Console.WriteLine("2. Create File/Directory");
                Console.WriteLine("3. Delete File/Directory");
                Console.WriteLine("4. Copy/Move File/Directory");
                Console.WriteLine("5. Read/Write File");
                Console.WriteLine("6. Exit");
                Console.Write("Enter your choice: ");

                int choice = Convert.ToInt32(Console.ReadLine());

                try
                {
                    switch (choice)
                    {
                        case 1:
                            ViewDirectoryContents();
                            break;
                        case 2:
                            CreateFileOrDirectory();
                            break;
                        case 3:
                            DeleteFileOrDirectory();
                            break;
                        case 4:
                            CopyMoveFileOrDirectory();
                            break;
                        case 5:
                            ReadWriteFile();
                            break;
                        case 6:
                            running = false;
                            break;
                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    LogAction($"Error: {ex.Message}");
                }
            }
        }

        static void ViewDirectoryContents()
        {
            Console.Write("Enter directory path: ");
            string path = Console.ReadLine();

            if (Directory.Exists(path))
            {
                string[] files = Directory.GetFiles(path);
                string[] directories = Directory.GetDirectories(path);

                Console.WriteLine("Files:");
                foreach (string file in files)
                {
                    Console.WriteLine(file);
                }

                Console.WriteLine("Directories:");
                foreach (string dir in directories)
                {
                    Console.WriteLine(dir);
                }
            }
            else
            {
                Console.WriteLine("Directory does not exist.");
            }
            LogAction("Viewed directory contents.");
        }

        static void CreateFileOrDirectory()
        {
            Console.WriteLine("Enter 'file' to create a file, or 'dir' to create a directory:");
            string choice = Console.ReadLine().ToLower();

            Console.Write("Enter the path for the new file or directory: ");
            string path = Console.ReadLine();

            if (choice == "file")
            {
                File.Create(path).Close();
                Console.WriteLine($"File created: {path}");
            }
            else if (choice == "dir")
            {
                Directory.CreateDirectory(path);
                Console.WriteLine($"Directory created: {path}");
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
            LogAction($"Created {choice}: {path}");
        }

        static void DeleteFileOrDirectory()
        {
            Console.Write("Enter the path to delete a file or directory: ");
            string path = Console.ReadLine();

            if (File.Exists(path))
            {
                File.Delete(path);
                Console.WriteLine($"File deleted: {path}");
            }
            else if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
                Console.WriteLine($"Directory deleted: {path}");
            }
            else
            {
                Console.WriteLine("File or directory does not exist.");
            }
            LogAction($"Deleted: {path}");
        }

        static void CopyMoveFileOrDirectory()
        {
            Console.WriteLine("Enter 'copy' to copy or 'move' to move a file/directory:");
            string choice = Console.ReadLine().ToLower();

            Console.Write("Enter the source path: ");
            string sourcePath = Console.ReadLine();

            Console.Write("Enter the destination path: ");
            string destinationPath = Console.ReadLine();

            if (choice == "copy")
            {
                if (Directory.Exists(sourcePath))
                {

                    Console.WriteLine("Directory copy not implemented.");
                }
                else if (File.Exists(sourcePath))
                {
                    File.Copy(sourcePath, destinationPath, true);
                    Console.WriteLine("File copied.");
                }
                else
                {
                    Console.WriteLine("Source does not exist.");
                }
            }
            else if (choice == "move")
            {
                if (Directory.Exists(sourcePath))
                {
                    Directory.Move(sourcePath, destinationPath);
                    Console.WriteLine("Directory moved.");
                }
                else if (File.Exists(sourcePath))
                {
                    File.Move(sourcePath, destinationPath);
                    Console.WriteLine("File moved.");
                }
                else
                {
                    Console.WriteLine("Source does not exist.");
                }
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
            LogAction($"{choice} from {sourcePath} to {destinationPath}");
        }

        static void ReadWriteFile()
        {
            Console.WriteLine("Enter 'read' to read a file or 'write' to write to a file:");
            string choice = Console.ReadLine().ToLower();

            Console.Write("Enter the file path: ");
            string path = Console.ReadLine();

            if (choice == "read")
            {
                if (File.Exists(path))
                {
                    string content = File.ReadAllText(path);
                    Console.WriteLine($"File content:\n{content}");
                }
                else
                {
                    Console.WriteLine("File does not exist.");
                }
            }
            else if (choice == "write")
            {
                Console.WriteLine("Enter the text to write to the file:");
                string text = Console.ReadLine();

                File.WriteAllText(path, text);
                Console.WriteLine("Text written to file.");
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
            LogAction($"{choice} file: {path}");
        }

        static void LogAction(string action)
        {
            File.AppendAllText("log.txt", $"{DateTime.Now}: {action}\n");
        }
    }
}
