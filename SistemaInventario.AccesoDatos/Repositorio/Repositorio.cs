using Microsoft.EntityFrameworkCore;
using SistemaInventario.AccesoDatos.Data;
using SistemaInventario.AccesoDatos.Repositorio.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SistemaInventario.AccesoDatos.Repositorio
{
    public class Repositorio<T> : Irepositorio<T> where T : class
    {
        private readonly ApplicationDbContext _db;//solo para leer
        internal DbSet<T> dbSet; //llamar la informacion

        //ponerles la informacion constructor
        public Repositorio(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }


        public async Task Agregar(T entidad)
        {
            await dbSet.AddAsync(entidad); //insert into
        }

        public async Task<T> obtener(int id)
        {
            return await dbSet.FindAsync(id);

        }
        public async Task<IEnumerable<T>> ObtenerTodos(Expression<Func<T, bool>> filtro = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string incluirPropiedades = null, bool IsTracking = true)
        {
            IQueryable<T> query = dbSet;
            if (filtro != null)
            {
                query = query.Where(filtro); //Select * from tabla where........
            }
            if (incluirPropiedades != null)
            {
                foreach (var incluirProp in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluirProp);

                }
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (!IsTracking)
            {
                query = query.AsNoTracking();
            }
            return await query.ToListAsync();

        }



        public async Task<T> ObtenerPrimero(Expression<Func<T, bool>> filtro = null, string incluirPropiedades = null, bool IsTracking = true)
        {
            IQueryable<T> query = dbSet;
            if (filtro != null)
            {
                query = query.Where(filtro); //Select * from tabla where........
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
    }
}
