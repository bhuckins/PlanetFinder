using PlanetFinder.AppCode.DataAnalysis;
using PlanetFinder.AppCode.DataCollection;
using PlanetFinder.AppCode.DataObjects;
using PlanetFinder.AppCode.DataStore;

namespace PlanetFinder.AppCode.Gateway
{
    public class DataAnalysisGateway
    {
        protected DataStorer DataStorer { get; set; }

        protected DataAnalyzer DataAnalyzer { get; set; }

        public DataAnalysisGateway(DataStorer dataStorer, DataAnalyzer dataAnalzyer)
        {
            this.DataStorer = dataStorer;
            this.DataAnalyzer = dataAnalzyer;
        }

        public List<string> AnalyzeAndStoreResults(bool deleteExistingPlanetGroups, out bool success)
        {
            // Should be a more robust logger, but use a list of strings for now
            List<string> log = new List<string>();

            if (DataStorer == null || DataAnalyzer == null)
            {
                log.Add("DataStorer or DataAnalyzer are null"); // Not the best error message, but this should never happen anyway
                success = false;
                return log;
            }

            log.Add("Getting planet data from store");

            IList<DataStorePlanet> planets = DataStorer.GetAllPlanets();

            if (planets == null || !planets.Any())
            {
                log.Add("Received no planets from data store");
                success = false;
                return log;
            }

            log.Add(string.Format("Receieved {0:n0} planets", planets.Count));

            log.Add("Analyzing planet data");

            IList<IPlanetGroup> planetGroups = DataAnalyzer.GetPlanetGroups(planets.OfType<IPlanet>().ToList());

            log.Add(string.Format("Analysis scuessful, found {0:n0} groups", planetGroups.Count));

            if (deleteExistingPlanetGroups)
            {
                log.Add("Deleting existing planet groups from data store");

                DataStorer.DeleteAllPlanetGroups();
            }

            log.Add("Saving analysis results to data store");

            if (DataStorer.SavePlanetGroupsToStore(planetGroups))
            {
                log.Add("Save successful");
                success = true;
            }
            else
            {
                log.Add("Save unsuccessful");
                success = false;
            }

            return log;
        }
    }
}
