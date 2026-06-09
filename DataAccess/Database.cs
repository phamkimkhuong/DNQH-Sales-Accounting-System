using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DNQH_KeToanBanHang.DataAccess
{
    public static class Database
    {
        private static string connectionString;

        static Database()
        {
            ConnectionStringSettings setting = ConfigurationManager.ConnectionStrings["DNQH_KeToanBanHang"];
            connectionString = setting != null ? setting.ConnectionString : null;
        }

        public static string ConnectionString
        {
            get
            {
                EnsureConfigured();
                return connectionString;
            }
        }

        internal static void ConfigureForTests(string testConnectionString)
        {
            if (string.IsNullOrWhiteSpace(testConnectionString))
                throw new ConfigurationErrorsException("Thiếu DNQH_TEST_CONNECTION_STRING.");

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(testConnectionString);
            string databaseName = builder.InitialCatalog ?? string.Empty;
            if (!databaseName.EndsWith("_Test", StringComparison.OrdinalIgnoreCase))
                throw new ConfigurationErrorsException(
                    "Connection string kiểm thử phải trỏ tới database có tên kết thúc bằng '_Test'.");

            connectionString = builder.ConnectionString;
        }

        public static SqlConnection GetConnection()
        {
            EnsureConfigured();
            return new SqlConnection(connectionString);
        }

        private static void EnsureConfigured()
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ConfigurationErrorsException(
                    "Thiếu connection string 'DNQH_KeToanBanHang' trong App.config.");
        }

        public static DataTable ExecuteQuery(
            string query,
            params SqlParameter[] parameters)
        {
            DataTable table = new DataTable();

            using (SqlConnection connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                if (parameters != null && parameters.Length > 0)
                    command.Parameters.AddRange(parameters);

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(table);
                }
            }

            return table;
        }

        public static int ExecuteNonQuery(
            string query,
            params SqlParameter[] parameters)
        {
            using (SqlConnection connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                if (parameters != null && parameters.Length > 0)
                    command.Parameters.AddRange(parameters);

                connection.Open();
                return command.ExecuteNonQuery();
            }
        }

        public static object ExecuteScalar(
            string query,
            params SqlParameter[] parameters)
        {
            using (SqlConnection connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                if (parameters != null && parameters.Length > 0)
                    command.Parameters.AddRange(parameters);

                connection.Open();
                return command.ExecuteScalar();
            }
        }

        public static void ExecuteTransaction(Action<SqlTransaction> action)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        action(transaction);
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public static T ExecuteTransaction<T>(Func<SqlTransaction, T> func)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        T result = func(transaction);
                        transaction.Commit();
                        return result;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public static bool TestConnection(out string message)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT @@VERSION", connection))
                    {
                        object result = command.ExecuteScalar();
                        message = result != null ? result.ToString() : "OK";
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }
    }
}
