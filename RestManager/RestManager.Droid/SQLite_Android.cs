using System;
using System.IO;
using RestManager.DBModels;
using RestManager.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_Android))]
namespace RestManager.Droid //���������� ���� DB �� ������ ��. ���������� ����. ���������� ����� ���������
{
    public class SQLite_Android : ISQLite
    {
        public SQLite_Android() { }
        public string GetDatabasePath(string sqliteFilename)
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, sqliteFilename);
            return path;
        }
    }
}