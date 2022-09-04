using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using SqlKata;
using TableLibFullIntegration.API.Models.DataTables;

namespace TableLibFullIntegration.API.Util
{
    public static class DataTablesUtils
    {
        public static DataTableParameters Deserialize(string json)
        {
            var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var result = JsonSerializer.Deserialize<DataTableParameters>(json, options);
            if (result != null) return result;

            throw new ArgumentNullException(nameof(result), nameof(json));
        }

        public static Query Search(Query sqlBuild, DataTableParameters request)
        {
            if (!String.IsNullOrEmpty(request!.Search.Value))
            {
                request.Columns.ForEach(column =>
                {
                    sqlBuild = column.Searchable == true
                        ? sqlBuild.OrWhereLike(column.Data, "%" + request.Search.Value + "%")
                        : sqlBuild.OrWhereLike(column.Data, request.Search.Value);
                });
            }

            return sqlBuild;
        }
        public static Query Order(Query sqlBuild, DataTableParameters request)
        {
            request.Order.ForEach(orderColumn =>
            {
                sqlBuild = orderColumn.Dir == "asc"
                ? sqlBuild.OrderBy(request.Columns[orderColumn.Column].Data)
                : sqlBuild.OrderByDesc(request.Columns[orderColumn.Column].Data);
            });
            return sqlBuild;
        }
    }
}