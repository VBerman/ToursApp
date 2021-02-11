namespace ToursApp.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HotelComment")]
    public partial class HotelComment
    {
        public int Id { get; set; }

        public int HotelId { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        [StringLength(100)]
        public string Author { get; set; }

        public DateTime CreationDate { get; set; }

        public virtual Hotel Hotel { get; set; }
    }
}
