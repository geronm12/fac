﻿namespace Servicio.Interfaces.Articulo.DTOs
{
    using Aplicacion.Constantes.Clases;
    using Servicio.Interfaces.Base;
    using System;
    public class ArticuloDto :  BaseDto
    {
      public TipoArticulo TipoArticulo { get; set; }
      public long MarcaId { get; set; }
      public long RubroId { get; set; }
      public long UnidadMedidaId { get; set; }
      public long IvaId { get; set; }
      public int Codigo { get; set; }
      public int CodigoDeProveedor { get; set; }
      public string CodigoBarra { get; set; }
      public string Abreviatura { get; set; }
      public string Descripcion { get; set; }
      public string Detalle { get; set; }
      public decimal PrecioCosto { get; set; }
      public string Ubicacion { get; set; }
      public byte[] Foto { get; set; }
      public bool ActivarLimiteVenta { get; set; }
      public decimal LimiteVenta { get; set; }
      public bool ActivarHoraVenta { get; set; }
      public DateTime HoraLimiteVenta { get; set; }
      public bool PermiteStockNegativo { get; set; }
      public bool DescuentaStock { get; set; }
      public decimal Stock { get; set; }
      public decimal StockMinimo { get; set; }
    }
}
