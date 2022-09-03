using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using Dapper;
using System.Text.Json;
using SqlKata.Compilers;
using SqlKata;
using TableLibFullIntegration.API.Utils;

namespace TableLibFullIntegration.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly IDatabaseConnection connection;

    public CustomerController(IDatabaseConnection connection)
    {
        this.connection = connection;
    }

    [HttpPost("/")]
    public ActionResult GetCustomers()
    {
        string json = Request.Form.Keys.First();
        var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        var request = JsonSerializer.Deserialize<DataTableParameters>(json, options);

        using (IDbConnection connection = new MySqlConnection("Server=<host>;Database=<db>;Uid=<User>;Pwd=<Pwd>;"))
        {
            //SqlKata Aprouch
            var compiler = new MySqlCompiler();

            string table = "customers";

            var sqlBuild = new Query(table);
            var sqlBuildCount = new Query(table);

            // Filtering
            if (!String.IsNullOrEmpty(request?.Search.Value))
            {
                request.Columns.ForEach(column =>
                {
                    sqlBuild = column.Searchable == true
                        // ? sqlBuild.WhereRaw("lower(?) = ?", column.Data, request.Search.Value)
                        ? sqlBuild.OrWhereLike(column.Data, "%" + request.Search.Value + "%")
                        // : sqlBuild.WhereRaw("lower(?) = ?", column.Data, request.Search.Value);
                        : sqlBuild.OrWhereLike(column.Data, request.Search.Value);
                });
            }

            // CountTotalRecords
            sqlBuildCount.SelectRaw("count(*) as Count");

            // Pagination
            sqlBuild.Limit(request.Length).Offset(request.Start);

            // Ordering
            request.Order.ForEach(orderColumn =>
            {
                sqlBuild = orderColumn.Dir == "asc"
                ? sqlBuild.OrderBy(request.Columns[orderColumn.Column].Data)
                : sqlBuild.OrderByDesc(request.Columns[orderColumn.Column].Data);
            });

            // Compiling
            var sqlResult = compiler.Compile(sqlBuild).ToString();
            var sqlResultCount = compiler.Compile(sqlBuildCount).ToString();

            // Execute
            var customers = connection.Query(sqlResult);
            var countTotalData = connection.ExecuteScalar(sqlResultCount);

            dynamic response = new
            {
                sql = new
                {
                    sqlResult,
                    sqlResultCount
                },
                data = customers,
                draw = request.Draw,
                recordsTotal = countTotalData,
                recordsFiltered = countTotalData,
            };

            return Ok(response);
        }
    }
    [HttpGet]
    public ActionResult get()
    {
        var compiler = new MySqlCompiler();

        var query = "SELECT  * FROM customers;";

        var sqlKata = new SqlKata.Query("customers");  // (connection, new MySqlCompiler);
        var sqlKataResult = compiler.Compile(sqlKata);

        return Ok(new
        {
            query,
            sqlKataResult
        });
    }
}
public class DataTableParameters
{
    public List<DataTableColumn> Columns { get; set; }
    public int Draw { get; set; }
    public int Length { get; set; }
    public List<DataOrder> Order { get; set; }
    public Search Search { get; set; }
    public int Start { get; set; }


}

public class Search
{
    public bool Regex { get; set; }
    public string Value { get; set; }
}

public class DataTableColumn
{
    public string Data { get; set; }
    public string Name { get; set; }
    public bool Orderable { get; set; }
    public bool Searchable { get; set; }
    public Search Search { get; set; }

}

public class DataOrder
{
    public int Column { get; set; }
    public string Dir { get; set; }
}
