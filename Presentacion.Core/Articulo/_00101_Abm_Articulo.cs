namespace Presentacion.Core.Articulo
{
    using System;
    using System.Windows.Forms;
    using Aplicacion.Constantes.Imagenes;
    using FormularioBase;
    using FormularioBase.Helpers;
    using static Aplicacion.Constantes.Clases.ValidacionDatosEntrada;
    using Servicio.Interfaces.Articulo;
    using Servicio.Interfaces.IVA;
    using Servicio.Interfaces.Marca;
    using Servicio.Interfaces.Rubro;
    using Servicio.Interfaces.UnidadMedida;
    using StructureMap;
    using System.Drawing;
    using Servicio.Interfaces.Articulo.DTOs;
    using Aplicacion.Constantes.Clases;

    public partial class _00101_Abm_Articulo : FormularioAbm
    {
        
        private readonly IUnidadMedidaServicio _unidadMedidaServicio;
        private readonly IMarcaServicio _marcaServicio;
        private readonly IIVAServicio _ivaServicio;
        private readonly IRubroServicio _rubroServicio;

        private readonly IArticuloServicio articuloServicio;
        public _00101_Abm_Articulo(TipoOperacion tipoOperacion, long? entidadId = null)
            : base(tipoOperacion, entidadId)
        {
            InitializeComponent();

            _marcaServicio = ObjectFactory.GetInstance<IMarcaServicio>();
            _ivaServicio = ObjectFactory.GetInstance<IIVAServicio>();
            _rubroServicio = ObjectFactory.GetInstance<IRubroServicio>();
            _unidadMedidaServicio = ObjectFactory.GetInstance<IUnidadMedidaServicio>();
            articuloServicio = ObjectFactory.GetInstance<IArticuloServicio>();

            AsignarEvento_EnterLeave(this);

            txtAbreviatura.KeyPress += delegate (object sender, KeyPressEventArgs args)
            {
                NoInyeccion(sender, args);
                
                NoSimbolos(sender, args);
            };

            txtcodigoBarra.KeyPress += delegate (object sender, KeyPressEventArgs args)
            {
                NoInyeccion(sender, args);
                NoSimbolos(sender, args);
            };

            txtCodigo.KeyPress += delegate (object sender, KeyPressEventArgs args)
            {
                NoInyeccion(sender, args);
                NoSimbolos(sender, args);
                NoLetras(sender, args);
            };

            txtCodigoDelProveedor.KeyPress += delegate (object sender, KeyPressEventArgs args)
            {
                NoInyeccion(sender, args);
                NoSimbolos(sender, args);
                NoLetras(sender, args);
            };

            txtDescripcion.KeyPress += delegate (object sender, KeyPressEventArgs args)
            {
                
                NoSimbolos(sender, args);
             
            };

            txtDetalle.KeyPress += delegate (object sender, KeyPressEventArgs args)
            {
                
                NoSimbolos(sender, args);
               
            };

            txtUbicacion.KeyPress += delegate (object sender, KeyPressEventArgs args)
            {
               
                NoSimbolos(sender, args);
              
            };

            CargarDatosObligatorios();

           
        }

        private void CargarDatosObligatorios()
        {
            AgregarControlesObligatorios(txtCodigo, "Codigo");
            AgregarControlesObligatorios(txtcodigoBarra, "Codigo de Barra");
            AgregarControlesObligatorios(txtCodigoDelProveedor, "Codigo de Proveedor");
            AgregarControlesObligatorios(txtDetalle, "Detalle");
            AgregarControlesObligatorios(txtDescripcion, "Descripcion");
            AgregarControlesObligatorios(txtAbreviatura, "Abreviatura");
            AgregarControlesObligatorios(txtUbicacion, "Ubicacion");
            AgregarControlesObligatorios(cmbIva, "Iva");
            AgregarControlesObligatorios(cmbMarca, "Marca");
            AgregarControlesObligatorios(cmbRubro, "Rubro");
            AgregarControlesObligatorios(cmbUnidad, "Unidad");
            AgregarControlesObligatorios(nudPrecioCosto, "Precio Costo");
            AgregarControlesObligatorios(nudStock, "Stock");
            AgregarControlesObligatorios(nudStockMin, "Stock Minimo");
           
        }

      

        public override void CargarDatos(long? entidadId)
        {

            dtpHoraVenta.Format = DateTimePickerFormat.Custom;
            dtpHoraVenta.CustomFormat = "HH:mm:ss tt";

            Poblar_ComboBox(cmbLista, Enum.GetValues(typeof(TipoArticulo)));
            var marcas = _marcaServicio.Get(string.Empty);
            var ivas = _ivaServicio.Get(string.Empty);
            var rubros = _rubroServicio.Get(string.Empty);
            var unidadMedidas = _unidadMedidaServicio.Get(string.Empty);

            Poblar_ComboBox(this.cmbMarca, marcas, "Descripcion", "Id");
            Poblar_ComboBox(this.cmbIva, ivas, "Descripcion", "Id");
            Poblar_ComboBox(this.cmbRubro, rubros, "Descripcion", "Id");
            Poblar_ComboBox(this.cmbUnidad, unidadMedidas, "Descripcion", "Id");

            if (entidadId.HasValue && entidadId > 0)
            {
                var entidad = articuloServicio.GetById(entidadId.Value);

                txtAbreviatura.Text = entidad.Abreviatura;
                txtCodigo.Text = entidad.Codigo.ToString();
                txtcodigoBarra.Text = entidad.CodigoBarra;
                txtDescripcion.Text = entidad.Descripcion;
                txtDetalle.Text = entidad.Detalle;
                txtUbicacion.Text = entidad.Ubicacion;
                txtCodigoDelProveedor.Text = entidad.CodigoDeProveedor.ToString();
                cmbIva.SelectedValue = entidad.IvaId;
                cmbMarca.SelectedValue = entidad.MarcaId;
                cmbRubro.SelectedValue = entidad.RubroId;
                cmbUnidad.SelectedValue = entidad.UnidadMedidaId;
                imgFoto.Image = Imagen.Convertir(entidad.Foto);
                nudLimiteVenta.Value = entidad.LimiteVenta;
                nudPrecioCosto.Value = entidad.PrecioCosto;
                nudStock.Value = entidad.Stock;
                nudStockMin.Value = entidad.StockMinimo;
                chkActivarHoraVenta.Checked = entidad.ActivarHoraVenta;
                ckbDescontarStock.Checked = entidad.DescuentaStock;
                chkActivarLimite.Checked = entidad.ActivarLimiteVenta;
                chkPermitirStockNeg.Checked = entidad.PermiteStockNegativo;
                dtpHoraVenta.Value = entidad.HoraLimiteVenta;
                cmbLista.SelectedItem = entidad.TipoArticulo;

                if (_tipoOperacion == TipoOperacion.Eliminar)
                {
                    DesactivarControles(this);
                    btnLimpiar.Visible = false;
                }
                
            }
            else
            {
                LimpiarControles(this);
                txtCodigo.Focus();
            }
        }

        public override void EjecutarComandoNuevo()
        {
            articuloServicio.Add(AsignarArticuloDto());
        }

        public override void EjecutarComandoModificar(long? entidadId)
        {
            articuloServicio.Update(AsignarArticuloDto(entidadId));
        }

        public override void EjecutarComandoEliminar(long? entidadId)
        {
            
            articuloServicio.Delete(entidadId.Value);
        }

        private ArticuloDto AsignarArticuloDto(long? entidadId = null)
        {
            var ivaPorcentaje = _ivaServicio.GetById((long)cmbIva.SelectedValue);

            return new ArticuloDto
            {
                Id = entidadId.HasValue ? _entidadId.Value : 0,
                Abreviatura = txtAbreviatura.Text,
                Codigo = Int32.Parse(txtCodigo.Text),
                CodigoBarra = txtcodigoBarra.Text,
                CodigoDeProveedor = Int32.Parse(txtCodigoDelProveedor.Text),
                Ubicacion = txtUbicacion.Text,
                Descripcion = txtDescripcion.Text,
                Detalle = txtDetalle.Text,
                IvaId = (long)cmbIva.SelectedValue,
                MarcaId = (long)cmbMarca.SelectedValue,
                RubroId = (long)cmbRubro.SelectedValue,
                UnidadMedidaId = (long)cmbUnidad.SelectedValue,
                LimiteVenta = nudLimiteVenta.Value,
                PrecioCosto = nudPrecioCosto.Value + (nudPrecioCosto.Value * (ivaPorcentaje.Porcentaje / 100) ),
                StockMinimo = nudStockMin.Value,
                Stock = nudStock.Value,
                ActivarHoraVenta = chkActivarHoraVenta.Checked,
                DescuentaStock = ckbDescontarStock.Checked,
                ActivarLimiteVenta = chkActivarLimite.Checked,
                PermiteStockNegativo = chkPermitirStockNeg.Checked,
                HoraLimiteVenta = dtpHoraVenta.Value,
                Foto = Imagen.Convertir(this.imgFoto.Image),
                TipoArticulo = (TipoArticulo)cmbLista.SelectedItem,                 
                RowVersion = _rowVersion
                
            };
        }

      

        private void btnAgregarImagen_Click(object sender, EventArgs e)
        {
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                imgFoto.Image = !string.IsNullOrEmpty(openFile.FileName)
                    ? Image.FromFile(openFile.FileName)
                    : Imagen.Camara;
            }
        }

        private void btnNuevaMarca_Click(object sender, EventArgs e)
        {
            var fNuevaMarca = new _00103_Abm_Marca(TipoOperacion.Nuevo);
            fNuevaMarca.ShowDialog();
            if (fNuevaMarca.RelizoAlgunaOperacion)
            {
                Poblar_ComboBox(this.cmbMarca,
                    _marcaServicio.Get(string.Empty),
                    "Descripcion",
                    "Id");
            }
        }

        private void btnNuevoRubro_Click(object sender, EventArgs e)
        {
            var fNuevoRubro = new _00105_Abm_Rubro(TipoOperacion.Nuevo);
            fNuevoRubro.ShowDialog();
            if (fNuevoRubro.RelizoAlgunaOperacion)
            {
                Poblar_ComboBox(this.cmbRubro,
                    _rubroServicio.Get(string.Empty),
                    "Descripcion",
                    "Id");
            }
        }

        private void btnNuevaUnidad_Click(object sender, EventArgs e)
        {
            var fNuevaUnidadMedida = new _00306_Abm_UnidadMedida(TipoOperacion.Nuevo);
            fNuevaUnidadMedida.ShowDialog();
            if (fNuevaUnidadMedida.RelizoAlgunaOperacion)
            {
                Poblar_ComboBox(this.cmbUnidad,
                    _unidadMedidaServicio.Get(string.Empty),
                    "Descripcion",
                    "Id");
            }
        }

        private void btnNuevoIva_Click(object sender, EventArgs e)
        {
            var fNuevoIva = new _00123_Abm_Iva(TipoOperacion.Nuevo);
            fNuevoIva.ShowDialog();
            if (fNuevoIva.RelizoAlgunaOperacion)
            {
                Poblar_ComboBox(this.cmbIva,
                    _ivaServicio.Get(string.Empty),
                    "Descripcion",
                    "Id");
            }
        }

        private void chkActivarHoraVenta_CheckedChanged(object sender, EventArgs e)
        {
            if (chkActivarHoraVenta.Checked)
            {
                dtpHoraVenta.Enabled = true;
            }
        }

        private void chkActivarLimite_CheckedChanged(object sender, EventArgs e)
        {
            if (chkActivarLimite.Checked)
            {
                nudLimiteVenta.Enabled = true;
            }
        }
    }
}
