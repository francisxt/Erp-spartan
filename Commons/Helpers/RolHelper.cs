using Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commons.Helpers
{
    public static class RolHelper
    {
        /// <summary>
        /// Verifica si el rol fue creado por la applicación
        /// </summary>
        /// <param name="rol">Nombre del rol</param>
        /// <returns>bool</returns>
        public static bool IsRolCreatedByApp(string rol)
        {
            if (!string.IsNullOrEmpty(rol))
            {
                return (rol.Equals(nameof(RolsAuthorization.Admin)) || rol.Equals(nameof(RolsAuthorization.Client)) || rol.Equals(nameof(RolsAuthorization.ClientsUser)));
            }
            return false;
        }
    }
}
