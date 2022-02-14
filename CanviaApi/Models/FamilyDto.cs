using Canvia.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CanviaApi.Models
{
    public class FamilyDto
    {
        public int Id { get; set; }

        [Required]
        public int SourcePersonId { get; set; }

        public PersonDto SourcePerson { get; set; }

        [Required]
        public int TargetPersonId { get; set; }

        public PersonDto TargetPerson { get; set; }

        [Required]
        public ERelation Relation { get; set; }
    }
}