import { ApplicationConfig, provideBrowserGlobalErrorListeners, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { FacturaRepositoryImpl } from './features/Factura/Servicios/Repositorios/factura-repositorio.impl';
import { FACTURA_REPOSITORY } from './core/Domain/Tokens/factura.repository';
import { errorHandlerInterceptor } from './core/interceptors/error.interceptor';
import { provideHttpClient, withFetch, withInterceptors } from '@angular/common/http';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),

    
    provideHttpClient(withFetch(), withInterceptors([errorHandlerInterceptor])),

    //Para inyectar el repositorio de Factura y su implementación. 
    //PAra que Angular pueda resolver las dependencias de FacturaRepositoryImpl y poder injectar la interfaz IFacturaRepository
    { provide: FACTURA_REPOSITORY, useClass: FacturaRepositoryImpl }
  ]
};
