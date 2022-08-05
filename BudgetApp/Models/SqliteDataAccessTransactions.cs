﻿using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace BudgetApp.Models
{
    internal class TransactionsDataAccess : SqliteDataAccess
    {
        internal static List<Transaction> LoadTransactions()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString("transactions")))
            {
                var output = cnn.Query<Transaction>("SELECT * FROM Transactions", new DynamicParameters());
                return output.ToList();
            }
        }

        internal static void SaveTransaction(Transaction transaction)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString("transactions")))
            {
                if (transaction.Category != "Ignore")
                {
                    cnn.Execute("INSERT INTO Transactions (Date, Description, Value, Category) VALUES (@Date, @Description, @Value, @Category)", transaction);
                }
            }
        }

        internal static void UpdateTransaction(Transaction transaction)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString("transactions")))
            {
                if (transaction.Category != "Ignore")
                {
                    cnn.Execute("UPDATE Transactions SET Category = @Category WHERE ID = @ID", transaction);
                }
            }
        }

        internal static void DeleteTransaction(Transaction transaction)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString("transactions")))
            {
                cnn.Execute("DELETE FROM Transactions WHERE ID = @ID", transaction);
            }
        }
    }
}
