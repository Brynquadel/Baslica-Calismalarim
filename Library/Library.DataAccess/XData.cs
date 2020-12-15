using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess
{
    internal static class XData<TEntity>
    {
        internal static string IdentityPattern = "Identity";

        internal static string RootName(TEntity entity)
        {
            return entity.GetType().Name.ToString();
        }
    }
}
