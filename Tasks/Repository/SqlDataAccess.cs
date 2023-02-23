using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json;

namespace Repository;

public class SqlDataAccess : ISqlDataAccess
{
    ILogger<SqlDataAccess> _logger;
    IConfiguration _Configuration;
    readonly string connectionString; 
    public SqlDataAccess(IConfiguration Configuration, ILogger<SqlDataAccess> logger)
    {
        _logger = logger;
        _Configuration = Configuration;

        connectionString = _Configuration.GetConnectionString("DefaultConnection");
    }
    #region ExecuteDatasetSP
    public async Task<DataSet> ExecuteDatasetSP(string spName, List<SqlParameter> SPParameters)
    {
       
        // Create & open a SqlConnection, and dispose of it after we are done
     
        using (SqlConnection connection = new SqlConnection())
        {
            try {
            connection.ConnectionString = connectionString;
            connection.Open(); 
            }
            catch(Exception ex){
                _logger.LogError(ex, "ExecuteDatasetSP failed opening connection", spName);
            }
            return await ExecuteDatasetSP(connection, spName, SPParameters);
        }
    }

    private async Task<DataSet> ExecuteDatasetSP(SqlConnection connection, string spName, List<SqlParameter> SPParameters)
    {
        
        if (connection == null) throw new ArgumentNullException("connection");

        // Create a command and prepare it for execution
        SqlCommand cmd = new SqlCommand();
        bool mustCloseConnection = false;
        await PrepareCommand(cmd, connection, (SqlTransaction)null, CommandType.StoredProcedure, spName, SPParameters, mustCloseConnection);

        // Create the DataAdapter & DataSet
        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
        {
            DataSet ds = new DataSet();

            // Fill the DataSet using default values for DataTable names, etc
            da.Fill(ds);

            // Detach the SqlParameters from the command object, so they can be used again
            cmd.Parameters.Clear();

            if (mustCloseConnection)
                connection.Close();

            // Return the dataset
            return ds;
        }
    }
    private async Task PrepareCommand(SqlCommand command, SqlConnection connection,
           SqlTransaction transaction, CommandType commandType, string commandText,
           List<SqlParameter> commandParameters, bool mustCloseConnection)
    {
       
        if (command == null) throw new ArgumentNullException("command");
        if (commandText == null || commandText.Length == 0)
            throw new ArgumentNullException("commandText");

        // If the provided connection is not open, we will open it
        if (connection.State != ConnectionState.Open)
        {
            mustCloseConnection = true;
            connection.Open();
        }
        else
            mustCloseConnection = false;

        // Associate the connection with the command
        command.Connection = connection;

        // Set the command text (stored procedure name or SQL statement)
        command.CommandText = commandText;

        // If we were provided a transaction, assign it
        if (transaction != null)
        {
            if (transaction.Connection == null)
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            command.Transaction = transaction;
        }

        command.CommandType = commandType;  // Set the command type

        // Attach the command parameters if they are provided
        if (commandParameters != null)
            await AttachParameters(command, commandParameters);
        return;
    }

