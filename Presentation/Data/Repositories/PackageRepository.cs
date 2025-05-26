using Presentation.Data.Contexts;
using Presentation.Data.Entities;

namespace Presentation.Data.Repositories;

public class PackageRepository(DataContext context) : BaseRepository<PackageEntity>(context), IPackageRepository
{
}
