using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Planner_SLR
{
    class Program
    {
        private static readonly List<Planner> Tasks = new List<Planner>();

        public static void Main(string[] args)
        {
            Console.Title = "Planner-SLR";
            
            //beginning of the main
            while (true)
                {
                    ExecuteAction();
                }
        }

        //The selection menu
        private static void ShowMenu()
        {
            //Screen cleaning
            Console.Clear();

            //Menu
            Console.WriteLine("**********************************************************************");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("             WARNING! AFTER YOUR CHANGES IN YOUR LIST");
            Console.WriteLine("         YOU MUST VIEW IT (PRESS [0]) AND SAVE IT (PRESS [5])");
            Console.WriteLine("             AND BE SURE OF THE CORRECTNESS OF THE DATA");
            Console.WriteLine("     IN ADDITION THE DATA AFTER RECORDING IN THE FILE WILL BE LOST!");
            Console.ResetColor();
            Console.WriteLine("**********************************************************************");
            Console.WriteLine();

            Console.WriteLine(" Welcome to your daily planner, choose an action: ");
            
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine(" [0] Show to-do list");
            Console.WriteLine(" [1] Add a new task");
            Console.WriteLine(" [2] Delete the task");
            Console.WriteLine(" [3] Edit the task");
            Console.WriteLine(" [4] Viewing the contents of a file");
            Console.WriteLine(" [5] Save data to the file");
            Console.WriteLine(" [6] Close application");
            Console.WriteLine("-------------------------------------------------");
        }

       //Method for entering values
        private static int InputSelect()
        {
            Console.Write("> ");
            try
            {
                //Read the entered text. The Convert.ToInt32 method produces an error if there are letters in the string
                //The error is delayed by the catch block
                var input = Convert.ToInt32(Console.ReadLine());

                //Validation of entered values
                if (!(input >= 0 && input <= 6))
                {
                    throw new Exception("Validation error");
                }

                return input;
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\r\nPlease enter a valid number. (Between 0 and 6)");
                Console.ResetColor();
                return InputSelect();
            }
        }

        private static void ExecuteAction()
        {
            while (true)
            {
                ShowMenu();
                int input = InputSelect();

                switch (input)
                {
                    case 0:
                        ShowTasks();
                        break;
                    case 1:
                        CreateTask();
                        break;
                    case 2:
                        DeleteTask();
                        break;
                    case 3:
                        EditTask();
                        break;
                    case 4:
                        SeeTheFile();
                        break;
                    case 5:
                        SaveInTheFile();
                        break;
                    case 6:
                        EndApplication();
                        break;

                    default:
                        //Verification will not be performed
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Unknown number");
                        Console.ResetColor();
                        continue;
                }
                break;
            }
        }

        private static int SelectTask()
        {
            PrintAllTasks();

            try
            {
                var nummer = Convert.ToInt32(Console.ReadLine());

                if (!(nummer >= 0 && nummer < Tasks.Count))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    throw new Exception("Validation error");
                }
                return nummer;
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("An error has occurred. Please, try again.\r\n");
                return SelectTask();
            }
        }

        private static void ShowTasks()
        {
            Console.Clear();
            Console.WriteLine("Show task`s number: " + Tasks.Count);

            PrintAllTasks();

            Console.WriteLine("\r\nBack with [Enter]");
            //Press [Enter] to return the menu.
            Console.ReadLine();
        }

        private static void PrintAllTasks()
        {
            foreach (var task in Tasks)
            {
                Console.WriteLine("Create: " + task.CreatedAt.ToShortDateString() + " " + task.CreatedAt.ToShortTimeString() + ": " + task.Description);
            }

            
        }

        private static void CreateTask()
        {
            Console.Clear();
            Console.WriteLine("Create task.");

            Console.Write("Enter description\r\n> ");
            var description = Console.ReadLine();
            var todo = new Planner(description);
            Tasks.Add(todo);

            Console.WriteLine("The task was created.\r\nClick [Enter]");
            Console.ReadLine();
        }

        private static void DeleteTask()
        {
            Console.Clear();
            Console.WriteLine("Delete task.");
            Console.WriteLine("To select the job to be deleted, enter its number (the numbering starts at 0)");

            if (Tasks.Count == 0)
            {
                Console.WriteLine("There are no tasks and so none can be deleted.\r\nClick [Enter]");
                Console.ReadLine();
                return;
            }

            int task = SelectTask();

            //Remove task from the list
            Tasks.RemoveAt(task);

            Console.WriteLine("The task was deleted successfully.\r\nClick [Enter]");
            Console.ReadLine();
        }

        private static void EditTask()
        {
            Console.Clear();
            Console.WriteLine("Edit task");
            Console.WriteLine("To select a variable task, enter its number (the numbering starts from 0)");
            if (Tasks.Count == 0)
            {
                Console.WriteLine("There are no tasks so none can be edited.\r\nClick [Enter]");
                Console.ReadLine();
                return;
            }

            var task = SelectTask();
            var taskObj = Tasks[task];

            Console.WriteLine("Old description: " + taskObj.Description);
            Console.Write("New description: ");
            var description = Console.ReadLine();

            taskObj.Description = description;

            Console.WriteLine("The task was successfully edited.\r\nClick [Enter]");
            Console.ReadLine();
        }

        private static void EndApplication()
        {
            Environment.Exit(0);
        }

        private static void SeeTheFile()
        {
            string line;
            string[] lines = File.ReadAllLines("Planner.txt");

            //Сhecking for an empty file
            if (lines.Length == 0)
            {
                Console.WriteLine("File is empty");
            }
            else
            {
                StreamReader sr = new StreamReader("Planner.txt");
                //Read the first line of text
                line = sr.ReadLine();

                //Continue to read until reach end of file
                while (line != null)
                {
                    Console.WriteLine(line);
                    // var description = line;
                    //  var todo = new Planner(description);
                    // Tasks.Add(todo);

                    //Read the next line
                    line = sr.ReadLine();
                }

                //Close the file
                sr.Close();
                Console.WriteLine("All data was successfully downloaded for viewing\r\nClick [Enter]");
            }
            Console.ReadLine();
        }

            private static void SaveInTheFile()
        {
           StreamWriter sw = new StreamWriter("PLanner.txt", false);
           foreach (var task in Tasks)
            {

                string lineffile = (task.CreatedAt.ToShortDateString() + " " + task.CreatedAt.ToShortTimeString() + ": " + task.Description);
                sw.WriteLine(lineffile);
            }                            
            sw.Close();
            Console.WriteLine("Data successfully recorded\r\nClick [Enter]");
            Console.ReadLine();
        }
    }
}