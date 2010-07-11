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
using Whee.WordBuilder.Model;
using Whee.WordBuilder.Model.Events;
using Whee.WordBuilder.Helpers;
using Whee.WordBuilder.UIHelpers;
using Whee.WordBuilder.ProjectV2;

namespace Whee.WordBuilder.Controller
{
	public class DocumentController
	{
		public DocumentController (IWarningViewHelper warningViewHelper, IFileSystem fileSystem, IFileDialogHelper fileDialogHelper, ITextViewHelper textView, Document model)
		{
			m_FileDialogHelper = fileDialogHelper;
			m_FileSystem = fileSystem;
			m_textView = textView;
			m_textView.BufferChanged += HandleM_textViewBufferChanged;
			m_model = model;
			m_model.DocumentChanged += HandleM_modelDocumentChanged;
			m_WarningViewHelper = warningViewHelper;
			m_WarningViewHelper.WarningActivated += HandleM_WarningViewHelperWarningActivated;
		}

		void HandleM_WarningViewHelperWarningActivated (object sender, WarningEventArgs e)
		{
			GotoLine(e.LineNumber - 1);
		}

		void HandleM_textViewBufferChanged (object sender, DocumentChangedEventArgs e)
		{
			if (!m_updating)
			{
				m_updating = true;
				m_model.Text = e.NewText;
				m_updating = false;
			}
            m_textView.DoHighlighting(GetProjectNode());
		}

		private void HandleM_modelDocumentChanged (object sender, DocumentChangedEventArgs e)
		{
			if (!m_updating) 
			{
				m_updating = true;
				m_textView.OnDocumentChanged(this, e.NewText, GetProjectNode());
				m_updating = false;
			}
		}

		private IWarningViewHelper m_WarningViewHelper;
		private IFileSystem m_FileSystem;
		private IFileDialogHelper m_FileDialogHelper;
		private ITextViewHelper m_textView;
		private Document m_model;
		private bool m_updating = false;
		
		public void New()
		{
			if (CheckSave() != SaveCheckDialogResult.Cancel)
			{
				m_updating = true;
				m_model.Text = "";
				m_textView.Clear();
				m_updating = false;
			}
		}		
		
		public void OnTextViewChanged(object sender, string newText)
		{
			m_updating = true;
			m_model.Text = newText;
			m_updating = false;
		}

		public void GotoLine(int linenumber)
		{
			m_textView.GotoLine(linenumber);
		}
		
		public void Save()
		{
			if (string.IsNullOrEmpty(m_model.FileName))
			{
				m_model.FileName = m_FileDialogHelper.ShowSaveDialog();
			}
			
			if (!string.IsNullOrEmpty(m_model.FileName))
			{			
				m_model.Save(m_FileSystem);
			}
		}
		
		public void SaveAs()
		{
			string originalFileName = m_model.FileName;

			m_model.FileName = m_FileDialogHelper.ShowSaveDialog();
			
			if (string.IsNullOrEmpty(m_model.FileName))
			{
				m_model.FileName = originalFileName;
			}
			else
			{
				m_model.Save(m_FileSystem);
			}
		}
		
		public void Open()
		{
			if (CheckSave() != SaveCheckDialogResult.Cancel)
			{
				m_model.FileName = m_FileDialogHelper.ShowOpenDialog();
				
				if (!string.IsNullOrEmpty(m_model.FileName))
				{
					m_model.Load(m_FileSystem);
				}
			}
		}
		
		public SaveCheckDialogResult CheckSave()
		{
			if (!m_model.Dirty)
			{
				return SaveCheckDialogResult.NoSave;
			}
			
			SaveCheckDialogResult result = m_FileDialogHelper.ShowSaveCheckDialog();
			
			if (SaveCheckDialogResult.Save == result)
			{
				Save();
			}
			
			return result;
		}

        public ProjectNode GetProjectNode()
        {
            m_WarningViewHelper.Clear();
            return Whee.WordBuilder.ProjectV2.ProjectSerializer.LoadString(m_model.Text, null, m_WarningViewHelper) as ProjectNode;
        }

        public Project Compile()
        {
            ProjectNode project = GetProjectNode();
            Project result = new Project(project);

            if (!m_WarningViewHelper.HasWarnings)
            {
                return result;
            }

            return null;
        }
	}
}
