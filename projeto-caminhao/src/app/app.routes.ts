import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: "",
        loadComponent: () => 
            import(
                "./components/caminhao-list/caminhao-list.component"
            ).then(m => m.CaminhaoListComponent)
    }
];
