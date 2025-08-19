import { Observable } from "rxjs";
import { Inject, Injectable } from "@angular/core";
import { IFacturaRepository } from "../Servicios/Repositorios/factura-repositorio.interface";
import { FACTURA_REPOSITORY } from "../../../core/Domain/Tokens/factura.repository";
import { ProductoModel } from "../Modelos/Producto.Model";

@Injectable({
  providedIn: 'root'
})
export class BuscarProductoByIdUseCase {
  constructor(@Inject(FACTURA_REPOSITORY) private repo: IFacturaRepository) {}

  excecuteBuscarProductosById(productoId: number): Observable<ProductoModel> {
    return this.repo.getProductoById(productoId);
  }
}