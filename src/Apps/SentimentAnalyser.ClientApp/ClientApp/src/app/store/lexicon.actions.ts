import { createAction, props } from '@ngrx/store';
import { PostSentimentDto, SentimentDto } from '../api/lexicon.api';

export const getAllSentiments = createAction(
    '[Sentiment Component] getAllSentiments'
);

export const getAllSentimentsSuccess = createAction(
    '[Sentiment Component] getAllSentiments Success',
    props<{ sentiments: SentimentDto[] }>()
);

export const getAllSentimentsFailure = createAction(
    '[Sentiment Component] getAllSentiments Failure',
    props<{ error: any }>()
);

export const addSentiment = createAction(
    '[Sentiment Component] addSentiment',
    props<{ sentiment: PostSentimentDto }>()
);

export const addSentimentSuccess = createAction(
    '[Sentiment Component] addSentiment Success',
    props<{ sentiment: SentimentDto }>()
);

export const addSentimentFailure = createAction(
    '[Sentiment Component] addSentiment Failure',
    props<{ error: any }>()
);

export const editSentiment = createAction(
    '[Sentiment Component] editSentiment',
    props<{ sentiment: SentimentDto }>()
);

export const editSentimentSuccess = createAction(
    '[Sentiment Component] editSentiment Success',
    props<{ sentiment: SentimentDto }>()
);

export const editSentimentFailure = createAction(
    '[Sentiment Component] editSentiment Failure',
    props<{ error: any }>()
);

export const deleteSentiment = createAction(
    '[Sentiment Component] deleteSentiment',
    props<{ sentimentId: string }>()
);

export const deleteSentimentSuccess = createAction(
    '[Sentiment Component] deleteSentiment Success',
    props<{ sentiment: SentimentDto }>()
);

export const deleteSentimentFailure = createAction(
    '[Sentiment Component] deleteSentiment Failure',
    props<{ error: any }>()
);
