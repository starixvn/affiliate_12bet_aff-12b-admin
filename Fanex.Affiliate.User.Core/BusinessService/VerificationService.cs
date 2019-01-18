using System;
using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Affiliate.User.Core.MetalModels.Email;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using Fanex.Affiliate.User.Core.Resources;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public class VerificationService : IVerificationService
    {
        private const int IntervalTimeByMinute = 5;

        private readonly ILanguageService _languageService;
        private readonly ITwoFactorAuthService _twoFactorAuthService;
        private readonly IAccountRepository _accountRepository;
        private readonly IEmailService _mailService;

        public VerificationService(
            ITwoFactorAuthService twoFactorAuthService,
            IAccountRepository accountRepository,
            IEmailService mailService,
            ILanguageService languageService)
        {
            _twoFactorAuthService = twoFactorAuthService;
            _accountRepository = accountRepository;
            _mailService = mailService;
            _languageService = languageService;
        }

        public SendCodeResult SendCode(SendCodeParam param)
        {
            if (CanSendCode(param.SentCodeTime))
            {
                var counter = new Random().Next(int.MinValue, int.MaxValue);

                var userInfo = _accountRepository.GetUserInfoById(param.AffId, false);
                var otp = _twoFactorAuthService.GenerateHotp(param.AffId.ToString(), counter);

                var emailParam = param.VerificationEmailParam;
                var verificationAccountEmailParam = new VerificationAccountEmailParam
                {
                    User = userInfo,
                    MailContact = emailParam.MailContact,
                    MailTo = userInfo.Email,
                    RootDomain = emailParam.RootDomain,
                    Code = otp,
                    Ip = emailParam.Ip
                };

                var userLang = SystemLanguage.IsValid(userInfo.PreferredLanguage) ? userInfo.PreferredLanguage : SystemLanguage.DefaultLanguageCode;
                _languageService.SetCulture(userLang);
                _mailService.SendVerificationEmail(verificationAccountEmailParam);
                _languageService.SetCulture(param.CurrentLanguage);

                return new SendCodeResult
                {
                    IsOk = true,
                    NewCounter = counter
                };
            }

            return new SendCodeResult
            {
                IsOk = false,
                Message = Lang.SendCodeFailed
            };
        }

        public bool IsValidOtp(string secretKey, int counter, string otp)
        {
            return _twoFactorAuthService.VerifyHotp(secretKey, counter, otp);
        }

        private static bool CanSendCode(long sentCodeTimeSession)
        {
            var now = DateTime.Now.AddMinutes(IntervalTimeByMinute * -1);
            var nowTick = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second).Ticks;
            var elapsedTick = nowTick - sentCodeTimeSession;

            return sentCodeTimeSession == 0 || elapsedTick >= 0;
        }
    }
}
