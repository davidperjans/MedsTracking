using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class MedicineReminder : BaseEntity
    {
        public Guid UserId { get; set; }
        public string Message { get; set; }
        public DateTime ReminderTime { get; set; }
        public bool IsCompleted { get; set; }
        public Guid MedicineId { get; set; }
        public Medicine Medicine { get; set; }
    }
}
