import { Inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { IFacturaRepository } from "../Servicios/Repositorios/factura-repositorio.interface";
import { FACTURA_REPOSITORY } from "../../../core/Domain/Tokens/factura.repository";
import { FacturaModel } from "../Modelos/Factura.Model";

@Injectable({
  providedIn: 'root'
})
export class BuscarFacturaByNumUseCase {
  constructor(@Inject(FACTURA_REPOSITORY) private repo: IFacturaRepository) {}

  executeFactPorNumero(numero: string): Observable<FacturaModel[]> {
    return this.repo.buscarFacturasPorNumero(numero);
  }

}