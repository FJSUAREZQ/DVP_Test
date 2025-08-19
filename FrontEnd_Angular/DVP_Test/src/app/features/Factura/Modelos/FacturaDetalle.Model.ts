export interface FacturaDetalleModel {
   detalleId: number;
  facturaId: number;
  productoId: number;
  cantidad: number;
  precioUnitario: number;
  totalLinea: number;
  fechaCreacion: string;
}
