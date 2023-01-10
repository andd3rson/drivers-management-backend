using Drivers_Management.Domain.Contracts.Services;
using Drivers_Management.Domain.Utils;
using Microsoft.AspNetCore.WebUtilities;

namespace Drivers_Management.Domain.Services;

public class UriServices : IUriServices
{
    private readonly string _baseUri;
    public UriServices(string baseUri)
    {
        _baseUri = baseUri;
    }
    public Uri GetPageUri(PaginationFilter filter, string route)
    {
        var enpointUri = new Uri(string.Concat(_baseUri, route));
        var modifiedUri = QueryHelpers.AddQueryString(enpointUri.ToString(), "pageNumber", filter.PageNumber.ToString());
        modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", filter.PageSize.ToString());
        return new Uri(modifiedUri);
    }   
}