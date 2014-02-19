using DNAide.Web;
using DNAide.Core;
using System.Data;
using DNAide.Data;
using System.Text;
namespace GenerateHTML
{
    /// <summary>
    /// Generate content for Banner
    /// Written by : Tong Hoang Dung on 24/03/2009
    /// </summary>
    public class Banner
    {
        public static string GetEditedBanner(int bannerID,int noRow)
        {
            DbResult result = DataHandler.Banner.GetBannerDetailOld(bannerID);
            if (result.ResultSet.Tables[0].Rows.Count > 0)
            {
                string temp = TemplateHandler.LoadTemplate("Media/MainListBanner.htm");
                string content = TemplateHandler.CopyBlock(temp, "Content");
                TemplateHandler.RemoveBlock(ref content, "ViewMode1");
                TemplateHandler.RemoveBlock(ref content, "ViewMode2");
                DataRow dr = result.ResultSet.Tables[0].Rows[0];
                
                DataView dv = DbHelper.Execute("Aff_BannerSelType", DBType.AFFILIATE_12BET).ResultSet.Tables[0].DefaultView;
                TemplateHandler.Assign(ref content, "order-val", noRow.ToString());
                TemplateHandler.Assign(ref content, "No-val", noRow.ToString());
                TemplateHandler.Assign(ref content, "rowstyle", "active_listbanner");                                
                TemplateHandler.Assign(ref content, "filenamevalue", dr["filename"].ToString());             
                TemplateHandler.Assign(ref content, "catevalue",Common.GetDataOptionBanner(dv,"slCategory","Category",dr["category"].ToString(),false));
                TemplateHandler.Assign(ref content, "langvalue", Common.GetDataOptionBanner(dv, "slLanguage", "Language", dr["language"].ToString(), false));
                TemplateHandler.Assign(ref content, "imagetypevalue", Common.GetDataOptionBanner(dv, "slImageType", "ImageType", dr["imagetype"].ToString(), false));
                TemplateHandler.Assign(ref content, "sizevalue", Common.GetDataOptionBanner(dv, "slSize", "Size", dr["size"].ToString(), false));
                TemplateHandler.Assign(ref content, "bannerid", dr["bannerid"].ToString());
                TemplateHandler.Assign(ref content, "currencyvalue", Common.GetDataOptionBanner(dv, "slCurrency", "Currency", dr["currency"].ToString(), false));
                return content;


            }
            
            return string.Empty;
        }
        public static string GetViewedBanner(int bannerID, int noRow)
        {
            DbResult result = DataHandler.Banner.GetBannerDetailOld(bannerID);
            if (result.ResultSet.Tables[0].Rows.Count > 0)
            {
                string temp = TemplateHandler.LoadTemplate("Media/MainListBanner.htm");
                string content = TemplateHandler.CopyBlock(temp, "Content");
                TemplateHandler.RemoveBlock(ref content, "UpdateMode1");
                TemplateHandler.RemoveBlock(ref content, "UpdateMode2");
                
                DataRow dr = result.ResultSet.Tables[0].Rows[0];
                string[] strSize = dr["size"].ToString().Split('x');
                int realWidth = CastAide.AsInt32(strSize[0]);
                int realHeight = CastAide.AsInt32(strSize[1]);
                TemplateHandler.Assign(ref content, "real-width", realWidth.ToString());
                TemplateHandler.Assign(ref content, "real-height", realHeight.ToString());
                string bannerType = string.Empty;
                if (System.IO.Path.GetExtension(dr["filename"].ToString()) == ".swf")
                {
                    TemplateHandler.RemoveBlock(ref content, "limit-img");
                    bannerType = "flash";
                }
                else
                {
                    TemplateHandler.RemoveBlock(ref content, "limit-fls");
                    bannerType = "image";
                }
                if (realWidth > 500 || realHeight > 500)
                {
                    TemplateHandler.Assign(ref content, "css-class", "hand");
                    string viewFull = string.Format("DoCal('BannerDetail.aspx?bannertype={0}&imagevalue={1}&lastupdate={2}&width={3}&height={4}','{5}',{6} + 100,{7} + 50);",
                                            bannerType, dr["filename"].ToString(), dr["Lastupdate"].ToString(), realWidth, realHeight, dr["bannerid"].ToString(),
                                            realWidth, realHeight);
                    TemplateHandler.Assign(ref content, "viewfull", viewFull);
                    if (realWidth > realHeight)
                    {
                        realHeight = 500 * realHeight / realWidth;
                        realWidth = 500;
                    }
                    else
                    {
                        realWidth = 500 * realWidth / realHeight;
                        realHeight = 500;
                    }
                }
                else
                {
                    TemplateHandler.Assign(ref content, "css-class", string.Empty);
                    TemplateHandler.Assign(ref content, "viewfull", string.Empty);
                }
                TemplateHandler.Assign(ref content, "width", realWidth.ToString());
                TemplateHandler.Assign(ref content, "height", realHeight.ToString());
                
                DataView dv = DbHelper.Execute("Aff_BannerSelType", DBType.AFFILIATE_12BET).ResultSet.Tables[0].DefaultView;
                TemplateHandler.Assign(ref content, "No-val", noRow.ToString());
                TemplateHandler.Assign(ref content, "order-val", noRow.ToString());
                if (noRow % 2 == 0)
                {
                    TemplateHandler.Assign(ref content, "rowstyle", "RecordFirst");
                }
                else
                {
                    TemplateHandler.Assign(ref content, "rowstyle", "RecordSecond");
                }
                

                TemplateHandler.Assign(ref content, "lastupdate", dr["Lastupdate"].ToString());
                TemplateHandler.Assign(ref content, "imagevalue", dr["filename"].ToString());
                TemplateHandler.Assign(ref content, "filenamevalue", dr["filename"].ToString());
                TemplateHandler.Assign(ref content, "catevalue", dr["category"].ToString());
                TemplateHandler.Assign(ref content, "langvalue", dr["language"].ToString());
                TemplateHandler.Assign(ref content, "imagetypevalue", dr["imagetype"].ToString());
                TemplateHandler.Assign(ref content, "sizevalue", dr["size"].ToString());
                TemplateHandler.Assign(ref content, "bannerid", dr["bannerid"].ToString());
                TemplateHandler.Assign(ref content, "currencyvalue", dr["currency"].ToString());
                return content;

            }

            return string.Empty;
        }

