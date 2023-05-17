using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using NewBank2.Commands;
using NewBank2.Models;
using DocumentFormat.OpenXml.InkML;

namespace NewBank2.ViewModel
{
    public class ActivateDeactivateAccountViewModel : BaseViewModel
    {
        // Define the ToggleAccountStatusCommand as an ICommand
        public ICommand ToggleAccountStatusCommand { get; }

        /* This method retrieves accounts from the database and updates the IsDollarAvailable,
         IsEuroAvailable, and IsRonAvailable properties.*/
        private void LoadAccounts()
        {
            using (var context = new LoginContext())
            {
                var user = context.Users.Include(u => u.Accounts).SingleOrDefault(u => u.Username == StoreUserViewModel.Username);

                IsDollarAvailable = user.Accounts.Any(a => a.Currency == CurrencyType.USD.ToString());
                IsEuroAvailable = user.Accounts.Any(a => a.Currency == CurrencyType.EUR.ToString());
                IsRonAvailable = user.Accounts.Any(a => a.Currency == CurrencyType.RON.ToString());
            }
        }

        public ActivateDeactivateAccountViewModel()
        {
            ToggleAccountStatusCommand = new RelayCommand(ToggleAccountStatus);
            LoadAccounts();
        }
        // Enum for different currency types.
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

        // More properties for the ViewModel, including currency availability and selection.
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
            set
            {
                if (SetProperty(ref _selectedCurrency, value))
                {
                    LoadAccounts();
                }
            }
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
        /* This method is responsible for toggling the account status (active or closed)
         and saving the changes to the database.*/
        private void ToggleAccountStatus(object parameter)
        {
            using (var context = new LoginContext())
            {
                var user = context.Users.Include(u => u.Accounts).SingleOrDefault(u => u.Username == StoreUserViewModel.Username);
                var account = user.Accounts.FirstOrDefault(a => a.Currency == SelectedCurrency.ToString());

                if (account == null)
                {
                    ErrorMessage = "The selected account could not be found.";
                    return;
                }

                // Toggle the account status
                account.Status = account.Status == "Active" ? "Closed" : "Active";

                // Save changes to the database
                context.SaveChanges();

                ErrorMessage = $"Account status changed to {account.Status} successfully!";
            }
        }
    }
}