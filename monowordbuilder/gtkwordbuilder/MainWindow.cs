using System;
using Gtk;

using Whee.WordBuilder.Controller;
using Whee.WordBuilder.Model;
using Whee.WordBuilder.Helpers;
using Whee.WordBuilder.UIHelpers;
using Whee.WordBuilder.Dialogs;

public partial class MainWindow : Gtk.Window
{
	public MainWindow () : base(Gtk.WindowType.Toplevel)
	{
		Build();
		
		m_DocumentController = new DocumentController(new GtkWarningViewHelper(warningsScrolledWindow, warningsTreeView), new FileSystem(), new FileDialogHelper(), new GtkTextViewHelper(this.codeTextview), new Document());
		m_GeneratorController = new GeneratorController(new FileSystem(), new GtkResultViewHelper(resultsTreeview), new ClipBoardHelper(), new GtkTextViewHelper(this.detailsTextview));
	}

	protected SaveCheckDialogResult Quit()
	{
		SaveCheckDialogResult result = m_DocumentController.CheckSave();
		if (SaveCheckDialogResult.Cancel != result)
		{
			Application.Quit();
		}
		
		return result;
	}

	protected override bool OnDeleteEvent (Gdk.Event evnt)
	{
		if (SaveCheckDialogResult.Cancel != this.Quit())
		{
			return base.OnDeleteEvent (evnt);
		}
		
		return true;
	}
	
	protected virtual void OnQuitActionActivated (object sender, System.EventArgs e)
	{
		this.Quit();
	}
	
	protected virtual void OnNewActionActivated (object sender, System.EventArgs e)
	{
		m_DocumentController.New();
	}
	
	protected virtual void OnSaveActionActivated (object sender, System.EventArgs e)
	{
		m_DocumentController.Save();
	}
	
	protected virtual void OnSaveAsActionActivated (object sender, System.EventArgs e)
	{
		m_DocumentController.SaveAs();
	}
	
	protected virtual void OnOpenActionActivated (object sender, System.EventArgs e)
	{
		m_DocumentController.Open();
	}
		
	protected virtual void OnGenerateActionActivated (object sender, System.EventArgs e)
	{
		m_GeneratorController.Generate(m_DocumentController.Compile());
	}
	
	protected virtual void OnClearAndGenerateActionActivated (object sender, System.EventArgs e)
	{
		m_GeneratorController.Clear();
		m_GeneratorController.Generate(m_DocumentController.Compile());
	}
		
	protected virtual void OnClearActionActivated (object sender, System.EventArgs e)
	{
		m_GeneratorController.Clear();
	}
	
	protected virtual void OnCopySelectedActionActivated (object sender, System.EventArgs e)
	{
		m_GeneratorController.Copy();
	}	
	
	protected virtual void OnCopyDescriptionsActionActivated (object sender, System.EventArgs e)
	{
		m_GeneratorController.CopyDescription();
	}
	
	protected virtual void OnExportActionActivated (object sender, System.EventArgs e)
	{
		ExportDialog dlg = new ExportDialog();
		
		if (dlg.Run() == (int)ResponseType.Ok)
		{
			m_GeneratorController.Export(dlg.GetExporter(), dlg.GetFilename());
		}

		dlg.Destroy();
	}
	
	private DocumentController m_DocumentController;
	private GeneratorController m_GeneratorController;
}
