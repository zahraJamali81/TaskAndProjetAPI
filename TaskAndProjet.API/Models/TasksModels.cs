using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskAndProjet.API.Models
{
    public class TaskModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [MaxLength(200)]
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool? TaskStatus { get; set; }
        public string? TaskStatusTitle => this.TaskStatus == false ? "انجام نشده" : "انجام شده";
        public DateTime? TaskDeadline { get; set; }

        public int ProjectId { get; set; }
        public ProjectsModels Projects { get; set; }

        public int UserId { get; set; }
       // [ForeignKey("FullUserId")]
        public UserModels Users { get; set; }

    }
}
