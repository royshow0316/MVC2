using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Models.Interface
{
    public interface IStructureRepository : IDisposable
    {
        void Create(Structure instance);

        void Update(Structure instance);

        void Delete(Structure instance);

        Structure Get(Guid projectId);

        IQueryable<Structure> GetAll();

        void SaveChanges();
    }
}