        // ++ Modified by Frank.Ngo on IRP 671 - 10 Jan 2012
        // add "type" parameter to indicate normal banner or media banner
        public static string GetBannerList(string category, bool isSearchByParentCateogry, string language, string imageType, string size, string currency, int pageIndex, ref int minNo, ref int maxNo, ref int totalBanner, ref int totalPage, int pageSize, int type, int afftype)
        {
            DbResult result = null;
            int nRows = 0;
            result = (isSearchByParentCateogry)
                ? DataHandler.Banner.GetBannerListByParentCategory(category, language, imageType, size, currency, pageIndex, pageSize, type, afftype)
                : DataHandler.Banner.GetBannerList(category, language, imageType, size, currency, pageIndex, pageSize, type, afftype);
            nRows = CastAide.AsInt32(result.Outputs["nrows"]);
            int pagesize = pageSize;
            int cols = 10;
            minNo = 0;
            maxNo = 0;
            if (nRows > 0)
            {
                string temp = TemplateHandler.LoadTemplate("Media/MainListBannerNew.htm");
                    
                string content = TemplateHandler.CopyBlock(temp, "Content");
                TemplateHandler.RemoveBlock(ref content, "UpdateMode1");
                TemplateHandler.RemoveBlock(ref content, "UpdateMode2");
                int totalpage = CastAide.AsInt32(result.Outputs["totalpages"]);
                DataTable dtbResult = result.ResultSet.Tables[0];
                StringBuilder contentList = new StringBuilder();
                int counter = 0;
                minNo = (pageIndex - 1) * pagesize + 1;
                foreach (DataRow dr in dtbResult.Rows)
                {
                    counter++;
                    string rowText = content;
                    if (counter % 2 == 0)
                    {
                        TemplateHandler.Assign(ref rowText, "rowstyle", "RecordFirst");
                    }
                    else{

                        TemplateHandler.Assign(ref rowText, "rowstyle", "RecordSecond");
                    }
                    
                    string[] strSize = dr["size"].ToString().Split('x');
                    int realWidth = CastAide.AsInt32(strSize[0]);
                    int realHeight = CastAide.AsInt32(strSize[1]);
                    TemplateHandler.Assign(ref rowText, "real-width", realWidth);
                    TemplateHandler.Assign(ref rowText, "real-height", realHeight);
                    string bannerType = string.Empty;
                    if (System.IO.Path.GetExtension(dr["filename"].ToString()) == ".swf")
                    {
                        TemplateHandler.RemoveBlock(ref rowText, "limit-img");
                        bannerType = "flash";
                    }
                    else
                    {
                        TemplateHandler.RemoveBlock(ref rowText, "limit-fls");
                        bannerType = "image";
                    }
                    if (realWidth > 500 || realHeight > 500)
                    {
                        TemplateHandler.Assign(ref rowText, "css-class", "hand");
                        string viewFull = string.Format("DoCal('BannerDetail.aspx?bannertype={0}&imagevalue={1}&lastupdate={2}&width={3}&height={4}','{5}',{6} + 100,{7} + 50);",
                                                bannerType, dr["filename"].ToString(), dr["Lastupdate"].ToString(), realWidth, realHeight, dr["bannerid"].ToString(),
                                                realWidth, realHeight);
                        TemplateHandler.Assign(ref rowText, "viewfull", viewFull);

                        if (realWidth > realHeight)
                        {
                            realHeight = 500 * realHeight / realWidth;
                            realWidth = 500;
                        }
                        else
                        {
                            realWidth = 500 * realWidth / realHeight;
                            realHeight = 500;
                        }
                    }
                    else
                    {
                        TemplateHandler.Assign(ref rowText, "css-class", string.Empty);
                        TemplateHandler.Assign(ref rowText, "viewfull", string.Empty);
                    }
                    
                    TemplateHandler.Assign(ref rowText, "No-val", (pageIndex - 1) * pagesize + counter);
                    TemplateHandler.Assign(ref rowText, "order-val", (pageIndex - 1) * pagesize + counter);
                    TemplateHandler.Assign(ref rowText, "lastupdate", dr["Lastupdate"].ToString());
                    TemplateHandler.Assign(ref rowText, "imagevalue", dr["filename"].ToString());
                    TemplateHandler.Assign(ref rowText, "filenamevalue", dr["filename"].ToString());                   
                    TemplateHandler.Assign(ref rowText, "catevalue", dr["category"].ToString());
                    TemplateHandler.Assign(ref rowText, "langvalue", dr["language"].ToString());
                    TemplateHandler.Assign(ref rowText, "imagetypevalue", dr["imagetype"].ToString());
                    TemplateHandler.Assign(ref rowText, "sizevalue", dr["size"].ToString());
                    TemplateHandler.Assign(ref rowText, "width", realWidth);
                    int intWidth = CastAide.AsInt32(strSize[0]);
                    TemplateHandler.Assign(ref rowText, "height", realHeight);
                    TemplateHandler.Assign(ref rowText, "tdheight", intWidth > 515 ? CastAide.AsString(CastAide.AsInt32(strSize[1]) + 17, string.Empty) + "px" : "auto");
                    TemplateHandler.Assign(ref rowText, "bannerid", dr["bannerid"].ToString());
                    TemplateHandler.Assign(ref rowText, "currencyvalue", dr["currency"].ToString());
                    contentList.Append(rowText);

                }
                maxNo = (pageIndex - 1) * pagesize + counter;
                totalBanner = nRows;
                if (totalpage > 0)
                {
                    //contentList.Append(Common.GenPagingFooter("", totalpage, pageIndex, cols));
                    totalPage = totalpage;
                }
                return contentList.ToString();
    
            }
            else
            {
                return Common.NoInfo(cols);   
            }
      
        }

