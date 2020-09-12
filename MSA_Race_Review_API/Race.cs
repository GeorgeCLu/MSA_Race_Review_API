using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MSA_Race_Review_API
{
    public class Race
    {
        public Race()
        {
            this.Review = new HashSet<Review>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int raceId { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The raceName value cannot exceed 50 characters. ")]  
        public string raceName { get; set; }
        [StringLength(50, ErrorMessage = "The championship value cannot exceed 50 characters. ")]  
        public string championship { get; set; }
        [Required]
        public int year { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The track value cannot exceed 50 characters. ")]  
        public string track { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The location value cannot exceed 50 characters. ")]  
        public string location { get; set; }
        public int averageScore { get; set; }
        public int scoreSum { get; set; }
        public int totalReviews { get; set; }
        // public ICollection<Review> Review { get; set; }
        public virtual ICollection<Review> Review { get; set; }
    }
}
