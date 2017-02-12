namespace CompanyMng.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Employee")]
    public partial class Employee
    {
        public int EmployeeID { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(150)]
        [EmailAddress]
        
        public string Email { get; set; }

        [Column(TypeName = "date")]
        public DateTime BirthDate { get; set; }

        public int DepartmentID { get; set; }

        public virtual Department Department { get; set; }
    }
}
