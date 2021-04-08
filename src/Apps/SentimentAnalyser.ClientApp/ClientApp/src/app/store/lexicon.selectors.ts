import { createFeatureSelector, createSelector } from "@ngrx/store";
import { lexiconFeature } from "./lexicon.reducer";
import { LexiconState } from "./lexicon.state";

export const lexiconFeatureSelector = createFeatureSelector(lexiconFeature);

export const allSentiments = createSelector(lexiconFeatureSelector, (state: LexiconState) => state.sentiments);

export const getError = createSelector(lexiconFeatureSelector, (state: LexiconState) => state.error);