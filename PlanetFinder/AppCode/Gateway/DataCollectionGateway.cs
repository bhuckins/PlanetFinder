using PlanetFinder.AppCode.DataCollection;
using PlanetFinder.AppCode.DataObjects;
using PlanetFinder.AppCode.DataStore;

namespace PlanetFinder.AppCode.Gateway
{
    public class DataCollectionGateway
    {
        protected DataCollector DataCollector { get; set; }

        protected DataStorer DataStorer { get; set; }

        public DataCollectionGateway(DataCollector collector, DataStorer dataStorer)
        {
            this.DataCollector = collector;
            this.DataStorer = dataStorer;
        }

        public List<string> CollectAndStore(bool deleteExistingPlanets, out bool success)
        {
            // Should be a more robust logger, but use a list of strings for now
            List<string> log = new List<string>();

            if (DataCollector == null || DataStorer == null)
            {
                log.Add("DataCollector or DataStorer are null"); // Not the best error message, but this should never happen anyway
                success = false;
                return log;
            }

            log.Add("Getting planet data from collector");

            List<IPlanet> planets = DataCollector.GetPlanets();

            if (planets == null || !planets.Any())
            {
                log.Add("Received no planets from collector");
                success = false;
                return log;
            }

            log.Add(string.Format("Receieved {0:n0} planets", planets.Count));

            if (deleteExistingPlanets)
            {
                log.Add("Deleting existing planets from data store");

                DataStorer.DeleteAllPlanets();
            }

            log.Add("Saving planets to data store");

            if (DataStorer.SavePlanetsToStore(planets))
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
