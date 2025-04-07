using ManagementSystem.DAL.SqlServer.Context;
using ManagementSystem.Domain.Entities;
using ManagementSystem.Repository.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ManagementSystem.DAL.SqlServer.Infrastructure;

public class SqlCustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _appDbContext;

    public SqlCustomerRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task AddAsync(Customer customer)
    {
        await _appDbContext.AddAsync(customer);
        await _appDbContext.SaveChangesAsync();

    }

    public async Task<bool> Delete(int id, int deletedBy)
    {
        var costumer = await _appDbContext.Customers.FindAsync(id);
        if (costumer == null)
        {
            return false;
        }

        costumer.IsDeleted = true;
        costumer.DeletedBy = deletedBy;
        costumer.DeletedDate = DateTime.Now;

        _appDbContext.Customers.Update(costumer);
        await _appDbContext.SaveChangesAsync();
        return true;
    }

    public IQueryable<Customer> GetAll()
    {
        return _appDbContext.Customers.Where(c => c.IsDeleted);
    }

    public async Task<IEnumerable<Customer>> GetAllInitialDataAsync()
    {
        return await _appDbContext.Customers
            .Where(c => !c.IsDeleted).ToListAsync();
    }

    public async Task<Customer> GetByIdAsync(int id)
    {
        return await _appDbContext.Customers.FirstOrDefaultAsync(c=>c.Id == id && !c.IsDeleted);
    }

    public void Update(Customer customer)
    {
        _appDbContext.Customers.Update(customer);
        _appDbContext.SaveChanges();
    }
}
