using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.Models
{
    public class EntityBase
    {
        public Guid ID { get; set; } = Guid.NewGuid();

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ClustedID { get; set; }
    }
}
