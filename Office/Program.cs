using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] Names = System.IO.File.ReadAllLines("Names.txt");
            int max_director_orders = 3;
            Random Randomizer = new Random(DateTime.Now.Millisecond);
            List<Employee> Staff = new List<Employee>();
            Staff.Add(new Employee(Names[Randomizer.Next(200)], true, false, false, Randomizer));
            Staff.Add(new Employee(Names[Randomizer.Next(200)], false, true, false, Randomizer));
            Staff.Add(new Employee(Names[Randomizer.Next(200)], false, false, true, Randomizer)); 
            for (int i = 0; i < Randomizer.Next(7, 98); i++)
            {
                Staff.Add(new Employee(Names[Randomizer.Next(200)], false, false, false, Randomizer));
            }
            List<Employee> Directors = new List<Employee>();
            List<Employee> Managers = new List<Employee>();
            List<Employee> Bookers = new List<Employee>();
            List<Employee> Programmers= new List<Employee>();
            List<Employee> Testers = new List<Employee>();
            List<Employee> Designers = new List<Employee>();
            for (int i = 0; i < Staff.Count; i++)
            {
                for (int j = 0; j < Staff[i].Posts.Count; j++)
                {
                    switch (Staff[i].Posts[j]) 
                    {
                        case (int)Functions.Booker:
                            Bookers.Add(Staff[i]);
                            break;
                        case (int)Functions.Designer:
                            Designers.Add(Staff[i]);
                            break;
                        case (int)Functions.Director:
                            Directors.Add(Staff[i]);
                            break;
                        case (int)Functions.Manager:
                            Managers.Add(Staff[i]);
                            break;
                        case (int)Functions.Programmer:
                            Programmers.Add(Staff[i]);
                            break;
                        case (int)Functions.Tester:
                            Testers.Add(Staff[i]);
                            break;
                    }
                }
            }
            int AssignedTasks = 0;
            int CompletedTasks = 0;
            int RemotedTasks = 0;

            for (int hour = 0; hour < 160; hour++)
            {
                foreach (Employee director in Directors)
                {
                    for (int i = 0; i < Randomizer.Next(max_director_orders + 1); i++)
                    {
                        Task Order = new Task(Randomizer);
                        AssignedTasks++;
                        bool OrderCanBeAppoint = false;
                        List<Employee> ProperWorkers = new List<Employee>();
                        switch (Order.Function) 
                        {
                            case (int)Functions.Booker:
                                ProperWorkers = new List<Employee>(Bookers);
                                break;
                            case (int)Functions.Designer:
                                ProperWorkers = new List<Employee>(Designers);
                                break;
                            case (int)Functions.Manager:
                                ProperWorkers = new List<Employee>(Managers);
                                break;
                            case (int)Functions.Programmer:
                                ProperWorkers = new List<Employee>(Programmers);
                                break;
                            case (int)Functions.Tester:
                                ProperWorkers = new List<Employee>(Testers);
                                break;
                        }
                        foreach (Employee workman in ProperWorkers)
                        {
                            if (!workman.Busy && workman.WorkedHours <= 40)
                            {
                                workman.Job = Order;
                                workman.Busy = true;
                                OrderCanBeAppoint = true;
                                break;
                            }
                        }
                        if (!OrderCanBeAppoint)
                        {
                            RemotedTasks++;
                        }
                    }
                }
                foreach (Employee workman in Staff)
                {
                    if (workman.Busy)
                    {
                        workman.Job.Remain--;
                        workman.WorkedHours++;
                        for (int i = 0; i < workman.Posts.Count; i++)
                        {
                            if (workman.Posts[i] == workman.Job.Function &&
                                (workman.Job.Function != (int)Functions.Booker || workman.Job.Function != (int)Functions.Manager))
                            {
                                workman.EarnedMoney += workman.Salaries[i];
                                break;
                            }
                        }
                        if (workman.Job.Remain == 0)
                        {
                            workman.Job = null;
                            workman.Busy = false;
                            CompletedTasks++;
                        }
                    }
                }
                //end of week
                if (hour % 40 == 0)
                {
                    foreach (Employee workman in Staff)
                    {
                        for (int i = 0; i < workman.Posts.Count; i++)
                        {
                            if (workman.Posts[i] == (int)Functions.Director || workman.Posts[i] == (int)Functions.Manager || workman.Posts[i] == (int)Functions.Booker)
                            {
                                workman.EarnedMoney += workman.Salaries[i];
                            }
                        }
                        workman.WorkedHours = 0;
                    }
                }
            }
            System.IO.StreamWriter Report = new System.IO.StreamWriter("Report.txt", false, Encoding.UTF8, 65536);
            Report.WriteLine("Amount of Employees: " + Staff.Count);
            Report.WriteLine("Personnel:");
            Report.WriteLine("Directors: " + Directors.Count);
            Report.WriteLine("Managers: " + Managers.Count);
            Report.WriteLine("Bookers: " + Bookers.Count);
            Report.WriteLine("Programmers: " + Programmers.Count);
            Report.WriteLine("Testers: " + Testers.Count);
            Report.WriteLine("Designers: " + Designers.Count);
            Report.WriteLine("\nAssigned Tasks: " + AssignedTasks);
            Report.WriteLine("Completed: " + CompletedTasks);
            Report.WriteLine("Remoted to Freelance: " + RemotedTasks);
            Report.WriteLine("\nBalance:");
            Report.WriteLine("{0,-24} | {1,-10}", "Name", "Salary");
            int Total = 0;
            foreach (Employee workman in Staff)
            {
                Report.WriteLine("{0,-24} | {1, -10}", workman.Name, workman.EarnedMoney);
                Total += workman.EarnedMoney;
            }
            Report.WriteLine("{0,-24} | {1,-10}", "In total", Total);
            Report.Close();
        }
    }
}