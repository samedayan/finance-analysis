import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from '../../material.module';
import { TablerIconsModule } from 'angular-tabler-icons';
import * as TablerIcons from 'angular-tabler-icons/icons';
import { UiComponentsRoutes } from './ui-components.routing';
import { PartnersComponent } from './partners/partners.component';
import { MatNativeDateModule } from '@angular/material/core';
import { AuditsComponent } from './audits/audits.component';
import { ToastrModule } from 'ngx-toastr';
import { AddPartnerComponent } from './partners/add-partner/add-partner.component';
import { UpdatePartnerComponent } from './partners/update-partner/update-partner.component';
import { AgreementsComponent } from './agreements/agreements.component';
import { AddAgreementComponent } from './agreements/add-agreement/add-agreement.component';
import { UpdateAgreementComponent } from './agreements/update-agreement/update-agreement.component';
import { AboutComponent } from './about/about.component';
import { WorkItemsComponent } from './workitems/workitems.component';
import { AddWorkItemComponent } from './workitems/add-workitem/add-workitem.component';
import { UpdateWorkItemComponent } from './workitems/update-workitem/update-workitem.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(UiComponentsRoutes),
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    TablerIconsModule.pick(TablerIcons),
    MatNativeDateModule,
    ToastrModule.forRoot({
      timeOut: 3000,
      positionClass: 'toast-top-right',
      preventDuplicates: true,
      progressBar: true,
      closeButton: true
    })
  ],
  declarations: [
    PartnersComponent,
    AgreementsComponent,
    AuditsComponent,
    AddPartnerComponent,
    UpdatePartnerComponent,
    AddAgreementComponent,
    UpdateAgreementComponent,
    AboutComponent,
    WorkItemsComponent,
    AddWorkItemComponent,
    UpdateWorkItemComponent
  ],
})
export class UicomponentsModule {}
