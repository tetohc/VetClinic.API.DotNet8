using System.ComponentModel.DataAnnotations;

namespace Proyecto2_JerryHurtado.API.Models.Enums
{
    public enum PetProcedureStatus
    {
        [Display(Name = "En proceso")]
        InProgress = 1,

        [Display(Name = "Facturado")]
        Billed,

        [Display(Name = "Agendado")]
        Scheduled
    }
}