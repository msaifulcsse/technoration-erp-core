using Entities.EntityModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.ViewModels
{
    public class VmStudentData
    {
        [Required(ErrorMessage = "StudentId is required")]
        public int StudentId { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Provide the student first name")]
        public string FirstName { get; set; }


        [StringLength(50)]
        [Required(ErrorMessage = "Provide the student last name")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Select the student department")]
        public int DepartmentId { get; set; }


        [Required(ErrorMessage = "Select the student batch")]
        public int BatchId { get; set; }


        [Required(ErrorMessage = "Select the student type")]
        public int StudentTypeId { get; set; }


        [StringLength(50)]
        [Required(ErrorMessage = "Provide the student registration number")]
        public string RegistrationNumber { get; set; }


        [Required(ErrorMessage = "Provide the student registration date")]
        public string RegistrationDate { get; set; }


        [Required(ErrorMessage = "Provide the student roll number")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid positive integer Number")]
        public int RollNumber { get; set; }


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

        public bool AppliedTermsAndCondition { get; set; }
    }

    public class VmStudentCustomData
    {
        public int StudentId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int DepartmentId { get; set; }

        public int BatchId { get; set; }

        public StudentType StudentType { get; set; }

        public string RegistrationNumber { get; set; }

        public string RegistrationDate { get; set; }

        public int RollNumber { get; set; }

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

    public class VmStudentRegistration
    {
        [StringLength(50)]
        [Required(ErrorMessage = "Provide Your First Name")]
        public string FirstName { get; set; }


        [StringLength(50)]
        [Required(ErrorMessage = "Provide Your Last Name")]
        public string LastName { get; set; }


        [StringLength(50)]
        [Required(ErrorMessage = "Provide Your Valid EmailAddress")]
        public string EmailAddress { get; set; }


        [StringLength(50)]
        [Required(ErrorMessage = "Provide Your Valid Contact Number")]
        public string ContactNumber { get; set; }


        [StringLength(50)]
        public string FatherName { get; set; }


        [StringLength(50)]
        public string MotherName { get; set; }


        [StringLength(50)]
        public string GurdianContactNumber { get; set; }


        [StringLength(10)]
        [Required(ErrorMessage = "Select Your Gender")]
        public string Gender { get; set; }


        [StringLength(20)]
        [Required(ErrorMessage = "Select Your Religion")]
        public string Religion { get; set; }


        [StringLength(20)]
        public string NationalID { get; set; }


        [StringLength(100)]
        [Required(ErrorMessage = "Provide Your Present Address")]
        public string PresentAddress { get; set; }


        [StringLength(100)]
        public string PermanentAddress { get; set; }


        public IFormFile ProfilePicture { get; set; }

        public bool AppliedTermsAndCondition { get; set; }
    }
}
