﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.API.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(30)]
        public string Username { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        [MaxLength(20)]
        public string Email { get; set; }

        public Cookbook Cookbook { get; set; }

        public IList<Recipe> Recipes { get; set; }
        //string/ zachashowane/password
        //string / enum / role
    }
}
