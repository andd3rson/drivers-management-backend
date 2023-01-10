namespace Drivers_Management.Application.Dtos
{
    public class PaginationQuery
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public PaginationQuery(int pageSize, int pageNumber)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize > 20 ? 20 : pageSize;
        }

        public PaginationQuery()
        {
            PageNumber = 1;
            PageSize = 20;
        }
    }
}