using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Proyecto2_JerryHurtado.API.Helpers
{
    /// <summary>
    /// Métodos de extensión para trabajar con enumeraciones y atributos de visualización.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Obtiene el valor del atributo <see cref="DisplayAttribute.Name"/> asociado a un valor de enumeración.
        /// </summary>
        /// <param name="enumValue">Valor de enumeración.</param>
        /// <returns>Nombre definido en el atributo <see cref="DisplayAttribute"/> o el nombre del enum si no se encuentra.</returns>
        public static string GetDisplayName(this Enum enumValue)
            => enumValue.GetType().GetMember(enumValue.ToString()).First().GetCustomAttribute<DisplayAttribute>()?.GetName()!;
    }
}