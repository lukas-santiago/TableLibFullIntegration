namespace TableLibFullIntegration.API.Models.DataTables;
public class DataTableParameters
{
    public List<DataTableColumn> Columns { get; set; }
    public int Draw { get; set; }
    public int Length { get; set; }
    public List<DataOrder> Order { get; set; }
    public Search Search { get; set; }
    public int Start { get; set; }
}

