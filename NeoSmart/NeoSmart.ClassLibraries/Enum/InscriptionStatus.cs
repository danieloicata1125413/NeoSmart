using System.ComponentModel;

namespace NeoSmart.ClassLibraries.Enum
{
    public enum InscriptionStatus
    {

        [Description("Matriculado")]
        Registered,

        [Description("Rechazado")]
        Refused,

        [Description("Confirmado")]
        Confirmed,

        [Description("Cancelado")]
        Cancelled,
    }
}
