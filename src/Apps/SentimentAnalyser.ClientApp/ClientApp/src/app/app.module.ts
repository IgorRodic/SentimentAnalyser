import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CalculationComponent } from './components/calculation/calculation.component';

import { MatToolbarModule } from  '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatButtonModule } from '@angular/material/button'
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LexiconComponent } from './components/lexicon/lexicon.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { MatTableModule } from '@angular/material/table'
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input'
import { MatButtonToggleModule } from '@angular/material/button-toggle'
import { AddSentimentDialog } from './components/add-sentiment-dialog/add-sentiment.dialog';
import { MatDialogModule } from '@angular/material/dialog';
import { DeleteDialog } from './components/delete-dialog/delete.dialog';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { LexiconReducer } from './store/lexicon.reducer'
import { LexiconEffects } from './store/lexicon.effect';
import { EditSentimentDialog } from './components/edit-sentiment-dialog/edit-sentiment.dialog';

@NgModule({
  declarations: [
    AppComponent,
    LexiconComponent,
    CalculationComponent,
    AddSentimentDialog,
    EditSentimentDialog,
    DeleteDialog
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MatToolbarModule,
    MatSidenavModule,
    MatListModule,
    MatButtonModule,
    MatIconModule,
    BrowserAnimationsModule,
    FormsModule,
    HttpClientModule,
    MatTableModule,
    MatPaginatorModule,
    MatInputModule,
    MatFormFieldModule,
    MatButtonToggleModule,
    MatDialogModule,
    FormsModule,
    ReactiveFormsModule,
    StoreModule.forRoot({
      lexicon: LexiconReducer
    }, {}),
    EffectsModule.forRoot([LexiconEffects])
  ],
  providers: [AddSentimentDialog, EditSentimentDialog, DeleteDialog],
  bootstrap: [AppComponent]
})
export class AppModule { }
