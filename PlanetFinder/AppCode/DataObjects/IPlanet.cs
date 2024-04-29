using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PlanetFinder.AppCode.DataObjects
{
    public interface IPlanet
    {
        public string Name { get; set; }

        public string Climate { get; set; }

        public string Gravity { get; set; }

        [DisplayFormat(DataFormatString ="{0:n0}")]
        public int? Population { get; set; }

        [DisplayFormat(DataFormatString ="{0:p0}")]
        [DisplayName("Surface Water %")]
        public decimal? SurfaceWater { get; set; }

        public string Terrain { get; set; }
    }
}
