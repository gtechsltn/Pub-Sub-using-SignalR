using Dapper;
using Microsoft.Data.SqlClient;
using SignalRClientAppCons.Data;
using SignalRClientAppCons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRClientAppCons.Repositorys
{
    public class MessageRepository
    {
        private readonly DapperContext _connectionString;

        public MessageRepository(DapperContext connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task SaveMessageAsync(Message message)
        {
            const string query = "INSERT INTO Messages (Content, Timestamp) VALUES (@Content, @Timestamp)";

            using var connection = _connectionString.CreateDbConnection();
            await connection.ExecuteAsync(query, new { message.Content, message.Timestamp });
        }
    }
}
