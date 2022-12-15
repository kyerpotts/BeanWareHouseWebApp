using FALib2;

namespace BeanWareHouseWebApp.Models
{
    public sealed class BeanWarehouseSingleton
    {
        private static BeanWarehouseSingleton? instance = null;
        private static readonly object padlock = new object();

        private static BeanWarehouse? beanWarehouse;

        BeanWarehouseSingleton() {
            beanWarehouse = new BeanWarehouse();
        }

        public static BeanWarehouseSingleton Instance
        {
            get
            {
                lock (padlock)
                {
                    if(instance == null)
                    {
                        instance = new BeanWarehouseSingleton();
                    }
                    return instance;
                }

            }
        }

        public Bean[] GetBeans()
        {
            return beanWarehouse.GetBeans();
        }

        public void AddBean(Bean bean)
        {
            beanWarehouse.AddBean(bean);
        }

        public void DeleteBean(int index)
        {
            beanWarehouse.DeleteBean(index);
        }

        public void ReplaceBean(Bean bean, int index)
        {
            beanWarehouse.ReplaceBean(bean, index);
        }

    }
}
