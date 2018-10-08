﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using knowledgeBaseLibrary.DataAccess;

namespace knowledgeBaseUI
{
    public partial class Search : Form
    {
        private readonly IDataConnection _dataConnection;
        public Search(IDataConnection dataConnection)
        {
            InitializeComponent();
            _dataConnection = dataConnection;
            //TODO: come posso generalizzare questo codice in modo che funzioni anche per SQL? Non posso far esporre proprieta' ad un'interfaccia
            // This line of code is generated by Data Source Configuration Wizard
            // Create a new DataSet
            System.Data.DataSet xmlDataSet = new System.Data.DataSet("XML DataSet");
            // Load the XML document to the DataSet
            xmlDataSet.ReadXml(@"C:\Users\rivaa\source\repos\cSharpLearning\knowledgeBaseApp\knowledgeBaseLibrary\Data\PostRepository.xml");
            // This line of code is generated by Data Source Configuration Wizard
            SearchListControl.DataSource = xmlDataSet.Tables["post"];
            //Use DataBindings instead?
            //SearchListControl.DataBindings.Add()
            SearchBarControl.Client = SearchListControl;
            SearchListControl.MouseDoubleClick += new MouseEventHandler(this.MouseDoubleClickOnItem);
        }

        private void MouseDoubleClickOnItem(object sender, MouseEventArgs e)
        {
            ShowPost showPost = new ShowPost(_dataConnection.GetPost(Guid.Parse((SearchListControl.SelectedValue).ToString())));
            showPost.Show();
            
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            var newPost = new NewPost();
            newPost.Show();
        }

        private void SearchListControl_DataSourceChanged(object sender, EventArgs e)
        {
            SearchListControl.Update();
            SearchListControl.Refresh();
        }

        /// <summary>
        /// Updates the list -  but in an unoptimized way
        /// TODO: change DataBinding implementation in order to speed up refresh
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Refresh_Click(object sender, EventArgs e)
        {
            System.Data.DataSet xmlDataSet = new System.Data.DataSet("XML DataSet");
            // Load the XML document to the DataSet
            xmlDataSet.ReadXml(@"C:\Users\rivaa\source\repos\cSharpLearning\knowledgeBaseApp\knowledgeBaseLibrary\Data\PostRepository.xml");
            // This line of code is generated by Data Source Configuration Wizard
            SearchListControl.DataSource = xmlDataSet.Tables["post"];
        }
    }

}
