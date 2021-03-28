using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.API.Enums
{
    public enum PreparationTime
    {
        [Display(Name = "< 15 min")]
        lessThan15 = 0,
        [Display(Name = "< 30 min")]
        lessThan30 = 1,
        [Display(Name = "< 60 min")]
        lessThan60 = 2,
        [Display(Name = "> 60 min")]
        moreThan60 = 3
    }
}
