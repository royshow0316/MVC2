using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Models.Interface
{
    public interface IStructureRepository : IDisposable
    {
        void Create(Structure entity);

        void Update(Structure entity);

        void Delete(Structure entity);

        Structure Get(Guid projectId);

        IQueryable<Structure> GetAll();

        void SaveChanges();
    }
}
