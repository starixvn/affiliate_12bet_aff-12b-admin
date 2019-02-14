using System;
using System.Security.Cryptography;
using System.Text;
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
        private readonly ICryptographyService _cryptographyService;

        public VerificationService(
            ITwoFactorAuthService twoFactorAuthService,
            IAccountRepository accountRepository,
            IEmailService mailService,
            ILanguageService languageService,
            ICryptographyService cryptographyService)
        {
            _twoFactorAuthService = twoFactorAuthService;
            _accountRepository = accountRepository;
            _mailService = mailService;
            _languageService = languageService;
            _cryptographyService = cryptographyService;
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
                    NewCounter = counter,
                    NewSendCodeTime = GetNewSentCodeTime()
                };
            }

            return new SendCodeResult
            {
                IsOk = false,
                Message = Lang.SendCodeFailed
            };
        }

        public bool IsValidOtp(VerificationOtpParam param)
        {
            var authMessage = param.LoginInfo + param.HashKey + param.Captcha + param.OtpCode;
            var hashMessage = CreateHashMessage(authMessage, param.HashKey);

            return string.Equals(hashMessage, param.HashMessage) &&
                _twoFactorAuthService.VerifyHotp(param.AffId, param.Counter, param.OtpCode);
        }

        public string CreateActionLink(string publicKey)
        {
            var queryString = "controller=VerificationAccount&action=VerifyAccount";

            return _cryptographyService.RsaEncrypt(publicKey, queryString);
        }

        private static bool CanSendCode(long sentCodeTimeSession)
        {
            var now = DateTime.Now.AddMinutes(IntervalTimeByMinute * -1);
            var nowTick = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second).Ticks;
            var elapsedTick = nowTick - sentCodeTimeSession;

            return sentCodeTimeSession == 0 || elapsedTick >= 0;
        }

        private static long GetNewSentCodeTime()
        {
            var now = DateTime.Now;

            return new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second).Ticks;
        }

        private static string CreateHashMessage(string message, string secret)
        {
            var secretKey = secret ?? string.Empty;
            var encoding = new ASCIIEncoding();
            var keyByte = encoding.GetBytes(secretKey);
            var messageBytes = encoding.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                var hashmessage = hmacsha256.ComputeHash(messageBytes);

                return ByteToString(hashmessage).ToLowerInvariant();
            }
        }

        private static string ByteToString(byte[] buff)
        {
            var builder = new StringBuilder();
            for (int i = 0; i < buff.Length; i++)
            {
                builder.Append(buff[i].ToString("X2")); // hex format
            }

            return builder.ToString();
        }
    }
}