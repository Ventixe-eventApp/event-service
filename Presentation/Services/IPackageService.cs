using Presentation.Models;

namespace Presentation.Services;

public interface IPackageService
{
    Task<PackageResult> AddPackageToEventAsync(CreatePackageRequest req);
}
