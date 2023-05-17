using Microsoft.EntityFrameworkCore;
using NewBank2.Commands;
using NewBank2.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace NewBank2.ViewModel
{
    public class CreateAccountViewModel : BaseViewModel
    {
        public CreateAccountViewModel()
        {
            CreateAccountCommand = new RelayCommand(CreateAccount);
            LoadAccounts();
        }
        /* This method retrieves the current user's accounts and updates
         the properties for available currency types accordingly.*/
        private void LoadAccounts()
        {
            using (var context = new LoginContext())
            {
                var user = context.Users.Include(u => u.Accounts).SingleOrDefault(u => u.Username == StoreUserViewModel.Username);

                IsDollarAvailable = !user.Accounts.Any(a => a.Currency == CurrencyType.USD.ToString());
                IsEuroAvailable = !user.Accounts.Any(a => a.Currency == CurrencyType.EUR.ToString());
                IsRonAvailable = !user.Accounts.Any(a => a.Currency == CurrencyType.RON.ToString());
            }
        }
        // Enumeration for available currency types
        public enum CurrencyType
        {
            USD,
            EUR,
            RON
        }
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

        public ICommand CreateAccountCommand { get; }

        // This method handles the creation of a new account for the selected currency.
        private void CreateAccount(object parameter)
        {
            using (var context = new LoginContext())
            {
                var user = context.Users.Include(u => u.Accounts).SingleOrDefault(u => u.Username == StoreUserViewModel.Username);
                var existingAccount = user.Accounts.FirstOrDefault(a => a.Currency == SelectedCurrency.ToString());
                // If an account with the selected currency already exists, show an error message.
                if (existingAccount != null)
                {
                    ErrorMessage = $"An account with {SelectedCurrency.ToString()} currency already exists for the current user.";
                    return;
                }

                // Create a new account with the selected currency and initial values
                var newAccount = new Account
                {
                    AccountId = Guid.NewGuid(),
                    Username = StoreUserViewModel.Username,
                    Balance = 0,
                    Currency = SelectedCurrency.ToString(),
                    Status = "Active",
                    DateOpened = DateTime.Now,
                    LastTransactionDate = null
                };

                // Add the new account to the current user's list of accounts
                user.Accounts.Add(newAccount);

                // Save changes to the database
                context.SaveChanges();

                ErrorMessage = $"Account with {SelectedCurrency.ToString()} currency created successfully!";
            }
        }
    }
}