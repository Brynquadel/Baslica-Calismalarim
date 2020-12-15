
using System.ComponentModel.DataAnnotations;

namespace Exmark.Console
{
    public class Book:XEntity
    {
        public string Name { get; set; }
       
        public bool MethodType { get; set; }

        public string Name2 { get; set; }
        public decimal Name3 { get; set; }
        [Key]
        public int Id { get; set; }
    }}
