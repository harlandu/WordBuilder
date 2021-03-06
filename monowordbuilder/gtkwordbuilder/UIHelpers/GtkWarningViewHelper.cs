//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4200
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using Gtk;

namespace Whee.WordBuilder.UIHelpers
{
	public class GtkWarningViewHelper : IWarningViewHelper
	{
		public GtkWarningViewHelper(ScrolledWindow warningsScrolledWindow, TreeView warningsTreeView)
		{
			m_WarningsScrolledWindow = warningsScrolledWindow;
			m_WarningsTreeView = warningsTreeView;

			TreeViewColumn warningCol = new TreeViewColumn();
			warningCol.Title = "Warnings";
			
			CellRendererText warningColRenderer = new CellRendererText();
			warningCol.PackStart(warningColRenderer, true);
		
			warningCol.AddAttribute(warningColRenderer, "text", 0);
		
			m_WarningsTreeView.AppendColumn(warningCol);
		
			m_Warnings = new ListStore(typeof(string), typeof(Whee.WordBuilder.ProjectV2.IProjectNode));
			m_WarningsTreeView.Model = m_Warnings;
			
			m_WarningsTreeView.RowActivated += HandleM_WarningsTreeViewRowActivated;
		}

		void HandleM_WarningsTreeViewRowActivated (object o, RowActivatedArgs args)
		{
			if (WarningActivated != null)
			{
				TreeIter iter;
				m_Warnings.GetIter(out iter, args.Path);

				Whee.WordBuilder.ProjectV2.IProjectNode node = (Whee.WordBuilder.ProjectV2.IProjectNode)m_Warnings.GetValue(iter, 1);
				WarningActivated(this, new WarningEventArgs(node.Index));
			}
		}
		
		private ScrolledWindow m_WarningsScrolledWindow;
		private TreeView m_WarningsTreeView;
		private ListStore m_Warnings;

		#region IWarningViewHelper implementation
		public void AddWarning(Whee.WordBuilder.ProjectV2.IProjectNode node, string message)
		{
			m_WarningsScrolledWindow.Visible = true;
			m_Warnings.AppendValues(message, node);
            m_HasWarnings = true;
		}

		public void Clear ()
		{
			m_WarningsScrolledWindow.Visible = false;
			m_Warnings.Clear();
            m_HasWarnings = false;
		}
		
		public event EventHandler<WarningEventArgs> WarningActivated;

        private bool m_HasWarnings = false;
        public bool HasWarnings
        {
            get { return m_HasWarnings; }
        }

        #endregion
    }
}
