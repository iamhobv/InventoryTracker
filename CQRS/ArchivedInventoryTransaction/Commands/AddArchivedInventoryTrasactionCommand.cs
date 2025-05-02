using InventoryTracker.CQRS.InventoryTransactions.Queries;
using InventoryTracker.Data;
using InventoryTracker.Models;
using InventoryTracker.Services;
using MediatR;

namespace InventoryTracker.CQRS.ArchivedInventoryTransaction.Commands
{
    public class AddArchivedInventoryTrasactionCommand : IRequest<bool>
    {
        //public int TransactionId { get; set; }
        public InventoryTransaction InventoryTransaction { get; set; }
    }

    public class AddArchivedInventoryTrasactionCommandHandler : IRequestHandler<AddArchivedInventoryTrasactionCommand, bool>
    {
        private readonly IMediator mediator;
        private readonly IGeneralRepo<Models.ArchivedInventoryTransaction> repo;

        public AddArchivedInventoryTrasactionCommandHandler(IMediator mediator, IGeneralRepo<Models.ArchivedInventoryTransaction> repo)
        {
            this.mediator = mediator;
            this.repo = repo;
        }
        public async Task<bool> Handle(AddArchivedInventoryTrasactionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //var res = await mediator.Send(new GetInventoryTransactionWithMoreThanOneYearQuery());
                //foreach (var item in res)
                //{
                //    Models.ArchivedInventoryTransaction archivedInventory = item.Map<Models.ArchivedInventoryTransaction>();
                //    archivedInventory.ArchiveDate = DateTime.Now;
                //    repo.Add(archivedInventory);
                //}


                Models.ArchivedInventoryTransaction archivedInventory = request.InventoryTransaction.Map<Models.ArchivedInventoryTransaction>();
                archivedInventory.ArchiveDate = DateTime.Now;
                repo.Add(archivedInventory);

                //repo.Save();
                return true;
            }
            catch (Exception)
            {

                return false;
            }


        }
    }
}
