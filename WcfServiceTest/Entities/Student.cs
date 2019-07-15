namespace Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Student")]
    public partial class Student
    {
        [Key]
        public int NControl { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(40)]
        public string Topic { get; set; }
    }
}
