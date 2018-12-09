// This file has been autogenerated from a class added in the UI designer.

using System;
using Entidades;
using Foundation;
using UIKit;
using CoreGraphics;
using System.Timers;
using AppSQLite.PickerViewModel;

namespace AppSQLite
{
    public partial class AltaViewController : UIViewController
	{
        static Timer aTimer;

        Conexion objConexion;

        Utilidades objUtilidades;

        public string CajaTexto { get; set; }

        UIPickerView pickerView;

        public string[] names = new string[] {
            "Amy Burns",
            "Kevin Mullins",
            "Craig Dunn",
            "Joel Martinez",
            "Charles Petzold"
        };

        public int esEdicion { get; set; } 

		public AltaViewController (IntPtr handle) : base (handle)
		{
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            objConexion = new Conexion();

            objUtilidades = new Utilidades(this);

            pickerView = new UIPickerView();

            var pickerModel = new PickerModel(txtRepreLegal,names);

            pickerView.Model = pickerModel;

            IniciarParametros();

            btnCancelar.Clicked += delegate {
                DismissViewController(true,null);
            };

            btnGuardar.Clicked += delegate {

                onGuardar();
            
            };

            btnGuardar2.TouchUpInside += delegate {
                
                onGuardar();
            };

            txtNombre.ShouldReturn += delegate (UITextField textField)
            {
                textField.ResignFirstResponder();

                return true;
            };


            txtRfc.ShouldReturn += delegate (UITextField textField){

                textField.ResignFirstResponder();

                return true;
            };

            txtNombre.ReturnKeyType = UIReturnKeyType.Done;


            txtNombre.EditingDidBegin += (sender, e) => {
                
                    InvokeOnMainThread(() => { 

                        if (aTimer != null)
                        {
                          aTimer.Stop();
                        }
                   

                        txtNombre.Layer.BorderColor = UIColor.Blue.CGColor;
                        txtNombre.Layer.BorderWidth = 0.5f;
                        txtNombre.Layer.CornerRadius = 20;

                        Validador.Hidden = true;
                    });
                
            };

            InicializarToolbar();

            //consultar datos
            if (esEdicion==1)
            {

            }
        }

        private void onGuardar()
        {
            //Validacion de campos
            bool esValido = ValidarCampos();
            if (!esValido)
            {
                return;
            }

            try
            {
                //Inseraccion de la informacion
                Empresa objEmpresa = new Empresa
                {
                    Nombre = txtNombre.Text,
                    Domicilio = txtDomicilio.Text,
                    RFC = txtRfc.Text,
                    RepresentanteLegal = txtRepreLegal.Text
                };
                int resultado = objConexion.conexion.Insert(objEmpresa);

                if (resultado == 1)
                {
                    objUtilidades.MessageBox("Empresa", "Se insertaron correctamente", "Alert");
                }

            }
            catch (Exception ex)
            {
                objUtilidades.MessageBox("Empresa", "Hubo un error " + ex.Message, "Alert");
            }
        }

        public bool ValidarCampos(){
            
            bool esValido = true;

            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                txtNombre.Layer.BorderColor = UIColor.Red.CGColor;

                txtNombre.Layer.BorderWidth = 0.5f;

                txtNombre.Layer.CornerRadius = 20;

                esValido = false;

                aTimer = new Timer();

                aTimer.Interval = 500;// 1000 segundos

                aTimer.Elapsed += OnTimedEvent;
  
                aTimer.Enabled = true;

                aTimer.Start();


            }

            return esValido;

        }

        void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            InvokeOnMainThread(() => { 
            
                var esVisible = Validador.Hidden;

                if (esVisible)
                    Validador.Hidden = false;
                else
                    Validador.Hidden = true;
                
            });

        }

        public void IniciarParametros(){
            
            Validador.Hidden = true;

            txtRepreLegal.Placeholder = "Representante Legal";
        }

        public void NavegarTextFields(UITextField textField){

            var nextTage = textField.Tag + 1;

            var nextResponder = textField.Superview.ViewWithTag(nextTage) as UIResponder;

            if (nextResponder != null) {
                
                nextResponder.BecomeFirstResponder();
            }
            else {
                
                textField.ResignFirstResponder();
            }

        }

        public void InicializarToolbar(){
            // Instanciar un toolbar
            UIToolbar toolbar = new UIToolbar();

            toolbar.BarStyle = UIBarStyle.Default;

            //Crear un label
            var labelTitulo = new UILabel(new CGRect(x: 0, y: 50, width: 150, height: 20))
            {
                BackgroundColor = UIColor.Clear,
                TextColor = UIColor.Gray.ColorWithAlpha(0.5f),
                TextAlignment = UITextAlignment.Left,
                Text = txtRepreLegal.Placeholder
            };
            labelTitulo.SizeToFit();



            // Crear los UIBarButtonItem de nuestra toolbar
            var tituloCajaTexto = new UIBarButtonItem(labelTitulo);
            var cancelarBoton = new UIBarButtonItem("Cancelar", UIBarButtonItemStyle.Done, (s, e) => { this.txtRepreLegal.ResignFirstResponder(); });
            var espacioEntreBoton = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace, null, null);
            var listoButton = new UIBarButtonItem("Listo", UIBarButtonItemStyle.Done,
                                                             (s, e) => {
                                                                this.txtRepreLegal.Text = names[pickerView.SelectedRowInComponent(0)];
                                                                this.txtRepreLegal.ResignFirstResponder();
                                                             });
            toolbar.SetItems(new UIBarButtonItem[] { cancelarBoton, espacioEntreBoton, tituloCajaTexto, espacioEntreBoton, listoButton }, true);

            toolbar.TranslatesAutoresizingMaskIntoConstraints = false;
            pickerView.TranslatesAutoresizingMaskIntoConstraints = false;

            toolbar.SizeToFit();

            // Tell the textbox to use the picker for input
            this.txtRepreLegal.InputView = pickerView;

            // Display the toolbar over the pickers
            this.txtRepreLegal.InputAccessoryView = toolbar;
        }

    }
}
