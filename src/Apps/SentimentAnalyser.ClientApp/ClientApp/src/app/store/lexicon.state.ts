import { SentimentDto } from "../api/lexicon.api";

export interface LexiconState {
    sentiments: SentimentDto[],
    error: any
}

export const initialState: LexiconState = {
    sentiments: [],
    error: undefined
}
