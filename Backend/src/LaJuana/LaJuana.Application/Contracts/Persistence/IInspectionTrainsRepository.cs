using LaJuana.Domain;

namespace LaJuana.Application.Contracts.Persistence
{
    public interface IInspectionTrainsRepository : IAsyncRepository<InspectionTrain>
    {
        Task<IEnumerable<InspectionTrain>> GetListInspectionTrains();
        Task<InspectionTrain> FindByIdAsync(Guid Id);
    }
}
