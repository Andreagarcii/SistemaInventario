using Microsoft.EntityFrameworkCore;
using SistemaInventario.AccesoDatos.Data;
using SistemaInventario.AccesoDatos.Repositorio.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesoDatos.Repositorio
{
    public class Repositorio<T> : IRepositorio <T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repositorio (ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        public async Task Agregar(T entidad)
        {
            await dbSet.AddAsync(entidad);//inser into
        }

        public async Task<T> obtener(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> ObtenerTodosAsync(Expression<Func<T, bool>> filtro = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string incluirPropiedades = null, bool IsTracking = true)
        {
            IQueryable<T> query = null;
            if(filtro != null) 
            {
                query = query.Where(filtro);
            }
            if(incluirPropiedades  != null)
            {
                foreach(var incluirProp in incluirPropiedades.Split(new char [] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluirProp);
                }
            }

            if(orderBy != null)
            {
                query = orderBy(query);
            }

            if (!IsTracking)
            {
                query=query.AsNoTracking();
            }
            return await query.ToListAsync();
        }
        

        public async Task<T> ObtenerPrimero(Expression<Func<T, bool>> filtro = null, string incluirPropiedades = null, bool IsTracking = true)
        {
            IQueryable<T> query = null;
            if (filtro != null)
            {
                query = query.Where(filtro);
            }
            if (incluirPropiedades != null)
            {
                foreach (var incluirProp in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluirProp);
                }
            }
            if (!IsTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.FirstOrDefaultAsync();
        }



        public void Remove(T entidad)
        {
            dbSet.Remove(entidad);
        }

        public void RemoverRango(IEnumerable<T> entidad)
        {
            dbSet.RemoveRange(entidad);
        }

        IEnumerable<T> IRepositorio<T>.ObtenerTodosAsync(Expression<Func<T, bool>> filtro, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, string incluirPropiedades, bool IsTracking)
        {
            throw new NotImplementedException();
        }
    }
}


