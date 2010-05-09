
// This file has been generated by the GUI designer. Do not modify.
namespace Zencomic
{
	public partial class PreferencesDialog
	{
		private global::Gtk.Alignment alignment1;

		private global::Gtk.HBox hbox3;

		private global::Gtk.Image panelIcon;

		private global::Gtk.Label label3;

		private global::Gtk.Alignment alignment4;

		private global::Gtk.Notebook notebook1;

		private global::Gtk.Alignment alignment2;

		private global::Gtk.Table table1;

		private global::Gtk.Frame frame1;

		private global::Gtk.Alignment GtkAlignment2;

		private global::Gtk.VBox vbox3;

		private global::Gtk.RadioButton radiobutton2;

		private global::Gtk.RadioButton radiobutton3;

		private global::Gtk.Label GtkLabel2;

		private global::Gtk.HBox hbox1;

		private global::Gtk.SpinButton popupDelayButton;

		private global::Gtk.Label label6;

		private global::Gtk.HBox hbox2;

		private global::Gtk.SpinButton showDelayButton;

		private global::Gtk.Label label5;

		private global::Gtk.Label label4;

		private global::Gtk.Label label1;

		private global::Gtk.Alignment alignment3;

		private global::Gtk.Alignment alignment5;

		private global::Gtk.Alignment alignment6;

		private global::Gtk.Frame frame2;

		private global::Gtk.Alignment GtkAlignment;

		private global::Gtk.Alignment alignment7;

		private global::Gtk.ScrolledWindow GtkScrolledWindow;

		private global::Gtk.TreeView comicTv;

		private global::Gtk.Label GtkLabel3;

		private global::Gtk.Label label2;

