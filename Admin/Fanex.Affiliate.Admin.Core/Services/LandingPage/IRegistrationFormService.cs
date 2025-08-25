using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.LandingPage;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels.LandingPage;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using System.Collections.Generic;
using System.Web;

namespace Fanex.Affiliate.Admin.Core.Services.LandingPage
{
    public interface IRegistrationFormService
    {
        #region Form Management

        /// <summary>
        /// Get list of registration forms
        /// </summary>
        EntityResult<RegistrationFormTemplate> GetRegistrationFormList(RegistrationFormListParam param = null);

        /// <summary>
        /// Get registration form by ID
        /// </summary>
        RegistrationFormTemplate GetRegistrationFormById(int formId);

        /// <summary>
        /// Create new registration form
        /// </summary>
        ResultInfo CreateRegistrationForm(CreateRegistrationFormParam param);

        /// <summary>
        /// Update existing registration form
        /// </summary>
        ResultInfo UpdateRegistrationForm(UpdateRegistrationFormParam param);

        /// <summary>
        /// Delete registration form
        /// </summary>
        ResultInfo DeleteRegistrationForm(DeleteRegistrationFormParam param);

        #endregion

        #region Form Configuration

        /// <summary>
        /// Get available form field types
        /// </summary>
        IEnumerable<FormFieldType> GetFormFieldTypes();

        /// <summary>
        /// Upload assets for registration forms
        /// </summary>
        ResultInfo UploadFormAssets(HttpPostedFileBase[] files, UploadFormAssetsParam param);

        /// <summary>
        /// Generate form preview
        /// </summary>
        FormPreview GenerateFormPreview(int formId);

        /// <summary>
        /// Validate form configuration
        /// </summary>
        ResultInfo ValidateFormConfiguration(string formFields, string formSettings);

        #endregion
    }
}
