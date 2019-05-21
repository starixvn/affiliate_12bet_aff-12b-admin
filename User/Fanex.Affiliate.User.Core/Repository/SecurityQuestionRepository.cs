using System;
using System.Collections.Generic;
using Fanex.Affiliate.User.Core.DTO;
using System.Data;

namespace Fanex.Affiliate.User.Core.Repository
{
    public class SecurityQuestionRepository : ISecurityQuestionRepository
    {
        public IEnumerable<SecurityQuestion> GetSecurityQuestions(string language, bool isIncludeOldData = false)
        {
            const string spKey = "Aff_User_SecurityQuestions_Sel";

            var securityQuestionList = new List<SecurityQuestion>();

            using (var objectDbBuilder = new ObjectDBBuilder(spKey))
            {
                objectDbBuilder.DB.AddInParameter("IsIncludedOldData", SqlDbType.Bit, isIncludeOldData);
                objectDbBuilder.DB.AddInParameter("Language", SqlDbType.VarChar, language);

                using (var reader = objectDbBuilder.DB.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        securityQuestionList.Add(new SecurityQuestion
                        {
                            QuestionId = Convert.ToByte(reader["QuestionID"]),
                            Question = Convert.ToString(reader["Question"])
                        });
                    }
                }
            }

            return securityQuestionList;
        }
    }
}
