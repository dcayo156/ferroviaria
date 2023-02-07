using LaJuana.Domain;

namespace LaJuana.Application.Contracts.Infrastructure
{
    public interface IInspectionTrainsRepository
    {
        Task<IEnumerable<InspectionTrain>> GetListInspectionTrains();
        Task<InspectionTrain> FindByIdAsync(string Code);
    }
}
