namespace CoffiNomad
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Category")]
    public partial class Category
    {
        public int CategoryID { get; set; }

        [Required]
        [StringLength(50)]
        public string Naam { get; set; }

        public virtual ICollection<Beoordeling> Beoordelingen { get; set; }
    }
}
