using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dovido_Onay_Sitemi.Connection
{
   public class DbAccess
    {
        private static DovidoEntitie _db = null;
        public static DovidoEntitie Db()
        {
            if (_db == null)
            {
                _db = new DovidoEntitie();
            }
            return _db;
        }
    }
}
