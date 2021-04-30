using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.API.Enums
{
    public enum DifficultyLevel
    {
        [Display(Name = "Easy")]
        Easy = 0, 
        [Display(Name = "Average difficulty")]
        AverageDifficulty = 1,
        [Display(Name = "Difficult")]
        Difficult = 2
    }
}
