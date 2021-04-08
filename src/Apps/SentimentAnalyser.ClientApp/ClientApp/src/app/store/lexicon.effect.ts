import { addSentiment, addSentimentSuccess, deleteSentiment, deleteSentimentSuccess, editSentiment, editSentimentSuccess, getAllSentiments, getAllSentimentsSuccess } from './lexicon.actions';
import { LexiconService } from './../services/lexicon.service';
import { createEffect, Actions, ofType } from '@ngrx/effects';
import { catchError, concatMap, map } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable()
export class LexiconEffects {

  getAllSentiments$ = createEffect(() =>
    this.actions$.pipe(
      ofType(getAllSentiments),
      concatMap(() => this.lexiconService.getAll()),
      map(value => getAllSentimentsSuccess({ sentiments: value.data })),
      catchError(error => {
        window.alert("Unable to load the sentiments.");
        location.reload();
        return [];
      })
    )
  );

  addSentiment$ = createEffect(() =>
    this.actions$.pipe(
      ofType(addSentiment),
      concatMap((action) => this.lexiconService.create(action.sentiment)),
      map(value => addSentimentSuccess({ sentiment: value.data })),
      catchError(error => {
        window.alert("The word already exists.");
        location.reload();
        return [];
      })
    )
  );

  editSentiment$ = createEffect(() =>
    this.actions$.pipe(
      ofType(editSentiment),
      concatMap((action) => this.lexiconService.update(action.sentiment)),
      map(value => editSentimentSuccess({ sentiment: value.data })),
      catchError(error => {
        window.alert("Unable to edit the word.");
        location.reload();
        return [];
      })
    )
  );

  deleteSentiment$ = createEffect(() =>
    this.actions$.pipe(
      ofType(deleteSentiment),
      concatMap((action) => this.lexiconService.delete(action.sentimentId)),
      map(value => deleteSentimentSuccess({ sentiment: value.data })),
      catchError(error => {
        window.alert("Unable to delete the word.");
        location.reload();
        return [];
      })
    )
  );

  constructor(private lexiconService: LexiconService, private actions$: Actions, private router: Router) {}
}