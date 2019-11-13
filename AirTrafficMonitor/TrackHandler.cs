using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;

namespace AirTrafficMonitor
{
    public class TrackHandler : ITrackHandler
    {
        public List<ITrack> tracklist { get; set; }        
        private ITransponderReceiver _receiver;
        public event EventHandler<TrackEvent> OnTrackCreated;
         //Constructor injection for dependency
        public TrackHandler(ITransponderReceiver receiver)
        {
            
            //Store real or fake transponder receiver
            this._receiver = receiver;

            //Attach the event to the real or fake Transponder receiver
            _receiver.TransponderDataReady += DataHandler;
        }

        public void DataHandler(object t, RawTransponderDataEventArgs eventArgs)
        {
            tracklist = new List<ITrack>();

            foreach (var data in eventArgs.TransponderData)  // kalder Rawhandler på hvert track den modtager.
            {
                Rawhandler(data);// Split tracks           
            }
            //OnTrackCreated(tracklist);
        }

        public void Rawhandler(string data) // tager data fra TransponderData som parameter og konvertere det til Tracks
        {           
            var _data = data.Split(';');
            Int32.TryParse(_data[1], out var coordinateX);
            Int32.TryParse(_data[2], out var coordinateY);
            Int32.TryParse(_data[3], out var altitude);
            DateTime dateTime;
            dateTime = DateTime.TryParseExact(_data[4], //anvender Datetime til at definere tidspunkt og data
                "yyyyMMddHHmmssfff",
                null,
                DateTimeStyles.None,   // anvender Datetime til at definere tidspunktet og dato
                out dateTime)
                ? dateTime
                : DateTime.MinValue;

            tracklist.Add(new Track()  // tilføjer et objekt af klassen Track til tracklisten.
            {
                tag = _data[0],
                X_coor = coordinateX,
                Y_coor = coordinateY,
                Altitude = altitude,
                timestamp = dateTime
            });
            
        }
        protected virtual void OnCreatedTrack(List<ITrack> tracklist)
        {
            OnTrackCreated?.Invoke(this, new TrackEvent() { tlist = tracklist });// Send Event
        }// sendEvent(newTrackArgs);
    }
            public class TrackEvent : EventArgs
                {
                    public List<ITrack> tlist { get; set; }
                } 
 
}
