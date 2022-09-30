using DigitalPoliceSystem.ValidationAttri;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DigitalPoliceSystem.Models
{
    [Table("Complaints")]
    public class Complaint
    {
        /// <summary>
        /// Database generated Identity Id for ComplaintId
        /// </summary>
        /// <remarks>
        /// It is primary key for Complaint
        /// </remarks>
        [Key]                                                               //Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ComplaintId { get; set; }

        /// <summary>
        /// Description for the Complaint
        /// </summary>
        /// <remarks>
        /// Description is required field and must have string length between 10 and 200
        /// </remarks>
        [Display(Name = "Complaint Description")]
        [Required(ErrorMessage = "{0} cannot be empty.")]
        [StringLength(maximumLength: 200, MinimumLength = 10, ErrorMessage = "{0} should have minimum of {2} characters.")]
        //[Range(minimum: 10, maximum: 1000, ErrorMessage = "{0} has to be between {1} and {2}")]
        public string ComplaintDescription { get; set; }


        /// <summary>
        /// Date when the Incident happened 
        /// </summary>
        /// <remarks>
        ///Date must be within the last two year
        ///</remarks>
        [ValidationOneYear]
        [Display(Name = "Incident Date")]
        [Required(ErrorMessage = "{0} cannot be empty.")]
        //[Range(typeof(DateTime), "2022-01-01", "2022-09-29",
        //       ErrorMessage = "Please don't select a future dates")]
        public DateTime IncidentDate { get; set; }







        #region Navigation Properties to the Master Model - ComplaintCategory

        /// <summary>
        /// Foreign Key relationship with Complaint Category Id
        /// </summary>
        /// <remarks>
        ///Navigation Properties to the Master Model - ComplaintCategory
        ///</remarks>
        [Required]
        public int ComplaintCategoryId { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(Complaint.ComplaintCategoryId))]
        public ComplaintCategory ComplaintCategory { get; set; }


        #endregion

        #region Navigation Properties to the Master Model - PoliceStation

        /// <summary>
        /// Foreign Key relationship with Police Station Id
        /// </summary>
        /// <remarks>
        ///Navigation Properties to the Master Model - Police Station
        ///</remarks>
        [Required]
        public int PoliceStationId { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(Complaint.PoliceStationId))]
        public PoliceStation PoliceStation { get; set; }


        #endregion


        #region Navigation Properties to the Master Model - ComplaintState

        /// <summary>
        /// Foreign Key relationship with Complaint State Id
        /// </summary>
        /// <remarks>
        /// Navigation Properties to the Master Model - ComplaintState
        /// </remarks>
        [Required]
        public int ComplaintStateId { get; set; }

        [ForeignKey(nameof(Complaint.ComplaintStateId))]
        public ComplaintState ComplaintState { get; set; }


        #endregion

        #region Navigation Properties to the Master Model - UserProperty

        /// <summary>
        /// Foreign Key relationship with Complaint State Id
        /// </summary>
        /// <remarks>
        /// Navigation Properties to the Master Model - UserProperty
        /// </remarks>
        [Required]
        public string UserPropertyId { get; set; }

        [ForeignKey(nameof(Complaint.UserPropertyId))]
        public UserProperty UserProperty { get; set; }


        #endregion
    }
}
