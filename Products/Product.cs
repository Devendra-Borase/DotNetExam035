using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Products.Models
{
    public class Product
    {   [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage ="Please Enter Name")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Please Enter Rate")]
        public decimal Rate { get; set; }

        [Required(ErrorMessage = "Please Enter Name")]
        [StringLength(200,ErrorMessage ="The {0} length  cannot exceeds{1} characters..")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please Enter Category Name")]
        public string CategoryName { get; set; }

        public static implicit operator SqlDataReader(Product v)
        {
            throw new NotImplementedException();
        }
    }
}