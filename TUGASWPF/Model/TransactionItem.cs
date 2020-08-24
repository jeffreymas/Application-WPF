﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUGASWPF.Model
{
    [Table("tb_m_transactionitem")]
    class TransactionItem
    {
        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; }
        public Transaction Transaction { get; set; }
        public Item Item { get; set; }

        public TransactionItem()
        {

        }
        public TransactionItem(int id, int quantity, Transaction transaction, Item item)
        {
            this.Id = id;
            this.Quantity = quantity;
            this.Transaction = transaction;
            this.Item = item;
        }
    }
}
