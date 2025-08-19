import { Component, signal } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { BuscarFacturaPage } from "./features/Factura/Paginas/Factura-Buscar/factura-Buscar.page";
import { CrearFacturaPage } from './features/Factura/Paginas/Factura-Crear/factura-Crear.page';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet,RouterOutlet, RouterLink, RouterLinkActive, BuscarFacturaPage,BuscarFacturaPage, CrearFacturaPage],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('DVP_Test');
}
