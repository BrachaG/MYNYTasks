import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SurveyComponent } from './pages/survey/survey.component';
import { TableModule } from 'primeng/table';
import { CalendarModule } from 'primeng/calendar';
import { DropdownModule } from 'primeng/dropdown';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SliderModule } from 'primeng/slider';
import { DialogModule } from 'primeng/dialog';
import { MultiSelectModule } from 'primeng/multiselect';
import { ContextMenuModule } from 'primeng/contextmenu';
import { ProgressBarModule } from 'primeng/progressbar';
import { ToastModule } from 'primeng/toast';
import { StyleClassModule } from 'primeng/styleclass';
import { LoginComponent } from './pages/login/login.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtInterceptor } from './jwt.interceptor';
import { SideBarComponent } from './side-bar/side-bar.component';
import { SidebarModule } from 'primeng/sidebar';
import { BarButtonsComponent } from './side-bar/bar-buttons/bar-buttons.component';
import { TasksComponent } from './tasks/tasks.component';
import { SurveyResultsComponent } from './pages/survey-results/survey-results.component';
import { AccordionModule } from 'primeng/accordion';
import { PaginatorModule } from 'primeng/paginator';
import { AmericanQuestionComponent } from "./pages/survey-results/american-question/american-question.component";
import { TextQuestionComponent } from "./pages/survey-results/text-question/text-question.component";
import { ScrollingModule } from '@angular/cdk/scrolling';
//import { ChartModule } from 'primeng/chart';
import { Chart } from 'chart.js/auto';
import { CreateTaskComponent } from './create-task/create-task.component';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { HeaderComponent } from './header/header.component';
import { SelectBranchComponent } from './pages/select-branch/select-branch.component';
import { AuthGuardService } from './services/auth-guard.service';
import {PaginatorModule } from 'primeng/paginator';
import { TargetsComponent } from './targets/targets.component';

@NgModule({
  declarations: [
    AppComponent,
    SurveyComponent,
    LoginComponent,
    SurveyResultsComponent,
    SideBarComponent,
    BarButtonsComponent,
    TasksComponent,
    AmericanQuestionComponent,
    TextQuestionComponent,
    CreateTaskComponent,
    HeaderComponent,
    SelectBranchComponent
    TargetsComponent,
    CreateTaskComponent
  ],
  imports: [
    BrowserAnimationsModule,
    BrowserModule,
    AppRoutingModule,
    TableModule,
    CalendarModule,
    SliderModule,
    DialogModule,
    MultiSelectModule,
    ContextMenuModule,
    DropdownModule,
    ButtonModule,
    ToastModule,
    InputTextModule,
    ProgressBarModule,
    StyleClassModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    AccordionModule,
    SidebarModule,
    InputTextareaModule,
    PaginatorModule,
    ScrollingModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true,

    }
    ,
      AuthGuardService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }