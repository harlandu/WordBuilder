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
		
			m_Warnings = new ListStore(typeof(string));
			m_WarningsTreeView.Model = m_Warnings;
			
			m_WarningsTreeView.RowActivated += HandleM_WarningsTreeViewRowActivated;
		}

		void HandleM_WarningsTreeViewRowActivated (object o, RowActivatedArgs args)
		{
			if (WarningActivated != null)
			{
				TreeIter iter;
				m_Warnings.GetIter(out iter, args.Path);
				
				string warning = (string)m_Warnings.GetValue(iter, 0);
				
				if (warning.StartsWith("Line "))
				{
					int line = 0;
					
					if (int.TryParse(warning.Substring(5, warning.IndexOf(':') - 5), out line))
					{
						WarningActivated(this, new WarningEventArgs(line));
					}
				}
			}
		}
		
		private ScrolledWindow m_WarningsScrolledWindow;
		private TreeView m_WarningsTreeView;
		private ListStore m_Warnings;

		#region IWarningViewHelper implementation
		public void AddWarning(string message)
		{
			m_WarningsScrolledWindow.Visible = true;
			m_Warnings.AppendValues(message);
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
