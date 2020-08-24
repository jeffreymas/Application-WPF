using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUGASWPF.Model
{
    [Table("tb_m_supplier")]
    class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? JoinDate { get; set; }

        public Supplier()
        {

        }

        public Supplier(int id, string name, DateTime joinDate)
        {
            this.Id = id;
            this.Name = name;
            this.JoinDate = joinDate;
        }
    }
}
