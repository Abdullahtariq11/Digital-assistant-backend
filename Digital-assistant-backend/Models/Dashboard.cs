using System.ComponentModel.DataAnnotations;

namespace Digital_assistant_backend.Models
{
    public class Dashboard
    {
        [Key] 
        public int Id { get; set; }
        public int CompletedTask { get; set; }
        public int PendingTask { get; set; }
        public int TotalTask { get; set; } = 0;
        public int CompletedProject { get; set; }
        public int PendingProject { get; set; }
        public int TotalProject { get; set; }
        public int HoldTask { get; set; }
        public int HoldProject { get; set; }

        //relationship with user
        public int UserId { get; set; }
        public  User? User { get; set; }
    }
}
