using System;
using System.Windows.Forms;
using System.Windows.Forms.Design; 
using System.Drawing.Design; 
using System.Drawing;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.Common;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.jCodesenum;

namespace JCodes.Framework.CommonControl.Settings
{
	/// <summary>
	/// Summary description for ImageEditor.
	/// </summary>
	public class ImageMapEditor : System.Drawing.Design.UITypeEditor   
	{
		
		#region properties
		
		private IWindowsFormsEditorService wfes = null ;
		private int m_selectedIndex = -1 ;
		private ImageListPanel m_imagePanel = null ;
	
		#endregion
		
		#region constructor

		public ImageMapEditor()
		{
			
		}

		#endregion

		#region Methods

		protected virtual ImageList GetImageList(object component) 
		{
			if (component is MozItem.ImageCollection) 
			{
				return ((MozItem.ImageCollection) component).GetImageList();
			}

			return null ;
		}

		#endregion
		
		#region overrides

		public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			wfes = (IWindowsFormsEditorService)	provider.GetService(typeof(IWindowsFormsEditorService));
			if((wfes == null) || (context == null))
				return null ;
			
			ImageList imageList = GetImageList(context.Instance) ;
			if ((imageList == null) || (imageList.Images.Count==0))
				return -1 ;

			m_imagePanel = new ImageListPanel(); 
						
			m_imagePanel.BackgroundColor = Color.FromArgb(241,241,241);
			m_imagePanel.BackgroundOverColor = Color.FromArgb(102,154,204);
			m_imagePanel.HLinesColor = Color.FromArgb(182,189,210);
			m_imagePanel.VLinesColor = Color.FromArgb(182,189,210);
			m_imagePanel.BorderColor = Color.FromArgb(0,0,0);
			m_imagePanel.EnableDragDrop = true;
			m_imagePanel.Init(imageList,12,12,6,(int)value);
			
			// add listner for event
			m_imagePanel.ItemClick += new ImageListPanelEventHandler(OnItemClicked);
			
			// set m_selectedIndex to -1 in case the dropdown is closed without selection
			m_selectedIndex = -1;
			// show the popup as a drop-down
			wfes.DropDownControl(m_imagePanel) ;
			
			// return the selection (or the original value if none selected)
			return (m_selectedIndex != -1) ? m_selectedIndex : (int) value ;
		}

		public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
		{
			if(context != null && context.Instance != null ) 
			{
				return UITypeEditorEditStyle.DropDown ;
			}
			return base.GetEditStyle (context);
		}
		

		public override bool GetPaintValueSupported(System.ComponentModel.ITypeDescriptorContext context)
		{
			return true;
		}

		public override void PaintValue(System.Drawing.Design.PaintValueEventArgs pe)
		{
			int imageIndex = -1 ;	
			// value is the image index
			if(pe.Value != null) 
			{
				try 
				{
					imageIndex = (int)Convert.ToUInt16( pe.Value.ToString() ) ;
				}
				catch (Exception ex)
				{
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(ImageMapEditor));
                    MessageDxUtil.ShowError(ex.Message); 
				}
			}
			// no instance, or the instance represents an undefined image
			if((pe.Context.Instance == null) || (imageIndex < 0))
				return ;
			// get the image set
			ImageList imageList = GetImageList(pe.Context.Instance) ;
			// make sure everything is valid
			if((imageList == null) || (imageList.Images.Count == 0) || (imageIndex >= imageList.Images.Count))
				return ;
			// Draw the preview image
			pe.Graphics.DrawImage(imageList.Images[imageIndex],pe.Bounds);
		}

		#endregion

		#region EventHandlers

		public void OnItemClicked(object sender, ImageListPanelEventArgs e)
		{
			m_selectedIndex = ((ImageListPanelEventArgs) e).SelectedItem;
			
			//remove listner
			m_imagePanel.ItemClick -= new ImageListPanelEventHandler(OnItemClicked);
			
			// close the drop-dwon, we are done
			wfes.CloseDropDown() ;

			m_imagePanel.Dispose() ;
			m_imagePanel = null ;
		}

		#endregion
	
	}
}
