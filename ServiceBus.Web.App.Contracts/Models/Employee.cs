using System.ComponentModel.DataAnnotations;

namespace ServiceBus.Web.App.Contracts.Models
{
    /// <summary>
    /// Employee Domain Model
    /// </summary>
    public class Employee
    {
        [Required(AllowEmptyStrings = false)]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(100)]
        public string LastName { get; set; }
    }
}
