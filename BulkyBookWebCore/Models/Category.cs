using System.ComponentModel.DataAnnotations;


namespace BulkyBookWebCore.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "İsim")]
        public string Name { get; set; }
        [Display(Name = "Görüntüleme sırası")]
        [Range(1, 100, ErrorMessage = "Görüntüleme sırası yalnızca 1 ile 100 arasında olmalıdır!!")]
        public int DisplayOrder { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

    }
}
