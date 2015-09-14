using System.Collections.Generic;
using System.Linq;
using Confidami.Model;
using Dapper;

namespace Confidami.Data
{
    public class CategoryRepository : BaseRepository
    {
        public IEnumerable<Category> GetCategories()
        {
            using (var conn = DbUtilities.Connection)
            {
                return conn.Query<Category>(QueryPostStore.AllCategory);
            }
        }

        public Category GetCategory(int idcategory)
        {
            using (var conn = DbUtilities.Connection)
            {
                return conn.Query<Category>(QueryPostStore.SingleCategory, new { idcategory }).SingleOrDefault();
            }
        }

    }
}