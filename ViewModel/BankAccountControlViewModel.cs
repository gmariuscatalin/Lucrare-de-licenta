using NewBank2.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace NewBank2.ViewModel
{
    public class BankAccountControlViewModel : BaseViewModel
    {
        private Account _account;

        // This property represents the bank account associated with the control.
        public Account Account
        {
            get { return _account; }
            set { SetProperty(ref _account, value); }
        }

        private ObservableCollection<Transaction> _transactions;

        // This property represents the list of transactions for the bank account.
        public ObservableCollection<Transaction> Transactions
        {
            get { return _transactions; }
            set { SetProperty(ref _transactions, value); }
        }

        private string _searchText;

        // This property represents the search text entered by the user.
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                SetProperty(ref _searchText, value);
                FilterTransactions();
            }
        }

        private ICollectionView _filteredTransactions;

        // This property represents the filtered list of transactions based on the search text.
        public ICollectionView FilteredTransactions
        {
            get { return _filteredTransactions; }
            set { SetProperty(ref _filteredTransactions, value); }
        }

        // This constructor initializes the bank account and loads the transactions.
        public BankAccountControlViewModel(Account account)
        {
            Account = account;
            LoadTransactions();
        }

        // This method loads the transactions for the bank account.
        private void LoadTransactions()
        {
            if (_account.Transactions != null)
            {
                // Sort the transactions by date and create an observable collection.
                var sortedTransactions = _account.Transactions.OrderByDescending(t => t.TransactionDate).ToList();
                Transactions = new ObservableCollection<Transaction>(sortedTransactions);

                // Create a collection view for the transactions and set the filter.
                FilteredTransactions = CollectionViewSource.GetDefaultView(Transactions);
                FilteredTransactions.Filter = FilterTransaction;
            }
        }

        // This method filters a transaction based on the search text.
        private bool FilterTransaction(object transaction)
        {
            var transactionItem = (Transaction)transaction;

            // If the search text is empty, include all transactions.
            if (string.IsNullOrEmpty(SearchText))
            {
                return true;
            }

            // Convert the search text and transaction properties to lowercase for case-insensitive comparison.
            var searchText = SearchText.ToLowerInvariant();
            var transactionDateText = transactionItem.TransactionDate.ToString().ToLowerInvariant();
            var amountText = transactionItem.AmountTr.ToString().ToLowerInvariant();

            // Check if the transaction properties contain the search text.
            return transactionItem.Category.ToLowerInvariant().Contains(searchText)
                   || transactionItem.UsernameTr.ToLowerInvariant().Contains(searchText)
                   || transactionDateText.Contains(searchText)
                   || amountText.Contains(searchText);
        }

        // This method refreshes the filtered transactions based on the search text.
        private void FilterTransactions()
        {
            FilteredTransactions.Refresh();
        }

        // This property represents the index of the bank account control.
        public int Index { get; set; }
    }
}