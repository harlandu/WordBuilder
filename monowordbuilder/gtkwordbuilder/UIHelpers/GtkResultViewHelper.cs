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
using System.Collections.Generic;
using Whee.WordBuilder.Model;
using Whee.WordBuilder.UIHelpers;
using Gtk;

namespace Whee.WordBuilder.UIHelpers
{
	public class GtkResultViewHelper : IResultViewHelper
	{
		public GtkResultViewHelper(TreeView resultsTreeView)
		{
			m_Columns = new Dictionary<string, string>();
			m_ResultsTreeView = resultsTreeView;
			m_ResultsTreeView.Selection.Changed += HandleM_ResultsTreeViewSelectionChanged;
			m_ResultsTreeView.Selection.Mode = SelectionMode.Multiple;
			
			TreeViewColumn wordCol = new TreeViewColumn();
			wordCol.Title = "Word";
			
			CellRendererText wordColRenderer = new CellRendererText();
			wordCol.PackStart(wordColRenderer, true);
		
			wordCol.AddAttribute(wordColRenderer, "text", 1);
		
			m_ResultsTreeView.AppendColumn(wordCol);
		
			m_Results = new ListStore(typeof(Context), typeof(string));
			m_ResultsTreeView.Model = m_Results;
		}

		void HandleM_ResultsTreeViewSelectionChanged(object sender, EventArgs e)
		{
			if (SelectionChanged != null)
			{
				SelectionChanged(this, EventArgs.Empty);
			}			
		}

		private TreeView m_ResultsTreeView;
		private ListStore m_Results;
		
		#region IResultViewHelper implementation
		public void Clear ()
		{
			m_Results.Clear();
			m_Columns.Clear();
			ResetColumns();
		}

		private void ResetColumns()
		{
			List<TreeViewColumn> colstoremove = new List<TreeViewColumn>(m_ResultsTreeView.Columns);
			
			foreach (TreeViewColumn col in colstoremove)
			{
				m_ResultsTreeView.RemoveColumn(col);
			}
			
			TreeViewColumn wordCol = new TreeViewColumn();
			wordCol.Title = "Word";
			
			CellRendererText wordColRenderer = new CellRendererText();
			wordCol.PackStart(wordColRenderer, true);
		
			wordCol.AddAttribute(wordColRenderer, "text", 1);
		
			m_ResultsTreeView.AppendColumn(wordCol);			
		}
		
		public void AddItem (Context context)
		{
			List<object> values = new List<object>();
			
			values.Add(context);
			values.Add(context.ToString());
			
			foreach(string col in m_Columns.Keys)
			{
				values.Add(context.GetColumn(m_Columns[col], null));
			}
			
			m_Results.AppendValues(values.ToArray());			
		}
				
		public System.Collections.Generic.List<Context> GetSelectedItems ()
		{
			List<Context> result = new List<Context>();
			TreePath[] selected = m_ResultsTreeView.Selection.GetSelectedRows();
			TreeIter iter;
			
			foreach (TreePath sel in selected)
			{
				m_ResultsTreeView.Model.GetIter(out iter, sel);
				Context context = (Context)m_ResultsTreeView.Model.GetValue(iter, 0);
				
				result.Add(context);
			}
			
			return result;
		}
		
		public System.Collections.Generic.List<Context> GetAllItems ()
		{
			List<Context> result = new List<Context>();
			
			TreeIter iter;

			for (bool run = m_Results.GetIterFirst(out iter); run; run = m_Results.IterNext(ref iter))
			{
				Context context = (Context)m_ResultsTreeView.Model.GetValue(iter, 0);
				
				result.Add(context);
			}
			
			return result;
		}

		public event EventHandler<EventArgs> SelectionChanged;		

		private Dictionary<string, string> m_Columns;
		
		public void AddColumn (string title, string accessor)
		{
			m_Columns[title] = accessor;
			
			List<Context> items = new List<Context>();
			TreeIter iter;
			for (bool run = m_Results.GetIterFirst(out iter); run; run = m_Results.IterNext(ref iter))
			{
				items.Add((Context)m_Results.GetValue(iter, 0));
			}
			
			List<Type> columns = new List<Type>();
			columns.Add(typeof(Context));
			columns.Add(typeof(string));

			List<TreeViewColumn> viewColumns = new List<TreeViewColumn>();

			ResetColumns();
			
			int i = 2;
			foreach(string col in m_Columns.Keys)
			{
				columns.Add(typeof(string));

				TreeViewColumn wordCol = new TreeViewColumn();
				wordCol.Title = col;
				
				CellRendererText wordColRenderer = new CellRendererText();
				wordCol.PackStart(wordColRenderer, true);
			
				wordCol.AddAttribute(wordColRenderer, "text", i);
				i++;
			
				viewColumns.Add(wordCol);
			}
			
			m_Results.Dispose();
			m_Results = new ListStore(columns.ToArray());
			m_ResultsTreeView.Model = m_Results;
			
			foreach(TreeViewColumn wordCol in viewColumns)
			{
				m_ResultsTreeView.AppendColumn(wordCol);
			}
			
			foreach (Context item in items)
			{
				AddItem(item);
			}
		}
		
		#endregion
	}
}