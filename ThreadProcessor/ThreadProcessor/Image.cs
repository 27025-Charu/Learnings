using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadProcessor
{
    internal class Image
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public int processingTime
        {
            get
            {
                return Convert.ToInt32((endTime - startTime).TotalMilliseconds);
            }
        }
    }
}
