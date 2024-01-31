using System.ComponentModel.DataAnnotations;

namespace eShopAPI.Model
{

    public abstract class CategoryModelBase
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
       
    }
    public class CategoryViewModel : CategoryModelBase
    {
        public Guid Id { get; set; }
    }
    public class CategoryCreateModel : CategoryModelBase
    {
    }
    public class CategoryUpdateModel : CategoryModelBase
    {
        [Required]
        public Guid Id { get; set; }
    }
}
