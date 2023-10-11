using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Inferastructure.Repository
{
    public class BaseRepository<T> : IGenric<T> where T : class
    {
        protected BnanKSAContext _context;
        

        public BaseRepository(BnanKSAContext context
           )
        {
            _context = context;
        }

        public IEnumerable<T> GetAll(string[] includes = null)
        {
            try
            {
                IQueryable<T> query = _context.Set<T>();

                if (includes != null)
                {
                    foreach (var include in includes)
                        query = query.Include(include);
                }
                return query.ToList();
            }
            catch (Exception)
            {

                return null;
            }
        }




        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await _context.Set<T>().ToListAsync();
            }
            catch (Exception)
            {

                return null;
            }
        }

        public T GetById(string id)
        {
            try
            {
                return _context.Set<T>().Find(id);
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<T> GetByIdAsync(string id)
        {
            try
            {
                return await _context.Set<T>().FindAsync(id);
            }
            catch (Exception)
            {

                return null;
            }
        }

        public T Find(Expression<Func<T, bool>> predicate, string[] includes = null)
        {
            try
            {
                IQueryable<T> query = _context.Set<T>();
                query = query.Where(predicate);

                if (includes != null)
                {
                    foreach (var include in includes)
                        query = query.Include(include);
                }
                return query.FirstOrDefault(predicate);
            }
            catch (Exception)
            {

                return null;
            }
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, string[] includes = null)
        {

            try
            {
                IQueryable<T> query = _context.Set<T>();
                query = query.Where(criteria);

                if (includes != null)
                {
                    foreach (var include in includes)
                        query = query.Include(include);
                }
                return query.Where(criteria).ToList();
            }
            catch (Exception)
            {

                return null;
            }
        }

       
        public T Add(T entity)
        {
            try
            {

                _context.Set<T>().Add(entity);
                return entity;

            }
            catch (Exception)
            {

                return null;
            }
        }
        public async Task<T> AddAsync(T entity)
        {
            try
            {

                await _context.Set<T>().AddAsync(entity);
                return entity;

            }
            catch (Exception)
            {

                return null;
            }
        }

        public IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            try
            {

                _context.Set<T>().AddRange(entities);
                return entities;

            }
            catch (Exception)
            {

                return null;
            }
        }

        public T Update(T entity)
        {
            _context.Update(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public void Attach(T entity)
        {
            _context.Set<T>().Attach(entity);
        }

        public void AttachRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AttachRange(entities);
        }

        public int Count()
        {
            try
            {
                return _context.Set<T>().Count();
            }
            catch (Exception)
            {

                return 0;
            }

        }

        public int Count(Expression<Func<T, bool>> criteria)
        {
            try
            {
                return _context.Set<T>().Count(criteria);
            }
            catch (Exception)
            {

                return 0;
            }
        }

        public async Task<int> CountAsync()
        {
            try
            {
                return await _context.Set<T>().CountAsync();
            }
            catch (Exception)
            {
                return 0;

            }
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> criteria)
        {
            try
            {
                return await _context.Set<T>().CountAsync(criteria);
            }
            catch (Exception)
            {

                return 0;
            }
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> predicate, string[] includes = null)
        {
            try
            {
                IQueryable<T> query = _context.Set<T>();
                query = query.Where(predicate);

                if (includes != null)
                {
                    foreach (var include in includes)
                        query = query.Include(include);
                }
                return await query.FirstOrDefaultAsync(predicate);
            }
            catch (Exception)
            {

                return null;
            }
        }


        //public string UploadPhoto(IFormFile image, string path)
        //{
        //    IFormFile formFile = image;
        //    var fileName = Guid.NewGuid().ToString() + formFile.FileName;
        //    var folderPath = Path.Combine(_environment.WebRootPath, path);
        //    var ImagePath = Path.Combine(folderPath, fileName);
        //    var ImageUrl = Path.Combine(path, fileName);
        //    if (!Directory.Exists(folderPath))
        //    {
        //        Directory.CreateDirectory(folderPath);
        //    }
        //    using (var fileStream = new FileStream(ImagePath, FileMode.Create))
        //    {
        //        formFile.CopyToAsync(fileStream);
        //        fileStream.Flush();
        //    }
        //    return ImageUrl;
        //}

        //public void DeletePhoto(string imagePath)
        //{
        //    var PathImage = Path.Combine(_environment.WebRootPath, imagePath);
        //    if (System.IO.File.Exists(PathImage))
        //        System.IO.File.Delete(PathImage);
        //}




    }
}
