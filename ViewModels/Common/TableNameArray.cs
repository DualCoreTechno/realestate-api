namespace ViewModels.Common
{
    public static class TableNameArray
    {
        public static readonly string[] MasterTableName = new[] 
        { 
            "Tbl_Activity", 
            "Tbl_ActivityChild", 
            "Tbl_ActivityParent",
            "Tbl_Area",
            "Tbl_BhkOffice",
            "Tbl_Budget",
            "Tbl_Buildings",
            "Tbl_DraftReason",
            "Tbl_EnquiryStatus",
            "Tbl_FurnitureStatus",
            "Tbl_Measurement",
            "Tbl_Nonuse",
            "Tbl_PropertyType",
            "Tbl_Price",
            "Tbl_PropertyStatus",
            "Tbl_Purpose",
            "Tbl_Role",
            "Tbl_Segment",
            "Tbl_Source"
        };

        public static readonly string[] TransactionTableName = new[]
        {
            "Tbl_Property",
            "Tbl_PropertyDeal",
            "Tbl_Enquiry"
        };

        public static readonly Dictionary<string, string?> TableAliasDictionary = new()
        {
            {"Tbl_Activity", "Activity"},
            {"Tbl_ActivityChild", "Child Activity"},
            {"Tbl_ActivityParent", "Parent Activity"},
            {"Tbl_Area", "Area"},
            {"Tbl_BhkOffice", "Bhk/Office"},
            {"Tbl_Budget", "Budget"},
            {"Tbl_Buildings", "Buildings"},
            {"Tbl_DraftReason", "Draft Reason"},
            {"Tbl_EnquiryStatus", "Enquiry Status"},
            {"Tbl_FurnitureStatus", "Furniture Status"},
            {"Tbl_Measurement", "Measurement"},
            {"Tbl_Nonuse", "Nonuse"},
            {"Tbl_PropertyType", "Property Type"},
            {"Tbl_Price", "Price"},
            {"Tbl_PropertyStatus", "Property Status"},
            {"Tbl_Purpose", "Purpose"},
            {"Tbl_Role", "Role"},
            {"Tbl_Role_Permission", "Role Permission"},
            {"Tbl_Segment", "Segment"},
            {"Tbl_Source", "Source"},
            {"Tbl_Users", "Users"},
            {"Tbl_MinMax", "Min Max"},
            {"Tbl_Property", "Property"},
            {"Tbl_PropertyDeal", "Property Deal"},
            {"Tbl_Enquiry", "Enquiry"},
            {"Tbl_EnquiryRemarks", "Enquiry Remarks"}
        };

        public static string? GetAliasForTable(string tableName)
        {
            if (TableAliasDictionary.TryGetValue(tableName, out string? alias))
            {
                return alias;
            }
            else
            {
                return null;
            }
        }
    }
}