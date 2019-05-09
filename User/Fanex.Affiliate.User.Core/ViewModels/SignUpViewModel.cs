using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.MetalModels;
using System.Collections.Generic;

namespace Fanex.Affiliate.User.Core.ViewModels
{
    public class SignUpViewModel
    {
        public IEnumerable<SecurityQuestion> SecurityQuestionList { get; set; }

        public UserSignUpParam UserSignUpParam { get; set; }
    }
}
