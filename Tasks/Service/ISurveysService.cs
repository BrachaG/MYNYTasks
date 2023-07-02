using Entities;
using Microsoft.AspNetCore.Mvc;

namespace Service
{
    public interface ISurveysService
    {
        Task<ActionResult<List<Survey>>> Get();
    }
}
