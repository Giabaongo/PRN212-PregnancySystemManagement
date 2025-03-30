using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using DAL.Models;

namespace DAL.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly PregnancyTrackingSystemContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(PregnancyTrackingSystemContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public virtual IEnumerable<T> GetAll()
    {
        return _dbSet.ToList();
    }

    public virtual T? GetById(int id)
    {
        return _dbSet.Find(id);
    }

    public virtual IEnumerable<T> Find(Expression<Func<T, bool>> expression)
    {
        return _dbSet.Where(expression);
    }

    public virtual void Add(T entity)
    {
        _dbSet.Add(entity);
        _context.SaveChanges();
    }

    public virtual void AddRange(IEnumerable<T> entities)
    {
        _dbSet.AddRange(entities);
        _context.SaveChanges();
    }

    public virtual void Remove(T entity)
    {
        _dbSet.Remove(entity);
        _context.SaveChanges();
    }

    public virtual void RemoveRange(IEnumerable<T> entities)
    {
        _dbSet.RemoveRange(entities);
        _context.SaveChanges();
    }

    public virtual void Update(T entity)
    {
        _dbSet.Update(entity);
        _context.SaveChanges();
    }
}