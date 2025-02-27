public class PaginationResult<T>
{
    public IEnumerable<T> Items { get; set; } = new List<T>();
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public bool HasNextPage {get;set;}
    public bool HasPreviousPage {get;set;}

}
public class PageRequest
{
    public int CurrentPage { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}