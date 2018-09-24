using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestManager.DBModels
{
    public interface ISQLite //через этот интерфейс вызывается обработчик пути базы на устройстве. нужен из за различных ОС
    {
        string GetDatabasePath(string filename);
    }
}
