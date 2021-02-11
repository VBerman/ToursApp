namespace ToursApp.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Tour")]
    public partial class Tour
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tour()
        {
            Hotels = new HashSet<Hotel>();
            Types = new HashSet<Type>();
        }

        public int Id { get; set; }

        public int TicketCount { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        public byte[] ImagePreview { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public bool IsActual { get; set; }
        //"prop"+tab+tab
        public ActualInfo ActualInfo
        {
            get 
            {
                
                // (Условие 1=2) ? "Истина" : "Ложь"
                var actualInfo = new ActualInfo() { Color = (IsActual) ? "Green" : "Red", Text = (IsActual) ? "Актуален" : "Не актуален" };
                //var actualInfo2 = new ActualInfo() { Color = (IsActual) ? "Green" : "Red", Text = (IsActual) ? "Актуален" : "Не актуален" };
                
                return actualInfo;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Hotel> Hotels { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Type> Types { get; set; }
    }

    public class ActualInfo
    {
        public string Text { get; set; }
        public string Color { get; set; }
    }
}
