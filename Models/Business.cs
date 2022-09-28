using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Rift.Controllers;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Rift.Models
{
    public class Business
    {

        public int Id { get; set; }

        [Remote(action: nameof(BusinessesController.VerifyCNPJExists), controller: "Businesses")]
        [Required(AllowEmptyStrings = false)]
        [DisplayName("CNPJ")]
        public long Cnpj { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayName("Razão Social")]
        public string SocialReason { get; set; }

        [DisplayName("Nome Fantasia")]
        public string? FantasyName { get; set; }

        [DisplayName("Endereço")]
        public string? Address { get; set; }

        [DisplayName("Telefone/Cel")]
        public long? Phone { get; set; }

        [DisplayName("Email")]
        public string? Email { get; set; }   
    }
}
