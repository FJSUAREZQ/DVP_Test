import { Observable } from "rxjs";
import { ClientesCombosModel } from "../../Modelos/ClientesCombos.Model";
import { FacturaCrearModel } from "../../Modelos/Factura-Crear/Factura-Crear.Model";
import { FacturaModel } from "../../Modelos/Factura.Model";
import { ProductoModel } from "../../Modelos/Producto.Model";
import { ProductosCombosModel } from "../../Modelos/ProductosCombos.Model";
import { FacturaApiService } from "../API/factura-api.service";
import { IFacturaRepository } from "./factura-repositorio.interface";
import { Injectable } from "@angular/core";

@Injectable()
export class FacturaRepositoryImpl implements IFacturaRepository {
  constructor(private api: FacturaApiService) {}

    getClientesCombo(): Observable<ClientesCombosModel[]> {
        return this.api.getClientesCombo();
    }

    getProductosCombo(): Observable<ProductosCombosModel[]> {
        return this.api.getProductosCombo();
    }

    getProductoById(id: number): Observable<ProductoModel> {
        return this.api.getProductosById(id);
    }

    buscarFacturasPorCliente(clienteId: number): Observable<FacturaModel[]> {
        return this.api.buscarFacturasPorCliente(clienteId);
    }

    buscarFacturasPorNumero(numero: string): Observable<FacturaModel[]> {
        return this.api.buscarFacturasPorNumero(numero);
    }

    guardarFactura(factura: FacturaCrearModel): Observable<void> {
        return this.api.guardarFactura(factura);
    }

  
}
