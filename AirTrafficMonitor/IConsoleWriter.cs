using System.Collections.Generic;

namespace AirTrafficMonitor
{
    interface IConsoleWriter
    {
        void printPlanes(List<ITrack> tracklist);
        void printOncollisioncourse(List<ITrack> OnCollisionsCourseList);
    }
}