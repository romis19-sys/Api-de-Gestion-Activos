// creamos la carpeta para meter la validacion de la cuota de los pagos
// cada pago, tiene que ser entre 1-5 dias del mes corriente.
using System.ComponentModel.DataAnnotations;

namespace GestionActivos.Application.Validations
{
    public class PrimerosCincoDiasAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateOnly fecha)
            {
                if (fecha.Day < 1 || fecha.Day > 5)
                {
                    return new ValidationResult("La fecha de cuota debe estar dentro de los primeros 5 días del mes.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
