using System.Security.Cryptography.X509Certificates;

namespace DB
{
    public static class Conexao
    {
        private static readonly string server = "RAFA\\SQLEXPRESS";
        private static readonly string database = "SoN_Financeiro";
        private static readonly string username = "sa";
        private static readonly string password = "TESTE123";

        public static string getStringConnection() => $"Server={server};Database={database};User Id={username};Password={password}";
    }
}
