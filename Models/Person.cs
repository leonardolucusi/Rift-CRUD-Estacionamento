using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Rift.Controllers;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Rift.Models
{
    public class Person
    {
        public enum Genders { Male, Female }
        
        public int Id { get; set; }

        [Remote(action: nameof(PeopleController.VerifyCPFExists), controller: "People")]
        [Required(AllowEmptyStrings = false)]
        [DisplayName("CPF")]
        public long Cpf { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayName("Nome Completo")]
        public string Name { get; set; }

        [DisplayName("Genero")]
        public Genders? Gender { get; set; }

        [DisplayName("Data de Nascimento")]
        public long? BirthDate { get; set; }

        [DisplayName("RG")]
        public long? Rg { get; set; }

        [DisplayName("Endereço")]
        public string? Address { get; set; }

        [DisplayName("Telefone/Cel")]
        public long? Phone { get; set; }

        [DisplayName("Email")]
        public string? Email { get; set; }

    }
}
