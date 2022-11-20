using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Andals.API.Models
{
    public class Title
    {
        [Key]

        public int ID { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<Position> positions { get; set; }


    }
}
