using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertToXML
{
    class DeviceDefinitionParser
    {
        #region [ Members ]

        private string[] m_input;
        private DeviceDefinition m_result;
        private bool m_simple;
        private bool m_reactance;
        private bool m_takagi;
        private bool m_modifiedTakagi;
        private bool m_novosel;

        #endregion

        #region [ Constructors ]

        public DeviceDefinitionParser()
        {
            m_input = new string[0];
            m_result = new DeviceDefinition();
            Simple = true;
            Reactance = true;
            Takagi = true;
            ModifiedTakagi = true;
            Novosel = true;
        }

        public DeviceDefinitionParser(string[] input)
        {
            m_input = input;
            m_result = new DeviceDefinition();
            Simple = true;
            Reactance = true;
            Takagi = true;
            ModifiedTakagi = true;
            Novosel = true;
            ParseInput();
        }

        public DeviceDefinitionParser(string[] input, bool simple, bool reactance, bool takagi, bool modifiedTakagi, bool novosel)
        {
            m_input = input;
            m_result = new DeviceDefinition();
            Simple = simple;
            Reactance = reactance;
            Takagi = takagi;
            ModifiedTakagi = modifiedTakagi;
            Novosel = novosel;
            ParseInput(input, simple, reactance, takagi, modifiedTakagi, novosel);
        }

        #endregion

        #region [ Properties ]

        public string[] Input
        {
            get
            {
                return m_input;
            }
            set
            {
                m_input = value;
            }
        }

        internal DeviceDefinition Result
        {
            get
            {
                return m_result;
            }
        }

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

        #endregion

        #region [ Methods ]

        public DeviceDefinition ParseInput()
        {
            return ParseInput(Input, Simple, Reactance, Takagi, ModifiedTakagi, Novosel);
        }
        public DeviceDefinition ParseInput(string[] input, bool simple, bool reactance, bool takagi, bool modifiedTakagi, bool novosel)
        {
            m_result.Simple = simple;
            m_result.Reactance = reactance;
            m_result.Takagi = takagi;
            m_result.ModifiedTakagi = modifiedTakagi;
            m_result.Novosel = novosel;

            int i = 0;
            try
            {
                while (i < input.Length)
                {
                    Device device = new Device();

                    // Get Device ID
                    while (!input[i].Contains("device id"))
                    {
                        i++;
                    }
                    device.ID = input[++i];

                    // Get Device Attributes
                    while (i < input.Length && !input[i].Equals("line id="))
                    {
                        if (device.Attributes.ContainsKey(input[i]))
                        {
                            device.Attributes[input[i]] = input[++i].Trim();
                        }

                        i++;
                    }

                    // Get Lines
                    while (i < input.Length && !input[i].Equals("device id="))
                    {
                        Line line = new Line();

                        // Get Line ID
                        if (input[i].Equals("line id="))
                        {
                            line.ID = input[++i];
                        }

                        // Get line attributes
                        while (i < input.Length && !input[i].Equals("line id=") && !input[i].Equals("device id="))
                        {
                            if (line.Attributes.ContainsKey(input[i]))
                            {
                                line.Attributes[input[i]] = input[++i].Trim();
                            }

                            i++;
                        }

                        device.Lines.Add(line);
                    }

                    m_result.Devices.Add(device);
                }
                return m_result;
            }

            catch (Exception e)
            {
                return m_result;
            }
            
        }

        /// <summary>
        /// Resets the Device Definitions Object (m_result)
        /// </summary>
        public void Clear()
        {
            m_result = new DeviceDefinition();
        }
        #endregion
    }
}
