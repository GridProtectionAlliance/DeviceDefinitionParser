using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertToXML
{
    class DeviceDefinition
    {
        #region [ Members ]

        private bool m_simple;
        private bool m_reactance;
        private bool m_takagi;
        private bool m_modifiedTakagi;
        private bool m_novosel;
        private List<Device> m_devices;

        #endregion

        #region [ Constructors ]

        public DeviceDefinition()
        {
            m_devices = new List<Device>();
        }

        #endregion

        #region [ Properties ]

        public bool Simple
        {
            get
            {
                return m_simple;
            }

            set
            {
                m_simple = value;
            }
        }

        public bool Reactance
        {
            get
            {
                return m_reactance;
            }

            set
            {
                m_reactance = value;
            }
        }

        public bool Takagi
        {
            get
            {
                return m_takagi;
            }

            set
            {
                m_takagi = value;
            }
        }

        public bool ModifiedTakagi
        {
            get
            {
                return m_modifiedTakagi;
            }

            set
            {
                m_modifiedTakagi = value;
            }
        }

        public bool Novosel
        {
            get
            {
                return m_novosel;
            }

            set
            {
                m_novosel = value;
            }
        }

        internal List<Device> Devices
        {
            get
            {
                return m_devices;
            }

            set
            {
                m_devices = value;
            }
        }

        #endregion

        #region [ Methods ]

        public override string ToString()
        {
            string analytics = "";

            if (Simple)
                analytics += "<faultLocation assembly=\"FaultAlgorithms.dll\" method=\"FaultAlgorithms.FaultLocationAlgorithms.Simple\" />\n";

            if (Reactance)
                analytics += "<faultLocation assembly = \"FaultAlgorithms.dll\" method = \"FaultAlgorithms.FaultLocationAlgorithms.Reactance\" />\n";

            if (Takagi)
                analytics += "<faultLocation assembly=\"FaultAlgorithms.dll\" method=\"FaultAlgorithms.FaultLocationAlgorithms.Takagi\" />\n";

            if (ModifiedTakagi)
                analytics += "<faultLocation assembly=\"FaultAlgorithms.dll\" method=\"FaultAlgorithms.FaultLocationAlgorithms.ModifiedTakagi\" />\n";

            if (Novosel)
                analytics += "<faultLocation assembly=\"FaultAlgorithms.dll\" method=\"FaultAlgorithms.FaultLocationAlgorithms.Novosel\" />\n";

            string devices = "";
            foreach (Device device in Devices)
            {
                devices += device.ToString();
            }
            string result =
                "<openFLE>\n"
                + "<analytics>\n"
                + analytics
                + "</analytics>\n"
                + devices
                + "</openFLE>\n";

            return result;
        }

        #endregion
    }
}
