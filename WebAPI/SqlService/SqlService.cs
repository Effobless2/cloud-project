using CloudAPI.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SqlService
{
    public class SqlServiceOptions
    {
        public string ConnectionString { get; set; }
    }

    public interface ISqlService
    {
        bool HasColumn(SqlDataReader Reader, string ColumnName);
        Task<SqlConnection> GetConnection();
        void CloseCOnnection(SqlConnection connection);

        Task<TodoElement> Get(int id);
        Task<List<TodoElement>> Get();
        Task Check(int id);
        Task<int> Create(TodoElement todo);
        Task Delete(int id);
        Task Edit(TodoElement todo);
    }

    public class SqlService : ISqlService
    {
        private string _connectionString;

        public SqlService(IOptions<SqlServiceOptions> options)
        {
            _connectionString = options.Value.ConnectionString;
        }
        public async Task<SqlConnection> GetConnection()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            return connection;

        }
        public bool HasColumn(SqlDataReader Reader, string ColumnName)
        {
            foreach (System.Data.DataRow row in Reader.GetSchemaTable().Rows)
            {
                if (row["ColumnName"].ToString() == ColumnName)
                {
                    return true;
                }
            }
            return false;
        }
        public void CloseCOnnection(SqlConnection connection)
        {
            connection.Close();
        }

        public async Task Edit(TodoElement todo)
        {
            using (SqlConnection connection = await GetConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "UPDATE Todo SET Done = @checked, TodoName = @name, Content = @content  WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", todo.Id);
                    cmd.Parameters.AddWithValue("@name", todo.Name);
                    cmd.Parameters.AddWithValue("@checked", todo.Checked);
                    cmd.Parameters.AddWithValue("@content", todo.Content);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        
        public async Task Check(int id)
        {
            using (SqlConnection connection = await GetConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "UPDATE Todo SET Done = Done ^ 1 WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public async Task<int> Create(TodoElement todo)
        {
            using (SqlConnection connection = await GetConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "INSERT INTO Todo(TodoName, Done, Content) VALUES (@Name, @Checked, @Content)";
                    cmd.Parameters.AddWithValue("@Name", todo.Name);
                    cmd.Parameters.AddWithValue("@Checked", todo.Checked);
                    cmd.Parameters.AddWithValue("@Content", todo.Content);
                    cmd.ExecuteNonQuery();
                }
            }
            return 0;
        }

        public async Task Delete(int id)
        {
            using (SqlConnection connection = await GetConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "DELETE Todo WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public async Task<TodoElement> Get(int id)
        {
            TodoElement result = new TodoElement();
            using (SqlConnection connection = await GetConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "SELECT Id, TodoName, Content, Done FROM Todo WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            FullFill(result, reader);
                        }
                    }
                }
            }
            return result;
        }

        public async Task<List<TodoElement>> Get()
        {
            List<TodoElement> result = new List<TodoElement>();
            using(SqlConnection connection = await GetConnection())
            {
                using(SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "SELECT Id, TodoName, Content, Done FROM Todo";
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TodoElement todo = new TodoElement();
                            FullFill(todo, reader);
                            result.Add(todo);
                        }
                    }
                }
            }
            return result;
        }

        private void FullFill(TodoElement result, SqlDataReader reader)
        {
            result.Id = HasColumn(reader, "Id") ? Convert.ToInt32(reader["Id"]) : 0;
            result.Name = HasColumn(reader, "TodoName") ? (string)reader["TodoName"] : "";
            result.Content = HasColumn(reader, "Content") ? (string)reader["Content"] : "";
            result.Checked = HasColumn(reader, "Done") ? Convert.ToBoolean(reader["Done"]) : false;
        }
    }
}
