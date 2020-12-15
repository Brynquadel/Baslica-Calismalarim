
namespace Library.DataAccess
{
    internal static class XConfig<Tentity>
    {
        internal static string DetectWorkFileName(Tentity entity)
        {
            if (entity.GetType().Name.ToString() == "Book") return "Books.xml";
            else if (entity.GetType().Name.ToString() == "Student") return "Students.xml";
            else if (entity.GetType().Name.ToString() == "Setting") return "Settings.xml";
            else
                return "";
        }
    }
}
