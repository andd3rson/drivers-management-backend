namespace Drivers_Management.Application.Dtos
{
    public class PagedResponse<T> : ApiResponse<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public Uri? NextPage { get; set; }
        public Uri? FirstPage { get; set; }
        public Uri? LastPage { get; set; }
        public Uri? PreviousPage { get; set; }
        public PagedResponse(T data, int pageNumber, int pageSize)
        {
            Data = data;
            PageNumber = pageNumber;
            PageSize = pageSize;
            Errors = null;
        }

    }
}