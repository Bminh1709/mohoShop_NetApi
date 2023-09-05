using mohoShop.Models;

namespace mohoShop.Helpers
{
    public class Paging
    {
        public ICollection<T> Paginate<T>(ICollection<T> list, int page = 1)
        {
            // Set items per page is 10 items
            int pageSize = 2;

            var totalCount = list.Count;
            var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);

            var itemsPerPage = list
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return itemsPerPage;
        }
    }
}
