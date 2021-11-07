using System;
using System.ComponentModel;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XF.Ctrls.iOS;
using XFDropdown = XF.Ctrls.Dropdown;
using XIDropdown = FPT.Framework.iOS.UI.DropDown.DropDown;

[assembly: ExportRenderer(typeof(XFDropdown), typeof(DropdownRenderer))]
namespace XF.Ctrls.iOS
{
    public class DropdownRenderer : LabelRenderer
    {
        XFDropdown xfDropdown = null;
        XIDropdown dropDown = null;
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                dropDown = new XIDropdown();
                xfDropdown = (XFDropdown)e.NewElement;
                dropDown.AnchorView = new WeakReference<UIView>(Control);
                nfloat y = dropDown.Bounds.Height;
                if (y == 0)
                    y += 40;
                dropDown.TopOffset = new CoreGraphics.CGPoint(0, -y);
                dropDown.BottomOffset = new CoreGraphics.CGPoint(0, y);
                string[] data = xfDropdown.ItemsSource.ToArray();
                dropDown.DataSource = data;
                Control.Text = data[0];
                dropDown.SelectionAction = (nint idx, string item) =>
                {
                    if (xfDropdown.SelectedIndex == idx)
                    {
                        dropDown.Dispose();
                        return;
                    }
                    xfDropdown.SelectedIndex = Convert.ToInt32(idx);
                    Control.Text = item;
                    xfDropdown.OnItemSelected(xfDropdown.SelectedIndex);
                };
                UITapGestureRecognizer labelTap = new UITapGestureRecognizer(() =>
                {
                    dropDown.Show();
                });
                if (xfDropdown.SelectedIndex > -1)
                    Control.Text = xfDropdown.ItemsSource[xfDropdown.SelectedIndex];
                Control.UserInteractionEnabled = true;
                Control.AddGestureRecognizer(labelTap);
            }
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            dropDown.Width = (nfloat)Element.Width;
            if (e.PropertyName == XFDropdown.SelectedIndexProperty.PropertyName)
            {
                if (xfDropdown.SelectedIndex > -1)
                    Control.Text = xfDropdown.ItemsSource[xfDropdown.SelectedIndex];
                dropDown.SelectRow(xfDropdown.SelectedIndex);
            }
            if (e.PropertyName == XFDropdown.ItemsSourceProperty.PropertyName)
            {
                string[] data = xfDropdown.ItemsSource.ToArray();
                dropDown.DataSource = data;
            }
        }
    }
}