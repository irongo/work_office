using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office
{
    public class Employee
    {
        string name;
        List<int> posts;
        List<int> salaries;
        bool busy;
        Task job;
        int week_schedule;
        int worked_hours;
        int earned_money;
        public string Name
        {
            get
            {
                return name;
            }
        }
        public List<int> Posts
        {
            get
            {
                return posts;
            }
        }
        public List<int> Salaries
        {
            get
            {
                return salaries;
            }
        }
        public bool Busy
        {
            get
            {
                return busy;
            }
            set
            {
                busy = value;
            }
        }
        public Task Job
        {
            get
            {
                return job;
            }
            set
            {
                job = value;
            }
        }
        public int WeekSchedule
        {
            get
            {
                return week_schedule;
            }
        }
        public int WorkedHours
        {
            get
            {
                return worked_hours;
            }
            set
            {
                worked_hours = value;
            }
        }
        public int EarnedMoney
        {
            get
            {
                return earned_money;
            }
            set
            {
                earned_money = value;
            }
        }
        public Employee(string Name, bool IsDirector, bool IsManager, bool IsBooker, Random Randomizer)
        {
            name = Name;
            posts = new List<int>();
            salaries = new List<int>();
            for (int i = 0; i < 6; i++)
            {
                if (Randomizer.Next(2) == 1 || 
                    (IsDirector && i == (int)Functions.Director) ||
                    (IsManager && i == (int)Functions.Manager) || 
                    (IsBooker && i == (int)Functions.Booker))
                {
                    posts.Add(i);
                    switch (i) 
                    {
                        case (int)Functions.Booker:
                            salaries.Add((int)Payments.Booker);
                            break;
                        case (int)Functions.Designer:
                            salaries.Add((int)Payments.Designer);
                            break;
                        case (int)Functions.Director:
                            salaries.Add((int)Payments.Director);
                            break;
                        case (int)Functions.Manager:
                            salaries.Add((int)Payments.Manager);
                            break;
                        case (int)Functions.Programmer:
                            salaries.Add((int)Payments.Programmer);
                            break;
                        case (int)Functions.Tester:
                            salaries.Add((int)Payments.Tester);
                            break;
                    }
                }
            }
            if (posts.Count == 0)
            {   
                posts.Add(Randomizer.Next(4, 6));
                switch (posts[0]) 
                {
                    case (int)Functions.Designer:
                        salaries.Add((int)Payments.Designer);
                        break;
                    case (int)Functions.Programmer:
                        salaries.Add((int)Payments.Programmer);
                        break;
                    case (int)Functions.Tester:
                        salaries.Add((int)Payments.Tester);
                        break;
                }
            }
            busy = false;
            job = null;
            week_schedule = Randomizer.Next(41);
        }
    }

    public enum Functions : int { Director, Manager, Booker, Programmer, Tester, Designer };
    public enum Payments : int { Director = 5000, Manager = 4000, Booker = 3000, Programmer = 75, Tester = 70, Designer = 80};
}
