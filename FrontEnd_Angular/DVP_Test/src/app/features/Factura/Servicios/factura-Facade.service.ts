import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { FacturaCrearModel } from "../Modelos/Factura-Crear/Factura-Crear.Model";
import { FacturaModel } from "../Modelos/Factura.Model";
import { ProductosCombosModel } from "../Modelos/ProductosCombos.Model";
import { CrearFacturaUseCase } from "../CasosUso/factura-crear.useCase";
import { BuscarFacturaByClienteIdUseCase } from "../CasosUso/factura-BuscarByCliente.useCase";
import { BuscarFacturaByNumUseCase } from "../CasosUso/factura-BuscarByNumFact.useCase";
import { traerProductosCombosUseCase } from "../CasosUso/productos-Combos.useCase";
import { traerClientesCombosUseCase } from "../CasosUso/clientes-Combos.useCase";
import { ProductoModel } from "../Modelos/Producto.Model";
import { ClientesCombosModel } from "../Modelos/ClientesCombos.Model";
import { BuscarProductoByIdUseCase } from "../CasosUso/productos-buscarByProductIduseCase";

@Injectable({
  providedIn: 'root' // 👈 Con esto el Facade está disponible en toda la app
})
export class FacturaFacade {

  // 👇 Inyectamos los casos de uso que necesitamos
  constructor(
    private crearFacturaUC: CrearFacturaUseCase,
    private buscarFacturasClienteUC: BuscarFacturaByClienteIdUseCase,
    private buscarFacturaByNumeroUC: BuscarFacturaByNumUseCase, 
    private traerClientesComboUC: traerClientesCombosUseCase,
    private traerProductosComboUC: traerProductosCombosUseCase,
    private buscarProductosByIdUC: BuscarProductoByIdUseCase  
  ) {}

  crearFactura(factura: FacturaCrearModel): Observable<void> {
    return this.crearFacturaUC.executeFactCrear(factura);
  }

  buscarFacturasPorCliente(clienteId: number): Observable<FacturaModel[]> {
    return this.buscarFacturasClienteUC.executeFactPorCliente(clienteId);
  }

    buscarFacturaPorNumero(numero: string): Observable<FacturaModel[]> {          
    return this.buscarFacturaByNumeroUC.executeFactPorNumero(numero);
  }

    traerClientesCombo(): Observable<ClientesCombosModel[]> {
    return this.traerClientesComboUC.executeClientesCombos();
  }

    traerProductosCombo(): Observable<ProductosCombosModel[]> {
    return this.traerProductosComboUC.executeProductosCombos();
  }

    buscarProductosById(productoId: number): Observable<ProductoModel> {
    return this.buscarProductosByIdUC.excecuteBuscarProductosById(productoId);
  }


}