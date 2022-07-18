using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualBank.Models
{
    public class DepositViewModel
    {
        public int AccountNumber { get; set; }
        public int SourceAccountNumber { get; set; }
        public int DestinationAccountNumber { get; set; }
        public float Balance { get; set; }
        public float Deposit { get; set; }
        public float Withdraw { get; set; }
        public float TranferAmount { get; set; }
    }
}
