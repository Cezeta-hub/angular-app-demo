import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { MenubarModule } from 'primeng/menubar';
import { MenuModule } from 'primeng/menu';
import { MegaMenuModule } from 'primeng/megamenu';
import { NavBarComponent } from './components/nav-bar/nav-bar.component';
import { SharedModule } from './modules/shared/shared.module';
import { ToolbarModule } from 'primeng/toolbar';
// import { HttpConfigInterceptor } from './http-config.interceptor';
import { SidebarModule } from 'primeng/sidebar';
import { ImageModule } from 'primeng/image';

@NgModule({
  declarations: [
    AppComponent,
    NavBarComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    HttpClientModule,
    MenubarModule,
    MenuModule,
    MegaMenuModule,
    ToastModule,
    SharedModule,
    ToolbarModule,
    SidebarModule,
    ImageModule
  ],
  providers: [
    MessageService,
    // {provide: HTTP_INTERCEPTORS, useClass: HttpConfigInterceptor, multi: true},
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  bootstrap: [AppComponent]
})
export class AppModule { }
