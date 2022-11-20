using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Andals.API.Models
{
    public class Position
    {
        [Key]
        public int ID { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }

        
        public int TitleID { get; set; }
        [ForeignKey("TitleID")]
        [JsonIgnore]
        public virtual Title Title { get; set; }

    }
}
