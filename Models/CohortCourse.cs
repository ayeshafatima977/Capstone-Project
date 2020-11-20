﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AZLearn.Models
{
    [Table(nameof(CohortCourse))]
    public class CohortCourse
    {
        //[Key]
        //[Column(TypeName = "int(10)")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int CohortCourseId { get; set; }

        //[Required]
        //[Column(TypeName = "int(10)")]
        [Key, Column(Order = 0)]
        public int CohortId { get; set; }

        //[Required]
        //[Column(TypeName = "int(10)")]
        [Key, Column(Order = 1)]
        public int CourseId { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }

        [Column(TypeName = "boolean")]
        public bool Archive { get; set; } = false;

        //[ForeignKey(nameof(CohortId))]
        [InverseProperty(nameof(Models.Cohort.CohortCourses))]
        public virtual Cohort Cohort { get; set; }

        //[ForeignKey(nameof(CourseId))]
        [InverseProperty(nameof(Models.Course.CohortCourses))]
        public virtual Course Course { get; set; }
    }
}
