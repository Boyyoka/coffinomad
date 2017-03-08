using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace CoffiNomad
{
    [Table("Beoordeling")]
    public partial class Beoordeling
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CategoryID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CaffeeID { get; set; }

        [Key]
        [Column("Beoordeling", Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Beoordeling1 { get; set; }

        public Category Category { get; set; }

        public Caffee Caffee { get; set; }


    }
}