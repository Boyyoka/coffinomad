namespace CoffiNomad
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Caffee")]
    public partial class Caffee
    {
        public int CaffeeID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Straat { get; set; }

        public int LocatieID { get; set; }

        public Locatie Locatie { get; set; }

        public ICollection<Beoordeling> Beoordelingen { get; set; }
    }
}
