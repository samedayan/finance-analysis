import { Routes } from '@angular/router';
import { PartnersComponent } from './partners/partners.component';
import { AgreementsComponent } from './agreements/agreements.component';
import { WorkItemsComponent } from './workitems/workitems.component';
import { AuditsComponent } from './audits/audits.component';
import { AboutComponent } from './about/about.component';

export const UiComponentsRoutes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'partners',
        component: PartnersComponent,
      },
      {
        path: 'agreements',
        component: AgreementsComponent,
      },
      {
        path: 'workitems',
        component: WorkItemsComponent,
      },
      {
        path: 'audits',
        component: AuditsComponent,
      },
      {
        path: 'about',
        component: AboutComponent,
      },
    ],
  },
];
