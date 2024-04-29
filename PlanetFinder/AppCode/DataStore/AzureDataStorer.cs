using PlanetFinder.AppCode.DataAnalysis;
using PlanetFinder.AppCode.DataObjects;
using System.Data;
using System.Data.SqlClient;
using System.Numerics;

namespace PlanetFinder.AppCode.DataStore
{
    public class AzureDataStorer : DataStorer
    {
        protected const string ConnectionString = @"Server=tcp:planet-finder.database.windows.net,1433;Initial Catalog=planet-finder;Persist Security Info=False;User ID=planetfinderapp;Password=Pa$$w0rd;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public override bool SavePlanetsToStore(IList<IPlanet> planets)
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

        public override IList<DataStorePlanet> GetAllPlanets()
        {
            const string SelectQuery = @"SELECT * FROM dbo.Planets";

            DataTable results = executeQuery(SelectQuery, null);

            return results.Rows.OfType<DataRow>()
                .Select(dr => new DataStorePlanet()
                {
                    ID = dr.Field<int>("ID"),
                    Name = dr.Field<string>("Name"),
                    Climate = dr.Field<string>("Climate"),
                    Gravity = dr.Field<string>("Gravity"),
                    Population = dr.Field<int?>("Population"),
                    SurfaceWater = dr.Field<decimal?>("SurfaceWater"),
                    Terrain = dr.Field<string>("Terrain")
                })
                .ToList();
        }

        public override bool DeleteAllPlanets()
        {
            const string DeleteQuery = @"DELETE FROM dbo.Planets";

            return executeNonQuery(DeleteQuery, null);
        }

        public override bool SavePlanetGroupsToStore(IList<IPlanetGroup> planetGroups)
        {
            const string InsertQuery = @"
INSERT INTO dbo.PlanetGroups
(
    Climate,
    Terrain,
    PlanetCount
)
SELECT
    @Climate,
    @Terrain,
    @PlanetCount
";
            // If there are no planet groups to save then the save was successful
            if (planetGroups == null || !planetGroups.Any())
                return true;
            bool anyInsertFailed = false;

            foreach (IPlanetGroup planetGroup in planetGroups)
            {
                List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter("@Climate", (object)planetGroup.Climate ?? DBNull.Value),
                    new SqlParameter("@Terrain", (object)planetGroup.Terrain ?? DBNull.Value),
                    new SqlParameter("@PlanetCount", planetGroup.PlanetCount)
                };

                if (!executeNonQuery(InsertQuery, parameters))
                    anyInsertFailed = true;
            }

            return !anyInsertFailed;
        }

        public override IList<DataStorePlanetGroup> GetAllPlanetGroups()
        {
            const string SelectQuery = @"SELECT * FROM dbo.PlanetGroups";

            DataTable results = executeQuery(SelectQuery, null);

            return results.Rows.OfType<DataRow>()
                .Select(dr => new DataStorePlanetGroup()
                {
                    ID = dr.Field<int>("ID"),
                    Climate = dr.Field<string>("Climate"),
                    Terrain = dr.Field<string>("Terrain"),
                    PlanetCount = dr.Field<int>("PlanetCount")
                })
                .ToList();
        }

        public override bool DeleteAllPlanetGroups()
        {
            const string DeleteQuery = @"DELETE FROM dbo.PlanetGroups";

            return executeNonQuery(DeleteQuery, null);
        }

        public override DataStorePlanetGroup GetPlanetGroup(int ID)
        {
            const string SelectQueryFormatString = @"SELECT * FROM dbo.PlanetGroups WHERE ID = {0}";

            string selectQuery = string.Format(SelectQueryFormatString, ID);

            DataTable dt = executeQuery(selectQuery, null);

            return new DataStorePlanetGroup()
            {
                ID = dt.Rows[0].Field<int>("ID"),
                Climate = dt.Rows[0].Field<string>("Climate"),
                Terrain = dt.Rows[0].Field<string>("Terrain"),
                PlanetCount = dt.Rows[0].Field<int>("PlanetCount")
            };
        }

        public override IList<DataStorePlanet> GetMatchingPlanets(string climate, string terrain)
        {
            string selectQuery = @"SELECT * FROM dbo.Planets WHERE ";

            List<SqlParameter> parameters = new List<SqlParameter>();

            // Can't just do a simple LIKE because "forest" will match on "rainforest"
            // Look for the filter being either the first word, or preceded by a space
            // This got way more complicated than I thought it would
            // Would redo this so the climate / terrain attributes are split out and stored in another table, to make it easier to query
            if (string.IsNullOrWhiteSpace(climate))
                selectQuery += " Planets.Climate IS NULL ";
            else
            {
                selectQuery += " (Planets.Climate LIKE @Climate OR Planets.Climate LIKE @Climate + ',%' OR Planets.Climate LIKE '% ' + @Climate OR Planets.Climate LIKE '% ' + @Climate + ',%' OR Planets.Climate LIKE @Climate + 's' OR Planets.Climate LIKE @Climate + 's,%' OR Planets.Climate LIKE '% ' + @Climate + 's' OR Planets.Climate LIKE '% ' + @Climate + 's,%')";
                parameters.Add(new SqlParameter("@Climate", climate));
            }

            if (string.IsNullOrWhiteSpace(terrain))
                selectQuery += " AND Planets.Terrain IS NULL ";
            else
            {
                selectQuery += " AND (Planets.Terrain LIKE @Terrain OR Planets.Terrain LIKE @Terrain + ',%' OR Planets.Terrain LIKE '% ' + @Terrain OR Planets.Terrain LIKE '% ' + @Terrain + ',%' OR Planets.Terrain LIKE @Terrain + 's' OR Planets.Terrain LIKE @Terrain + 's,%' OR Planets.Terrain LIKE '% ' + @Terrain + 's' OR Planets.Terrain LIKE '% ' + @Terrain + 's,%')";
                parameters.Add(new SqlParameter("@Terrain", terrain));
            }

            DataTable results = executeQuery(selectQuery, parameters);

            return results.Rows.OfType<DataRow>()
                .Select(dr => new DataStorePlanet()
                {
                    ID = dr.Field<int>("ID"),
                    Name = dr.Field<string>("Name"),
                    Climate = dr.Field<string>("Climate"),
                    Gravity = dr.Field<string>("Gravity"),
                    Population = dr.Field<int?>("Population"),
                    SurfaceWater = dr.Field<decimal?>("SurfaceWater"),
                    Terrain = dr.Field<string>("Terrain")
                })
                .ToList();
        }

        private static DataTable executeQuery(string query, List<SqlParameter> parameters)
        {
            DataTable queryResults = new DataTable();

            // Should wrap this in a try / catch block, with logging any exceptions and returning false
            // But for now just have any exceptions carry through

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                if (parameters != null && parameters.Any())
                {
                    foreach (SqlParameter parameter in parameters)
                        cmd.Parameters.Add(parameter);
                }

                connection.Open();

                adapter.Fill(queryResults);

                connection.Close();
            }

            return queryResults;
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
