using TheDojoLeague.Models;    //because BaseEntity is in this namespace
using System.Collections.Generic;

namespace DapperApp.Factories
{
    public interface IFactoryDojo<T> where T : BaseEntityDojo
    {
        void Add(T instance);
        // T FindByID(int id);
        IEnumerable<T> FindAll();
        //what else might you want to include here?
    }
}