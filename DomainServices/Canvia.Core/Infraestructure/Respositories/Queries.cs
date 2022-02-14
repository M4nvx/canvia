using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canvia.Core.Infraestructure.Respositories
{
    public static class Queries
    {
        public static async Task<string> ExecuteQueryHelper(string query, List<System.Data.SqlClient.SqlParameter> parameters, System.Data.Common.DbConnection connection)
        {
            await connection.OpenAsync();
            using (var cmd = connection.CreateCommand())
            {
                if (parameters != null && parameters.Any())
                {
                    foreach (var p in parameters)
                    {
                        cmd.Parameters.Add(p);
                    }
                }

                cmd.CommandText = query;
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    var dataTable = new DataTable();
                    dataTable.Load(dr);
                    return dataTable.Rows.Count > 0 ? JsonConvert.SerializeObject(dataTable, Newtonsoft.Json.Formatting.Indented) : "{Result: 'Ok'}";
                }
            }

        }
    }
}
