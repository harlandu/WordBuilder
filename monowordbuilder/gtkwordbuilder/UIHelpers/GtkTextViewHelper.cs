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
using Whee.WordBuilder.UIHelpers;
using Whee.WordBuilder.ProjectV2;
using Gtk;

namespace Whee.WordBuilder.UIHelpers
{


	public class GtkTextViewHelper : ITextViewHelper
	{
		public GtkTextViewHelper (TextView textView)
		{
			m_textView = textView;
			m_textView.Buffer.Changed += HandleM_textViewBufferChanged;

            TextTag tag = new TextTag("Command");
            tag.ForegroundGdk = new Gdk.Color(0, 192, 0);
            m_textView.Buffer.TagTable.Add(tag);
            
            tag = new TextTag("Directive");
            tag.ForegroundGdk = new Gdk.Color(0, 128, 0);
            m_textView.Buffer.TagTable.Add(tag);

            tag = new TextTag("BlockStarter");
            tag.ForegroundGdk = new Gdk.Color(0, 0, 128);
            m_textView.Buffer.TagTable.Add(tag);

            tag = new TextTag("BlockEnder");
            tag.ForegroundGdk = new Gdk.Color(0, 0, 128);
            m_textView.Buffer.TagTable.Add(tag);

            tag = new TextTag("Name");
            tag.ForegroundGdk = new Gdk.Color(0, 128, 192);
            m_textView.Buffer.TagTable.Add(tag);

            tag = new TextTag("Number");
            tag.ForegroundGdk = new Gdk.Color(0, 0, 255);
            m_textView.Buffer.TagTable.Add(tag);

            tag = new TextTag("Comment");
            tag.ForegroundGdk = new Gdk.Color(128, 128, 128);
            tag.BackgroundGdk = new Gdk.Color(204, 204, 204);
            m_textView.Buffer.TagTable.Add(tag);

            tag = new TextTag("Error");
            tag.ForegroundGdk = new Gdk.Color(0, 0, 0);
            tag.BackgroundGdk = new Gdk.Color(255, 0, 0);
            m_textView.Buffer.TagTable.Add(tag);
        }

		void HandleM_textViewBufferChanged (object sender, EventArgs e)
		{
            if (BufferChanged != null)
			{
				BufferChanged.Invoke(sender, new Whee.WordBuilder.Model.Events.DocumentChangedEventArgs(m_textView.Buffer.Text));
			}
		}
		
		private TextView m_textView;
		
		public void Clear() 
		{
			m_textView.Buffer.Clear();
		}
		
		public void OnDocumentChanged(object sender, string newText, IProjectNode project)
		{
			m_textView.Buffer.Text = newText;
		}

        public void DoHighlighting(ProjectNode project)
        {
            m_textView.Buffer.RemoveAllTags(m_textView.Buffer.StartIter, m_textView.Buffer.EndIter);
            foreach (Token tok in project.Serializer.Tokens)
            {
                TextIter start = m_textView.Buffer.GetIterAtOffset(tok.Offset);
                TextIter end = m_textView.Buffer.GetIterAtOffset(tok.Offset + tok.Length);
                m_textView.Buffer.ApplyTag(tok.Type.ToString(), start, end);
            }
        }

		public void GotoLine (int linenumber)
		{
			TextIter iter = m_textView.Buffer.GetIterAtLine(linenumber);
			m_textView.Buffer.PlaceCursor(iter);
			m_textView.PlaceCursorOnscreen();
			m_textView.GrabFocus();
		}
		
		public event EventHandler<Whee.WordBuilder.Model.Events.DocumentChangedEventArgs> BufferChanged;		
	}
}
