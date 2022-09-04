namespace TableLibFullIntegration.API.Models.DataTables;

public class DataTableColumn
{
    public string Data { get; set; }
    public string Name { get; set; }
    public bool Orderable { get; set; }
    public bool Searchable { get; set; }
    public Search Search { get; set; }

}

