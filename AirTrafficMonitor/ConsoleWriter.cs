using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitor
{
   public class ConsoleWriter : IConsoleWriter
    {
        public void printPlanes(List<ITrack> trackList) // har til opgave at udskrive de fly som er i airspace
        {

            foreach (Track track in trackList)
            {
                Console.WriteLine("flynummer:" + track.tag + "X coordinat:" + track.X_coor + "Y coordinat:" + track.Y_coor + "højde:" + track.Altitude + "meter" + track.CompassCourse + track.timestamp);
            }
        }

        public void printOncollisioncourse(List<ITrack> _collisionTracks)
        {
                
            foreach (Track track in _collisionTracks)
            {
                Console.WriteLine("flynummer:" + track.tag + "DANGER on collision imminent, change course Imiadiatly!!");
            }

        }
    }

}
