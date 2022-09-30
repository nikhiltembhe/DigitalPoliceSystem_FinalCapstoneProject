using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalPoliceSystem.Models
{
    [Table("UserProperties")]
    public class UserProperty
    {
        /// <summary>
        /// It is auto populated when new User Sign up, it's User Id gets store in this field
        /// </summary>
        /// <remarks>
        /// It is primary key for User Property
        /// </remarks>
        [Key]                                                               //Primary Key
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string UserPropertyId { get; set; }

        
        /// <summary>
        /// It is auto populated when new User Sign up, it's User Email Id gets store in this field
        /// </summary>
        public string UserPropertyEmailId { get; set; }


        /// <summary>
        /// Name field for User Property
        /// </summary>
        /// <remarks>
        /// This field cannnot be empty and will accept only characters but cannot have more than 60 characters
        /// </remarks>
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "{0} cannot be empty.")]
        [StringLength(60, ErrorMessage = "{0} cannot have more than {1} characters.")]
        [RegularExpression(@"^[A-Za-z]+[\s][A-Za-z]+$", ErrorMessage = "Use only characters!")]
        public string UserPropertyName { get; set; }

        /// <summary>
        /// Phone Number field for User Property
        /// </summary>
        /// <remarks>
        /// This field cannot be empty and will accept only 10 numeric digits
        /// </remarks>
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "{0} cannot be empty.")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Enter only 10 digits")]
        public string UserPropertyPhoneNumer { get; set; }

        /// <summary>
        /// Address field for User Property
        /// </summary>
        /// <remarks>
        /// This field cannot be empty and will accept only characters but cannot have more than 100 characters
        /// </remarks>
        [Display(Name = "Address")]
        [Required(ErrorMessage = "{0} cannot be empty")]
        [StringLength(100, ErrorMessage = "{0} cannot have more than {1} characters.")]
        public string UserPropertyAddress { get; set; }


        /// <summary>
        /// Navigation Properties to the Transaction Model - Complaint
        /// </summary>
        #region Navigation Properties to the Transaction Model - Complaint

        public ICollection<Complaint> Complaints { get; set; }

        #endregion

    }
}
