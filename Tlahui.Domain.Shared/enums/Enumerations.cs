using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tlahui.Domain.Shared
{


    public enum DataType
    {
        text = 1, integer_number = 10, decimal_number = 11, date = 20, time = 21, datetime = 22, boolean = 3
    }


    public enum ControlType
    {
        none=0,
        inputbox =1, text_area = 2,
        checkbox =3, radiobutton=4,
        select_fixed_elements = 10, select_localapi_elements = 11, select_localapi_autocomplete_elements = 12,
        rediobuttonlist =20,
        checkboxlist =30 ,
        datepicker =40,
        timepicker =41,
        datetimepicker =42
    }

    public enum DataSourceType {
     none=0,  server_session=1, user_input=10,  form_parameter=20, local_api=30
    }

     public enum BooleanDisplayType
    {
        none = 0, YesNo = 1, TrueFalse = 2, ZeroOne = 3
    }

   
}
