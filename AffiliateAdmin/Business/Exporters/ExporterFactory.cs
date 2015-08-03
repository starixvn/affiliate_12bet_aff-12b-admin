namespace Exporters
{
    public class ExporterFactory
    {
         public static AffiliateListExporter GetExporter(string status)
         {
             switch (status)
             {
                 case "approved":
                     return new ApprovedAffiliateListExporter();
                 case "pending":
                     return new PendingAffiliateListExporter();
                 case "disapproved":
                     return new DisapprovedAffiliateListExporter();
                 case "closed":
                     return new ClosedAffiliateListExporter();
                 default:
                     return new ApprovedAffiliateListExporter();
             }
         }
    }
}