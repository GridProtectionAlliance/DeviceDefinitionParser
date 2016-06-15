using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using Microsoft.Win32;

namespace ConvertToXML
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        #region [ Members ]
        // Fields
        private CSVtoXMLParser m_parser;
        List<string> m_files;


        #endregion

        #region [ Constructors ]

        public MainWindow()
        {
            InitializeComponent();
            m_files = new List<string>();
            m_parser = new CSVtoXMLParser();
            DataContext = m_parser;
        }

        #endregion

        #region [ Methods ]
        private void Magic(object sender, RoutedEventArgs e)
        {
            Parse();
        }

        private void Parse()
        {
            foreach (string csvToLoad in m_files)
            {
                char[] splitOn = new char[] { ',', '\n', '\r' };
                char[] trimOn = new char[] { '\n', '\r' };

                string[] input = File.ReadAllText(csvToLoad).Split(splitOn);
                foreach (string word in input)
                    word.TrimStart(trimOn);

                m_parser.Input = input;
                m_parser.ParseInput();
            }

            if (m_files.Count > 0)
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.AddExtension = true;
                dialog.OverwritePrompt = true;
                dialog.Filter = "XML files|*.xml|All Files|*.*";
                dialog.InitialDirectory = Path.GetDirectoryName(m_files.Last());
                if (dialog.ShowDialog() == false)
                {
                    return;
                }
                string outputXMLFile = dialog.FileName;
                File.WriteAllText(outputXMLFile, m_parser.Result.ToString());

                XDocument document = new XDocument();
                document = XDocument.Parse(m_parser.Result.ToString());
                document.Save(outputXMLFile);

                UserFeedback.Text = document.ToString();
            }
            else
            {
                UserFeedback.Text = "No files to perfom magic on :(";
            }
            m_parser.Clear();
        }

        private void Dropped(object sender, DragEventArgs e)
        {
            string[] newFiles = (string[])e.Data.GetData(DataFormats.FileDrop);
            m_files.AddRange(newFiles);

            UserFeedback.Text = "Files added.\nPress the Magic button to process files.\n";
            foreach (string file in m_files)
            {
                UserFeedback.Text += file;
                UserFeedback.Text += "\n";
            }

        }

        private void ClearFileList(object sender, RoutedEventArgs e)
        {
            ClearFileList();
        }

        private void ClearFileList()
        {
            m_files.Clear();
            m_parser.Clear();
            UserFeedback.Text = "File list cleared :)";
        }

        #endregion
    }
}
