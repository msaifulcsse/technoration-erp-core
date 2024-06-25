using Entities.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.EntityModels
{
    public class Employee : BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }

        [StringLength(50)]
        public string EmailAddress { get; set; }

        [StringLength(50)]
        public string RegistrationNumber { get; set; }


        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string ContactNumber { get; set; }

        [StringLength(50)]
        public string FatherName { get; set; }

        [StringLength(50)]
        public string MotherName { get; set; }

        [StringLength(50)]
        public string GurdianNumber { get; set; }

        [StringLength(10)]
        public string Gender { get; set; }

        [StringLength(20)]
        public string Religion { get; set; }

        [StringLength(20)]
        public string NationalID { get; set; }

        [StringLength(100)]
        public string PresentAddress { get; set; }

        [StringLength(100)]
        public string PermanentAddress { get; set; }

        [StringLength(500)]
        public string ProfilePicture { get; set; }
        public DateTime? JoiningDate { get; set; }
        public decimal? MonthlySalary { get; set; }

        public EmployeeType Type { get; set; }

        public int? DepartmentId { get; set; }
        public virtual Department DepartmentInfo { get; set; }
    }

    public enum EmployeeType
    {
        Administrator = 1,
        HRManagement = 2,
        AccountManagement = 3,
        FacultyMember = 4,
        Supervisor = 5,
        Laboratorian = 6,
        Technician = 7,
        OfficeClerk = 8,
        SecurityGuard = 9,
        OtherStaff = 10
    }
}
