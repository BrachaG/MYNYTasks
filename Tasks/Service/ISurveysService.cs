using Entities;

namespace Service
{
    public interface ISurveysService
    {
        Task<List<Survey>> Get(string userId);
    }
}