        public static string GetBannerListOld(string category, string language, string imageType, string size, string currency, int pageIndex, ref int minNo, ref int maxNo, ref int totalBanner, ref int totalPage, int pageSize)
        {

            DbResult result = null;
            int nRows = 0;
            result = DataHandler.Banner.GetBannerListOld(category, language, imageType, size, currency, pageIndex, pageSize, Common.NormalBanner);
            nRows = CastAide.AsInt32(result.Outputs["nrows"]);
            int pagesize = pageSize;
            int cols = 10;
            minNo = 0;
            maxNo = 0;
            if (nRows > 0)
            {
                string temp = TemplateHandler.LoadTemplate("Media/MainListBanner.htm");
                string content = TemplateHandler.CopyBlock(temp, "Content");
                TemplateHandler.RemoveBlock(ref content, "UpdateMode1");
                TemplateHandler.RemoveBlock(ref content, "UpdateMode2");
                int totalpage = CastAide.AsInt32(result.Outputs["totalpages"]);
                DataTable dtbResult = result.ResultSet.Tables[0];
                StringBuilder contentList = new StringBuilder();
                int counter = 0;
                minNo = (pageIndex - 1) * pagesize + 1;
                foreach (DataRow dr in dtbResult.Rows)
                {
                    counter++;
                    string rowText = content;
                    if (counter % 2 == 0)
                    {
                        TemplateHandler.Assign(ref rowText, "rowstyle", "RecordFirst");
                    }
                    else
                    {

                        TemplateHandler.Assign(ref rowText, "rowstyle", "RecordSecond");
                    }

                    string[] strSize = dr["size"].ToString().Split('x');
                    int realWidth = CastAide.AsInt32(strSize[0]);
                    int realHeight = CastAide.AsInt32(strSize[1]);
                    TemplateHandler.Assign(ref rowText, "real-width", realWidth);
                    TemplateHandler.Assign(ref rowText, "real-height", realHeight);
                    string bannerType = string.Empty;
                    if (System.IO.Path.GetExtension(dr["filename"].ToString()) == ".swf")
                    {
                        TemplateHandler.RemoveBlock(ref rowText, "limit-img");
                        bannerType = "flash";
                    }
                    else
                    {
                        TemplateHandler.RemoveBlock(ref rowText, "limit-fls");
                        bannerType = "image";
                    }
                    if (realWidth > 500 || realHeight > 500)
                    {
                        TemplateHandler.Assign(ref rowText, "css-class", "hand");
                        string viewFull = string.Format("DoCal('BannerDetail.aspx?bannertype={0}&imagevalue={1}&lastupdate={2}&width={3}&height={4}','{5}',{6} + 100,{7} + 50);",
                                                bannerType, dr["filename"].ToString(), dr["Lastupdate"].ToString(), realWidth, realHeight, dr["bannerid"].ToString(),
                                                realWidth, realHeight);
                        TemplateHandler.Assign(ref rowText, "viewfull", viewFull);

                        if (realWidth > realHeight)
                        {
                            realHeight = 500 * realHeight / realWidth;
                            realWidth = 500;
                        }
                        else
                        {
                            realWidth = 500 * realWidth / realHeight;
                            realHeight = 500;
                        }
                    }
                    else
                    {
                        TemplateHandler.Assign(ref rowText, "css-class", string.Empty);
                        TemplateHandler.Assign(ref rowText, "viewfull", string.Empty);
                    }

                    TemplateHandler.Assign(ref rowText, "No-val", (pageIndex - 1) * pagesize + counter);
                    TemplateHandler.Assign(ref rowText, "order-val", (pageIndex - 1) * pagesize + counter);
                    TemplateHandler.Assign(ref rowText, "lastupdate", dr["Lastupdate"].ToString());
                    TemplateHandler.Assign(ref rowText, "imagevalue", dr["filename"].ToString());
                    TemplateHandler.Assign(ref rowText, "filenamevalue", dr["filename"].ToString());
                    TemplateHandler.Assign(ref rowText, "catevalue", dr["category"].ToString());
                    TemplateHandler.Assign(ref rowText, "langvalue", dr["language"].ToString());
                    TemplateHandler.Assign(ref rowText, "imagetypevalue", dr["imagetype"].ToString());
                    TemplateHandler.Assign(ref rowText, "sizevalue", dr["size"].ToString());
                    TemplateHandler.Assign(ref rowText, "width", realWidth);
                    int intWidth = CastAide.AsInt32(strSize[0]);
                    TemplateHandler.Assign(ref rowText, "height", realHeight);
                    TemplateHandler.Assign(ref rowText, "tdheight", intWidth > 515 ? CastAide.AsString(CastAide.AsInt32(strSize[1]) + 17, string.Empty) + "px" : "auto");
                    TemplateHandler.Assign(ref rowText, "bannerid", dr["bannerid"].ToString());
                    TemplateHandler.Assign(ref rowText, "currencyvalue", dr["currency"].ToString());
                    contentList.Append(rowText);

                }
                maxNo = (pageIndex - 1) * pagesize + counter;
                totalBanner = nRows;
                if (totalpage > 0)
                {
                    //contentList.Append(Common.GenPagingFooter("", totalpage, pageIndex, cols));
                    totalPage = totalpage;
                }
                return contentList.ToString();

            }
            else
            {
                return Common.NoInfo(cols);
            }

        }
    }
}
