export interface SentimentDto {
    id: number;
    word: string;
    sentimentScore: number;
}

export interface PostSentimentDto {
    word: string;
    sentimentScore: number;
}