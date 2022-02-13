﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HolidayApi.Models
{
    public class HolidayType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HolidayTypeId { get; set; }
        public string Name { get; set; }
        public string CountryCode { get; set; }
        public Country Country { get; set; }
    }
}
