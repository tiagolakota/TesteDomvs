using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteDomvs.Negocio.Util
{
    public class Localizacao
    {
        const double AVERAGE_RADIUS_OF_EARTH_KM = 6371;
        const double PIx = 3.141592653589793;

        public double Radians(double x)
        {
            return x * PIx / 180;
        }

        //Harversine Formula para calcular a distância entre dois pontos.
        //https://stackoverflow.com/questions/27928/calculate-distance-between-two-latitude-longitude-points-haversine-formula?noredirect=1&lq=1
        public int CalculateDistanceInKilometer(double userLat, double userLng,
          double venueLat, double venueLng)
        {

            double latDistance = Radians(userLat - venueLat);
            double lngDistance = Radians(userLng - venueLng);

            double a = Math.Sin(latDistance / 2) * Math.Sin(latDistance / 2)
              + Math.Cos(Radians(userLat)) * Math.Cos(Radians(venueLat))
              * Math.Sin(lngDistance / 2) * Math.Sin(lngDistance / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return (int)(Math.Round(AVERAGE_RADIUS_OF_EARTH_KM * c));
        }

        public bool IsLatitude(double lat)
        {
            return Math.Abs(lat) <= 90;
        }

        public bool IsLongitude(double lng)
        {
            return Math.Abs(lng) <= 180;
        }
    }
}