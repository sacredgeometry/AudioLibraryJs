using AudioLibrary.DataContext.OrmLiteRepositories;
using AudioLibrary.DataInterface;
using Funq;
using ServiceStack.MiniProfiler;
using ServiceStack.MiniProfiler.Data;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;

namespace AudioLibrary.Web.App_Start
{
    public class OrmLiteConfigure
    {
        public static void Initialize(Container container, string connectionString)
        {
            container.Register<IDbConnectionFactory>(c =>
                                                     new OrmLiteConnectionFactory(connectionString,
                                                                                  SqlServerOrmLiteDialectProvider
                                                                                      .Instance)
                                                         {
                                                             ConnectionFilter =
                                                                 x => new ProfiledDbConnection(x, Profiler.Current)
                                                         });

            var todoRepo = (ToDoOrmLiteRepository) container.Resolve<IToDoRepository>();
            todoRepo.CreateMissingTables();
        }
    }
}