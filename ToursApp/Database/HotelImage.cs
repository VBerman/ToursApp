namespace ToursApp.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HotelImage")]
    public partial class HotelImage
    {
        public int Id { get; set; }

        public int HotelId { get; set; }

        [Required]
        public byte[] ImageSource { get; set; }

        public virtual Hotel Hotel { get; set; }
    }
}
