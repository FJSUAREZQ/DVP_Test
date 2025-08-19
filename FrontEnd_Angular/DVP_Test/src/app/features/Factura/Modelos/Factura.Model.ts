
export interface FacturaModel {
  facturaId: number;
  numeroFactura: string;
  clienteId: number;
  cliente: string;
  email: string;
  direccion: string;
  ciudad: string;
  departamento: string;
  subtotal: number;
  impuestos: number;
  total: number;
  fechaCreacion: string;
}
