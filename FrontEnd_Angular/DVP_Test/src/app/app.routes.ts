import { Routes } from '@angular/router';
import { BuscarFacturaPage } from './features/Factura/Paginas/Factura-Buscar/factura-Buscar.page';
import { CrearFacturaPage } from './features/Factura/Paginas/Factura-Crear/factura-Crear.page';

export const routes: Routes = [
    { path: '', redirectTo: 'buscar', pathMatch: 'full' },
    { path: 'buscar', component: BuscarFacturaPage },
    { path: 'crear', component: CrearFacturaPage },
    { path: '**', redirectTo: 'buscar' }
];
