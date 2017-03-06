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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CaffeeID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public byte[] Straat { get; set; }

        public int LocatieID { get; set; }

        public virtual Locatie Locatie { get; set; }
    }
}
