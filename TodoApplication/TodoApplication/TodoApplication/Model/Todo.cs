using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApplication.Model
{
    public class Todo
    {
        private DateOnly _date;
        public string EmployeeId { get; set; }
        public string TaskId { get; set; }
        public string TaskHeading { get; set; }
        public string Description { get; set; }

        public DateOnly TargetDate
        {
            get => _date;
            set
            {
                if (value > DateOnly.FromDateTime(DateTime.Now))
                {
                    _date = value;
                }
                else
                {
                    throw new ArgumentException("Target date must be a future date.", nameof(value));
                }
            }
        }
        public Recurrence TaskRecurrence { get; set; }
        public List<Todo> Completed { get; set; }
        public DateOnly RecurrenceDate { get; set; }
    }
}
