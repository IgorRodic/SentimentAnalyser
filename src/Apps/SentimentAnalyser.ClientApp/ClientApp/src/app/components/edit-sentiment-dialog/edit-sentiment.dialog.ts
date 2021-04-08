import { Component, Inject } from "@angular/core";
import { FormControl, Validators } from "@angular/forms";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { Store } from "@ngrx/store";
import { editSentiment } from "src/app/store/lexicon.actions";
import { LexiconState } from "src/app/store/lexicon.state";
import { SentimentDto } from '../../api/lexicon.api';

const wordPattern = '([a-zA-z\']+)';

@Component({
    selector: 'edit-sentiment-dialog',
    templateUrl: 'edit-sentiment.dialog.html',
    styleUrls: ['./edit-sentiment.dialog.scss']
})
export class EditSentimentDialog {

    public id: number;

    public word = new FormControl('', [Validators.required, Validators.pattern(wordPattern)]);

    public sentimentScore = new FormControl('', [Validators.required, Validators.min(-1), Validators.max(1)]);

    constructor(
        public dialogRef: MatDialogRef<EditSentimentDialog>,
        @Inject(MAT_DIALOG_DATA) public data: any,
        private store: Store<LexiconState>
    ) {
        this.id = data.id;
        this.word.setValue(data.word);
        this.sentimentScore.setValue(data.sentimentScore);
    }

    editSentiment() {
        let sentiment: SentimentDto = {
            id: this.id,
            word: this.word.value,
            sentimentScore: this.sentimentScore.value
        }

        this.store.dispatch(editSentiment({sentiment}));
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