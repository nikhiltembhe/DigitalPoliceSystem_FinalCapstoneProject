using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalPoliceSystem.Models
{
    [Table("ComplaintStates")]
    public class ComplaintState
    {
        /// <summary>
        /// Database generated Identity Id for Complaint State Id
        /// </summary>
        /// <remarks>
        /// It is primary key for Complaint State
        /// </remarks>
        [Key]                                                               //Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ComplaintStateId { get; set; }


        /// <summary>
        /// Name for Complaint State
        /// </summary>
        /// <remarks>
        /// This field cannnot be empty and will accept only characters but cannot have more than 60 characters
        /// </remarks>
        [Display(Name = "Compliant Status")]
        [Required(ErrorMessage = "{0} cannot be empty.")]
        [StringLength(60, ErrorMessage = "{0} cannot have more than {1} characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Use only characters!")]
        public string CompliantStateName { get; set; }


        /// <summary>
        /// Navigation Properties to the Transaction Model - Complaint
        /// </summary>
        #region Navigation Properties to the Transaction Model - Complaint

        public ICollection<Complaint> Complaints { get; set; }

        #endregion
    }
}
