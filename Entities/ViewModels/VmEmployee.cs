using Entities.EntityModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.ViewModels
{
    public class VmEmployeeData
    {
        [Required(ErrorMessage = "Employee ID is required")]
        public int EmployeeId { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Provide the employee first name")]
        public string FirstName { get; set; }


        [StringLength(50)]
        [Required(ErrorMessage = "Provide the employee last name")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Select the employee department")]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Provide the employee designation")]
        public string Designation { get; set; }


        [Required(ErrorMessage = "Select the employee type")]
        public int EmployeeTypeId { get; set; }


        [StringLength(50)]
        [Required(ErrorMessage = "Provide the employee registration number")]
        public string RegistrationNumber { get; set; }


        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Required(ErrorMessage = "Please provide a password.")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [StringLength(100)]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again!")]
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessage = "Provide the employee joining date")]
        public string JoiningDate { get; set; }


        [StringLength(50)]
        public string EmailAddress { get; set; }


        [StringLength(50)]
        public string ContactNumber { get; set; }


        [StringLength(50)]
        public string FatherName { get; set; }


        [StringLength(50)]
        public string MotherName { get; set; }


        [StringLength(50)]
        public string GurdianContactNumber { get; set; }


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

        public IFormFile ProfilePicture { get; set; }
    }

    public class VmEmployeeCustomData
    {
        public int EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public EmployeeType EmployeeType { get; set; }

        public string RegistrationNumber { get; set; }

        public string JoiningDate { get; set; }

        public string EmailAddress { get; set; }

        public string ContactNumber { get; set; }

        public string FatherName { get; set; }

        public string MotherName { get; set; }

        public string GurdianContactNumber { get; set; }

        public string Gender { get; set; }

        public string Religion { get; set; }

        public string NationalID { get; set; }

        public string PresentAddress { get; set; }

        public string PermanentAddress { get; set; }

        public string ProfilePicture { get; set; }
    }
}
