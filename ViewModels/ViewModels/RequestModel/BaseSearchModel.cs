using System.ComponentModel.DataAnnotations;

namespace ViewModels.ViewModels
{
    public class BaseSearchModel
    {
        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; } = 1;

        [Range(0, int.MaxValue)]
        public int RowPerPage { get; set; } = 10;

        public string SortBy { get; set; } = String.Empty;

        public int Offset => (PageNumber - 1) * RowPerPage;

        public int GetLimit() => RowPerPage;

        public string Text { get; set; } = String.Empty;

        //public ListSortDirectionEnum SortOrder { get; set; } = ListSortDirectionEnum.Ascending;
    }
}