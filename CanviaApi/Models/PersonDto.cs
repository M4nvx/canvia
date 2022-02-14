using Canvia.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CanviaApi.Models
{
    public class PersonDto
    {
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        public DateTime? Birthdate { get; set; }

        public DateTime? Deathdate { get; set; }

        [Required]
        public ESex Sex { get; set; }
    }
}