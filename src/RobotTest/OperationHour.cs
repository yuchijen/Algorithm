using System;
using System.Collections.Generic;
using System.Text;

namespace RobotTest
{
    //Horoscope assessment test 
    public class OperationHour
    {
        Dictionary<string, string[]> opMap;
        public OperationHour()
        {
            opMap = new Dictionary<string, string[]>();
            opMap.Add("Monday", new string[2] { "10:00a", "6:00p" });
            opMap.Add("Tuesday", new string[2] { "10:00a", "6:00p" });
            opMap.Add("Wednesday", new string[2] { "10:00a", "6:00p" });
            opMap.Add("Thursday", new string[2] { "10:00a", "7:00p" });
            opMap.Add("Friday", new string[2] { "10:00a", "6:00p" });
            opMap.Add("Saturday", new string[2] { "10:00a", "6:00p" });
            opMap.Add("Sunday", null);
        }

        public string GetOperationHours
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (var pair in opMap)
                {
                    sb.Append(pair.Key).Append("  ");
                    if (pair.Value != null)
                        sb.Append(pair.Value[0] + "-" + pair.Value[1]);
                    else
                        sb.Append("Closed");
                    sb.AppendLine();
                }
                return sb.ToString();
            }
        }
    }

}
