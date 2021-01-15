using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MassAdjustment;
using Fanex.Affiliate.Admin.Core.Repository.Affiliate;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Utilities;
using System;
using System.Web;

namespace Fanex.Affiliate.Admin.Core.Services.MassAdjustment
{
    public class MassAdjustmentService : IMassAdjustmentService
    {
        private const int MaxRows = 500;
        private const string ValidExtension = ".xlsx";

        private readonly IMassAdjustmentRepository _massAdjustmentRepository;
        private readonly ISystemFile _systemFile;

        public MassAdjustmentService(IMassAdjustmentRepository massAdjustmentRepository,
            ISystemFile systemFile)
        {
            _massAdjustmentRepository = massAdjustmentRepository;
            _systemFile = systemFile;
        }

        public AdjustmentResultInfo InsertMassAdjusment(HttpPostedFileBase file)
        {
            var validateFileResult = CreateErrorResult();

            if(validateFileResult.Result.IsSuccessful)
            {
                return _massAdjustmentRepository.InsertMassAdjusment();
            }

            return validateFileResult;
        }

        public AdjustmentResultInfo ValidateFile(string fileName, int rows)
        {
            if (ValidExtension.Equals(_systemFile.GetExtension(fileName), StringComparison.CurrentCultureIgnoreCase) || rows > MaxRows)
            {
                return CreateErrorResult();
            }

            return new AdjustmentResultInfo();
        }

        private static AdjustmentResultInfo CreateErrorResult()
        {
            return new AdjustmentResultInfo {
                Result = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = Lang.InvalidAdjustmentFile
                }
            };
        }
    }
}
