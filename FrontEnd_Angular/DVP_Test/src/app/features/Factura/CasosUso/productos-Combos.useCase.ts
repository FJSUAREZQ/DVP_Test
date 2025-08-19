import { Inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { IFacturaRepository } from "../Servicios/Repositorios/factura-repositorio.interface";
import { FACTURA_REPOSITORY } from "../../../core/Domain/Tokens/factura.repository";
import { ProductosCombosModel } from "../Modelos/ProductosCombos.Model";

@Injectable({
  providedIn: 'root'
})
export class traerProductosCombosUseCase {
  constructor(@Inject(FACTURA_REPOSITORY) private repo: IFacturaRepository) {}

  executeProductosCombos(): Observable<ProductosCombosModel[]> {
    return this.repo.getProductosCombo();
  }

}