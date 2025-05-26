using Presentation.Data.Entities;
using Presentation.Data.Repositories;
using Presentation.Models;

namespace Presentation.Services;

public interface IPackageService
{
    Task<PackageResult> AddPackageToEventAsync(CreatePackageRequest req);
}

public class PackageService(IEventPackageRepository eventpackageRepository, IPackageRepository packageRepository) : IPackageService
{
    private readonly IEventPackageRepository _eventpackageRepository = eventpackageRepository;
    private readonly IPackageRepository _packageRepository = packageRepository;

    public async Task<PackageResult> AddPackageToEventAsync(CreatePackageRequest req)
    {
        try
        {

            var package = new PackageEntity
            {
                PackageName = req.PackageName,
                SeactionType = req.SeactionType,
                Description = req.Description,
                Price = req.Price,
                AvailableQuantity = req.AvailableQuantity
            };

            var packageResult = await _packageRepository.CreateAsync(package);
            if (!packageResult.Succeeded)
                return new PackageResult { Succeeded = false, Error = "Failed to create package." };

            var eventPackage = new EventPackageEntity
            {
                EventId = req.EventId,
                Package = package
            };

            var eventPackageResult = await _eventpackageRepository.CreateAsync(eventPackage);
            return eventPackageResult.Succeeded
                ? new PackageResult { Succeeded = true }
                : new PackageResult { Succeeded = false, Error = eventPackageResult.Error };


        }
        catch (Exception ex)
        {
            return new PackageResult { Succeeded = false, Error = ex.Message };
        }
    }

 

}
