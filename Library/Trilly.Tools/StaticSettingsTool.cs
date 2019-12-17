using System;
using System.Collections.Generic;
using System.Text;

namespace Trilly.Tools
{
    public static class StaticSettingsTool
    {
        /// <summary>
        /// Zwraca adres bazy danych w sqlserver dla projektów testowych
        /// </summary>
        /// <returns></returns>
        public static string GetTrilloDbAddressForTest()
        {
            var server = "localhost,1845";
            var database = "TrillyDb";
            var user = "sa";
            var password = "79VqT2b#";

            return $"Server={server};Database={database};User={user};Password={password};MultipleActiveResultSets=true;";
        }
    }
}
