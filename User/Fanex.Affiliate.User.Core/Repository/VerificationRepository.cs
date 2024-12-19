namespace Fanex.Affiliate.User.Core.Repository
{
    using Criterias;
    using Data;
    using Data.Repository;
    using DTO;
    using DTO.VerificationAccount;
    using MetalModels;

    public class VerificationRepository : BaseRepository, IVerificationRepository
    {
        private readonly IObjectDBBuilder _builder;
        private readonly IDynamicRepository _repository;

        public VerificationRepository(IObjectDBBuilder builder, IDynamicRepository repository)
        {
            _builder = builder;
            _repository = repository;
        }

        public ResultInfo UpdateVerificationInfo(AuthResultBaseParam param)
        {
            const string SpKey = "Aff_User_VerificationUpd";

            return Execute(SpKey, param);
        }

        public VerificationInfo GetLoginInfoByUserDevice(int affId, string ipAddress, string deviceInfo)
        {
            var loginInfoCriteria = new GetLoginInfoByUserDeviceCriteria(affId, ipAddress, deviceInfo);
            return _repository.Get<VerificationInfo>(loginInfoCriteria);
        }

        public ResultInfo InsertUserDeviceInfo(AuthResultBaseParam param)
        {
            const string SpKey = "Aff_User_VerificationSuccessIns";

            return Execute(SpKey, param);
        }

        private ResultInfo Execute(string spKey, AuthResultBaseParam param)
        {
            using (var db = _builder.Build(spKey))
            {
                AssignUpdateLoginResultParam(db, param);
                db.ExecuteNonQuery();

                return GetResultInfo(db);
            }
        }

        private static void AssignUpdateLoginResultParam(IObjectDb objectDb, AuthResultBaseParam param)
        {
            objectDb.SetParameterValue("AffId", param.AffId);
            objectDb.SetParameterValue("IpAddress", param.IpAddress);
            objectDb.SetParameterValue("DeviceInfo", param.DeviceInfo);
            objectDb.SetParameterValue("Language", param.Language);

            AssignResultOutputParam(objectDb);
        }
    }
}