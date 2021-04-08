import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CalculationComponent } from './components/calculation/calculation.component';
import { LexiconComponent } from './components/lexicon/lexicon.component';

const routes: Routes = [
  { path: '', redirectTo: 'lexicon', pathMatch: 'full' },
  { path: 'lexicon', component: LexiconComponent },
  { path: 'calculation', component: CalculationComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
