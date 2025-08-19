import { Observable } from "rxjs";
import { FacturaModel } from "../../Modelos/Factura.Model";
import { ClientesCombosModel } from "../../Modelos/ClientesCombos.Model";
import { ProductosCombosModel } from "../../Modelos/ProductosCombos.Model";
import { ProductoModel } from "../../Modelos/Producto.Model";
import { FacturaCrearModel } from "../../Modelos/Factura-Crear/Factura-Crear.Model";

export interface IFacturaRepository {
  getClientesCombo(): Observable<ClientesCombosModel[]>;
  getProductosCombo(): Observable<ProductosCombosModel[]>;
  getProductoById(id: number): Observable<ProductoModel>;
  buscarFacturasPorCliente(clienteId: number): Observable<FacturaModel[]>;
  buscarFacturasPorNumero(numero: string): Observable<FacturaModel[]>;
  guardarFactura(factura: FacturaCrearModel): Observable<void>;

}
