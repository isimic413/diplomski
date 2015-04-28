using System;
using System.Text;

namespace ExamPreparation.Common.Filters
{
    public class ExamFilter : IFilter
    {
        public string SortOrder { get; private set; }
        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }

        private readonly int DefaultPageSize = 20;
        private readonly string DefaultSortOrder = "TestingAreaId, Year, Month";

        public ExamFilter(string sortOrder, int pageNumber, int pageSize)
        {
            try
            {
                SortOrder = SetSortOrder(sortOrder, "");
                SetPageNumberAndSize(pageNumber, pageSize);
            }
            catch (ArgumentException e)
            {
                throw e;
            }
        }

        public ExamFilter(string sortOrder, string sortDirection, int pageNumber, int pageSize)
        {
            try
            {
                SortOrder = SetSortOrder(sortOrder, sortDirection);
                SetPageNumberAndSize(pageNumber, pageSize);
            }
            catch (ArgumentException e)
            {
                throw e;
            }
        }
        private void SetPageNumberAndSize(int pageNumber = 1, int pageSize = 0)
        {
            PageNumber = (pageNumber > 0) ? pageNumber : 1;
            PageSize = (pageSize > 0 && pageSize <= DefaultPageSize) ? pageSize : DefaultPageSize;
        }

        private string SetSortOrder(string sortOrder, string sortDirection)
        {
            switch (sortOrder)
            {
                case "Year":
                    return "Year" + SetSortDirection(sortDirection) + ", Month, TestingAreaId";

                case "TestingAreaId":
                    return "TestingAreaId" + SetSortDirection(sortDirection) + ", Year, Month";

                case "":
                    return DefaultSortOrder;

                default:
                    throw new ArgumentException("Invalid SortOrder.");
            }
        }

        private string SetSortDirection(string sortDirection)
        {
            if (sortDirection.ToLower() == "asc" || sortDirection == "")
            {
                return "";
            }
            else if (sortDirection.ToLower() == "desc")
            {
                return " descending";
            }
            else
            {
                throw new ArgumentException("Invalid SortDirection.");
            }
        }
    }
}
