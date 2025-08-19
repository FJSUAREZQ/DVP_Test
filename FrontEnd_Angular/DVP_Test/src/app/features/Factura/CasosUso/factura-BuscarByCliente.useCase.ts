import { Observable } from "rxjs";
import { FacturaModel } from "../Modelos/Factura.Model";
import { Inject, Injectable } from "@angular/core";
import { IFacturaRepository } from "../Servicios/Repositorios/factura-repositorio.interface";
import { FACTURA_REPOSITORY } from "../../../core/Domain/Tokens/factura.repository";

@Injectable({
  providedIn: 'root'
})
export class BuscarFacturaByClienteIdUseCase {
  constructor(@Inject(FACTURA_REPOSITORY) private repo: IFacturaRepository) {}

  executeFactPorCliente(clienteId: number): Observable<FacturaModel[]> {
    return this.repo.buscarFacturasPorCliente(clienteId);
  }
}