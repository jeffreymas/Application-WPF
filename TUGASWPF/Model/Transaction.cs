using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUGASWPF.Model
{
    [Table("tb_m_transaction")]
    class Transaction
    {
        [Key]
        public string Id { get; set; }
        public DateTime OrderDate { get; set; }

        public Transaction()
        {

        }
        public Transaction(string id, DateTime orderDate)
        {
            this.Id = id;
            this.OrderDate = orderDate;
        }
    }
}
