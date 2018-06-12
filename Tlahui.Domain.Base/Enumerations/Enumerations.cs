using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tlahui.Domain.Base.Enumerations
{
    //Añadir elementos en orden alfabético con el formato // # Char
    // Y despues el nombre de la clase principal que los utiliza

    // # R
    // --------------------

    // Rateable
    public enum PeriodRateType{
        //Un solo registro permanente para el tipo de datos, por ejemplo un contador 
        //para evitar hacer queries a la bse de datos para obtener conteos con poca variación
        Permanent=0
    }

}
