import { Routes } from '@angular/router';
import { NbpComponent } from './view/nbp/nbp.component';
import { AiComponent } from './view/ai/ai.component';

export const routes: Routes = [
    {
        path: '',
        component: NbpComponent,
        title: 'Strona główna',
    },
    {
        path: 'ai',
        component: AiComponent,
        title: 'AI'
    }
];
