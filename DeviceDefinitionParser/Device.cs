using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertToXML
{
    class Device
    {
        #region [ Members ]

        private List<Line> m_lines;
        private Dictionary<string, string> m_attributes;


        #endregion

        #region [ Constructors ]

        public Device()
        {
            m_attributes = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                {"id", ""},
                {"make", ""},
                {"model", ""},
                {"stationID", ""},
                {"stationName", ""},
                {"stationLatitude", ""},
                {"stationLongitude", ""}
            };
            m_lines = new List<Line>();
        }

        #endregion

        #region [ Properties ]

        public string ID
        {
            get
            {
                return m_attributes["id"];
            }

            set
            {
                m_attributes["id"] = value;
            }
        }

        public string Make
        {
            get
            {
                return m_attributes["make"];
            }

            set
            {
                m_attributes["make"] = value;
            }
        }

        public string Model
        {
            get
            {
                return m_attributes["model"];
            }

            set
            {
                m_attributes["model"] = value;
            }
        }

        public string StationID
        {
            get
            {
                return m_attributes["stationID"];
            }

            set
            {
                m_attributes["stationID"] = value;
            }
        }

        public string StationName
        {
            get
            {
                return m_attributes["stationName"];
            }

            set
            {
                m_attributes["stationName"] = value;
            }
        }

        public string StationLatitude
        {
            get
            {
                return m_attributes["stationLatitude"];
            }

            set
            {
                m_attributes["stationLatitude"] = value;
            }
        }

        public string StationLongitude
        {
            get
            {
                return m_attributes["stationLongitude"];
            }

            set
            {
                m_attributes["stationLongitude"] = value;
            }
        }

        internal List<Line> Lines
        {
            get
            {
                return m_lines;
            }

            set
            {
                m_lines = value;
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
            string lineStrings = "";
            foreach (Line line in Lines)
            {
                lineStrings += line.ToString();
            }

            string result = "<device id=\"" + ID + "\">\n<attributes>\n";

            foreach (KeyValuePair<string, string> attribute in m_attributes)
            {
                if (attribute.Value != "" && attribute.Key != "id")
                {
                    result += "<" + attribute.Key + ">" + attribute.Value + "</" + attribute.Key + ">\n";
                }
            }
            result += "</attributes>\n";

            result += "<lines>\n" + lineStrings + "</lines>\n" + "</device>\n";

            return result;
        }

        #endregion
    }
}
