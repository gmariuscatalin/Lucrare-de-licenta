using Microsoft.EntityFrameworkCore;
using NewBank2.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace NewBank2.ViewModel
{
    public class DashboardViewModel : BaseViewModel
    {
        // Private field for holding the collection of bank accounts
        private ObservableCollection<BankAccountControlViewModel> _bankAccounts;

        // Private field for holding the LoginContext object
        private readonly LoginContext _loginContext;

        // Public property for the collection of bank accounts
        public ObservableCollection<BankAccountControlViewModel> BankAccounts
        {
            get { return _bankAccounts; }
            set { SetProperty(ref _bankAccounts, value); }
        }
        public DashboardViewModel()
        {
            _loginContext = new LoginContext();
            BankAccounts = new ObservableCollection<BankAccountControlViewModel>();
            LoadBankAccounts();
        }
        /* This method retrieves the bank accounts from the database and adds
         them to the BankAccounts ObservableCollection.*/
        private void LoadBankAccounts()
        {
            BankAccounts.Clear();

            // Load the bank accounts from the database
            var accounts = _loginContext.Accounts
                .Include(a => a.Transactions)
                .Where(a => a.Username == StoreUserViewModel.Username)
                .ToList();
            // Add the retrieved accounts to the ObservableCollection (up to a maximum of 3 accounts)
            for (int i = 0; i < accounts.Count && i < 3; i++)
            {
                var bankAccountControlViewModel = new BankAccountControlViewModel(accounts[i])
                {
                    Index = i // Set the index property
                };

                BankAccounts.Add(bankAccountControlViewModel);
            }
        }
    }
}