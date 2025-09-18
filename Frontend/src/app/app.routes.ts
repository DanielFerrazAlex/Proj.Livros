import { Routes } from '@angular/router';
import { Home } from './pages/home/home';
import { Register } from './pages/register/register';
import { Edit } from './pages/edit/edit';


export const routes: Routes = [
    {
        path:'', 
        component: Home
    },
    {
        path:'Cadastrar', 
        component: Register
    },
    {
        path:'Editar/:id', 
        component: Edit
    }
];
