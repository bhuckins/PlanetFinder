using PlanetFinder.AppCode.DataObjects;
using System.Data.SqlClient;

namespace PlanetFinder.AppCode.DataStore
{
    public class AzureDataStorer : DataStorer
    {
        protected const string ConnectionString = @"Server=tcp:planet-finder.database.windows.net,1433;Initial Catalog=planet-finder;Persist Security Info=False;User ID=planetfinderapp;Password=Pa$$w0rd;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        //protected const string ConnectionString = @"Server=tcp:planet-finder.database.windows.net,1433;Initial Catalog=planet-finder;Persist Security Info=False;User ID=sqladmin;Password=admin123!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public override bool SaveToStore(IList<IPlanet> planets)
        {
            const string InsertQuery = @"
INSERT INTO dbo.Planets
(
    Name,
    Climate,
    Gravity,
    Population,
    SurfaceWater,
    Terrain
)
SELECT
    @Name,
    @Climate,
    @Gravity,
    @Population,
    @SurfaceWater,
    @Terrain
";
            // If there are no planets to save then the save was successful
            if (planets == null || !planets.Any())
                return true;

            bool anyInsertFailed = false;

            foreach (IPlanet planet in planets)
            {
                List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter("@Name", planet.Name),
                    new SqlParameter("@Climate", (object)planet.Climate ?? DBNull.Value),
                    new SqlParameter("@Gravity", (object)planet.Gravity ?? DBNull.Value),
                    new SqlParameter("@Population", (object)planet.Population ?? DBNull.Value),
                    new SqlParameter("@SurfaceWater", (object)planet.SurfaceWater ?? DBNull.Value),
                    new SqlParameter("@Terrain", (object)planet.Terrain ?? DBNull.Value)
                };

                if (!executeNonQuery(InsertQuery, parameters))
                    anyInsertFailed = true;
            }

            return !anyInsertFailed;
        }

        public override IList<IPlanet> GetAllPlanets()
        {
            throw new NotImplementedException();
        }

        public override bool DeleteAllPlanets()
        {
            const string DeleteQuery = @"DELETE FROM dbo.Planets";

            return executeNonQuery(DeleteQuery, null);
        }

        private static bool executeNonQuery(string query, List<SqlParameter> parameters)
        {
            // Should wrap this in a try / catch block, with logging any exceptions and returning false
            // But for now just have any exceptions carry through

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                if (parameters != null && parameters.Any())
                {
                    foreach (SqlParameter parameter in parameters)
                        cmd.Parameters.Add(parameter);
                }

                connection.Open();

                cmd.ExecuteNonQuery();

                connection.Close();
            }

            return true;
        }
    }
}
