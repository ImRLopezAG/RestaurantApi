using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Restaurant.Infrastructure.Persistence.Core;
using Restaurant.Core.Domain.Entities;
using Restaurant.Core.Application.Interfaces.Repositories;
using Restaurant.Infrastructure.Persistence.Context;

namespace Restaurant.Infrastructure.Persistence.Repositories;

public class TableRepository: GenericRepository<Table>, ITableRepository
{
    private readonly RestaurantContext _context;
    public TableRepository(RestaurantContext dbContext) : base(dbContext)=> _context = dbContext;
}
