//******************************************************************************************************
//  MainWindow.xaml.cs - Gbtc
//
//  Copyright © 2016, Grid Protection Alliance.  All Rights Reserved.
//
//  Licensed to the Grid Protection Alliance (GPA) under one or more contributor license agreements. See
//  the NOTICE file distributed with this work for additional information regarding copyright ownership.
//  The GPA licenses this file to you under the MIT License (MIT), the "License"; you may
//  not use this file except in compliance with the License. You may obtain a copy of the License at:
//
//      http://opensource.org/licenses/MIT
//
//  Unless agreed to in writing, the subject software distributed under the License is distributed on an
//  "AS-IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. Refer to the
//  License for the specific language governing permissions and limitations.
//
//  Code Modification History:
//  ----------------------------------------------------------------------------------------------------
//  06/13/2016 - Stephen Jenks
//       Generated original version of source code.
//
//******************************************************************************************************

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
        private DeviceDefinitionParser m_parser;
        private List<string> m_files;
        private Microsoft.Office.Interop.Excel.Application m_application;


        #endregion

        #region [ Constructors ]

        public MainWindow()
        {
            InitializeComponent();
            m_files = new List<string>();
            m_parser = new DeviceDefinitionParser();
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

            m_application.Workbooks.Close();

            string[] result = cellValues.ToArray();
            return result;
        }

        private void Parse()
        {

            foreach (string excelFile in m_files)
            {
                if (Path.GetExtension(excelFile).ToLower() == ".xlsx")
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

                ClearFileList();
                UserFeedback.Text = document.ToString();
            }
            else
            {
                UserFeedback.Text = "There's no files to work my magic on. This makes me sad :(";
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
