import { state } from '@angular/animations';
import { createReducer, on } from '@ngrx/store';
import { addSentiment, editSentiment, deleteSentiment, addSentimentSuccess, addSentimentFailure, getAllSentimentsSuccess, getAllSentiments, getAllSentimentsFailure, deleteSentimentSuccess, deleteSentimentFailure, editSentimentSuccess, editSentimentFailure } from './lexicon.actions';
import { initialState } from './lexicon.state';

export const LexiconReducer = createReducer(
  initialState,

  on(getAllSentimentsSuccess, (state, { sentiments }) => ({
    ...state,
    sentiments: sentiments
  })),

  on(getAllSentimentsFailure, (state, { error }) => ({
    ...state,
    error: 'Unable to load sentiments.'
  })),

  on(addSentimentSuccess, (state, { sentiment }) => ({
    ...state,
    sentiments: [...state.sentiments, sentiment]
  })),

  on(addSentimentFailure, (state, { error }) => {
    return {
      ...state,
      error: 'The word already exists.'
    };
  }),

  on(editSentimentSuccess, (state, { sentiment }) => ({
    ...state,
    sentiments: state.sentiments.map((value) => 
      value.id === sentiment.id ? { id: sentiment.id, word: sentiment.word, sentimentScore: sentiment.sentimentScore } : value
    ) 
  })),

  on(editSentimentFailure, (state, { error }) => {
    return {
      ...state,
      error: 'Unable to edit the word.'
    };
  }),

  on(deleteSentimentSuccess, (state, { sentiment }) => ({
    ...state,
    sentiments: state.sentiments.filter(function(value) { 
        return value.id !== sentiment.id;
    })
  })),

  on(deleteSentimentFailure, (state, { error }) => {
    return {
      ...state,
      error: 'Unable to delete the word.'
    };
  })
);
 
export function counterReducer(state, action) {
  return LexiconReducer(state, action);
}

export const lexiconFeature = "lexicon";
