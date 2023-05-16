using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using NewBank2.Commands;
using NewBank2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace NewBank2.ViewModel
{
    public class SettingsViewModel : BaseViewModel
    {
        public ICommand ClearUserDataCommand { get; }
        public ICommand ExportBankStatementCommand { get; }
        public SettingsViewModel()
        {
            ExportBankStatementCommand = new RelayCommand(_ => ExportBankStatement());
            ClearUserDataCommand = new RelayCommand(ExecuteClearUserDataCommand);
        }

        // This method executes the clear user data command, resetting the user data
        private void ExecuteClearUserDataCommand(object obj)
        {
            StoreUserViewModel.Id = Guid.Empty;
            StoreUserViewModel.Username = null;
            StoreUserViewModel.Password = null;
            StoreUserViewModel.Name = null;
            StoreUserViewModel.LastName = null;
            StoreUserViewModel.ProfilePicture = null;
        }

        // This method exports the bank statement, retrieving user accounts and transactions
        private void ExportBankStatement()
        {
            using (var context = new LoginContext())
            {
                var user = context.Users.Include(u => u.Accounts)
                                .ThenInclude(a => a.Transactions)
                                .SingleOrDefault(u => u.Username == StoreUserViewModel.Username);

                var accounts = user.Accounts;
                var transactions = accounts.SelectMany(a => a.Transactions);

                CreateBankStatement(accounts, transactions);
            }
        }

        private void CreateBankStatement(IEnumerable<Account> accounts, IEnumerable<Transaction> transactions)
        {
            // Create a new Excel workbook
            using (var workbook = new XLWorkbook())
            {
                // Add a worksheet to the workbook
                var worksheet = workbook.Worksheets.Add("Bank Statement");

                // Set the header titles
                worksheet.Cell("A1").Value = "Client Name";
                worksheet.Cell("B1").Value = "Type of Accounts";
                worksheet.Cell("C1").Value = "Balance";
                worksheet.Cell("D1").Value = "Status Accounts";
                worksheet.Cell("E1").Value = "Transaction Date";
                worksheet.Cell("F1").Value = "Currency";
                worksheet.Cell("G1").Value = "Transaction Balance";
                worksheet.Cell("H1").Value = "Category";
                worksheet.Cell("I1").Value = "Username";

                // Adjust column widths
                worksheet.Column("A").Width = 20;
                worksheet.Column("B").Width = 20;
                worksheet.Column("C").Width = 20;
                worksheet.Column("D").Width = 20;
                worksheet.Column("E").Width = 20;
                worksheet.Column("F").Width = 20;
                worksheet.Column("G").Width = 20;
                worksheet.Column("H").Width = 20;
                worksheet.Column("I").Width = 20;

                // Set the client name
                worksheet.Cell("A2").Value = $"{StoreUserViewModel.Name} {StoreUserViewModel.LastName}";

                int row = 2;

                // Iterate through each account
                foreach (var account in accounts)
                {
                    // Add the account information to the worksheet
                    worksheet.Cell(row, 2).Value = account.Currency;
                    worksheet.Cell(row, 3).Value = account.Balance;
                    worksheet.Cell(row, 4).Value = account.Status;

                    // Filter the transactions for the last 3 months and the specified categories
                    var filteredTransactions = transactions
                        .Where(t => t.AccountId == account.AccountId &&
                        t.TransactionDate >= DateTime.Now.AddMonths(-3))
                    .ToList();

                    // Iterate through each transaction
                    foreach (var transaction in filteredTransactions)
                    {
                        // Add the transaction information to the worksheet
                        worksheet.Cell(row, 5).Value = transaction.TransactionDate;
                        worksheet.Cell(row, 6).Value = transaction.CurrencyTr;
                        worksheet.Cell(row, 7).Value = transaction.AmountTr;
                        worksheet.Cell(row, 8).Value = transaction.Category;

                        // Add the UsernameTr value only for the specified categories
                        if (transaction.Category == "Pay Utility" || transaction.Category == "Between Users")
                        {
                            worksheet.Cell(row, 9).Value = transaction.UsernameTr;
                        }

                        row++;
                    }

                    row++;
                }

                // Save the workbook to the Downloads folder
                string downloadsFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
                string filePath = Path.Combine(downloadsFolderPath, $"BankStatement_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.xlsx");
                workbook.SaveAs(filePath);
                string recipientEmail = StoreUserViewModel.Username;
                SendBankStatementEmail(filePath, recipientEmail);
            }

        }
        private void SendBankStatementEmail(string filePath, string recipientEmail)
        {
            // Set up email configuration
            string smtpHost = "smtp.gmail.com";
            int smtpPort = 587;
            string fromEmail = "newbank1903@gmail.com";
            string fromEmailPassword = "brdrevqrhdxkhaot";
            string subject = "Bank Statement";
            string body = "Please find the attached bank statement.";

            // Create a new MimeMessage
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("NewBank", fromEmail));
            message.To.Add(new MailboxAddress("", recipientEmail));
            message.Subject = subject;

            // Create the email body with the Excel file as an attachment
            var bodyBuilder = new BodyBuilder
            {
                TextBody = body,
            };
            bodyBuilder.Attachments.Add(filePath);
            message.Body = bodyBuilder.ToMessageBody();

            // Send the email using MailKit
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect(smtpHost, smtpPort, false);
                client.Authenticate(fromEmail, fromEmailPassword);
                client.Send(message);
                client.Disconnect(true);
            }
        }

    }
}
