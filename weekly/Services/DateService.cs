using System;
using System.Collections.Generic;
using System.Globalization;
using weekly.Models;

namespace weekly.Services
{
    public class DateService
    {
        private readonly ToDoService _toDos;
        public DateService(ToDoService toDoService)
        {
            _toDos = toDoService;
        }

        public Week ArrangeWeekly()
        {
            Week weekly = this.GetCurrentWeek();
            List<ToDo> toDos = _toDos.Get();

            foreach (ToDo todo in toDos)
            {
                foreach (Day day in weekly.Days)
                {
                    if (todo.Date.Equals(day.Date))
                    {
                        day.ToDos.Add(todo);
                    }
                }
            }
            return weekly;
        }

        public Week GetCurrentWeek()
        {
            Week week = new Week();
            week.Days = new List<Day>();
            
            CultureInfo myCI = new CultureInfo("da-DK");
            Calendar myCal = myCI.Calendar;
            CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
            DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;

            week.WeekNr = myCal.GetWeekOfYear(DateTime.Now, myCWR, myFirstDOW);

            var day = myCal.GetDayOfWeek(DateTime.Now);
            int firstDayOfWeekOffset = 0;

            switch (day)
            {
                case DayOfWeek.Monday:
                    firstDayOfWeekOffset = 0;
                    break;
                case DayOfWeek.Tuesday:
                    firstDayOfWeekOffset = -1;
                    break;
                case DayOfWeek.Wednesday:
                    firstDayOfWeekOffset = -2;
                    break;
                case DayOfWeek.Thursday:
                    firstDayOfWeekOffset = -3;
                    break;
                case DayOfWeek.Friday:
                    firstDayOfWeekOffset = -4;
                    break;
                case DayOfWeek.Saturday:
                    firstDayOfWeekOffset = -5;
                    break;
                case DayOfWeek.Sunday:
                    firstDayOfWeekOffset = -6;
                    break;
            }
            
            
            
            DateTime thisDate = DateTime.Now;
            DateTime thisMonday = thisDate.AddDays(firstDayOfWeekOffset);

            Day thisDay = new Day();
            thisDay.ToDos = new List<ToDo>();
            thisDay.Date = thisMonday.ToString("d", myCI);
            thisDay.Name = myCal.GetDayOfWeek(thisMonday).ToString();
            week.Days.Add(thisDay);

            for (int i = 1; i < 7; i++)
            {
                thisDay = new Day();
                thisDay.ToDos = new List<ToDo>();
                thisDay.Date = thisMonday.AddDays(i).ToString("d", myCI);
                thisDay.Name = myCal.GetDayOfWeek(thisMonday.AddDays(i)).ToString();
                week.Days.Add(thisDay);
            }
            
            return week;
        }

    }
}