import { Observable } from "rxjs";
import { Inject, Injectable } from "@angular/core";
import { IFacturaRepository } from "../Servicios/Repositorios/factura-repositorio.interface";
import { FacturaCrearModel } from "../Modelos/Factura-Crear/Factura-Crear.Model";
import { FACTURA_REPOSITORY } from "../../../core/Domain/Tokens/factura.repository";

@Injectable({
  providedIn: 'root'
})
export class CrearFacturaUseCase {
  constructor(@Inject(FACTURA_REPOSITORY) private repo: IFacturaRepository) {}

  executeFactCrear(factura: FacturaCrearModel): Observable<void> {
    return this.repo.guardarFactura(factura);
  }
}
