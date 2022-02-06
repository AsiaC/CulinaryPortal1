using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CulinaryPortal.Domain.Enums
{
    public enum DifficultyLevel
    {
        [Display(Name = "Easy")]
        easy = 0,
        [Display(Name = "Average difficulty")]
        averageDifficulty = 1,
        [Display(Name = "Difficult")]
        difficult = 2
    }
}
