using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDL.Model
{
    public class Reading
    {
        public int ReadingId { get; set; }
        public int sensorId { get; set; }
        public int appartmentId { get; set; }
        public float value { get; set; }
        public DateTime timestamp { get; set; }
    }
}
