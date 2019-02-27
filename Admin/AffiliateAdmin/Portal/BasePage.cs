using System;
using System.Web;
using Affiliate.Infrastructure.Helpers;

namespace AffiliateAdmin.Portal
{
    public class BasePage : System.Web.UI.Page
    {
        protected string _XsrfTokenFormValue;
        private string _locale;

        protected override void OnInit(EventArgs e)
        {
            _XsrfTokenFormValue = AntiForgeryHelper.GenerateXsrfFormToken(HttpContext.Current);

            base.OnInit(e);
        }

        protected override void InitializeCulture()
        {
            _locale = (HttpContext.Current.Session["Culture"] == null) ? "en-US" : HttpContext.Current.Session["Culture"].ToString();
            if (HttpContext.Current.Session["Culture"] != null)
            {
                this.Culture = _locale;
                this.UICulture = _locale;
            }
            else
            {
                HttpContext.Current.Session["Culture"] = _locale;
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(_locale);
                System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture(_locale);
            }

            CheckUnderMaintenance();

            base.InitializeCulture();
        }

        public virtual void CheckUnderMaintenance()
        {
            if (Common.IsUnderMaintenance())
            {
                Response.Redirect("~/UnderMaintenance.aspx");
            }
        }

        public string Locale
        {
            get
            {
                return _locale;
            }
        }
    }
}