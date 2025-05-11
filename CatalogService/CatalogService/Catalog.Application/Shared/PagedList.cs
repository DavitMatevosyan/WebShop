namespace Catalog.Application.Shared;

public class PagedList<T> : List<T>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}