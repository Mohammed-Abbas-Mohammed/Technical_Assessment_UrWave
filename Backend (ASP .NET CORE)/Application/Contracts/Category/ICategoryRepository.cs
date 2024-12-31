using Application.Contracts.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technical_Assessment_Overview.Category;

namespace Application.Contracts.Category
{
    public interface ICategoryRepository: IGenericRepository<CategoryC>
    {
        Task<IEnumerable<CategoryC>> GetByNameAsync(string categoryName);
    }
}
