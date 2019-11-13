using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AirTrafficMonitor
{
    class LogCollision : ILog
    {
        public LogCollision(IOnCollisionCourse LogCollision)
        {
            LogCollision.CreateSeperation += OnCreateSeperation;
        }

        public void OnCreateSeperation(object s, SeperationEvent collision)
        {
            var text = CreateString(collision.Collisiontracks);
            var file = @".\Collisions.txt";

            using (StreamWriter writer = File.AppendText(file))
            {
                foreach (var line in text)
                {
                    writer.WriteLine((string) line);
                }
            }
        }

        public string[] CreateString(List<OnCollisionCourse> col)
        {
            var lin = new String[col.Count];
            for (int i = 0; i < col.Count; i++)
            {
                lin[i] = "Tracks on collision: ";

                for (int k = 0; k < col[i]._collisionFlights.Count; k++)
                {
                    lin[i] += col[i]._collisionFlights[k].tag + ", ";
                }

                lin[i] += col[i]._collisionTime;
            }

            return lin;
        }
    }
}
