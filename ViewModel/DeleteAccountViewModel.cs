using Microsoft.EntityFrameworkCore;
using NewBank2.Commands;
using NewBank2.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace NewBank2.ViewModel
{
    public class DeleteAccountViewModel : BaseViewModel
    {
        public ICommand DeleteAccountCommand { get; }
        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }

        // Private field for holding the collection of accounts
        private ObservableCollection<Account> _accounts;
        public ObservableCollection<Account> Accounts
        {
            get => _accounts;
            set => SetProperty(ref _accounts, value);
        }
        public DeleteAccountViewModel()
        {
            DeleteAccountCommand = new RelayCommand(DeleteAccount);
            LoadAccounts();
        }

        // This method loads the accounts associated with the current user
        private void LoadAccounts()
        {
            using (var context = new LoginContext())
            {
                var user = context.Users.Include(u => u.Accounts).SingleOrDefault(u => u.Username == StoreUserViewModel.Username);
                Accounts = new ObservableCollection<Account>(user.Accounts);

                IsEuroAvailable = user.Accounts.Any(a => a.Currency == CurrencyType.EUR.ToString());
                IsDollarAvailable = user.Accounts.Any(a => a.Currency == CurrencyType.USD.ToString());
                IsRonAvailable = user.Accounts.Any(a => a.Currency == CurrencyType.RON.ToString());
            }
        }
        public enum CurrencyType
        {
            USD,
            EUR,
            RON
        }
        private bool _isRonAvailable = true;
        public bool IsRonAvailable
        {
            get => _isRonAvailable;
            set => SetProperty(ref _isRonAvailable, value);
        }
        private bool _isDollarSelected;
        public bool IsDollarSelected
        {
            get => _isDollarSelected;
            set
            {
                SetProperty(ref _isDollarSelected, value);
                if (value)
                {
                    SelectedCurrency = CurrencyType.USD;
                    IsEuroSelected = false;
                    IsRonSelected = false;
                }
            }
        }

        private bool _isEuroSelected;
        public bool IsEuroSelected
        {
            get => _isEuroSelected;
            set
            {
                SetProperty(ref _isEuroSelected, value);
                if (value)
                {
                    SelectedCurrency = CurrencyType.EUR;
                    IsDollarSelected = false;
                    IsRonSelected = false;
                }
            }
        }

        private bool _isRonSelected;
        public bool IsRonSelected
        {
            get => _isRonSelected;
            set
            {
                SetProperty(ref _isRonSelected, value);
                if (value)
                {
                    SelectedCurrency = CurrencyType.RON;
                    IsDollarSelected = false;
                    IsEuroSelected = false;
                }
            }
        }

        private CurrencyType _selectedCurrency;
        public CurrencyType SelectedCurrency
        {
            get => _selectedCurrency;
            set => SetProperty(ref _selectedCurrency, value);
        }

        private bool _isEuroAvailable = true;
        public bool IsEuroAvailable
        {
            get => _isEuroAvailable;
            set => SetProperty(ref _isEuroAvailable, value);
        }

        private bool _isDollarAvailable = true;
        public bool IsDollarAvailable
        {
            get => _isDollarAvailable;
            set => SetProperty(ref _isDollarAvailable, value);
        }

        // This method deletes the selected account if its balance is 0
        private void DeleteAccount(object parameter)
        {
            using (var context = new LoginContext())
            {
                var user = context.Users.Include(u => u.Accounts).SingleOrDefault(u => u.Username == StoreUserViewModel.Username);
                var accountToDelete = user.Accounts.FirstOrDefault(a => a.Currency == SelectedCurrency.ToString());

                if (accountToDelete != null)
                {
                    if (accountToDelete.Balance == 0)
                    {
                        context.Accounts.Remove(accountToDelete);
                        context.SaveChanges();

                        ErrorMessage=$"Account with {SelectedCurrency.ToString()} currency deleted successfully!";
                    }
                    else
                    {
                        ErrorMessage=$"Cannot delete account with {SelectedCurrency.ToString()} currency. Account balance must be 0.";
                    }
                }
                else
                {
                    ErrorMessage=$"An account with {SelectedCurrency.ToString()} currency does not exist for the current user.";
                }
            }
        }
    }
}