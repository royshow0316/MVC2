using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MVC.Models.Interface;

namespace MVC.Models.Repository
{
    public class StructureRepository : IStructureRepository, IDisposable
    {
        protected ApplicationDbContext Context
        {
            get;
            private set;
        }

        public StructureRepository()
        {
            this.Context = new ApplicationDbContext();
        }

        public void Create(Structure instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                Context.Structure.Add(instance);
                this.SaveChanges();
            }
        }

        public void Update(Structure instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                Context.Entry(instance).State = EntityState.Modified;
                this.SaveChanges();
            }
        }

        public void Delete(Structure instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                Context.Entry(instance).State = EntityState.Deleted;
                this.SaveChanges();
            }
        }

        public Structure Get(Guid id)
        {
            return Context.Structure.FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<Structure> GetAll()
        {
            return Context.Structure.OrderBy(x => x.Id);
        }

        public void SaveChanges()
        {
            this.Context.SaveChanges();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.Context != null)
                {
                    this.Context.Dispose();
                    this.Context = null;
                }
            }
        }
    }
}