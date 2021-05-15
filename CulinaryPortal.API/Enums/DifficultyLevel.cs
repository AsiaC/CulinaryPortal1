using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.API.Enums
{
    //[JsonConverter(typeof(StringEnumConverter))]
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
