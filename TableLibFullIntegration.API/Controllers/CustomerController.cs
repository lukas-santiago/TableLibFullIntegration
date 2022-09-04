using System.Data;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using SqlKata.Compilers;
using SqlKata;
using TableLibFullIntegration.API.Models.DataTables;
using TableLibFullIntegration.API.Util;
using System.Data.Common;

namespace TableLibFullIntegration.API.Controllers;

[Route("api/[controller]")]
public sealed class CustomerController : CustomBaseController
{
    public CustomerController(DbConnection connection) : base(connection)
    {
    }

    [HttpPost("/")]
    public ActionResult GetCustomers()
    {
        string table = "customers";
        var compiler = new MySqlCompiler();

        //SqlKata Aprouch
        var sqlBuild = new Query(table);

        if (!Request.HasFormContentType)
        {
            return Ok(connection.Query<dynamic>(compiler.Compile(sqlBuild).ToString()));
        }

        DataTableParameters request;
        try
        {
            string json = Request?.Form.Keys.First();
            request = DataTablesUtils.Deserialize(json);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex);
        }
        // Filtering
        sqlBuild = DataTablesUtils.Search(sqlBuild, request);
        // CountTotalRecords
        var sqlBuildCount = sqlBuild.Clone().SelectRaw("count(*) as Count");
        // Pagination
        sqlBuild.Limit(request!.Length).Offset(request.Start);
        // Ordering
        sqlBuild = DataTablesUtils.Order(sqlBuild, request);

        // Compiling
        var sqlResult = compiler.Compile(sqlBuild).ToString();
        var sqlResultCount = compiler.Compile(sqlBuildCount).ToString();

        // Execute
        var customers = connection.Query(sqlResult);
        var countTotalData = connection.ExecuteScalar<int>(sqlResultCount);

        DataTablesResponse response = new DataTablesResponse()
        {
            // sql = new
            // {
            //     sqlResult,
            //     sqlResultCount
            // },
            data = customers,
            draw = request.Draw,
            recordsTotal = countTotalData,
            recordsFiltered = countTotalData,
        };

        return Ok(response);
    }
}
