using OnlineShoppingStore.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShoppingStore.Repository
{
    public class GenericUnitOfWork: IDisposable
    {
        private readonly OnlineShoppingStoreEntities1 DBEntity = new OnlineShoppingStoreEntities1();
        public IRepository<Tbl_EntityType> GetRepositoryInstance<Tbl_EntityType>() where Tbl_EntityType : class {

            return new GenericRepository<Tbl_EntityType>(DBEntity);
        }
        public void SaveChanges() {

            DBEntity.SaveChanges();
        }

        protected void Dispose(bool disposing)
        {
            if (!this.disposed) {

                if (disposing) {
                    DBEntity.Dispose();
                }

            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool disposed = false;

    }
}