		private global::Gtk.Button buttonOk;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget Zencomic.PreferencesDialog
			this.Name = "Zencomic.PreferencesDialog";
			this.Title = global::Mono.Unix.Catalog.GetString ("Zencomic preferences");
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			this.DefaultWidth = 0;
			this.DefaultHeight = 0;
			this.AcceptFocus = false;
			this.FocusOnMap = false;
			// Internal child Zencomic.PreferencesDialog.VBox
			global::Gtk.VBox w1 = this.VBox;
			w1.Name = "dialog1_VBox";
			w1.BorderWidth = ((uint)(2));
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.alignment1 = new global::Gtk.Alignment (0.5f, 0.5f, 1f, 1f);
			this.alignment1.Name = "alignment1";
			this.alignment1.LeftPadding = ((uint)(10));
			// Container child alignment1.Gtk.Container+ContainerChild
			this.hbox3 = new global::Gtk.HBox ();
			this.hbox3.Name = "hbox3";
			this.hbox3.Spacing = -11;
			// Container child hbox3.Gtk.Box+BoxChild
			this.panelIcon = new global::Gtk.Image ();
			this.panelIcon.Name = "panelIcon";
			this.hbox3.Add (this.panelIcon);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox3[this.panelIcon]));
			w2.Position = 0;
			w2.Expand = false;
			w2.Fill = false;
			// Container child hbox3.Gtk.Box+BoxChild
			this.label3 = new global::Gtk.Label ();
			this.label3.Name = "label3";
			this.label3.Xpad = 30;
			this.label3.Ypad = 18;
			this.label3.Xalign = 0f;
			this.label3.LabelProp = global::Mono.Unix.Catalog.GetString ("<b><big>Preferences</big></b>");
			this.label3.UseMarkup = true;
			this.hbox3.Add (this.label3);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox3[this.label3]));
			w3.Position = 1;
			w3.Expand = false;
			w3.Fill = false;
			this.alignment1.Add (this.hbox3);
			w1.Add (this.alignment1);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(w1[this.alignment1]));
			w5.Position = 0;
			w5.Expand = false;
			w5.Fill = false;
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.alignment4 = new global::Gtk.Alignment (0.5f, 0.5f, 1f, 1f);
			this.alignment4.Name = "alignment4";
			// Container child alignment4.Gtk.Container+ContainerChild
			this.notebook1 = new global::Gtk.Notebook ();
			this.notebook1.CanFocus = true;
			this.notebook1.Name = "notebook1";
			this.notebook1.CurrentPage = 0;
			this.notebook1.BorderWidth = ((uint)(3));
			// Container child notebook1.Gtk.Notebook+NotebookChild
			this.alignment2 = new global::Gtk.Alignment (0.5f, 0.5f, 1f, 1f);
			this.alignment2.Name = "alignment2";
			this.alignment2.BorderWidth = ((uint)(9));
			// Container child alignment2.Gtk.Container+ContainerChild
			this.table1 = new global::Gtk.Table (((uint)(4)), ((uint)(2)), false);
			this.table1.Name = "table1";
			this.table1.RowSpacing = ((uint)(13));
			this.table1.ColumnSpacing = ((uint)(6));
			this.table1.BorderWidth = ((uint)(6));
			// Container child table1.Gtk.Table+TableChild
			this.frame1 = new global::Gtk.Frame ();
			this.frame1.Name = "frame1";
			this.frame1.ShadowType = ((global::Gtk.ShadowType)(0));
			// Container child frame1.Gtk.Container+ContainerChild
			this.GtkAlignment2 = new global::Gtk.Alignment (0f, 0f, 1f, 1f);
			this.GtkAlignment2.Name = "GtkAlignment2";
			this.GtkAlignment2.LeftPadding = ((uint)(12));
			// Container child GtkAlignment2.Gtk.Container+ContainerChild
			this.vbox3 = new global::Gtk.VBox ();
			this.vbox3.Name = "vbox3";
			this.vbox3.Spacing = 6;
			this.vbox3.BorderWidth = ((uint)(6));
			// Container child vbox3.Gtk.Box+BoxChild
			this.radiobutton2 = new global::Gtk.RadioButton (global::Mono.Unix.Catalog.GetString ("Notification"));
			this.radiobutton2.TooltipMarkup = "Use freedesktop notification server over D-Bus";
			this.radiobutton2.CanFocus = true;
			this.radiobutton2.Name = "radiobutton2";
			this.radiobutton2.DrawIndicator = true;
			this.radiobutton2.UseUnderline = true;
			this.radiobutton2.Group = new global::GLib.SList (global::System.IntPtr.Zero);
			this.vbox3.Add (this.radiobutton2);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.vbox3[this.radiobutton2]));
			w6.Position = 0;
			w6.Expand = false;
			w6.Fill = false;
			// Container child vbox3.Gtk.Box+BoxChild
			this.radiobutton3 = new global::Gtk.RadioButton (global::Mono.Unix.Catalog.GetString ("Window"));
			this.radiobutton3.TooltipMarkup = "Use a dedicated Gtk window";
			this.radiobutton3.CanFocus = true;
			this.radiobutton3.Name = "radiobutton3";
			this.radiobutton3.DrawIndicator = true;
			this.radiobutton3.UseUnderline = true;
			this.radiobutton3.Group = this.radiobutton2.Group;
			this.vbox3.Add (this.radiobutton3);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.vbox3[this.radiobutton3]));
			w7.Position = 1;
			w7.Expand = false;
			w7.Fill = false;
			this.GtkAlignment2.Add (this.vbox3);
			this.frame1.Add (this.GtkAlignment2);
			this.GtkLabel2 = new global::Gtk.Label ();
			this.GtkLabel2.Name = "GtkLabel2";
			this.GtkLabel2.LabelProp = global::Mono.Unix.Catalog.GetString ("<b>Popup method</b>");
			this.GtkLabel2.UseMarkup = true;
			this.frame1.LabelWidget = this.GtkLabel2;
			this.table1.Add (this.frame1);
			global::Gtk.Table.TableChild w10 = ((global::Gtk.Table.TableChild)(this.table1[this.frame1]));
			w10.TopAttach = ((uint)(3));
			w10.BottomAttach = ((uint)(4));
			w10.XOptions = ((global::Gtk.AttachOptions)(4));
			w10.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.hbox1 = new global::Gtk.HBox ();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.popupDelayButton = new global::Gtk.SpinButton (1, 100, 1);
			this.popupDelayButton.CanFocus = true;
			this.popupDelayButton.Name = "popupDelayButton";
			this.popupDelayButton.Adjustment.PageIncrement = 10;
			this.popupDelayButton.ClimbRate = 1;
			this.popupDelayButton.Numeric = true;
			this.popupDelayButton.SnapToTicks = true;
			this.popupDelayButton.Value = 30;
			this.hbox1.Add (this.popupDelayButton);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.popupDelayButton]));
			w11.Position = 0;
			w11.Expand = false;
			w11.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.label6 = new global::Gtk.Label ();
			this.label6.Name = "label6";
			this.label6.LabelProp = global::Mono.Unix.Catalog.GetString ("Popup showing time (in second)");
			this.hbox1.Add (this.label6);
			global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.label6]));
			w12.Position = 1;
			w12.Expand = false;
			w12.Fill = false;
			this.table1.Add (this.hbox1);
			global::Gtk.Table.TableChild w13 = ((global::Gtk.Table.TableChild)(this.table1[this.hbox1]));
			w13.TopAttach = ((uint)(1));
			w13.BottomAttach = ((uint)(2));
			w13.XOptions = ((global::Gtk.AttachOptions)(4));
			w13.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.hbox2 = new global::Gtk.HBox ();
			this.hbox2.Name = "hbox2";
			this.hbox2.Spacing = 6;
			// Container child hbox2.Gtk.Box+BoxChild
			this.showDelayButton = new global::Gtk.SpinButton (1, 100, 1);
			this.showDelayButton.CanFocus = true;
			this.showDelayButton.Name = "showDelayButton";
			this.showDelayButton.Adjustment.PageIncrement = 10;
			this.showDelayButton.ClimbRate = 1;
			this.showDelayButton.Numeric = true;
			this.showDelayButton.SnapToTicks = true;
			this.showDelayButton.Value = 15;
			this.hbox2.Add (this.showDelayButton);
			global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.showDelayButton]));
			w14.Position = 0;
			w14.Expand = false;
			w14.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.label5 = new global::Gtk.Label ();
			this.label5.Name = "label5";
			this.label5.LabelProp = global::Mono.Unix.Catalog.GetString ("Delay between apparition (in minutes)");
			this.hbox2.Add (this.label5);
			global::Gtk.Box.BoxChild w15 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.label5]));
			w15.Position = 1;
			w15.Expand = false;
			w15.Fill = false;
			this.table1.Add (this.hbox2);
			global::Gtk.Table.TableChild w16 = ((global::Gtk.Table.TableChild)(this.table1[this.hbox2]));
			w16.XOptions = ((global::Gtk.AttachOptions)(4));
			w16.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label4 = new global::Gtk.Label ();
			this.label4.Sensitive = false;
			this.label4.Name = "label4";
			this.label4.LabelProp = global::Mono.Unix.Catalog.GetString (" ");
			this.table1.Add (this.label4);
			global::Gtk.Table.TableChild w17 = ((global::Gtk.Table.TableChild)(this.table1[this.label4]));
			w17.TopAttach = ((uint)(2));
			w17.BottomAttach = ((uint)(3));
			w17.XOptions = ((global::Gtk.AttachOptions)(4));
			w17.YOptions = ((global::Gtk.AttachOptions)(4));
			this.alignment2.Add (this.table1);
			this.notebook1.Add (this.alignment2);
			// Notebook tab
			this.label1 = new global::Gtk.Label ();
			this.label1.Name = "label1";
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString ("General");
			this.notebook1.SetTabLabel (this.alignment2, this.label1);
			this.label1.ShowAll ();
			// Container child notebook1.Gtk.Notebook+NotebookChild
			this.alignment3 = new global::Gtk.Alignment (0.5f, 0.5f, 1f, 1f);
			this.alignment3.Name = "alignment3";
			// Container child alignment3.Gtk.Container+ContainerChild
			this.alignment5 = new global::Gtk.Alignment (0.5f, 0.5f, 1f, 1f);
			this.alignment5.Name = "alignment5";
			this.alignment5.TopPadding = ((uint)(2));
			// Container child alignment5.Gtk.Container+ContainerChild
			this.alignment6 = new global::Gtk.Alignment (0.5f, 0.5f, 1f, 1f);
			this.alignment6.Name = "alignment6";
			// Container child alignment6.Gtk.Container+ContainerChild
			this.frame2 = new global::Gtk.Frame ();
			this.frame2.Name = "frame2";
			this.frame2.ShadowType = ((global::Gtk.ShadowType)(0));
			this.frame2.BorderWidth = ((uint)(6));
			// Container child frame2.Gtk.Container+ContainerChild
			this.GtkAlignment = new global::Gtk.Alignment (0f, 0f, 1f, 1f);
			this.GtkAlignment.Name = "GtkAlignment";
			this.GtkAlignment.LeftPadding = ((uint)(12));
			this.GtkAlignment.BorderWidth = ((uint)(3));
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			this.alignment7 = new global::Gtk.Alignment (0.5f, 0.5f, 1f, 1f);
			this.alignment7.Name = "alignment7";
			this.alignment7.TopPadding = ((uint)(5));
			// Container child alignment7.Gtk.Container+ContainerChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.comicTv = new global::Gtk.TreeView ();
			this.comicTv.CanFocus = true;
			this.comicTv.Name = "comicTv";
			this.GtkScrolledWindow.Add (this.comicTv);
			this.alignment7.Add (this.GtkScrolledWindow);
			this.GtkAlignment.Add (this.alignment7);
			this.frame2.Add (this.GtkAlignment);
			this.GtkLabel3 = new global::Gtk.Label ();
			this.GtkLabel3.Name = "GtkLabel3";
			this.GtkLabel3.LabelProp = global::Mono.Unix.Catalog.GetString ("<b>Loaded comics</b>");
			this.GtkLabel3.UseMarkup = true;
			this.frame2.LabelWidget = this.GtkLabel3;
			this.alignment6.Add (this.frame2);
			this.alignment5.Add (this.alignment6);
			this.alignment3.Add (this.alignment5);
			this.notebook1.Add (this.alignment3);
			global::Gtk.Notebook.NotebookChild w27 = ((global::Gtk.Notebook.NotebookChild)(this.notebook1[this.alignment3]));
			w27.Position = 1;
			// Notebook tab
			this.label2 = new global::Gtk.Label ();
			this.label2.Name = "label2";
			this.label2.LabelProp = global::Mono.Unix.Catalog.GetString ("Comics");
			this.notebook1.SetTabLabel (this.alignment3, this.label2);
			this.label2.ShowAll ();
			this.alignment4.Add (this.notebook1);
			w1.Add (this.alignment4);
			global::Gtk.Box.BoxChild w29 = ((global::Gtk.Box.BoxChild)(w1[this.alignment4]));
			w29.Position = 1;
			// Internal child Zencomic.PreferencesDialog.ActionArea
			global::Gtk.HButtonBox w30 = this.ActionArea;
			w30.Name = "dialog1_ActionArea";
			w30.Spacing = 10;
			w30.BorderWidth = ((uint)(5));
			w30.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(4));
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonOk = new global::Gtk.Button ();
			this.buttonOk.CanDefault = true;
			this.buttonOk.CanFocus = true;
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.UseStock = true;
			this.buttonOk.UseUnderline = true;
			this.buttonOk.Label = "gtk-close";
			this.AddActionWidget (this.buttonOk, -7);
			global::Gtk.ButtonBox.ButtonBoxChild w31 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w30[this.buttonOk]));
			w31.Expand = false;
			w31.Fill = false;
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.Show ();
			this.showDelayButton.ValueChanged += new global::System.EventHandler (this.OnShowDelayButtonValueChanged);
			this.popupDelayButton.ValueChanged += new global::System.EventHandler (this.OnPopupDelayButtonValueChanged);
			this.radiobutton2.Toggled += new global::System.EventHandler (this.OnRadiobutton2Toggled);
			this.radiobutton3.Toggled += new global::System.EventHandler (this.OnRadiobutton3Toggled);
		}
	}
}
