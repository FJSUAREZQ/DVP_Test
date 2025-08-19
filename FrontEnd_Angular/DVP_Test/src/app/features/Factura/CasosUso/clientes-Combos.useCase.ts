import { Inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { IFacturaRepository } from "../Servicios/Repositorios/factura-repositorio.interface";
import { FACTURA_REPOSITORY } from "../../../core/Domain/Tokens/factura.repository";
import { ClientesCombosModel } from "../Modelos/ClientesCombos.Model";

@Injectable({
  providedIn: 'root'
})
export class traerClientesCombosUseCase {
  constructor(@Inject(FACTURA_REPOSITORY) private repo: IFacturaRepository) {}

  executeClientesCombos(): Observable<ClientesCombosModel[]> {
    return this.repo.getClientesCombo();
  }

}