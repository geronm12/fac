﻿namespace Dominio.Entidades.MetaData
{
    using Aplicacion.Constantes.Clases;
    using System;
    using System.ComponentModel.DataAnnotations;

    public interface IArticulo
    {
        [Required(ErrorMessage = "El campo {0} es Obligatorio.")]
        long MarcaId { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio.")]
        long RubroId { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio.")]
        long UnidadMedidaId { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio.")]
        long IvaId { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio.")]
        int Codigo { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio.")]
        string CodigoBarra { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio.")]
        int CodigoDeProveedor { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio.")]
        TipoArticulo TipoArticulo { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio.")]
        decimal PrecioCosto { get; set; }
        string Abreviatura { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio.")]
        [StringLength(400, ErrorMessage = "El campo {0} debe ser menor a {1} caracteres.")]
        string Descripcion { get; set; }

        [StringLength(4000, ErrorMessage = "El campo {0} debe ser menor a {1} caracteres.")]
        string Detalle { get; set; }

        [StringLength(400, ErrorMessage = "El campo {0} debe ser menor a {1} caracteres.")]
        string Ubicacion { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio.")]
        byte[] Foto { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio.")]
        bool ActivarLimiteVenta { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio.")]
        decimal LimiteVenta { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio.")]
        bool ActivarHoraVenta { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio.")]
        [DataType(DataType.Time)]
        DateTime HoraLimiteVenta { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio.")]
        bool PermiteStockNegativo { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio.")]
        bool DescuentaStock { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio.")]
        decimal Stock { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio.")]
        decimal StockMinimo { get; set; }
    }
}
