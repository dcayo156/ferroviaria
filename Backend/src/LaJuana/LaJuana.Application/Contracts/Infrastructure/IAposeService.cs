using LaJuana.Domain;

namespace LaJuana.Application.Contracts.Infrastructure
{
    public interface IAposeService
    {
        Task<InspectionTrain> ReadDocInspectionIntegral(string pathFile, InspectionTrain item);
        Task<string> SaveDocInspectionIntegral(string pathFile, List<InspectionTrain> item);
    }
}
