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
        private List<string> m_lineParameters;
        private List<string> m_impedances;
        private List<string> m_channels;
        private Dictionary<string, string> m_attributes;


        #endregion

        #region [ Constructors ]

        public Line()
        {
            m_lineParameters = new List<string>() { "name", "voltage", "ratings50F", "length", "endStationID", "endStationName" };
            m_impedances = new List<string>() { "R1", "X1", "R0", "X0" };
            m_channels = new List<string>() { "VA", "VB", "VC", "IA", "IB", "IC", "IR" };

            m_attributes = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                {"id", "" },
                {"name", "" },
                {"voltage", "" },
                {"ratings50F", "" },
                {"length", "" },
                {"endStationID", "" },
                {"endStationName", "" },
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

            foreach (string field in m_lineParameters)
            {
                if (m_attributes[field] != "")
                {
                    result += "<" + field + ">" + m_attributes[field] + "</" + field + ">\n";
                }
            }

            bool impedancesIncluded = false;

            foreach (string impedance in m_impedances)
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
                foreach (string impedance in m_impedances)
                {
                    if (m_attributes[impedance] != "")
                    {
                        result += "<" + impedance + ">" + m_attributes[impedance] + "</" + impedance + ">\n";
                    }
                }
                result += "</impedances>\n";
            }

            bool channelsIncluded = false;

            foreach (string impedance in m_impedances)
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
                foreach (string channel in m_channels)
                {
                    if (m_attributes[channel] != "")
                    {
                        result += "<" + channel + ">" + m_attributes[channel] + "</" + channel + ">\n";
                    }
                }
                result += "</channels>\n";
            }

            result += "</line\n>";

            return result;
        }

        #endregion
    }
}
