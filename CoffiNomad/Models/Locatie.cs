namespace CoffiNomad
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Locatie")]
    public partial class Locatie
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Locatie()
        {
            Caffees = new HashSet<Caffee>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LocatieID { get; set; }

        [StringLength(50)]
        public string Stad { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Caffee> Caffees { get; set; }
    }
}
