using System.ComponentModel.DataAnnotations;

namespace Proyecto2_JerryHurtado.API.Models.Enums
{
    public enum PetSpecies
    {
        [Display(Name = "Caballo")]
        Horse = 1,

        [Display(Name = "Perro")]
        Dog,

        [Display(Name = "Gato")]
        Cat,

        [Display(Name = "Pez")]
        Fish,

        [Display(Name = "Cabra")]
        Goat,

        [Display(Name = "Conejo")]
        Rabbit,

        [Display(Name = "Vaca")]
        Cow,

        [Display(Name = "Cerdo")]
        Pig,

        [Display(Name = "Roedor")]
        Rodent,

        [Display(Name = "Serpiente")]
        Snake,

        [Display(Name = "Otro")]
        Other
    }
}