﻿#pragma checksum "C:\Users\MCT\Desktop\oscilloscope_keller\oscilloscope_keller\MainPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "D7A006542C808DE996AFF363F584A8709C128FDB1D84A554F1A2D064ADF3E042"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace oscilloscope_keller
{
    partial class MainPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.685")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // MainPage.xaml line 22
                {
                    this.poly = (global::Windows.UI.Xaml.Shapes.Polyline)(target);
                }
                break;
            case 3: // MainPage.xaml line 23
                {
                    this.polyB = (global::Windows.UI.Xaml.Shapes.Polyline)(target);
                }
                break;
            case 4: // MainPage.xaml line 25
                {
                    this.TriggerSlider = (global::Windows.UI.Xaml.Controls.Slider)(target);
                    ((global::Windows.UI.Xaml.Controls.Slider)this.TriggerSlider).ValueChanged += this.SliderValueChange;
                }
                break;
            case 5: // MainPage.xaml line 34
                {
                    this.A = (global::Windows.UI.Xaml.Controls.CheckBox)(target);
                    ((global::Windows.UI.Xaml.Controls.CheckBox)this.A).Click += this.ChannelA_Click;
                }
                break;
            case 6: // MainPage.xaml line 35
                {
                    this.B = (global::Windows.UI.Xaml.Controls.CheckBox)(target);
                    ((global::Windows.UI.Xaml.Controls.CheckBox)this.B).Click += this.ChannelB_Click;
                }
                break;
            case 7: // MainPage.xaml line 36
                {
                    this.Trigger = (global::Windows.UI.Xaml.Controls.CheckBox)(target);
                    ((global::Windows.UI.Xaml.Controls.CheckBox)this.Trigger).Click += this.TriggerClick;
                }
                break;
            case 8: // MainPage.xaml line 38
                {
                    this.Vmax = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 9: // MainPage.xaml line 39
                {
                    this.Vmin = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 10: // MainPage.xaml line 40
                {
                    this.Vavg = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 11: // MainPage.xaml line 41
                {
                    this.Veff = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.685")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

