public T GetField<T>(string Field, string Table, string Where)
{
    string sql = string.Format("select {0} from {1} where {2}", Field, Table, (Where.Equals(string.Empty) ? "1=1" : Where));
 
    using (OleDbConnection conn = new OleDbConnection(SlxConnectionString))
    {
        conn.Open();
        using (OleDbCommand cmd = new OleDbCommand(sql, conn))
        {
            object fieldval = cmd.ExecuteScalar();
            return fieldval == DBNull.Value ? default(T) : (T)fieldval;
        }
    }
}