
using System.ComponentModel.DataAnnotations;

namespace Exmark.Console
{
    public class Student:XEntity
    {
        public string Name { get; set; }
       
        public string Name2 { get; set; }
        public decimal Name3 { get; set; }
        [Key]
        public int Id { get; set; }
    }}
