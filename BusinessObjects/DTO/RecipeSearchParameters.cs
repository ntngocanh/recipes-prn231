using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
    public class RecipeSearchParameters
    {
        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }

        public DateTime MinDate { get; set; } = DateTime.Now.AddMonths(-1);
        public DateTime MaxDate { get; set; } = DateTime.Now;
        public bool ValidYearRange => MaxDate > MinDate;

    }
}
