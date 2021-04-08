import { Component, Inject } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { Store } from "@ngrx/store";
import { addSentiment, getAllSentiments } from "src/app/store/lexicon.actions";
import { LexiconState } from "src/app/store/lexicon.state";
import { PostSentimentDto } from '../../api/lexicon.api';
import { FormControl, Validators } from '@angular/forms';

const wordPattern = '([a-zA-z\']+)';

@Component({
    selector: 'add-sentiment-dialog',
    templateUrl: 'add-sentiment.dialog.html',
    styleUrls: ['./add-sentiment.dialog.scss']
})
export class AddSentimentDialog {

    public word = new FormControl('', [Validators.required, Validators.pattern(wordPattern)]);

    public sentimentScore = new FormControl('', [Validators.required, Validators.min(-1), Validators.max(1)]);

    constructor(
        public dialogRef: MatDialogRef<AddSentimentDialog>,
        @Inject(MAT_DIALOG_DATA) public data: any,
        private store: Store<LexiconState>
    ) {
    }

    addSentiment() {
        let sentiment: PostSentimentDto = {
            word: this.word.value,
            sentimentScore: this.sentimentScore.value
        }

        this.store.dispatch(addSentiment({sentiment}));
        this.closeDialog();
    }

    closeDialog(): void {
        this.dialogRef.close();
    }

    getWordScoreErrorMessage() {
        if (this.word.hasError('required')) {
            return 'Word is required.';
        }
    
        return 'The word has to be a valid word.';
    }

    getSentimentScoreErrorMessage() {
        if (this.sentimentScore.hasError('required')) {
            return 'Score is required.';
        }
    
        return 'Score has to be between -1 and 1.';
    }
}