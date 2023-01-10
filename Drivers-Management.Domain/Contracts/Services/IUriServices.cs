using Drivers_Management.Domain.Utils;

namespace Drivers_Management.Domain.Contracts.Services;

public interface IUriServices
{
    public Uri GetPageUri(PaginationFilter filter, string route);
}