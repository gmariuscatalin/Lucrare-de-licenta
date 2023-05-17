using Microsoft.EntityFrameworkCore;
using NewBank2.Commands;
using NewBank2.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NewBank2.ViewModel
{
    public class TransactionViewModel : BaseViewModel
    {
        private string _errorMessageUtility;
        public string ErrorMessageUtility
        {
            get => _errorMessageUtility;
            set
            {
                _errorMessageUtility = value;
                OnPropertyChanged();
            }
        }
        private string _errorMessageUser;
        public string ErrorMessageUser
        {
            get => _errorMessageUser;
            set
            {
                _errorMessageUser = value;
                OnPropertyChanged();
            }
        }
        private string _errorMessageAccounts;
        public string ErrorMessageAccounts
        {
            get => _errorMessageAccounts;
            set
            {
                _errorMessageAccounts = value;
                OnPropertyChanged();
            }
        }
        private Account _selectedToAccount;
        private decimal _exchangeRate;
        public ObservableCollection<string> UtilityCompanies { get; set; }
        private string _selectedUtilityCompany;
        private static readonly HttpClient httpClient = new HttpClient();
        private ObservableCollection<Account> _fromAccounts;
        private Account _selectedFromAccount;
        private string _toUsername;
        private string _amount;
        private decimal? _amountDecimal;

        /* This private method, GetExchangeRateAsync, makes an HTTP request
         to the specified API to obtain the exchange rate between two currencies.
         The method takes two parameters, 'fromCurrency' and 'toCurrency', which
         represent the currency codes to convert from and to, respectively.*/
        private async Task<decimal> GetExchangeRateAsync(string fromCurrency, string toCurrency)
        {
            string apiKey = "kwGoa58s29HK8xzUvLjZhbwQHKumrYPS";
            string apiUrl = $"https://api.apilayer.com/exchangerates_data/convert?amount=1&from={fromCurrency}&to={toCurrency}";

            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("apikey", apiKey);
            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(content);
                decimal rate = json["result"].Value<decimal>();
                return rate;
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}");
            }
        }
        public TransactionViewModel()
        {
            UtilityCompanies = new ObservableCollection<string>
    {
        "DIGI",
        "Enel",
        "Orange",
        "Vodafone"
    };
            PayUtilityCommand = new RelayCommand(ExecutePayUtility);
            TransferCommand = new RelayCommand(TransferMoney);
            TransferBetweenAccountsCommand = new RelayCommand(TransferBetweenAccounts);
            LoadFromAccounts();
        }

        public ICommand PayUtilityCommand { get; set; }
        public ICommand TransferCommand { get; }
        public ICommand TransferBetweenAccountsCommand { get; }

        public Account SelectedToAccount
        {
            get => _selectedToAccount;
            set => SetProperty(ref _selectedToAccount, value);
        }
        public string SelectedUtilityCompany
        {
            get => _selectedUtilityCompany;
            set => SetProperty(ref _selectedUtilityCompany, value);
        }
        public decimal ExchangeRate
        {
            get => _exchangeRate;
            set => SetProperty(ref _exchangeRate, value);
        }
        public ObservableCollection<Account> FromAccounts
        {
            get => _fromAccounts;
            set => SetProperty(ref _fromAccounts, value);
        }

        public Account SelectedFromAccount
        {
            get => _selectedFromAccount;
            set => SetProperty(ref _selectedFromAccount, value);
        }

        public string ToUsername
        {
            get => _toUsername;
            set => SetProperty(ref _toUsername, value);
        }

        public string Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                decimal.TryParse(value, out decimal result);
                SetProperty(ref _amountDecimal, result);
            }
        }
        public decimal? AmountDecimal
        {
            get { return _amountDecimal; }
            set { SetProperty(ref _amountDecimal, value); }
        }

        /* This private method, LoadFromAccounts, retrieves the accounts associated with the
         current user and stores them in an ObservableCollection of type Account.
         The method uses Entity Framework to access the database.*/
        private void LoadFromAccounts()
        {
            using var context = new LoginContext();
            var currentUser = context.Users
                                     .Include(u => u.Accounts)
                                     .FirstOrDefault(u => u.Username == StoreUserViewModel.Username);
            if (currentUser != null)
            {
                FromAccounts = new ObservableCollection<Account>(currentUser.Accounts);
            }
        }

        /* This private method, ExecutePayUtility, processes a utility payment from the
         selected user account to the selected utility company. It performs various checks
         to ensure the validity of the operation before updating the account balances and
         adding a new transaction record to the database.*/
        private void ExecutePayUtility(object obj)
        {
            if (!ValidateUtilityPayment())
            {
                return;
            }

            using var context = new LoginContext();
            var currentUser = context.Users.Include(u => u.Accounts).FirstOrDefault(u => u.Username == StoreUserViewModel.Username);
            var utilityUser = context.Users.Include(u => u.Accounts).FirstOrDefault(u => u.Username == SelectedUtilityCompany);

            if (utilityUser == null)
            {
                ErrorMessageUtility = "Utility company not found.";
                return;
            }

            var fromAccount = currentUser?.Accounts.FirstOrDefault(a => a.AccountId == SelectedFromAccount.AccountId);
            var utilityAccount = utilityUser?.Accounts.FirstOrDefault(a => a.Currency == SelectedFromAccount.Currency);

            if (!AreAccountsValid(fromAccount, utilityAccount))
            {
                return;
            }

            if (fromAccount.Balance < AmountDecimal)
            {
                ErrorMessageUtility = "Insufficient balance in the account.";
                return;
            }

            PerformUtilityPayment(fromAccount, utilityAccount, context);
        }

        private bool ValidateUtilityPayment()
        {
            if (SelectedFromAccount == null || SelectedUtilityCompany == null || AmountDecimal == null || AmountDecimal <= 0)
            {
                ErrorMessageUtility = "Please fill in all fields and ensure the amount is greater than 0.";
                return false;
            }
            return true;
        }

        private bool AreAccountsValid(Account fromAccount, Account utilityAccount)
        {
            if (fromAccount == null)
            {
                ErrorMessageUtility = "The selected account could not be found.";
                return false;
            }
            if (utilityAccount == null)
            {
                ErrorMessageUtility = "The utility company does not have an account with the specified currency.";
                return false;
            }
            if (fromAccount.Status != "Active" || utilityAccount.Status != "Active")
            {
                ErrorMessageUtility = "Your account must be 'Active' to proceed with the payment.";
                return false;
            }
            return true;
        }

        private void PerformUtilityPayment(Account fromAccount, Account utilityAccount, LoginContext context)
        {
            // Perform payment
            fromAccount.Balance -= AmountDecimal;
            utilityAccount.Balance += AmountDecimal;

            // Update last transaction date
            fromAccount.LastTransactionDate = DateTime.Now;
            utilityAccount.LastTransactionDate = DateTime.Now;

            // Save transactions
            var transaction = new Transaction
            {
                TransactionId = Guid.NewGuid(),
                AccountId = fromAccount.AccountId,
                CurrencyTr = fromAccount.Currency,
                AmountTr = -AmountDecimal,
                UsernameTr = SelectedUtilityCompany,
                Category = "Pay Utility",
                TransactionDate = DateTime.Now
            };

            context.Transactions.Add(transaction);

            // Save changes
            context.SaveChanges();

            ErrorMessageUtility = $"Utility payment of {Amount} {fromAccount.Currency} to {SelectedUtilityCompany} successful.";
        }

        /* This private method, TransferBetweenAccounts, processes a transfer between two
         different accounts belonging to the same user. It performs various checks
         to ensure the validity of the operation before updating the account balances
         and adding new transaction records to the database.*/
        private async void TransferBetweenAccounts(object obj)
        {
            if (!ValidateSelectedAccounts())
            {
                return;
            }

            ExchangeRate = await GetExchangeRateAsync(SelectedFromAccount.Currency, SelectedToAccount.Currency);
            if (ExchangeRate == null)
            {
                ErrorMessageAccounts = "Error fetching exchange rate.";
                return;
            }

            using var context = new LoginContext();
            var currentUser = context.Users.Include(u => u.Accounts).FirstOrDefault(u => u.Username == StoreUserViewModel.Username);

            var fromAccount = currentUser?.Accounts.FirstOrDefault(a => a.AccountId == SelectedFromAccount.AccountId);
            var toAccount = currentUser?.Accounts.FirstOrDefault(a => a.AccountId == SelectedToAccount.AccountId);

            if (!AreTransferAccountsValid(fromAccount, toAccount))
            {
                return;
            }

            PerformAccountTransfer(fromAccount, toAccount, context);
        }

        private bool ValidateSelectedAccounts()
        {
            if (SelectedFromAccount == null || SelectedToAccount == null)
            {
                ErrorMessageAccounts = "Please select both accounts.";
                return false;
            }
            if (SelectedFromAccount.AccountId == SelectedToAccount.AccountId)
            {
                ErrorMessageAccounts = "Cannot transfer between the same account.";
                return false;
            }
            return true;
        }

        private bool AreTransferAccountsValid(Account fromAccount, Account toAccount)
        {
            if (fromAccount == null || toAccount == null)
            {
                ErrorMessageAccounts = "One or both of the selected accounts could not be found.";
                return false;
            }
            if (fromAccount.Status != "Active" || toAccount.Status != "Active")
            {
                ErrorMessageAccounts = "Both accounts must have the status 'Active' to proceed with the payment.";
                return false;
            }
            if (AmountDecimal <= 0)
            {
                ErrorMessageAccounts = "Amount must be greater than 0.";
                return false;
            }
            if (fromAccount.Balance < AmountDecimal)
            {
                ErrorMessageAccounts = "Insufficient balance in the account.";
                return false;
            }
            return true;
        }

        private void PerformAccountTransfer(Account fromAccount, Account toAccount, LoginContext context)
        {
            // Perform transfer
            decimal? convertedAmount = AmountDecimal * ExchangeRate;
            fromAccount.Balance -= AmountDecimal;
            toAccount.Balance += convertedAmount;

            // Update last transaction dates
            fromAccount.LastTransactionDate = DateTime.Now;
            toAccount.LastTransactionDate = DateTime.Now;

            // Save transactions
            var outTransaction = new Transaction
            {
                TransactionId = Guid.NewGuid(),
                AccountId = fromAccount.AccountId,
                CurrencyTr = fromAccount.Currency,
                AmountTr = -AmountDecimal,
                UsernameTr = $"To account {toAccount.Currency}",
                Category = "Between Accounts",
                TransactionDate = DateTime.Now
            };

            var inTransaction = new Transaction
            {
                TransactionId = Guid.NewGuid(),
                AccountId = toAccount.AccountId,
                CurrencyTr = toAccount.Currency,
                AmountTr = convertedAmount,
                UsernameTr = $"From account {fromAccount.Currency}",
                Category = "Between Accounts",
                TransactionDate = DateTime.Now
            };

            context.Transactions.AddRange(new[] { outTransaction, inTransaction });

            // Save changes
            context.SaveChanges();

            ErrorMessageAccounts = "Transfer successful.";
        }

        /* This private method, TransferMoney, processes a transfer between two
         different users' accounts. It performs various checks to ensure the
         validity of the operation before updating the account balances and
         adding new transaction records to the database.*/
        private void TransferMoney(object obj)
        {
            using var context = new LoginContext();
            var currentUser = context.Users.Include(u => u.Accounts).FirstOrDefault(u => u.Username == StoreUserViewModel.Username);
            var toUser = context.Users.Include(u => u.Accounts).FirstOrDefault(u => u.Username == ToUsername);

            if (!ValidateUserTransfer(currentUser, toUser))
            {
                return;
            }

            var fromAccount = currentUser?.Accounts.FirstOrDefault(a => a.Currency == SelectedFromAccount.Currency);
            var toAccount = toUser.Accounts.FirstOrDefault(a => a.Currency == SelectedFromAccount.Currency);

            if (!AreTransferMoneyAccountsValid(fromAccount, toAccount))
            {
                return;
            }

            PerformUserTransfer(fromAccount, toAccount, context, currentUser.Username);
        }

        private bool ValidateUserTransfer(User currentUser, User toUser)
        {
            if (currentUser.Username == ToUsername)
            {
                ErrorMessageUser = "You cannot transfer money to yourself. Please enter a different email.";
                return false;
            }
            if (toUser == null)
            {
                ErrorMessageUser = "User not found.";
                return false;
            }
            return true;
        }

        private bool AreTransferMoneyAccountsValid(Account fromAccount, Account toAccount)
        {
            if (fromAccount == null)
            {
                ErrorMessageUtility = "The selected account could not be found.";
                return false;
            }
            if (toAccount == null)
            {
                ErrorMessageUser = $"The user does not have an account with the specified currency.";
                return false;
            }
            if (fromAccount.Status != "Active")
            {
                ErrorMessageUser = "Your account must be 'Active' to proceed with the payment.";
                return false;
            }
            if (toAccount.Status != "Active")
            {
                ErrorMessageUser = "The user you are trying to send money must have the account 'Active'.";
                return false;
            }
            if (AmountDecimal <= 0)
            {
                ErrorMessageUser = "Amount must be greater than 0.";
                return false;
            }
            if (fromAccount.Balance < AmountDecimal)
            {
                ErrorMessageUser = "Insufficient balance in the account.";
                return false;
            }
            return true;
        }

        private void PerformUserTransfer(Account fromAccount, Account toAccount, LoginContext context, string username)
        {
            // Perform transfer
            fromAccount.Balance -= AmountDecimal;
            toAccount.Balance += AmountDecimal;

            // Update last transaction dates
            fromAccount.LastTransactionDate = DateTime.Now;
            toAccount.LastTransactionDate = DateTime.Now;

            // Save transaction
            var outTransaction = new Transaction
            {
                TransactionId = Guid.NewGuid(),
                AccountId = fromAccount.AccountId,
                CurrencyTr = fromAccount.Currency,
                AmountTr = -AmountDecimal,
                UsernameTr = ToUsername,
                Category = "Between Users",
                TransactionDate = DateTime.Now
            };
            var inTransaction = new Transaction
            {
                TransactionId = Guid.NewGuid(),
                AccountId = toAccount.AccountId,
                CurrencyTr = toAccount.Currency,
                AmountTr = +AmountDecimal,
                UsernameTr = username,
                Category = "Between Users",
                TransactionDate = DateTime.Now
            };
            context.Transactions.AddRange(new[] { outTransaction, inTransaction });

            // Save changes
            context.SaveChanges();

            ErrorMessageUser = "Transfer successful.";
        }

        public void RadioButtonChecked(object obj)
        {
            // Reset the input values
            Amount = null;
            AmountDecimal = null;
            SelectedFromAccount = null;
            ToUsername = null;
            SelectedToAccount = null;
            SelectedUtilityCompany = null;
            ErrorMessageUser = null;
            ErrorMessageAccounts = null;
            ErrorMessageUtility = null;
        }
    }
}