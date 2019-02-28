namespace Fanex.Affiliate.DataService.CheckSum
{
    using System;
    using MailDeliver.MailClient.API;
    using MailDeliver.Model;
    using Repository;
    using Utilities;

    public class DailyMemberCheckSumRunner : IDailyMemberCheckSumRunner
    {
        private readonly IDailyMemberRepository _dailyMemberRepository;
        private readonly IEmailBuilder _emailBuilder;
        private readonly IAppSettings _appSettings;
        private const string NotFoundError = "Not found discrepancy data";

        public DailyMemberCheckSumRunner() : this(null, null, null)
        {
        }

        public DailyMemberCheckSumRunner(
            IDailyMemberRepository dailyMemberRepository,
            IEmailBuilder emailBuilder,
            IAppSettings appSettings
           )
        {
            _appSettings = appSettings ?? new AppSettings();
            _dailyMemberRepository = dailyMemberRepository ?? new DailyMemberRepository();
            _emailBuilder = emailBuilder ?? new EmailBuilder();
        }

        public void Run()
        {
            // decide time to compare data(check data 2 times: first at 11:00-12:00, second at 23:00-24:00)
            if (DateTime.Now.Hour == _appSettings.CheckingTime || _appSettings.IsLocalTest)
            {
                var date = DateTime.Now.AddDays(_appSettings.DayToAdd);
                CheckDailyMember(date);
                CheckDailyMemberIncomeAccess(date);
            }
        }

        private void CheckDailyMember(DateTime date)
        {
            var result = _dailyMemberRepository.CheckSumDailyMemberFor12Bet(date);
            var emailContent = result.isMatch ? NotFoundError : _emailBuilder.GenerateDailyMemberContent(result, _appSettings.EmailTemplateFolder);
            SendMail(emailContent);
        }

        private void CheckDailyMemberIncomeAccess(DateTime date)
        {
            var result = _dailyMemberRepository.CheckSumDailyMemberForIncomeAccess(date);
            var emailContent = result.isMatch ? NotFoundError : _emailBuilder.GenerateDailyMemberIncomeAccessContent(result, _appSettings.EmailTemplateFolder);
            SendMail(emailContent);
        }

        private void SendMail(string content)
        {
            const string Title = "Affiliate_12BET DB Checksum - There is an error when summarize data to MemberDaily table.";

            var email = new MailRecord
            {
                MailTo = _appSettings.ToMail,
                Subject = Title,
                Title = Title,
                Body = content,
                MailBcc = _appSettings.BccMail,
                MailCc = _appSettings.CcMail
            };

            using (var mailDeliver = new MailDeliver())
            {
                mailDeliver.SendMail(email);
            }
        }
    }
}