using System;
using System.Security.Cryptography;
using System.Text;
using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.VerificationAccount;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Affiliate.User.Core.MetalModels.Email;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using Fanex.Affiliate.User.Core.Resources;
using Fanex.Affiliate.User.Core.Utilities;
using OtpNet;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public class VerificationService : IVerificationService
    {
        private const int SecretKeyExistedCode = 101;
        private const int IntervalTimeByMinute = 5;
        private const int ExpirationByMinute = 1;

        private readonly ILanguageService _languageService;
        private readonly ITwoFactorAuthService _twoFactorAuthService;
        private readonly IAccountRepository _accountRepository;
        private readonly IEmailService _mailService;
        private readonly ICryptographyService _cryptographyService;
        private readonly IWrapperUtility _wrapperUtility;

        public VerificationService(
            ITwoFactorAuthService twoFactorAuthService,
            IAccountRepository accountRepository,
            IEmailService mailService,
            ILanguageService languageService,
            ICryptographyService cryptographyService,
            IWrapperUtility wrapperUtility)
        {
            _twoFactorAuthService = twoFactorAuthService;
            _accountRepository = accountRepository;
            _mailService = mailService;
            _languageService = languageService;
            _cryptographyService = cryptographyService;
            _wrapperUtility = wrapperUtility;
        }

        public SendCodeResult SendCode(SendCodeParam param)
        {
            if (CanSendCode(param.SentCodeTime))
            {
                var counter = _wrapperUtility.GetRandomGenerator().Next(int.MinValue, int.MaxValue);

                var userInfo = _accountRepository.GetUserInfoById(param.AffId, false);
                if (string.IsNullOrEmpty(userInfo.SecretKey))
                {
                    userInfo.SecretKey = TryCreateSecretKey(userInfo.AffId);
                }

                var otp = _twoFactorAuthService.GenerateHotp(userInfo.SecretKey, counter);

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
                    NewSendCodeTime = GetNewSendCodeTime()
                };
            }

            return CreateSendCodeFailedResult();
        }

        public bool IsValidOtp(VerificationOtpParam param)
        {
            var authMessage = param.LoginInfo + param.HashKey + param.Captcha + param.OtpCode;
            var hashMessage = CreateHashMessage(authMessage, param.HashKey);

            return string.Equals(hashMessage, param.HashMessage) &&
                _twoFactorAuthService.VerifyHotp(param.SecretKey, param.Counter, param.OtpCode);
        }

        public VerificationAction CreateActionLink(string publicKey)
        {
            var nowTicks = _wrapperUtility.GetNowDateTime().Ticks.ToString();
            var queryString = "controller=VerificationAccount&action=VerifyAccount&t=" + nowTicks + "&e=" + ExpirationByMinute;

            return new VerificationAction
            {
                Link = ConverterUtils.Base64Encode(_cryptographyService.RsaEncrypt(publicKey, queryString)),
                TimeToken = nowTicks
            };
        }

        private bool CanSendCode(long sendCodeTimeSession)
        {
            var maxTime = _wrapperUtility.GetNowDateTime().AddMinutes(IntervalTimeByMinute * -1).Ticks;

            return sendCodeTimeSession == 0 || maxTime - sendCodeTimeSession >= 0;
        }

        private long GetNewSendCodeTime()
        {
            var now = _wrapperUtility.GetNowDateTime();

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

                return ConverterUtils.ToHex(hashmessage).ToLowerInvariant();
            }
        }

        private string TryCreateSecretKey(int affId)
        {
            var secretKey = GenerateSecretKey();
            var resultCode = _accountRepository.CreateSecretKey(affId, secretKey);

            while (resultCode == SecretKeyExistedCode)
            {
                secretKey = GenerateSecretKey();
                resultCode = _accountRepository.CreateSecretKey(affId, secretKey);
            }

            return secretKey;
        }

        private static string GenerateSecretKey()
        {
            return Base32Encoding.ToString(KeyGeneration.GenerateRandomKey(20));
        }

        private static SendCodeResult CreateSendCodeFailedResult()
        {
            return new SendCodeResult
            {
                IsOk = false,
                Message = Lang.SendCodeFailed
            };
        }
    }
}