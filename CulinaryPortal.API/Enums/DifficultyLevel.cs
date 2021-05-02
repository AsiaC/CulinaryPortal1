using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.API.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
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
