﻿using Application.Contracts.General;
using DTOs.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technical_Assessment_Overview.Product;

namespace Application.Contracts.Product
{
    public interface IProductRepository: IGenericRepository<ProductP>
    {
        Task DeleteRangeAsync(IEnumerable<ProductP> entities);
        public Task<IEnumerable<ProductP>> SearchByNameAsync(string name);
        public Task<EntityPaginated<ProductP>> GetAllPaginatedAsync(int pageNumber, int count);
        public Task<IQueryable<ProductP>> GetFilteredProductsAsync();
    }
}
