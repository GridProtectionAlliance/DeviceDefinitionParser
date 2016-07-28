using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertToXML
{
    class Line
    {
        #region [ Members ]

        //Fields
        Dictionary<string, string> m_attributes;

        #endregion

        #region [ Constructors ]

        public Line()
        {
            m_attributes = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                {"ID", "" },
                {"Name", "" },
                {"Voltage", "" },
                {"Ratings50F", "" },
                {"Length", "" },
                {"EndStationID", "" },
                {"EndStationName", "" },
                {"R1", "" },
                {"R0", "" },
                {"X1", "" },
                {"X0", "" },
                {"VA", "" },
                {"VB", "" },
                {"VC", "" },
                {"IA", "" },
                {"IB", "" },
                {"IC", "" },
                {"IR", "" }
            };
        }

        #endregion

        #region [ Properties ]
        public string ID
        {
            get
            {
                return Attributes["id"];
            }

            set
            {
                Attributes["id"] = value;
            }
        }

        public string Name
        {
            get
            {
                return Attributes["name"];
            }

            set
            {
                Attributes["name"] = value;
            }
        }

        public string Voltage
        {
            get
            {
                return Attributes["voltage"];
            }

            set
            {
                Attributes["voltage"] = value;
            }
        }

        public string Ratings50F
        {
            get
            {
                return Attributes["ratings50F"];
            }

            set
            {
                Attributes["ratings50F"] = value;
            }
        }

        public string Length
        {
            get
            {
                return Attributes["length"];
            }

            set
            {
                Attributes["length"] = value;
            }
        }

        public string EndStationID
        {
            get
            {
                return Attributes["endStationID"];
            }

            set
            {
                Attributes["endStationID"] = value;
            }
        }

        public string EndStationName
        {
            get
            {
                return Attributes["endStationName"];
            }

            set
            {
                Attributes["endStationName"] = value;
            }
        }

        public string R1
        {
            get
            {
                return Attributes["R1"];
            }

            set
            {
                Attributes["R1"] = value;
            }
        }

        public string R0
        {
            get
            {
                return Attributes["R0"];
            }

            set
            {
                Attributes["R0"] = value;
            }
        }

        public string X1
        {
            get
            {
                return Attributes["X1"];
            }

            set
            {
                Attributes["X1"] = value;
            }
        }

        public string X0
        {
            get
            {
                return Attributes["X0"];
            }

            set
            {
                Attributes["X0"] = value;
            }
        }

        public string VA
        {
            get
            {
                return Attributes["VA"];
            }

            set
            {
                Attributes["VA"] = value;
            }
        }

        public string VB
        {
            get
            {
                return Attributes["VB"];
            }

            set
            {
                Attributes["VB"] = value;
            }
        }

        public string VC
        {
            get
            {
                return Attributes["VC"];
            }

            set
            {
                Attributes["VC"] = value;
            }
        }

        public string IA
        {
            get
            {
                return Attributes["IA"];
            }

            set
            {
                Attributes["IA"] = value;
            }
        }

        public string IB
        {
            get
            {
                return Attributes["IB"];
            }

            set
            {
                Attributes["IB"] = value;
            }
        }

        public string IC
        {
            get
            {
                return Attributes["IC"];
            }

            set
            {
                Attributes["IC"] = value;
            }
        }

        public string IR
        {
            get
            {
                return Attributes["IR"];
            }

            set
            {
                Attributes["IR"] = value;
            }
        }

        public Dictionary<string, string> Attributes
        {
            get
            {
                return m_attributes;
            }
        }

        #endregion

        #region [ Methods ]

        public override string ToString()
        {
            string result = "<line id=\"" + ID + "\">\n";
            List<string> lineParameters = new List<string>() { "Name", "Voltage", "Ratings50F", "Length", "EndStationID", "EndStationName" };
            List<string> impedances = new List<string>() { "R1", "X1", "R0", "R1" };
            List<string> channels = new List<string>() { "VA", "VB", "VC", "IA", "IB", "IC", "IR" };

            foreach (string field in lineParameters)
            {
                if (m_attributes[field] != "")
                {
                    result += "<" + field.ToLower() + ">" + m_attributes[field] + "</" + field.ToLower() + ">\n";
                }
            }

            bool impedancesIncluded = false;

            foreach (string impedance in impedances)
            {
                if (m_attributes[impedance] != "")
                {
                    impedancesIncluded = true;
                    break;
                }
            }

            if (impedancesIncluded == true)
            {
                result += "<impedances>\n";
                foreach (string impedance in impedances)
                {
                    if (m_attributes[impedance] != "")
                    {
                        result += "<" + impedance.ToLower() + ">" + m_attributes[impedance] + "</" + impedance.ToLower() + ">\n";
                    }
                }
                result += "</impedances>\n";
            }

            bool channelsIncluded = false;

            foreach (string impedance in impedances)
            {
                if (m_attributes[impedance] != "")
                {
                    channelsIncluded = true;
                    break;
                }
            }

            if (channelsIncluded == true)
            {
                result += "<channels>\n";
                foreach (string channel in channels)
                {
                    if (m_attributes[channel] != "")
                    {
                        result += "<" + channel.ToLower() + ">" + m_attributes[channel] + "</" + channel.ToLower() + ">\n";
                    }
                }
                result += "</channels>\n";
            }

            result += "</line\n>";

            string resultBackup =
                "<line id=\"" + ID + "\">\n"
                + "<name>" + Name + "</name>\n"
                + "<voltage>" + Voltage + "</voltage>\n"
                + "<rating50F>" + Ratings50F + "</rating50F>\n"
                + "<length>" + Length + "</length>\n"
                + "<endStationID>" + EndStationID + "</endStationID>\n"
                + "<endStationName>" + EndStationName + "</endStationName>\n"
                + "<impedances>\n"
                + "<R1>" + R1 + "</R1>\n"
                + "<X1>" + X1 + "</X1>\n"
                + "<R0>" + R0 + "</R0>\n"
                + "<X0>" + X0 + "</X0>\n"
                + "</impedances>\n"
                + "<channels>\n"
                + "<VA>" + VA + "</VA>\n"
                + "<VB>" + VB + "</VB>\n"
                + "<VC>" + VC + "</VC>\n"
                + "<IA>" + IA + "</IA>\n"
                + "<IB>" + IB + "</IB>\n"
                + "<IC>" + IC + "</IC>\n"
                + "<IR>" + IR + "</IR>\n"
                + "</channels>\n"
                + "</line\n>";

            return result;
        }

        #endregion
    }
}
