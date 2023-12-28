using eShopAPI.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore.InMemory.Storage.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace eShopAPI.Controllers.Tests
{
    public static class eShopDB
    {
        static StoreDBContext _storeDbContext;

        public static StoreDBContext storeDbContext
        {
            get
            {
                if (_storeDbContext == null)
                {
                    var options = new DbContextOptionsBuilder<StoreDBContext>().UseInMemoryDatabase("storedb");
                    _storeDbContext = new StoreDBContext(options.Options);
                    _storeDbContext.Database.EnsureCreated();
                }
                return _storeDbContext;
            }
        }
    } 
}