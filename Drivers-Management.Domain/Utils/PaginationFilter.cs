namespace Drivers_Management.Domain.Utils;

public class PaginationFilter
{

    public PaginationFilter()
    {
    }

    public PaginationFilter(int pageSize, int pageNumber)
    {
        PageSize = pageSize;
        PageNumber = pageNumber;
    }

    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}