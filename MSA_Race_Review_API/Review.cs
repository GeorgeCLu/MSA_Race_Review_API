using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MSA_Race_Review_API
{
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int reviewId { get; set; }
        [Required]
        [StringLength(255)]
        public string reviewText { get; set; }
        [Required]
        public int? reviewScore { get; set; }
        [Required]
        [StringLength(20)]
        public string reviewerName { get; set; }
        public int upvotes { get; set; }
        public DateTime timeCreated { get; set; }
        [Required]
        [ForeignKey("raceId")]
        public int raceId { get; set; }
    }
}