    private async Task AttachParameters(SqlCommand command, List<SqlParameter> commandParameters)
    {
        
        if (command == null) throw new ArgumentNullException("command");
        if (commandParameters != null)
            foreach (SqlParameter p in commandParameters)
            {
                if (p != null)
                {
                    // Check for derived output value with no value assigned
                    if ((p.Direction == ParameterDirection.InputOutput ||
                        p.Direction == ParameterDirection.Input) &&
                        (p.Value == null))
                        p.Value = DBNull.Value;
                    command.Parameters.Add(p);
                }
            }
    }
    #endregion  ExecuteDatasetSP
    #region ExecuteScalarSP
    public async Task<object> ExecuteScalarSP(string spName, params SqlParameter[] commandParameters)
    {
        // Create & open a SqlConnection, and dispose of it after we are done
        using (SqlConnection connection = new SqlConnection())
        {
            try { 
            //connection.Open();
            connection.ConnectionString = connectionString;
            connection.Open();
            }
              catch (Exception ex)
            {
                _logger.LogError(ex, "ExecuteScalarSP failed opening connection", spName);
            }

            // Call the overload that takes a connection in place of the connection string
            return await ExecuteScalarSP(connection, spName, commandParameters);
        }
    }
    private async Task<object> ExecuteScalarSP(SqlConnection connection, string spName, params SqlParameter[] commandParameters)
    {
        if (connection == null) throw new ArgumentNullException("connection");

        // Create a command and prepare it for execution\\
        SqlCommand cmd = new SqlCommand();

        bool mustCloseConnection = false;
        await PrepareCommand(cmd, connection, (SqlTransaction)null, CommandType.StoredProcedure, spName, commandParameters, mustCloseConnection);

        // Execute the command & return the results
        object retval = cmd.ExecuteScalar();

        // Detach the SqlParameters from the command object, so they can be used again
        cmd.Parameters.Clear();

        if (mustCloseConnection)
            connection.Close();

        return retval;
    }
    private async Task PrepareCommand(SqlCommand command, SqlConnection connection,
            SqlTransaction transaction, CommandType commandType, string commandText,
            SqlParameter[] commandParameters, bool mustCloseConnection)
    {
            "commandParameters is: {commandParameters} mustCloseConnection is: {mustCloseConnection} In PrepareCommand of sqlDataAccess returns void");
        if (command == null) throw new ArgumentNullException("command");
        if (commandText == null || commandText.Length == 0)
            throw new ArgumentNullException("commandText");

        // If the provided connection is not open, we will open it
        if (connection.State != ConnectionState.Open)
        {
            mustCloseConnection = true;
            connection.Open();
        }
        else
            mustCloseConnection = false;

        // Associate the connection with the command
        command.Connection = connection;

        // Set the command text (stored procedure name or SQL statement)
        command.CommandText = commandText;

        // If we were provided a transaction, assign it
        if (transaction != null)
        {
            if (transaction.Connection == null)
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            command.Transaction = transaction;
        }

        command.CommandType = commandType;  // Set the command type

        // Attach the command parameters if they are provided
        if (commandParameters != null)
            await AttachParameters(command, commandParameters);
        return;
    }
    private async Task AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
    {
        if (command == null) throw new ArgumentNullException("command");
        if (commandParameters != null)
            foreach (SqlParameter p in commandParameters)
                if (p != null)
                {

                    // Check for derived output value with no value assigned
                    if ((p.Direction == ParameterDirection.InputOutput ||
                        p.Direction == ParameterDirection.Input) &&
                        (p.Value == null))
                        p.Value = DBNull.Value;
                    command.Parameters.Add(p);
                }
    }
    #endregion ExecuteScalarSP 
    #region ExecuteDatatableSP
    public async Task<DataTable> ExecuteDatatableSP(string spName, List<SqlParameter> SPParameters)
    {
        // Create & open a SqlConnection, and dispose of it after we are done
        using (SqlConnection connection = new SqlConnection())
        {
            try { 
            connection.ConnectionString = connectionString;
            connection.Open();
}
            catch (Exception ex)
            {
                _logger.LogError(ex, "ExecuteDatatableSP failed opening connection", spName);
            }
            // Call the overload that takes a connection in place of the connection string
            return await ExecuteDatatableSP(connection, spName, SPParameters);
        }
    }

    private async Task<DataTable> ExecuteDatatableSP(SqlConnection connection, string spName, List<SqlParameter> SPParameters)
    {
        if (connection == null) throw new ArgumentNullException("connection");

        // Create a command and prepare it for execution
        SqlCommand cmd = new SqlCommand();
        bool mustCloseConnection = false;
        await PrepareCommand(cmd, connection, (SqlTransaction)null, CommandType.StoredProcedure, spName, SPParameters, mustCloseConnection);

        // Create the DataAdapter & DataSet
        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
        {
            DataTable ds = new DataTable();

            // Fill the DataSet using default values for DataTable names, etc
            da.Fill(ds);

            // Detach the SqlParameters from the command object, so they can be used again
            cmd.Parameters.Clear();

            if (mustCloseConnection)
                connection.Close();

            // Return the dataset
            return ds;
        }
    }
    #endregion  ExecuteDatasetSP
}
