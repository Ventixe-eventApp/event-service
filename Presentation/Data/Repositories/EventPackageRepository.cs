using Presentation.Data.Contexts;
using Presentation.Data.Entities;

namespace Presentation.Data.Repositories;

public class EventPackageRepository(DataContext context) : BaseRepository<EventPackageEntity>(context), IEventPackageRepository
{
}
