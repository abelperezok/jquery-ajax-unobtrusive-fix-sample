using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace jquery_ajax_unobtrusive_fix_sample.Models
{
    public class SimpleModel
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public IFormFile File { get; set; }
    }
}