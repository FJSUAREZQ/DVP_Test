import { InjectionToken } from '@angular/core';
import { IFacturaRepository } from '../../../features/Factura/Servicios/Repositorios/factura-repositorio.interface';

//define la inyección de dependencia para el repositorio de facturas
export const FACTURA_REPOSITORY = new InjectionToken<IFacturaRepository>('IFacturaRepository');