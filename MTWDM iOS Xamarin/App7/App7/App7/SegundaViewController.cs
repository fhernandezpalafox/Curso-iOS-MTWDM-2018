// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;

namespace App7
{
	public partial class SegundaViewController : UIViewController
	{
        public string MsgEnviado;

		public SegundaViewController (IntPtr handle) : base (handle)
		{
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            lblMensaje.Text = MsgEnviado;
        }
    }
}