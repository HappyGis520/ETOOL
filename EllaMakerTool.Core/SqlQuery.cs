using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EllaMakerTool
{
    public static class SqlQuery
    {
        public static DataTable SqlQueryForDataTable(this SqlConnection conn,
                 string sql)
        {
            //SqlConnection conn = (SqlConnection)db.Connection;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            cmd.CommandTimeout = 180;

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adapter.Fill(table);
                 
            
            return table;
        }
    }
}
