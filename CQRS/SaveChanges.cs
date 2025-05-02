using InventoryTracker.Data;
using MediatR;

namespace InventoryTracker.CQRS
{
    public class SaveChanges : IRequest<bool>
    {

    }
    public class SaveChangesHandler : IRequestHandler<SaveChanges, bool>
    {
        private readonly IGeneralRepo<Models.Category> repo;

        public SaveChangesHandler(IGeneralRepo<Models.Category> repo)
        {
            this.repo = repo;
        }
        public async Task<bool> Handle(SaveChanges request, CancellationToken cancellationToken)
        {
            try
            {
                repo.Save();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
