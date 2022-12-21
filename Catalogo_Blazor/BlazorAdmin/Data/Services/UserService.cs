using BlazorAdmin.Data.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BlazorAdmin.Data.Services;

public class UserService : IUserService
{
    private readonly SqlConnectionConfiguration _configuration;

    public UserService(SqlConnectionConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<List<User>> GetUsers()
    {
        try
        {
            List<User> users = new List<User>();
            using (SqlConnection con = new SqlConnection(_configuration.ConnectionString))
            {
                const string query = "select * from dbo.AspNetUsers";
                SqlCommand cmd = new SqlCommand(query, con)
                {
                    CommandType = CommandType.Text
                };

                con.Open();

                SqlDataReader rdr = await cmd.ExecuteReaderAsync();
                while (rdr.Read())
                {
                    User user = new()
                    {
                        Id = Guid.Parse(rdr["Id"].ToString()),
                        UserName = rdr["UserName"].ToString(),
                        Email = rdr["Email"].ToString(),
                        RoleId = new Guid()
                    };
                    users.Add(user);
                }
                cmd.Dispose();
            }

            return users;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<User> GetUser(Guid id)
    {
        try
        {
            User user = new User();
            using (SqlConnection con = new SqlConnection(_configuration.ConnectionString))
            {
                const string query = "select * from dbo.AspNetUsers where Id = @Id";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Id", id);
                    con.Open();
                    using (SqlDataReader rdr = await cmd.ExecuteReaderAsync())
                    {
                        if (rdr.Read())
                        {
                            user.Id = Guid.Parse(rdr["Id"].ToString());
                            user.UserName = rdr["UserName"].ToString();
                            user.Email = rdr["Email"].ToString();
                        }
                    }
                }
            }
            return user;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<bool> DeleteUser(Guid id)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(_configuration.ConnectionString))
            {
                const string query = "delete FROM dbo.AspNetUsers Where Id=@Id";
                SqlCommand cmd = new SqlCommand(query, con)
                {
                    CommandType = CommandType.Text,
                };

                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                int result = await cmd.ExecuteNonQueryAsync();

                //con.Close();
                cmd.Dispose();
            }
            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }




    public async Task<bool> UpdateUserRole(Guid id, User user)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(_configuration.ConnectionString))
            {
                const string query = "insert into dbo.AspNetUserRoles " +
                    "(UserId,RoleId) values(@UserId, @RoleId)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@UserId", id);
                    cmd.Parameters.AddWithValue("@RoleId", user.RoleId);

                    con.Open();
                    int result = await cmd.ExecuteNonQueryAsync();
                }
            }
            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
