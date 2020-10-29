using manahil.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace manahil.ViewModels
{
    public class DepositAndTransferViewModel:DepositAccount
    {
        public DepositAndTransferViewModel()
        {
            TransferAccounts = new List<TransferAccount>();
        }
        public List<TransferAccount> TransferAccounts { get; set; }
    }
}
