﻿using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class TargetsService : ITargetsService
    {
        ISqlDataAccess _SqlDataAccess;
        ILogger<TargetsService> _logger;
        IObjectGenerator<Target> _targetObjectGenerator;

        public TargetsService(ISqlDataAccess SqlDataAccess, ILogger<TargetsService> logger, IObjectGenerator<Target> userObjectGenerator)
        {
            _SqlDataAccess = SqlDataAccess;
            _logger = logger;
            _targetObjectGenerator = userObjectGenerator;
        }
        public async Task<ActionResult<List<Target>>> GetTargetsByUserId(int userId, int permissionLevelId)
        {
            _logger.LogDebug("GetTargetsByUserId", userId, permissionLevelId);
            List<SqlParameter> parameters = new List<SqlParameter> {
            { new SqlParameter("id",userId )},
            { new SqlParameter("PermissionLevelId", permissionLevelId)}
                };
            try
            {
                DataTable targets = await _SqlDataAccess.ExecuteDatatableSP("su_Get_Targets", parameters);
                if (targets.Rows.Count > 0)
                {
                    List<Target> t = _targetObjectGenerator.GeneratListFromDataTable(targets);
                    return new ObjectResult(t) { StatusCode = 200 };
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("GetTargetsByUserId ", ex);
                return new ObjectResult(null) { StatusCode = 500 };

            }
            return null;
        }

        public async Task<ActionResult<string>> AddTarget(string comment, int typeTargetId, int[] personId, int BranchId, DateTime? targetDate, int creatorId,int PermissionLevelId)
        {
            if (PermissionLevelId==(int)PermissionLevelEnum.PermissionLevel.coordinator)
            {
                int[] coordinator = { creatorId };
                personId = coordinator;
            }
            DataTable personIds = new DataTable();
            personIds.Columns.Add("Id", typeof(int));

            // Populate the DataTable with the list of PersonId values
            foreach (int id in personId)
            {
                personIds.Rows.Add(id);
            }

            List<SqlParameter> parameters = new List<SqlParameter> {
            new SqlParameter("Comment", comment),
            new SqlParameter("typeTargetId", typeTargetId),
            new SqlParameter
            {
                ParameterName = "Ids",
                SqlDbType = SqlDbType.Structured,
                TypeName = "dbo.PersonIds",
                Value = personIds
            },
             new SqlParameter("BranchId", BranchId),
            new SqlParameter("TargetDate", targetDate)
            ,
            new SqlParameter("CreatorId", creatorId)};
            
            try
            {
                await _SqlDataAccess.ExecuteDatatableSP("su_Insert_Target", parameters);
                return new ObjectResult("Target inserted successfully") { StatusCode = 200 };

            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to insert target", ex);
                return new ObjectResult("Failed to insert target") { StatusCode = 500 };
            }
        }
    }
}
