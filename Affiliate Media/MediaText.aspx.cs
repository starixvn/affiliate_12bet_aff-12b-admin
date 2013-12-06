using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;

using DNAide.Core;
using DNAide.Data;


namespace AffiliateMedia
{
    public partial class MediaTextPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Context.Response.ContentType = "text/plain";
            if (Context.Request.QueryString["textid"] != null)
            {
                int BannerId = Convert.ToInt32(Context.Request.QueryString["textid"].ToString());
                DbResult result = GetTextDetail(BannerId);
                if (result.ResultSet.Tables.Count > 0)
                {
                    DataTable dtbResult = result.ResultSet.Tables[0];
                    Context.Response.Write("document.getElementById('_affMediaText').innerHTML = '" + dtbResult.Rows[0]["Message"] + "';");
                }
            }
        }
        public static DbResult GetTextDetail(int textId)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("textid", textId);
            return DbHelper.Execute("Aff_TextSelDetail", hsParam, "affiliate_12bet");
        }
    }
}
