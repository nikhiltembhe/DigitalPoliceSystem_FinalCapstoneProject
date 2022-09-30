using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace DigitalPoliceSystem.Models
{
    [Table("PoliceStations")]
    public class PoliceStation
    {
        /// <summary>
        /// Database generated Identity Id for Police Station Id
        /// </summary>
        /// <remarks>
        /// It is primary key for Police Station
        /// </remarks>
        [Key]                                                               //Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PoliceStationId { get; set; }

        /// <summary>
        /// Name for Police Station
        /// </summary>
        /// <remarks>
        /// This field cannnot be empty and will accept only characters but cannot have more than 60 characters
        /// </remarks>
        [Display(Name = "Police Station")]
        [Required(ErrorMessage = "{0} cannot be empty.")]
        [StringLength(60, ErrorMessage = "{0} cannot have more than {1} characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Use only characters!")]
        public string PoliceStationName { get; set; }

        /// <summary>
        /// Phone Number field for the Police Station
        /// </summary>
        /// <remarks>
        /// This field cannot be empty and will accept only 10 numeric digits
        /// </remarks>
        [Display(Name = "Police Station Phone")]
        [Required(ErrorMessage = "{0} cannot be empty.")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Enter only 10 digits")]
        public string PoliceStationPhNo { get; set; }

        /// <summary>
        /// Address field for the Police Station 
        /// </summary>
        /// <remarks>
        /// This field cannot be empty and will accept only characters but cannot have more than 100 characters
        /// </remarks>
        [Display(Name = "Police Station Address")]
        [Required(ErrorMessage = "{0} cannot be empty")]
        [StringLength(100, ErrorMessage = "{0} cannot have more than {1} characters.")]
        public string PoliceStationAddress { get; set; }


        /// <summary>
        /// Navigation Properties to the Transaction Model - Complaint
        /// </summary>
        #region Navigation Properties to the Transaction Model - Complaint

        public ICollection<Complaint> Complaints { get; set; }

        #endregion
    }
}
