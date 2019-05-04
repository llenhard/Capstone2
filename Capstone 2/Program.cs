using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone_2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Task> tasks = new List<Task>();
            bool quitting = false;

            Console.WriteLine("Welcome to the Task Manager.");

            while (!quitting)
            {
                Console.WriteLine("" +
                    "Would you like to:" +
                    "\n1. List tasks" +
                    "\n2. Add task" +
                    "\n3. Delete task" +
                    "\n4. Toggle task complete" +
                    "\n5. Exit");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        DisplayTasks(tasks);
                        break;
                    case "2":
                        AddTask(tasks);
                        break;
                    case "3":
                        DeleteTask(tasks);
                        break;
                    case "4":
                        ToggleComplete(tasks);
                        break;
                    case "5":
                        quitting = true;
                        Console.WriteLine("Goodbye!");
                        break;
                    default:
                        Console.WriteLine("That wasn't one of the options. Try again and be sure to enter the correct option!");
                        break;
                }
            }
        }

        public static bool IsTask(List<Task> tasks, string input)
        {//sanity check method
            try
            {
                int index = int.Parse(input) - 1;
                tasks[index].GetType();//in hindsight i probably could have just done count and made sure the int wasnt higher than it
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void DeleteTask(List<Task> tasks)
        {
            Console.Write("Enter the number of a task you would like to delete: ");
            string input = Console.ReadLine();

            if (IsTask(tasks, input))
            {
                tasks.RemoveAt(int.Parse(input) - 1);
                Console.WriteLine($"Task #{input} removed.");
                return;
            }

            Console.WriteLine("Error! Enter a valid task number.");
            DeleteTask(tasks);//im addicted to recurring methods, makes it easier on me instead of writing while loops in main method
            return;//lemme know if i shouldnt do recurring methods like this
        }

        public static void ToggleComplete(List<Task> tasks)
        {
            Console.Write("Enter the number of the task you would like to toggle completion of: ");
            string input = Console.ReadLine();

            if(IsTask(tasks, input))
            {
                tasks.ElementAt(int.Parse(input) - 1).ToggleComplete();//toggle complete is a method in Task.cs as well, didnt think that through
                Console.WriteLine($"Task #{input} is now {(tasks.ElementAt(int.Parse(input)-1).Complete ? "complete":"incomplete")}.");
                return;
            }

            Console.WriteLine("Error! Enter a valid task number.");
            ToggleComplete(tasks);
            return;

        }

        public static void AddTask(List<Task> toAddTo)
        {
            string inputName, inputDesc;
            DateTime inputDate;
            
            try
            {//had date entry fisrst since it's the only part of this you can even mess up
                Console.Write("Due date(mm/dd/yyyy): ");//and im too lazy to make it do name first
                inputDate = DateTime.Parse(Console.ReadLine()).Date;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Invalid date format. Please enter it again in the proper format.");
                AddTask(toAddTo);
                return;
            }

            Console.Write("Team Member: ");
            inputName = Console.ReadLine();

            Console.Write("Task description: ");
            inputDesc = Console.ReadLine();

            Task toAdd = new Task(inputName, inputDesc, inputDate);
            toAddTo.Add(toAdd);

            return;
        }
        
        public static void DisplayTasks(List<Task> tasks)
        {
            int amount = tasks.Count;
            string[] name = new string[amount], desc = new string[amount];
            DateTime[] due = new DateTime[amount];
            bool[] complete = new bool[amount];

            foreach(Task task in tasks)
            {//arrays for formatting easier
                for(int index = 0; index < amount; index++)
                {
                    name[index] = task.Name;
                    desc[index] = task.Desc;
                    due[index] = task.Due;
                    complete[index] = task.Complete;
                }
            }

            Console.WriteLine("Tasks:");//i stole this from the internet but at least it looks pretty
            string data = string.Format("{0,10} {1,10} {2,15} {3,20} \n",
            "Complete", "Due Date", "Team Member", "Description");

            for (int index = 0; index < desc.Length; index++)
                data += string.Format("{4}.{0,-10} {1,-10}    {2,-15}    {3,-20}\n",
                complete[index], due[index].ToString("mm/dd/yyyy"), name[index], desc[index], index+1);

            Console.WriteLine($"\n{data}");

            return;
        }
    }
}
