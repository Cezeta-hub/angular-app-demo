import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

// import { NotificationService } from 'src/app/globalServices/notification.service';

import { PageContainerComponent } from '../../components/page-container/page-container.component';

import { TableModule } from 'primeng/table';
import { ToolbarModule } from 'primeng/toolbar';
import { ButtonModule } from 'primeng/button';
import { PanelModule } from 'primeng/panel';
import { CardModule } from 'primeng/card';
import { AccordionModule } from 'primeng/accordion';
import { SkeletonModule } from 'primeng/skeleton';
import { StepsModule } from "primeng/steps";
// -- Forms
import { FormsModule } from '@angular/forms';
import { DropdownModule } from 'primeng/dropdown';

import { DynamicDialogModule } from 'primeng/dynamicdialog';
import { PageContainerToolbarRightComponent } from 'src/app/components/page-container-toolbar-right/page-container-toolbar-right.component';
import { InputTextModule } from 'primeng/inputtext';
import { InputNumberModule } from 'primeng/inputnumber';
import { NgxSpinnerModule } from 'ngx-spinner';
import { LoaderWrapperComponent } from 'src/app/components/loader-wrapper/loader-wrapper.component';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { CalendarModule } from 'primeng/calendar';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { RadioButtonModule } from 'primeng/radiobutton';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { MultiSelectModule } from 'primeng/multiselect';
import { CheckboxModule } from 'primeng/checkbox';

import { ConfirmationService } from 'primeng/api';
import { DividerModule } from 'primeng/divider';
import { TagModule } from 'primeng/tag';
import { BadgeComponent } from 'src/app/components/badge/badge.component';

@NgModule({
  declarations: [
    PageContainerComponent,
    PageContainerToolbarRightComponent,
    LoaderWrapperComponent,
    BadgeComponent,
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    TableModule,
    ButtonModule,
    DropdownModule,
    ToolbarModule,
    PanelModule,
    CardModule,
    AccordionModule,
    SkeletonModule,
    StepsModule,
    DynamicDialogModule,
    InputTextModule,
    InputNumberModule,
    NgxSpinnerModule,
    AutoCompleteModule,
    RadioButtonModule,
    CalendarModule,
    ConfirmDialogModule,
    InputTextareaModule,
    MultiSelectModule,
    CheckboxModule,
    DividerModule,
    TagModule
  ],
  exports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    TableModule,
    ButtonModule,
    DropdownModule,
    ToolbarModule,
    PanelModule,
    CardModule,
    AccordionModule,
    SkeletonModule,
    StepsModule,
    DynamicDialogModule,
    InputTextModule,
    InputNumberModule,
    NgxSpinnerModule,
    PageContainerComponent,
    PageContainerToolbarRightComponent,
    LoaderWrapperComponent,
    ConfirmDialogModule,
    AutoCompleteModule,
    RadioButtonModule,
    CalendarModule,
    InputTextareaModule,
    MultiSelectModule,
    CheckboxModule,
    DividerModule,
    TagModule,
    BadgeComponent
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  providers: [ConfirmationService]
})
export class SharedModule { }
