using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalPoliceSystem.Models
{
    [Table("ComplaintCategories")]
    public class ComplaintCategory
    {

        /// <summary>
        /// Database generated Identity Id for Complaint Category Id
        /// </summary>
        /// <remarks>
        /// It is primary key for Complaint Category
        /// </remarks>
        [Key]                                                               //Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ComplaintCategoryId { get; set; }

        /// <summary>
        /// Name for Complaint Category
        /// </summary>
        /// <remarks>
        /// This field cannnot be empty and will accept only characters but cannot have more than 60 characters
        /// </remarks>
        [Display(Name = "Compliant Category")]
        [Required(ErrorMessage = "{0} cannot be empty.")]
        [StringLength(60, ErrorMessage = "{0} cannot have more than {1} characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Use only characters!")]
        public string CompliantCategoryName { get; set; }

        /// <summary>
        /// Navigation Properties to the Transaction Model - Complaint
        /// </summary>
        #region Navigation Properties to the Transaction Model - Complaint

        public ICollection<Complaint> Complaints { get; set; }

        #endregion

    }
}
