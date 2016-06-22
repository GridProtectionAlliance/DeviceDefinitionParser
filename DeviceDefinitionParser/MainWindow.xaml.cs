using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using Microsoft.Win32;
using Microsoft.Office.Interop.Excel;

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
        private List<string> m_files;
        private Microsoft.Office.Interop.Excel.Application m_application;


        #endregion

        #region [ Constructors ]

        public MainWindow()
        {
            InitializeComponent();
            m_files = new List<string>();
            m_parser = new CSVtoXMLParser();
            m_application = new Microsoft.Office.Interop.Excel.Application();
            m_application.Visible = false;
            DataContext = m_parser;
        }

        #endregion

        #region [ Methods ]
        private void Magic(object sender, RoutedEventArgs e)
        {
            Parse();
        }

        public string[] ReadExcelFile(string input)
        {
            Workbook workbook = m_application.Workbooks.Open(input);
            Worksheet worksheet = workbook.Sheets[2];

            List<string> cellValues = new List<string>();

            int columnCount = 2;
            int rowCount = worksheet.UsedRange.Rows.Count;

            object[,] usedCells = worksheet.UsedRange.Value2;
            for (int row = 1; row <= rowCount; row++)
            {
                for(int column = 1; column <= columnCount; column++)
                {
                    cellValues.Add((usedCells[row, column]?.ToString() ?? ""));
                }
            }

            
            string[] result = cellValues.ToArray();


            return result;
        }

        private void Parse()
        {

            foreach (string excelFile in m_files)
            {
                if (Path.GetExtension(excelFile) == ".xlsx")
                {
                    string[] input = ReadExcelFile(excelFile);
                    m_parser.Input = input;
                    m_parser.ParseInput();
                }
                
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
