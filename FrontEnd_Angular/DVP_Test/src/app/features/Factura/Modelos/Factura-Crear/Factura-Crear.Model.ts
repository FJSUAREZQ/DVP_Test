import { FacturaDetalleLineaModel } from "./Factura-DetalleLinea.Model";

export interface FacturaCrearModel {
  numeroFactura: string;
  clienteId: number;
  usuarioId: number;
  subtotal: number;
  impuestos: number;
  total: number;
  detalles: FacturaDetalleLineaModel[];
}
