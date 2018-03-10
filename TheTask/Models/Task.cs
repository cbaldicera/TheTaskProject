using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TheTask.Models
{
    public class Task
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Título")]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Descrição")]
        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Data de Execução")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ExecutionDate { get; set; }
    }
}