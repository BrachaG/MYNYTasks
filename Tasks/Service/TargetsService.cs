using Microsoft.Extensions.Logging;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class TargetsService : ITargetsService
    {
        ISqlDataAccess _SqlDataAccess;
        ILogger<TargetsService> _logger;

        public TargetsService(ISqlDataAccess SqlDataAccess, ILogger<TargetsService> logger)
        {
            _SqlDataAccess= SqlDataAccess;
            _logger = logger;
        }
        public async string GetTargetsByUserId(int UserId)
        {
            _logger.LogDebug("GetTargetsByUserId", UserId);

            throw new NotImplementedException();
        }
    }
}
