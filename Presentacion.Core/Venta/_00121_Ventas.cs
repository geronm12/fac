namespace Presentacion.Core.Venta
{
    using System;
    using System.Linq;
    using System.Windows.Forms;
    using Aplicacion.Constantes.Clases;
    using FormularioBase;
    using Presentacion.Core.Articulo.Clases;
    using Presentacion.Core.LookUp;
    using Servicio.Interfaces.Articulo;
    using Servicio.Interfaces.Articulo.DTOs;
    using Servicio.Interfaces.ListaDePrecio;
    using Servicio.Interfaces.Persona;
    using Servicio.Interfaces.Persona.DTOs;
    using StructureMap;

    public partial class _00121_Ventas : Formulario
    {
        private readonly IClienteServicio _clienteServicio;
        private readonly IEmpleadoServicio _empleadoServicio;
        private readonly IListaDePrecioServicios _listaPrecioServicio;
        private readonly IArticuloServicio _articuloServicio;
        private EmpleadoDto _vendedor;
        private ClienteDto _cliente;
        private FacturaView _factura;

        public _00121_Ventas(IEmpleadoServicio empleadoServicio,
            IClienteServicio clienteServicio,
            IListaDePrecioServicios listaPrecioServicio
            ,IArticuloServicio articuloServicio)
        {
            InitializeComponent();

            _listaPrecioServicio = listaPrecioServicio;
            _clienteServicio = clienteServicio;
            _empleadoServicio = empleadoServicio;
            _articuloServicio = articuloServicio;

            _vendedor = null;
            _cliente = null;

            _factura = ObjectFactory.GetInstance<FacturaView>();
            _factura.VendedorId = IdentidadUsuarioLogin.EmpleadoId;
            _factura.ApYNomDelVendedor = IdentidadUsuarioLogin.ApyNom;

            AsignarEvento_EnterLeave(this);
        }

        private void _00121_Ventas_Load(object sender, System.EventArgs e)
        {
            CargarDatosDeCabecera();
            CargarDatosDeLosItems();
            CargarDatosDelPie();
        }

        private void CargarDatosDelPie()
        {
            //Paso 1 - Subtotal en 0 

            //Paso 2 - Descuento en 0

            nudPorcentajeDescuento.Value = 0;

            //Paso 3 - Total en 0
        }

        private void CargarDatosDeLosItems()
        {
            dgvGrilla.DataSource = _factura.Items.ToList();

            //FormatearGrilla(dgvGrilla); 
        }

        public override void FormatearGrilla(DataGridView dgv)
        {
            base.FormatearGrilla(dgv);

        }

        private void CargarDatosDeCabecera()
        {
            //Paso 3 - Obtener el vendedor que esta Login
            _vendedor = (EmpleadoDto)_empleadoServicio.GetById(typeof(EmpleadoDto), IdentidadUsuarioLogin.EmpleadoId);

            if (_vendedor == null) throw new Exception("Ocurrio un error al Obtener el Vendedor por defecto");

            txtVendedor.Text = _factura.ApYNomDelVendedor;


            try
            {

                _factura = new FacturaView();
                nudPorcentajeDescuento.Value = 0;
                //Paso 1 - Obtener los tipos de factura
                //Paso 2 - Obtener el siguiente nro de Factura
              
                
                cmbTipoFactura.SelectedItem = TipoComprobante.B;
                
                    //Paso 4 - Obtener la fecha en el tiket del dia en curso


                    dtpFechaFactura.Value = DateTime.Now;

                    //Pase 5 - Obtener cliente (Consumidor Final => 999999)

                    _cliente = (ClienteDto)_clienteServicio.Get(typeof(ClienteDto), "Cliente").FirstOrDefault();
                
                if (_cliente == null) throw new Exception("Ocurrio un error al Obetner el cliente por defecto");
                
                    txtApyNomCliente.Text = _cliente.ApyNom;
                    txtDireccionCliente.Text = _cliente.Direccion;
                    txtDniCuitCuilCliente.Text = _cliente.Dni;
                    txtCondicionIva.Text = _cliente.CondicionIva;

                    //Datos de lista de precios

                    var listaPrecios = _listaPrecioServicio.Get(string.Empty);

                    Poblar_ComboBox(cmbListaPrecio, listaPrecios, "Descripcion", "Id");//Poblar lista de precios


                //Paso 5.1 Asignar a la clase view
                //Paso 5.2 Mostrar los datos por pantalla
                    PoblarGrillaItems();
                //Pase 6 - Obtener lista de precios
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Close();
            }

        }

        private void PoblarGrillaItems()
        {
            dgvGrilla.DataSource = _factura.Items.ToList();

            FormatearGrilla(dgvGrilla);

            dgvGrilla.Columns["Codigoarticulo"].Visible = true;
            dgvGrilla.Columns["Codigoarticulo"].HeaderText = "Codigo";
            dgvGrilla.Columns["Codigoarticulo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvGrilla.Columns["CodigoDeBarraArticulo"].Visible = true;
            dgvGrilla.Columns["CodigoDeBarraArticulo"].HeaderText = "Codigo De Barras"; ;
            dgvGrilla.Columns["CodigoDeBarraArticulo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvGrilla.Columns["DescripcionArticulo"].Visible = true;
            dgvGrilla.Columns["DescripcionArticulo"].HeaderText = "Descripcion";
            dgvGrilla.Columns["DescripcionArticulo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvGrilla.Columns["Cantidad"].Visible = true;
            dgvGrilla.Columns["Cantidad"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvGrilla.Columns["PrecioArticulo"].Visible = true;
            dgvGrilla.Columns["PrecioArticulo"].HeaderText = "Precio";
            dgvGrilla.Columns["PrecioArticulo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvGrilla.Columns["SubTotal"].Visible = true;


        }

        private void btnBuscarEmpleado_Click(object sender, EventArgs e)
        {
            var fBuscarEmpleado = ObjectFactory.GetInstance<LookUpEmpleado>();
            fBuscarEmpleado.ShowDialog();

            
            //Si seleccionó un empleado debe mostrar datos
        }

        private void _00121_Ventas_Activated(object sender, EventArgs e)
        {
            txtCodigoArticulo.Focus();
        }

        private void txtCodigoArticulo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtCodigoArticulo.Text))
                {
                    MessageBox.Show("Por favor Ingrese un Codigo");
                    txtCodigoArticulo.Clear();
                    txtCodigoArticulo.Focus();
                    return;
                }

                AgregarItem(txtCodigoArticulo.Text, (long)cmbListaPrecio.SelectedValue);

                ActualizarTotales();
                PoblarGrillaItems();

                /*
                var listaPrecioSeleccionadaId = (long)cmbListaPrecio.SelectedValue;

                var articulo = _articuloServicio.GetByCodigo(txtCodigoArticulo.Text, listaPrecioSeleccionadaId);

                if (articulo != null)
                { 
                }*/
            }
        }

        private void AgregarItem(string codigoArticulo, long listaPrecioId)
        {
            var articulo = _articuloServicio.GetByCodigo(codigoArticulo, listaPrecioId);

            if (articulo != null)
            {
                if (!ValidarLimiteVentaPorHora(articulo)) return;

                var item = _factura.Items.FirstOrDefault(x => x.ArticuloId == articulo.Articulo && x.ListaPrecioId == listaPrecioId);

                if (item == null) //Item Nuevo
                {
                    if (!ValidarLimiteVentaPorCantidad(articulo, 1)) return;

                    _factura.Items.Add(new ItemsView
                    {
                        ArticuloId = articulo.Articulo,
                        PrecioArticulo = articulo.Precio,
                        CodigoDeBarraArticulo = articulo.CodigoBarra,
                        Codigoarticulo = articulo.Codigo,
                        DescripcionArticulo = articulo.Descripcion,
                        Cantidad = 1,
                        ListaPrecioId = listaPrecioId

                    });
                }
                else //Existe Item
                {
                    if (!ValidarLimiteVentaPorCantidad(articulo, item.Cantidad + 1)) return;

                    item.Cantidad += 1;
                }
            }
            else
            {
                var fLookUpArticulo = ObjectFactory.GetInstance<LookUpArticulo>();
                fLookUpArticulo.ShowDialog();

                if (fLookUpArticulo.EntidadSeleccionada != null)
                {
                    AgregarItem(((ArticuloDto)fLookUpArticulo.EntidadSeleccionada).CodigoBarra, 1);
                }
            }


            txtCodigoArticulo.Clear();
            txtCodigoArticulo.Focus();
        }

        private bool ValidarLimiteVentaPorCantidad(ArticuloVentaDto articulo, decimal cantidad)
        {
            if (articulo.ActivarLimiteVenta && articulo.LimiteVenta < cantidad)
            {
                MessageBox.Show($"El producto {articulo.Descripcion} no se puede vender mas de {articulo.LimiteVenta} unidades");

                return true;
            }
            return true;
        }

        private bool ValidarLimiteVentaPorHora(ArticuloVentaDto artic)
        {
            var horaLimite = new TimeSpan(artic.HoraLimiteVenta.Hour,
                    artic.HoraLimiteVenta.Minute, 0);
            if (artic.ActivarHoraVenta && DateTime.Now.TimeOfDay > horaLimite)
            
                MessageBox.Show($"El producto {artic.Descripcion} no se puede vender");

               return true;
        }

        private void ActualizarTotales() 
        {
            _factura.PorcentajeDescuento = nudPorcentajeDescuento.Value;

            txtSubTotal.Text = _factura.SubTotal.ToString("C");
            txtMontoDescuento.Text = _factura.MontoDeDescuento.ToString("C");
            txtTotal.Text = _factura.Total.ToString("C");
            
        }

        private void nudPorcentajeDescuento_ValueChanged(object sender, EventArgs e)
        {
            ActualizarTotales();
        }

        private void btnBuscarArticulo_Click(object sender, EventArgs e)
        {
            var fLookUp = ObjectFactory.GetInstance<LookUpArticulo>();

            fLookUp.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (_factura.Items.Any())
            {
                if (MessageBox.Show(@"Hay Articulos no facturados, Desea salir del sist.", "Atencion"
                    , MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    this.Close();
                }
            }

            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*
            if (!_factura.Items.Any())
            {
                MessageBox.Show("No hay articulos para facturar");
                return;
            }*/


            var formaPago = new FormaPago.FormaPago(_factura);
            formaPago.ShowDialog();
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }
    }
}
