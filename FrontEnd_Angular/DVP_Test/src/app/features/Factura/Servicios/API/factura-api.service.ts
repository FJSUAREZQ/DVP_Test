import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ClientesCombosModel } from "../../Modelos/ClientesCombos.Model";
import { ProductosCombosModel } from "../../Modelos/ProductosCombos.Model";
import { ProductoModel } from "../../Modelos/Producto.Model";
import { FacturaModel } from "../../Modelos/Factura.Model";
import { FacturaCrearModel } from "../../Modelos/Factura-Crear/Factura-Crear.Model";
import { environment } from "../../../../../environments/environment";


@Injectable({ providedIn: 'root' })
export class FacturaApiService {

  private readonly endpoint = environment.apiFakeProductsBaseUrl; 
    
  constructor(private http: HttpClient) {}

  getClientesCombo(): Observable<ClientesCombosModel[]> {
    return this.http.get<ClientesCombosModel[]>(`${this.endpoint}/Clientes/listarCombos`);
  }

  getProductosCombo(): Observable<ProductosCombosModel[]> {
    return this.http.get<ProductosCombosModel[]>(`${this.endpoint}/Productos/listarCombos`);
  }

  getProductosById(id: number): Observable<ProductoModel> {
    return this.http.get<ProductoModel>(`${this.endpoint}/Productos/buscar/${id}`);
  }


  buscarFacturasPorCliente(clienteId: number): Observable<FacturaModel[]> {
    return this.http.get<FacturaModel[]>(`${this.endpoint}/Facturas/buscarClienteId/${clienteId}`);
  }

  buscarFacturasPorNumero(numero: string): Observable<FacturaModel[]> {
    return this.http.get<FacturaModel[]>(`${this.endpoint}/Facturas/buscarNumFactura/${numero}`);
  }

  //--
   guardarFactura(factura: FacturaCrearModel): Observable<void> {
    return this.http.post<void>(`${this.endpoint}/Facturas/crear`, factura);
  }




}
