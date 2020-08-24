using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUGASWPF.Models
{
    [Table("tb_m_supplier")]
    class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? JoinDate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Newpass { get; set; }

        public Supplier()
        {

        }
        public Supplier( string name)
        { 
            this.Name = name;
        }
        public Supplier(string name, DateTime? joinDate)
        { 
            this.Name = name;
            this.JoinDate = joinDate;
        }
        public int getId()
        {
            return this.Id;
        }
        public Supplier (string email, string password)
        {

        }
    }
    
}
