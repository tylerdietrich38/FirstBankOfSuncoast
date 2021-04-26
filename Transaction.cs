using System;

namespace FirstBankOfSuncoast
{
    class Transaction
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public string AccountType { get; set; }
        public string TransactionType { get; set; }

        // public string BalanceList()
        // {
        //     var newBalanceList = $""
        // }

        public string TransactionList()
        {
            var newTransactionList = $"Here is the {TransactionType} of ${Amount} from your {AccountType} account . ";
            return newTransactionList;
        }

    }
}
